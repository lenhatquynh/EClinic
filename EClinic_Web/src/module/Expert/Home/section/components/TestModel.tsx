import ModalSuccess from "components/Common/Modal/ModalSuccess"
import { SelectCustom } from "components/Common/Select/SelectCustom"
import Tag from "components/Common/Tag"
import { UpdateCover } from "components/Common/UpLoadImage"
import CustomButton from "components/User/Button"
import { CustomInput } from "components/User/Input"
import { usePredictMutation, useGetAllModelQuery } from "hooks/query/ai"
import { useState } from "react"
import { useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { useSelector } from "react-redux"
import { ROLE } from "shared/constant/constant"
import colorsProvider from "shared/theme/colors"
import { RootState } from "store/store"
import { PredictModel } from "types/AI"
import * as yup from "yup"
import { yupResolver } from "@hookform/resolvers/yup"
import InputField from "components/User/Input/InputField"

const TestModel = () => {
  const role = useSelector((auth: RootState) => auth.auth.user.role)
  const schema = yup.object({
    ModelID:
      role === ROLE.EXPERT
        ? yup.string().trim().required("Please select model")
        : yup.string().trim()
  })

  const [show, setShow] = useState(false)
  const model = useGetAllModelQuery()
  const { data, isLoading, mutate } = usePredictMutation(role)
  const {
    handleSubmit,
    setValue,
    watch,
    formState: { errors },
    control
  } = useForm<PredictModel>({
    mode: "onSubmit",
    resolver: yupResolver(schema)
  })
  const watchCoverImage = watch("file")
  const modelId = watch("ModelID")

  const onFileChange = (file: File | null) => {
    setValue("file", file)
  }
  const onSubmit = async (value: PredictModel) => {
    if (value.file) {
      mutate(value, {
        onSuccess() {
          setShow(true)
        },
        onError() {
          toast.error("Prediction failure")
        }
      })
    } else {
      toast.error("Please input image")
    }
  }

  return (
    <>
      <form
        className="flex flex-col gap-6 background-primary"
        onSubmit={handleSubmit(onSubmit)}
      >
        <h3 className="pb-4 text-lg font-medium">{"Detect disease"}</h3>
        <div className="flex flex-col space-y-5 ">
          <div className="flex flex-col gap-y-2">
            <span className="text-gray-500">Cover</span>
            <UpdateCover
              onFileChange={onFileChange}
              imageUrl={watchCoverImage || null}
            />
          </div>
          {role === ROLE.EXPERT && (
            <SelectCustom
              className="w-6/12"
              error={errors.ModelID?.message}
              value={modelId!}
              isLoading={model.isLoading}
              placeholder="Model"
              size="medium"
              options={model.data?.data.data.map((item) => ({
                label: item.ModelName,
                value: item.ModelID
              }))}
              onSelectOption={(value) => setValue("ModelID", value.value)}
            />
          )}
          <div className="flex flex-col gap-y-2">
            <span className="text-gray-500">Note</span>
            <InputField
              size="medium"
              control={control}
              name="note"
              multiline
              rows={4}
              label={"Note about a test"}
            />
          </div>

          <CustomButton type="submit" className="w-full" isLoading={isLoading}>
            Detect
          </CustomButton>
        </div>
      </form>
      <ModalSuccess isSuccess={show} setIsSuccess={() => setShow(false)}>
        <h1 className="text-4xl font-bold text-center text-black1">
          Predict Success
        </h1>
        <div className="flex items-center mt-4 gap-x-2">
          <p className="text-center text-gray80">The result is:</p>
          <Tag
            className="px-4 py-2 text-xl"
            color={
              data?.data === "Monkeypox"
                ? colorsProvider.success
                : data?.data === "Chickenpox"
                ? colorsProvider.error
                : data?.data === "Measles"
                ? colorsProvider.primary
                : data?.data === "Normal"
                ? colorsProvider.secondary
                : colorsProvider.error
            }
          >
            {data?.data}
          </Tag>
        </div>
        <CustomButton className="mt-3" onClick={() => setShow(false)}>
          Close
        </CustomButton>
      </ModalSuccess>
    </>
  )
}

export default TestModel
