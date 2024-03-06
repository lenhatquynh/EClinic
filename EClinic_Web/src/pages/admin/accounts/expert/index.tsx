import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const Expert = dynamic(() => import("module/Admin/Account/Expert"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <Expert />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
