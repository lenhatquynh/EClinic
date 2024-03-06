import { yupResolver } from "@hookform/resolvers/yup"
import {
  FormControl,
  FormControlLabel,
  FormLabel,
  InputAdornment,
  InputLabel,
  MenuItem,
  Radio,
  RadioGroup,
  Select,
  Skeleton
} from "@mui/material"
import DatePickerCustom from "components/Common/DatePicker/DatePickerCustom"
import Spinner from "components/Common/Loading/LoadingIcon"
import CustomButton from "components/User/Button"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import {
  useAllRelationship,
  useGetBloodTypes
} from "hooks/query/profile/useProfile"
import { useImageFile } from "hooks/useImageFile"
import Image from "next/image"
import { ChangeEvent, useEffect } from "react"
import { Controller, FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { RELATIONSHIPS } from "shared/constant/constant"
import { IProfile, IRelationShip } from "types/Profile.type"
import * as yup from "yup"
interface Props {
  // eslint-disable-next-line no-unused-vars
  onSubmit: (value: FieldValues) => void
  // eslint-disable-next-line no-unused-vars
  onDelete: (profileId: string) => void
  labelForm: string
  profile?: IProfile & IRelationShip
  isLoading: boolean
}

const schema = yup.object({
  email: yup
    .string()
    .trim()
    .required("Please enter your email")
    .email("Please enter a valid email address"),
  firstName: yup.string().trim().required("Please enter your first name"),
  lastName: yup.string().trim().required("Please enter your last name"),
  phone: yup
    .string()
    .trim()
    .nullable()
    .required("Please enter your phone number")
    .matches(
      /(84|0[3|5|7|8|9])+([0-9]{8})\b/,
      "Please enter valid phone number"
    ),
  dateOfBirth: yup.string().trim().required("Please enter your date of birth"),
  address: yup.string().trim().required("Please enter your address").nullable(),
  bloodType: yup.string().trim().required("Please choose your blood"),
  weight: yup
    .number()
    .min(1, "Weight must be greater than 0")
    .typeError("Weight must be numbers")
    .required("Please enter your weight"),
  height: yup
    .number()
    .min(1, "Height must be greater than 0")
    .typeError("Height must be numbers")
    .required("Please enter your weight")
})
const Edit = ({
  onSubmit,
  onDelete,
  labelForm,
  profile,
  isLoading = false
}: Props) => {
  const confirm = useConfirm()
  const { t } = useTranslation(["base"])

  const relationShipQuery = useAllRelationship()
  const getBloodTypes = useGetBloodTypes()
  const {
    handleSubmit,
    control,
    setError,
    getValues,
    watch,
    reset,
    setValue,
    formState: { errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: profile
  })
  watch("avatar", null)

  const onFileChange = (file: File) => {
    setValue("avatar", file)
  }
  const handleDelete = async (profileId: string) => {
    if (confirm) {
      const choice = await confirm({
        title: "Delete profile",
        content: "Are you sure you want to delete this profile?"
      })
      if (choice) {
        onDelete(profileId)
      }
    }
  }
  const handleOnSubmit = (value: FieldValues) => {
    console.log("handleOnSubmit ~ value:", value)
    if (getValues("avatar") === undefined) {
      toast.error("Please choose your avatar")
    } else {
      onSubmit(value)
    }
  }
  useEffect(() => {
    if (
      profile === undefined &&
      relationShipQuery.isSuccess &&
      getBloodTypes.isSuccess
    ) {
      reset({
        firstName: "",
        lastName: "",
        email: "",
        address: "",
        phone: "",
        bloodType: getBloodTypes.data?.data[0],
        weight: 0,
        height: 0,
        gender: true,
        relationshipID: relationShipQuery.data?.data[0].relationshipID,
        avatar: ""
      })
    }
  }, [profile, relationShipQuery.isSuccess, getBloodTypes.isSuccess])
  return (
    <>
      <h3 className="pb-4 text-lg font-medium">{labelForm}</h3>
      {getBloodTypes.isLoading ||
        (relationShipQuery.isLoading && (
          <>
            <div className="flex items-center justify-center w-full h-full">
              <Spinner color="#024ED5" />
            </div>
          </>
        ))}
      <div className="flex flex-col justify-start">
        <form
          onSubmit={handleSubmit(handleOnSubmit)}
          className="flex flex-col space-y-5"
        >
          <Uploadfile
            imageUrl={profile?.avatar as string | "/images/default.jpeg"}
            onFileChange={onFileChange}
          />
          <div className="flex items-start space-x-3">
            <InputField
              size="medium"
              control={control}
              name="firstName"
              label={t("base:pages.profileUser.form.first_name")}
            />
            <InputField
              size="medium"
              control={control}
              name="lastName"
              label={t("base:pages.profileUser.form.last_name")}
            />
          </div>
          <InputField
            disabled={
              profile?.relationshipName === RELATIONSHIPS.ME ? true : false
            }
            size="medium"
            control={control}
            name="email"
            label={t("base:pages.profileUser.form.email")}
          />
          <InputField
            size="medium"
            control={control}
            name="address"
            label={t("base:pages.profileUser.form.address")}
          />
          <div className="flex space-x-3">
            <DatePickerCustom
              size="medium"
              label={t("base:pages.profileUser.form.date_of_birth")}
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
            {getBloodTypes.isLoading ? (
              <Skeleton
                variant="rounded"
                className="rounded-xl"
                width={380}
                height={54}
              />
            ) : (
              getBloodTypes.data?.data && (
                <Controller
                  name="bloodType"
                  control={control}
                  render={(field) => {
                    return (
                      <FormControl fullWidth>
                        <InputLabel>
                          {t("base:pages.profileUser.form.blood_type")}
                        </InputLabel>
                        <Select
                          onChange={field.field.onChange}
                          value={
                            field.field.value || getBloodTypes.data?.data[0]
                          }
                          label={t("base:pages.profileUser.form.blood_type")}
                        >
                          {getBloodTypes?.data?.data.map((item, index) => (
                            <MenuItem value={item} key={index}>
                              {item}
                            </MenuItem>
                          ))}
                        </Select>
                      </FormControl>
                    )
                  }}
                />
              )
            )}
          </div>
          <InputField
            size="medium"
            control={control}
            name="phone"
            label={t("base:pages.profileUser.form.phone_number")}
          />
          <Controller
            name="gender"
            control={control}
            render={(field) => {
              return (
                <FormControl>
                  <FormLabel>
                    {t("base:pages.profileUser.form.gender")}
                  </FormLabel>
                  <RadioGroup
                    defaultValue={true}
                    value={field.field.value}
                    row
                    onChange={(e) => {
                      field.field.onChange(e.target.value)
                    }}
                  >
                    <FormControlLabel
                      value={true}
                      control={<Radio size="small" />}
                      label={t("base:pages.profileUser.form.feMale")}
                    />
                    <FormControlLabel
                      value={false}
                      control={<Radio size="small" />}
                      label={t("base:pages.profileUser.form.male")}
                    />
                  </RadioGroup>
                </FormControl>
              )
            }}
          />

          <div className="flex items-start space-x-3">
            <InputField
              size="medium"
              control={control}
              name="weight"
              label={t("base:pages.profileUser.form.weight")}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">Kg</InputAdornment>
                )
              }}
            />
            <InputField
              size="medium"
              control={control}
              name="height"
              label={t("base:pages.profileUser.form.height")}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">Cm</InputAdornment>
                )
              }}
            />
          </div>
          {profile?.relationshipName !== RELATIONSHIPS.ME &&
            (relationShipQuery.isLoading ? (
              <div className="flex flex-wrap gap-5 mt-2">
                {Array(10)
                  .fill(0)
                  .map((_, index) => (
                    <Skeleton
                      key={index}
                      variant="rounded"
                      height={40}
                      width={80 + (Math.floor(Math.random() * 10) + 1) * 10}
                    />
                  ))}
              </div>
            ) : (
              <Controller
                name="relationshipID"
                control={control}
                render={(field) => {
                  return (
                    <FormControl>
                      <FormLabel>
                        {t("base:pages.profileUser.form.relationship")}
                      </FormLabel>
                      <RadioGroup
                        value={
                          field.field.value ||
                          relationShipQuery.data?.data[0].relationshipID
                        }
                        row
                        onChange={field.field.onChange}
                        className="gap-3 mt-2 ml-3"
                      >
                        {relationShipQuery.isSuccess &&
                          relationShipQuery.data.data.map((item, index) => {
                            if (item.relationshipName !== RELATIONSHIPS.ME) {
                              return (
                                <FormControlLabel
                                  className="px-3 border border-gray-200 border-solid rounded-md shadow-sm"
                                  key={index}
                                  value={item.relationshipID}
                                  control={<Radio size="small" />}
                                  label={item.relationshipName}
                                />
                              )
                            }
                          })}
                      </RadioGroup>
                    </FormControl>
                  )
                }}
              />
            ))}

          <div className="flex justify-end space-x-4">
            {profile !== undefined &&
              profile?.relationshipName !== RELATIONSHIPS.ME && (
                <CustomButton
                  kind="secondary"
                  className="text-red-600 border-red-600 hover:border-red-500 "
                  onClick={() => handleDelete(profile.profileID)}
                >
                  Delete
                </CustomButton>
              )}

            <CustomButton
              disabled={isLoading}
              isLoading={isLoading}
              kind="primary"
              type="submit"
            >
              {profile
                ? t("base:pages.profileUser.update")
                : t("base:pages.profileUser.add")}
            </CustomButton>
          </div>
        </form>
      </div>
    </>
  )
}

// eslint-disable-next-line no-unused-vars
interface IFileProps {
  imageUrl: string | null
  // eslint-disable-next-line no-unused-vars
  onFileChange: (file: File) => void
}
export const Uploadfile = ({ imageUrl, onFileChange }: IFileProps) => {
  const { image, handleImageChange, setImage } = useImageFile(imageUrl)
  const onImageChange = (e: ChangeEvent<HTMLInputElement>) => {
    const file = handleImageChange(e)
    onFileChange(file!)
  }
  useEffect(() => {
    if (typeof imageUrl === "string" || !imageUrl) {
      setImage(imageUrl)
    }
  }, [imageUrl])
  return (
    <>
      <div className="flex justify-center mt-8">
        <div className="rounded-lg lg:w-1/2">
          <div className="flex items-center justify-center w-full">
            <label className="relative flex flex-col items-center justify-center w-40 h-40 transition-all border border-gray-400 border-dashed rounded-full cursor-pointer hover:bg-gray-100 hover:border-gray-300">
              {image && (
                <Image
                  src={image}
                  fill
                  alt="avatar"
                  className="object-cover rounded-full"
                />
              )}
              <div className="flex flex-col items-center justify-center ">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="w-12 h-12 text-gray-400 group-hover:text-gray-600"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                >
                  <path
                    fillRule="evenodd"
                    d="M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z"
                    clipRule="evenodd"
                  />
                </svg>
                <p className="pt-1 text-xs tracking-wider text-gray-400 group-hover:text-gray-600">
                  Select a photo
                </p>
              </div>
              <input
                type="file"
                className="opacity-0"
                onChange={(e) => onImageChange(e)}
              />
            </label>
          </div>
        </div>
      </div>
    </>
  )
}

export default Edit
