import AdminLayout from "layout/Management/AdminLayout"
import AdminPaymentPage from "module/Admin/Payment/AdminPaymentPage"
import { NextPageWithLayout } from "pages/page"

const Page: NextPageWithLayout = () => {
  return <AdminPaymentPage />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
