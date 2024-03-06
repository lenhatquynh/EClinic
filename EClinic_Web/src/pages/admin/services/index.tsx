import AdminLayout from "layout/Management/AdminLayout"
import AdminServicePage from "module/Admin/Service/AdminServicePage"
import { NextPageWithLayout } from "pages/page"

const Page: NextPageWithLayout = () => {
  return <AdminServicePage />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
