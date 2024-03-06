import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const SchedulePage = dynamic(
  () => import("module/Supporter/Schedule/SchedulePage"),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  return <SchedulePage />
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
