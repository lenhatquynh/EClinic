import ExpertLayout from "layout/Management/ExpertLayout"
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
  return <ExpertLayout>{page}</ExpertLayout>
}

export default Page
