import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const DetailPageServer = dynamic(() => import("module/User/Forum/DetailPage"))
const Detail: NextPageWithLayout = () => {
  return <DetailPageServer />
}

Detail.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}

export default Detail
