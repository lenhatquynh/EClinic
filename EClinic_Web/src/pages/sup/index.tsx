import SupporterLayout from "layout/Management/SupporterLayout"
import { NextPageWithLayout } from "pages/page"

const Page: NextPageWithLayout = () => {
  return <div>Supporter page</div>
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}
export default Page
