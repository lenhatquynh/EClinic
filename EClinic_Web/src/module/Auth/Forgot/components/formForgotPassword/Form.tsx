import { yupResolver } from "@hookform/resolvers/yup"
import { useMutation } from "@tanstack/react-query"
import Spinner from "components/Common/Loading/LoadingIcon"
import CustomButton from "components/User/Button"
import { CustomInput, CustomInputPassword } from "components/User/Input"
import InputField from "components/User/Input/InputField"
import { useAccountResetPassowordMutation } from "hooks/query/account/useAccount"
import ConfirmCode from "module/Auth/SignUp/components/ConfirmCode"
import Link from "next/link"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { authService } from "services/auth.service"
import { RESEND_CODE_TYPE } from "shared/constant/constant"
import { routers } from "shared/constant/routers"
import * as yup from "yup"

const schema = yup.object({
  email: yup
    .string()
    .trim()
    .required("Please enter your email")
    .email("Please enter a valid email address"),
  newPassword: yup
    .string()
    .trim()
    .min(8, "Your password must be at least 8 characters or greater")
    .required("Please enter your password"),
  confirmPassword: yup
    .string()
    .trim()
    .oneOf([yup.ref("newPassword"), null], "Passwords must match")
})
const Form = () => {
  const router = useRouter()
  const [countError, setCountError] = useState(0)
  const [keyVerify, setKeyVerify] = useState<string | null>(null)
  const accountResetPassowordMutation = useAccountResetPassowordMutation()
  const resendCodeMutaiton = useMutation({
    mutationFn: (data: { key: string; type: number }) =>
      authService.resendCode(data.key, data.type)
  })
  const confirmResetPasswordMutation = useMutation({
    mutationFn: (data: { code: string; key: string }) =>
      authService.confirmResetPassword(data.code, data.key)
  })
  const {
    handleSubmit,
    control,
    getValues,
    formState: { errors, isSubmitting }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema)
  })
  const handleSubmitForgot = async (values: any) => {
    accountResetPassowordMutation.mutate(
      {
        ...values
      },
      {
        onSuccess: (data) => {
          setKeyVerify(data?.data)
          toast.success("Please check your email")
        },
        onError: (data: any) => {
          toast.error(
            data?.response?.data?.message || "Reset password failed!!"
          )
        }
      }
    )
  }
  const handleResetCode = async () => {
    resendCodeMutaiton.mutate(
      {
        key: keyVerify || "",
        type: RESEND_CODE_TYPE.RESET_PASSWORD
      },
      {
        onSuccess: () => {
          setCountError(0)
          toast.success("Send code again succesfully")
        },
        onError: () => {
          toast.error("Send code again failed!!")
        }
      }
    )
  }
  const handleSendcode = (code: string) => {
    confirmResetPasswordMutation.mutate(
      {
        code,
        key: keyVerify || ""
      },
      {
        onSuccess: () => {
          toast.success("Reset password successfully!!")
          router.push(routers.signIn)
        },
        onError: (data: any) => {
          setCountError((prev) => prev + 1)
          toast.error(data?.response?.data?.message || "Send code failed!!")
        }
      }
    )
  }
  return (
    <>
      {keyVerify ? (
        <ConfirmCode
          isLoading={confirmResetPasswordMutation.isLoading}
          countError={countError}
          handleSencode={handleSendcode}
          handleResendCode={handleResetCode}
          handleBack={() => {
            setKeyVerify(null)
            countError > 0 && setCountError(0)
          }}
          email={getValues("email")}
        />
      ) : (
        <>
          <div className="flex flex-col space-y-[10px] text-[#4E5D78] font-bold text-center max-w-[280px] md:max-w-[488px] mx-auto mt-[42px]">
            <h1 className="text-lg md:text-[30px]">Reset your password</h1>
          </div>
          <div className="w-full px-4 py-6 bg-white rounded-md mt-7">
            <form
              onSubmit={handleSubmit(handleSubmitForgot)}
              className="flex flex-col space-y-5"
            >
              <InputField control={control} name="email" label={"Email"} />

              <CustomInputPassword
                label="New Password"
                control={control}
                name="newPassword"
                error={!!errors.newPassword}
                errorMessage={errors.newPassword?.message?.toString()}
              />
              <CustomInputPassword
                label="Confirm password"
                placeholder="Confirm password"
                control={control}
                name="confirmPassword"
                error={!!errors.confirmPassword}
                errorMessage={errors.confirmPassword?.message?.toString()}
              />
              <CustomButton
                kind="primary"
                className="rounded-md md:h-[42px]"
                type="submit"
              >
                {isSubmitting ? <Spinner /> : "Reset"}
              </CustomButton>
            </form>
            <p className="text-center mt-5 text-[#4E5D78">
              Back to{" "}
              <Link
                href={"/sign-in"}
                className="no-underline hover:underline hover:decoration-primary"
              >
                <span className="text-primary">Sign In</span>
              </Link>
            </p>
          </div>
        </>
      )}
    </>
  )
}

export default Form
