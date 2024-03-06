import AuthLayout from "layout/Auth/AuthLayout"
import { NextPageWithLayout } from "./page"
import dynamic from "next/dynamic"
const SignupPage = dynamic(() => import("module/Auth/SignUp/SignupPage"), {
  ssr: false
})

const Signup: NextPageWithLayout = () => {
  return <SignupPage />
}
Signup.getLayout = (page) => {
  return <AuthLayout>{page}</AuthLayout>
}

export default Signup
