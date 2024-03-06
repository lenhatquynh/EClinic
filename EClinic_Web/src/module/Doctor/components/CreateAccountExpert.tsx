import { yupResolver } from "@hookform/resolvers/yup"
import {
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup
} from "@mui/material"
import DatePickerCustom from "components/Common/DatePicker/DatePickerCustom"
import SwitchCustom from "components/Common/IOSSwitch"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { CustomInput } from "components/User/Input"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import dayjs from "dayjs"
import {
  CreateExpertProfile,
  UpdateExpertProfile,
  useCreateExpertMutation,
  useUpdateExpertMutation
} from "hooks/query/profile/useProfile"
import { Uploadfile } from "module/User/Profile/section/profile/components/form/Edit"
import { useEffect } from "react"
import { FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import colorsProvider from "shared/theme/colors"
import { IProfileExpert } from "types/Profile.type"
import * as yup from "yup"

const schema = yup.object({
  email: yup
    .string()
    .trim()
    .required("Please enter email")
    .email("Please enter a valid email address"),
  firstName: yup.string().trim().required("Please enter first name"),
  lastName: yup.string().trim().required("Please enter last name"),
  phone: yup
    .string()
    .trim()
    .required("Please enter phone number")
    .matches(
      /(84|0[3|5|7|8|9])+([0-9]{8})\b/,
      "Please enter valid phone number"
    ),
  workStart: yup.string().trim().required("Please enter date of working start"),
  address: yup.string().trim().required("Please enter address")
})
interface Props {
  labelForm: string
  profile?: IProfileExpert
  mode?: "update" | "create"
}
const CreateAccountExpert = ({
  labelForm,
  profile,
  mode = "create"
}: Props) => {
  const confirm = useConfirm()
  const createExpertMutation = useCreateExpertMutation()
  const updateExpertMutation = useUpdateExpertMutation()
  const {
    handleSubmit,
    control,
    setError,
    watch,
    reset,
    setValue,
    formState: { errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: profile
  })
  const avatar = watch("avatar", profile?.avatar)

  const watchGender = watch("gender", profile ? profile?.gender : true)
  const watchEndable = watch(
    "enabledAccount",
    profile?.enabledAccount ? true : false
  )
  const onFileChange = (file: File) => {
    setValue("avatar", file)
  }
  const resetForm = () => {
    reset({
      profileID: "",
      userID: "",
      firstName: "",
      lastName: "",
      avatar: null,
      gender: true,
      dateOfBirth: dayjs().toString(),
      address: "",
      email: "",
      phone: "",
      workStart: dayjs().toString(),
      description: ""
    })
  }
  const onSubmit = async (value: FieldValues) => {
    if (mode === "update") {
      if (confirm) {
        const choice = await confirm({
          title: "Update account",
          content: "Are you sure you want to update this profile?"
        })
        if (choice) {
          updateExpertMutation.mutate(
            {
              ...value,
              workEnd: dayjs().format("YYYY-MM-DDTHH:mm:ss")
            } as UpdateExpertProfile,
            {
              onSuccess: (data) => {
                if (data.isSuccess) {
                  toast.success("Update successfully")
                } else {
                  toast.error("Update error")
                }
              },
              onError: () => {
                toast.error("Update error")
              }
            }
          )
        }
      }
    } else {
      if (value.avatar === null) {
        toast.error("Please upload avatar")
        return
      }
      createExpertMutation.mutate(
        {
          ...value,
          workEnd: dayjs().format("YYYY-MM-DDTHH:mm:ss")
        } as CreateExpertProfile,
        {
          onSuccess: (data) => {
            if (data.isSuccess) {
              toast.success("Add successfully")
              resetForm()
            } else {
              toast.error("Add error")
            }
          },
          onError: (data: any) => {
            toast.error(data?.response?.data?.message || "Create fail")
          }
        }
      )
    }
  }
  useEffect(() => {
    if (profile === undefined) {
      resetForm()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [profile])

  return (
    <form
      className="flex flex-col w-full gap-y-6 md:gap-y-0 md:flex-row gap-x-5"
      onSubmit={handleSubmit(onSubmit)}
    >
      <div className="relative background-primary w-full md:max-w-[360px] flex flex-col items-center h-fit py-16">
        <Tag
          className="absolute top-0 right-0 -translate-x-1/4 translate-y-2/4"
          color={watchEndable ? colorsProvider.success : colorsProvider.error}
        >
          {watchEndable ? "Active" : "Banned"}
        </Tag>

        <Uploadfile imageUrl={avatar || null} onFileChange={onFileChange} />
        <p className="text-xs text-disable max-w-[200px] text-center leading-relaxed mt-3">
          Allowed *.jpeg, *.jpg, *.png, *.gif
        </p>
        <div className="flex items-center justify-between w-full max-w-[260px] mt-6">
          <div className="flex flex-col">
            <span className="text-base font-medium text-black2">Publish</span>
            <p className="text-xs text-disable">Apply disable account</p>
          </div>
          <SwitchCustom
            checked={watchEndable}
            onChange={(e) => {
              setValue("enabledAccount", e.target.checked)
            }}
          />
        </div>
      </div>
      <div className="flex-1 background-primary">
        <h3 className="pb-4 text-lg font-medium">{labelForm}</h3>
        <div className="flex flex-col justify-start">
          <div className="flex flex-col space-y-5">
            <div className="flex items-start space-x-3">
              <InputField
                size="medium"
                label="First name"
                control={control}
                name="firstName"
              />
              <InputField
                size="medium"
                label="Last name"
                control={control}
                name="lastName"
              />
            </div>

            <div className="flex space-x-3">
              <InputField
                size="medium"
                label="Address"
                control={control}
                name="address"
              />
            </div>
            <div className="flex space-x-3">
              <DatePickerCustom
                size="medium"
                label="Date of birth"
                control={control}
                name="dateOfBirth"
                onErrorField={(reason) => {
                  const message =
                    reason === "invalidDate"
                      ? "Please enter valid date"
                      : reason === "disableFuture"
                      ? "The birthday cannot be less than the current date"
                      : ""
                  setError("dateOfBirth", { type: "focus", message })
                }}
                errorMessage={errors.dateOfBirth?.message?.toString()}
              />
              <InputField
                size="medium"
                label="Email"
                control={control}
                name="email"
              />
            </div>
            <div className="flex space-x-3">
              <DatePickerCustom
                size="medium"
                label="Date start work"
                control={control}
                name="workStart"
                onErrorField={(reason) => {
                  const message =
                    reason === "invalidDate"
                      ? "Please enter valid date"
                      : reason === "disableFuture"
                      ? "The birthday cannot be less than the current date"
                      : ""
                  setError("workStart", { type: "focus", message })
                }}
                errorMessage={errors.workStart?.message?.toString()}
              />
              <InputField
                size="medium"
                label="Phone number"
                control={control}
                name="phone"
              />
            </div>
            <FormControl>
              <FormLabel>Gender</FormLabel>
              <RadioGroup
                row
                value={watchGender}
                onChange={(e) =>
                  setValue("gender", e.target.value === "true" ? true : false)
                }
                name="radio-buttons-group"
              >
                <FormControlLabel
                  value={true}
                  control={<Radio size="small" />}
                  label="Female"
                />
                <FormControlLabel
                  value={false}
                  control={<Radio size="small" />}
                  label="Male"
                />
              </RadioGroup>
            </FormControl>
            <InputField
              size="medium"
              label="About"
              control={control}
              name="description"
              multiline
              rows={4}
            />
            <CustomButton
              kind="primary"
              type="submit"
              className="ml-auto w-fit"
              isLoading={
                updateExpertMutation.isLoading || createExpertMutation.isLoading
              }
            >
              {mode === "create" ? "Create account" : "Update account"}
            </CustomButton>
          </div>
        </div>
      </div>
    </form>
  )
}

export default CreateAccountExpert
