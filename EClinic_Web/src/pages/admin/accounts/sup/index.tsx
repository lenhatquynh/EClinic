import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const Supporter = dynamic(() => import("module/Admin/Account/Supporter"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <Supporter />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
