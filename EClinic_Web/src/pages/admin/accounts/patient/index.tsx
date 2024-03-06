import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const Patient = dynamic(() => import("module/Admin/Account/Patient"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <Patient />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
