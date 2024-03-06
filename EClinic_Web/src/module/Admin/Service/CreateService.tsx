import { yupResolver } from "@hookform/resolvers/yup"
import {
  FormControl,
  FormHelperText,
  InputAdornment,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent
} from "@mui/material"
import SwitchCustom from "components/Common/IOSSwitch"
import CustomButton from "components/User/Button"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import {
  CreateServiceItem,
  UpdateService,
  useCreateServiceMutation,
  useGetAllSpecializationQuery,
  useUpdateServiceMutation
} from "hooks/query/service/useService"
import { useEffect } from "react"
import { FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { Service, Specialization } from "types/Service"
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
  serviceName: yup.string().trim().required("Please enter title"),
  price: yup
    .number()
    .required("Please enter price")
    .min(1, "Price must be greater than 0"),
  estimatedTime: yup
    .string()
    .trim()
    .required("Please enter estimate time")
    .min(1, "Estimate must be greater than 0"),
  specialization: yup.object({
    specializationID: yup
      .string()
      .trim()
      .required("Please choose a specialization")
  })
})
interface Props {
  service?: Service
  mode?: "update" | "create"
}
const CreateService = ({ service, mode = "create" }: Props) => {
  const confirm = useConfirm()
  const specializations = useGetAllSpecializationQuery({
    pageNumber: 1,
    pageSize: 100
  })
  const createService = useCreateServiceMutation()
  const updateService = useUpdateServiceMutation()
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
    defaultValues: service
  })
  const watchIsActive = watch("isActive", service?.isActive ? true : false)
  const watchSpec = watch("specialization", service?.specialization)
  const onSubmit = async (value: FieldValues) => {
    const specializationID = value.specialization.specializationID
    const status = value.isActive
    if (mode === "update") {
      if (confirm) {
        const choice = await confirm({
          title: "Update Service",
          content: "Are you sure want to update this service?"
        })
        if (choice) {
          updateService.mutate(
            {
              ...value,
              specializationID
            } as UpdateService,
            {
              onSuccess: (data) => {
                if (data.isSuccess) {
                  if (!status) {
                    toast.success(
                      "Please update service package that use this service!"
                    )
                  }
                  toast.success("Update a service successfully!")
                } else {
                  toast.error("Update a service fail!")
                }
              },
              onError: () => {
                toast.error("Update a service fail")
              }
            }
          )
        }
      }
    } else {
      createService.mutate(
        {
          ...value,
          specializationID
        } as CreateServiceItem,
        {
          onSuccess: (data) => {
            if (data?.isSuccess) {
              toast.success("Create a service successfully")
              resetForm()
            } else {
              toast.error("Create a service fail")
            }
          },
          onError: () => {
            toast.error("Create a service fail")
          }
        }
      )
    }
  }
  const resetForm = () => {
    reset({
      serviceName: "",
      price: 0,
      estimatedTime: 0,
      isActive: false,
      specialization: {}
    })
  }
  useEffect(() => {
    if (service === undefined) {
      resetForm()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [service])
  const handleChange = (event: SelectChangeEvent<string>) => {
    const {
      target: { value }
    } = event
    setValue(
      "specialization",
      specializations.data?.data.data.filter(
        (spe) => spe.specializationID === value
      )[0] as Specialization
    )
  }
  return (
    <form className="grid grid-cols-8 gap-6" onSubmit={handleSubmit(onSubmit)}>
      <div className="col-span-8 background-primary">
        <div className="flex items-center justify-between w-full mb-3">
          <span className="text-base font-medium text-black2">Publish</span>
          <SwitchCustom
            checked={watchIsActive}
            onChange={() => setValue("isActive", !watchIsActive)}
          />
        </div>
        <div className="flex flex-col justify-start mb-3">
          <div className="flex flex-col space-y-5">
            <InputField
              size="medium"
              control={control}
              name="serviceName"
              label={"Service name"}
            />
          </div>
        </div>
        <div className="flex flex-col justify-start mb-3">
          <div className="flex flex-col space-y-5">
            <InputField
              size="medium"
              control={control}
              name="price"
              type="number"
              label={"Price"}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">VNĐ</InputAdornment>
                )
              }}
            />
          </div>
        </div>
        <div className="flex flex-col justify-start mb-3">
          <div className="flex flex-col space-y-5">
            <InputField
              size="medium"
              control={control}
              name="estimatedTime"
              type="number"
              label={"Estimated Time"}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">Minutes</InputAdornment>
                )
              }}
            />
          </div>
        </div>
        <div className="flex flex-col justify-start mb-3">
          <div className="flex flex-col space-y-5">
            <FormControl>
              <InputLabel>Specialization</InputLabel>
              <Select
                error={!!errors.specialization?.specializationID?.message}
                value={watchSpec?.specializationID || ""}
                label="Specialization"
                onChange={handleChange}
                MenuProps={MenuProps}
              >
                {specializations.data?.data.data.map((item) => (
                  <MenuItem
                    key={item.specializationID}
                    value={item.specializationID}
                  >
                    {item.specializationName}
                  </MenuItem>
                ))}
              </Select>
              <FormHelperText error>
                {errors.specialization?.specializationID?.message}
              </FormHelperText>
            </FormControl>
          </div>
        </div>
        <div className="flex w-full mt-4 space-x-5">
          <CustomButton
            kind="primary"
            type="submit"
            className="w-full "
            isLoading={createService.isLoading}
          >
            {mode === "create" ? "Create" : "Update"}
          </CustomButton>
        </div>
      </div>
    </form>
  )
}
export default CreateService
