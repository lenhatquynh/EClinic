import { yupResolver } from "@hookform/resolvers/yup"
import { Divider } from "@mui/material"
import { useQueryClient } from "@tanstack/react-query"

import GoogleIcon from "components/Common/Icon/GoogleIcon"
import Spinner from "components/Common/Loading/LoadingIcon"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { CustomInputPassword } from "components/User/Input"
import InputField from "components/User/Input/InputField"
import useUserGoogle from "hooks/auth/useUserGoogle"
import jwt_decode from "jwt-decode"
import ButtonIcon from "module/Auth/components/ButtonIcon"
import Link from "next/link"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { authService } from "services/auth.service"
import { QUERY_KEYS } from "shared/constant/constant"
import { routerByRole } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { token } from "shared/utils/token"
import { loginUser } from "store/module/auth/action-creators"
import { useAppDispatch } from "store/store"
import { ITokenDecode } from "types/Token"
import * as yup from "yup"

const schema = yup.object({
  username: yup.string().trim().required("Please enter your user name"),
  password: yup.string().trim().required("Please enter your password")
})
const FormLogin = () => {
  const queryClient = useQueryClient()
  const { action, profile, error } = useUserGoogle()
  const dispatch = useAppDispatch()
  const router = useRouter()
  const [isError, setIsError] = useState(false)
  const {
    handleSubmit,
    control,
    formState: { isSubmitting, errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema)
  })
  const handleSignIn = async (values: FieldValues) => {
    try {
      const result = await authService.signIn(values.username, values.password)
      if (result.isSuccess) {
        handleNavigate(result.data.accessToken, result.data.refreshToken)
        setIsError(false)
      } else {
        setIsError(true)
      }
    } catch (error) {
      setIsError(true)
      console.log("handleSignIn ~ error", error)
    }
  }
  const handleNavigate = (accessToken: string, refreshToken: string) => {
    token.saveToken(accessToken, refreshToken)
    const payload = jwt_decode(accessToken) as ITokenDecode
    dispatch(
      loginUser({
        userId: payload.UserID,
        role: payload.role
      })
    )
    router.push(routerByRole(payload.role), undefined, { shallow: true })
    queryClient.refetchQueries([QUERY_KEYS.PROFILE])
  }
  useEffect(() => {
    if (error) {
      toast.error("Login failed, please try again!!!")
    }
  }, [error])
  useEffect(() => {
    if (profile) {
      const login = async () => {
        const res = await authService.signInWithGoogle(profile.access_token)
        try {
          if (res.isSuccess) {
            handleNavigate(res.data.accessToken, res.data.refreshToken)
            toast.success("Sign in successfully")
          } else {
            toast.error(res.message || "Login failed, please try again!!!")
          }
        } catch (error) {
          console.error("login ~ error:", error)
          toast.error("Login failed, please try again!!!")
        }
      }
      login()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [profile])
  return (
    <div className="w-full px-4 py-6 rounded-md mt-7">
      <ButtonIcon
        text="Log in with Google"
        icon={<GoogleIcon />}
        onClick={() => action()}
      />
      <Divider className="my-[30px]">
        <span className="text-[10px] text-[#4E5D78] md:text-lg">OR</span>
      </Divider>
      <form
        onSubmit={handleSubmit(handleSignIn)}
        className="flex flex-col mt-3 space-y-5 md:mt-5"
      >
        <InputField label="User name" control={control} name="username" />
        <CustomInputPassword
          label="Password"
          control={control}
          name="password"
          error={!!errors.password}
          errorMessage={errors.password?.message?.toString()}
        />
        <Link
          href={"/reset-password"}
          className="!mt-2 text-right no-underline hover:underline hover:decoration-primary"
        >
          <span className="text-primary">Forgot password!</span>
        </Link>
        {isError && (
          <Tag color={colorsProvider.error} className="w-full">
            <div className="flex items-center w-full py-1 gap-x-2">
              <span>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-5 h-5"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126zM12 15.75h.007v.008H12v-.008z"
                  />
                </svg>
              </span>
              <span>User Name or password incorrect</span>
            </div>
          </Tag>
        )}
        <CustomButton
          kind="primary"
          className="rounded-md md:h-[42px]"
          type="submit"
          disabled={isSubmitting}
        >
          {isSubmitting ? <Spinner /> : "Sign In"}
        </CustomButton>
      </form>
      <p className="text-center mt-5 text-[#4E5D78">
        {"You haven't any account?"}{" "}
        <Link
          href={"/sign-up"}
          className="no-underline hover:underline hover:decoration-primary"
        >
          <span className="text-primary">Sign Up</span>
        </Link>
      </p>
    </div>
  )
}

export default FormLogin
