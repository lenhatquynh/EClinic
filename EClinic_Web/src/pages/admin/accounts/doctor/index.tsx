import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const Doctor = dynamic(() => import("module/Admin/Account/Doctor"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <Doctor />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
