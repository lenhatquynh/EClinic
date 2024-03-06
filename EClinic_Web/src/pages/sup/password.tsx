import SupporterLayout from "layout/Management/SupporterLayout"
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
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
