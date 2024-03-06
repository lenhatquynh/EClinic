import AuthLayout from "layout/Auth/AuthLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "./page"
const SignInPage = dynamic(() => import("module/Auth/SignIn/SignInPage"), {
  ssr: false
})

const SignIn: NextPageWithLayout = () => {
  return <SignInPage />
}
SignIn.getLayout = (page) => {
  return <AuthLayout>{page}</AuthLayout>
}

export default SignIn
