import AuthLayout from "layout/Auth/AuthLayout"
import ForgotPasswordPage from "module/Auth/Forgot/ForgotPasswordPage"
import React from "react"
import { NextPageWithLayout } from "./page"

const ResetPassword: NextPageWithLayout = () => {
  return <ForgotPasswordPage />
}
ResetPassword.getLayout = (page) => {
  return <AuthLayout>{page}</AuthLayout>
}

export default ResetPassword
