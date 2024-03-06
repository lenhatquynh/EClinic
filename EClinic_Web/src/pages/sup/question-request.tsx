import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const QuestionRequest = dynamic(
  () => import("module/Supporter/Quesiton/QuestionRequest"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <QuestionRequest />
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
