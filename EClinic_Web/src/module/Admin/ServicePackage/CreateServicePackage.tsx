import { yupResolver } from "@hookform/resolvers/yup"
import {
  Box,
  Chip,
  FormControl,
  FormHelperText,
  InputAdornment,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent
} from "@mui/material"
import { useQueryClient } from "@tanstack/react-query"
import Editor from "components/Common/Editor/Editor"
import SwitchCustom from "components/Common/IOSSwitch"
import { UpdateCover } from "components/Common/UpLoadImage"
import CustomButton from "components/User/Button"
import { CustomInput } from "components/User/Input"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import {
  CreateServicePackageItem,
  UpdateServicePackage,
  useCreateServicePackageMutation,
  useGetAllServiceForAdQuery,
  useUpdateServicePackageMutation
} from "hooks/query/service/useService"
import { useEffect } from "react"
import { FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { QUERY_KEYS } from "shared/constant/constant"
import { isQuillEmpty } from "shared/helpers/helper"
import { Service, ServicePackage } from "types/Service"
import * as yup from "yup"
const ITEM_HEIGHT = 48
const ITEM_PADDING_TOP = 8
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250
    }
  }
}
const schema = yup.object({
  servicePackageName: yup.string().trim().required("Please enter title"),
  description: yup
    .string()
    .trim()
    .required("Please enter description")
    .test("is-quill-empty", "Please enter description", (value) => {
      return !isQuillEmpty(value || "")
    }),
  price: yup
    .number()
    .typeError("Please enter a valid price")
    .positive("Price must be a positive number")
    .required("Please enter price"),
  discount: yup
    .number()
    .typeError("Please enter a valid discount")
    .min(0, "Discount cannot be negative")
    .max(100, "Discount cannot be greater than 100")
    .required("Please enter discount"),
  estimatedTime: yup
    .number()
    .typeError("Please enter a valid estimated time")
    .positive("Estimated time must be a positive number")
    .required("Please enter estimate time"),
  serviceItems: yup
    .array()
    .min(1, "Please choose at least one service")
    .required("Please choose service")
})
interface Props {
  labelForm: string
  servicePackage?: ServicePackage
  mode?: "update" | "create"
}
const CreateServicePackage = ({
  labelForm,
  servicePackage,
  mode = "create"
}: Props) => {
  const queryClient = useQueryClient()
  const confirm = useConfirm()
  const services = useGetAllServiceForAdQuery({
    pageNumber: 1,
    pageSize: 100
  })
  const createServicePackage = useCreateServicePackageMutation()
  const updateServicePackage = useUpdateServicePackageMutation()
  const {
    handleSubmit,
    control,
    watch,
    reset,
    setValue,
    formState: { errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: servicePackage
  })
  const serviceSelected = watch("serviceItems")
  const watchDesc = watch("description")
  const watchImage = watch("image")
  const watchPrice = watch("price")
  const watchDiscount = watch("discount")
  const watchIsActive = watch(
    "isActive",
    servicePackage?.isActive ? true : false
  )
  const onFileChange = (file: File | null) => {
    setValue("image", file)
  }
  const onSubmit = async (value: FieldValues) => {
    if (!value.image) {
      toast.error("Please upload image")
      return
    }
    const newServices =
      value.serviceItems.length > 0
        ? value.serviceItems.map((item: Service) => item.serviceID)
        : []
    const image = typeof value.image === "string" ? null : value.image
    if (mode === "update") {
      if (confirm) {
        const choice = await confirm({
          title: "Update Service Package",
          content: "Are you sure want to update this service package?"
        })
        if (choice) {
          updateServicePackage.mutate(
            {
              ...value,
              image,
              serviceItemIds: newServices
            } as UpdateServicePackage,
            {
              onSuccess: (data) => {
                if (data?.isSuccess) {
                  toast.success("Update completed")
                  queryClient.invalidateQueries([QUERY_KEYS.SERVICE_PACKAGE])
                } else {
                  toast.error("Update a service package fail")
                }
              },
              onError: () => {
                toast.error("Update a service package fail")
              }
            }
          )
        }
      }
    } else {
      if (!watchImage) {
        toast.error("Please upload image")
        return
      }
      createServicePackage.mutate(
        {
          ...value,
          serviceItemIds: newServices
        } as CreateServicePackageItem,
        {
          onSuccess: (data) => {
            if (data?.isSuccess) {
              toast.success("Create a service package successfully")
              resetForm()
              queryClient.invalidateQueries([QUERY_KEYS.SERVICE_PACKAGE])
            } else {
              toast.error("Create a service package fail")
            }
          },
          onError: (data: any) => {
            toast.error(
              data?.response?.data?.message || "Create a service package fail!!"
            )
          }
        }
      )
    }
  }
  const resetForm = () => {
    reset({
      servicePackageName: "",
      description: "",
      image: "",
      price: 0,
      discount: 0,
      estimatedTime: 0,
      isActive: false,
      serviceItems: []
    })
  }
  useEffect(() => {
    if (servicePackage === undefined) {
      resetForm()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [servicePackage])
  const handleChange = (event: SelectChangeEvent<string[]>) => {
    const {
      target: { value }
    } = event
    let newServices: Service[] = []
    let newPrice: number = 0
    let newEstimatedTime: number = 0
    if (typeof value === "string") {
      newServices =
        services.data?.data.data.filter((item) => {
          if (item.serviceID === value) {
            if (item.isActive) {
              newPrice = item.price
              newEstimatedTime = item.estimatedTime
            }
            return item
          }
        }) || []
    } else {
      newServices =
        services.data?.data.data.filter((item) => {
          if (value.some((has) => has === item.serviceID)) {
            if (item.isActive) {
              newPrice += item.price
              newEstimatedTime += item.estimatedTime
            }
            return item
          }
        }) || []
    }
    setValue("serviceItems", newServices)
    setValue("price", newPrice)
    setValue("estimatedTime", newEstimatedTime)
  }
  const getServiceName = (serviceID: string) => {
    const selectedService = services.data?.data.data.find(
      (service) => service.serviceID === serviceID
    )
    return selectedService ? selectedService.serviceName : ""
  }
  return (
    <form className="grid grid-cols-8 gap-6" onSubmit={handleSubmit(onSubmit)}>
      <div className="col-span-5 background-primary">
        <h3 className="pb-4 text-lg font-medium">{labelForm}</h3>
        <div className="flex flex-col justify-start">
          <div className="flex flex-col space-y-5">
            <InputField
              size="medium"
              control={control}
              name="servicePackageName"
              label={"Service Package Title"}
            />
            <div className="flex flex-col gap-y-2">
              <span className="text-gray-500">Image</span>
              <UpdateCover
                isDelete={false}
                isError={!!errors.image}
                onFileChange={onFileChange}
                imageUrl={watchImage || null}
              />
            </div>

            <div className="flex flex-col gap-y-2">
              <span className="text-gray-500">Description</span>
              <Editor
                isError={!!errors.description?.message}
                onChange={(data: string) => {
                  setValue("description", data)
                }}
                value={watchDesc}
              />
              <FormHelperText error>
                {errors.description?.message}
              </FormHelperText>
            </div>
          </div>
        </div>
      </div>

      <div className="col-span-3">
        <div className="flex flex-col py-8 space-y-6 background-primary">
          <div className="flex items-center justify-between w-full">
            <span className="text-base font-medium text-black2">Publish</span>
            <SwitchCustom
              checked={watchIsActive}
              onChange={() => setValue("isActive", !watchIsActive)}
            />
          </div>
          <FormControl>
            <InputLabel>Services</InputLabel>
            <Select
              multiple
              value={serviceSelected?.map((item) => item.serviceID) || []}
              onChange={handleChange}
              error={!!errors.serviceItems}
              input={<OutlinedInput label="Services" />}
              renderValue={(selected) => (
                <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                  {selected.map((value) => (
                    <Chip key={value} label={getServiceName(value)} />
                  ))}
                </Box>
              )}
              MenuProps={MenuProps}
            >
              {services.data?.data.data.map((name, index) => (
                <MenuItem key={index} value={name.serviceID}>
                  {name.serviceName}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText error>
              {errors.serviceItems?.message}
            </FormHelperText>
          </FormControl>
          <InputField
            InputProps={{
              endAdornment: (
                <InputAdornment position="start">VNĐ</InputAdornment>
              )
            }}
            size="medium"
            control={control}
            name="price"
            type="number"
            label={"Price"}
          />
          <InputField
            InputProps={{
              endAdornment: <InputAdornment position="start">%</InputAdornment>
            }}
            size="medium"
            control={control}
            name="discount"
            type="number"
            label={"Discount"}
          />
          <CustomInput
            InputProps={{
              endAdornment: (
                <InputAdornment position="start">VNĐ</InputAdornment>
              )
            }}
            disabled
            size="medium"
            label="Discount Price"
            value={watchPrice * (1 - watchDiscount / 100)}
          />
          <InputField
            InputProps={{
              endAdornment: (
                <InputAdornment position="start">Minute</InputAdornment>
              )
            }}
            size="medium"
            control={control}
            name="estimatedTime"
            type="number"
            label={"EstimatedTime"}
          />
        </div>
        <div className="flex w-full mt-4 space-x-5">
          <CustomButton
            kind="primary"
            type="submit"
            className="w-full "
            isLoading={createServicePackage.isLoading}
          >
            {mode === "create" ? "Create" : "Update"}
          </CustomButton>
        </div>
      </div>
    </form>
  )
}
export default CreateServicePackage
