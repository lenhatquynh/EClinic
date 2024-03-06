import AdminLayout from "layout/Management/AdminLayout"
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
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
