import DoctorLayout from "layout/Management/DoctorLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ChangePasswordPage = dynamic(
  () => import("module/Doctor/ChangePassword"),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  return <ChangePasswordPage />
}
Page.getLayout = (page) => {
  return <DoctorLayout>{page}</DoctorLayout>
}

export default Page
