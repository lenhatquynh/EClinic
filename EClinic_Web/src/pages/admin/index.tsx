import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const AdminHomePage = dynamic(() => import("module/Admin/Home/AdminHomePage"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <AdminHomePage />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
