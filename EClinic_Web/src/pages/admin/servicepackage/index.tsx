import dynamic from "next/dynamic"
import AdminLayout from "layout/Management/AdminLayout"
import { NextPageWithLayout } from "pages/page"

const AdminServicePackage = dynamic(
  () =>
    import("module/Admin/ServicePackage/AdminServicePackagePage").then(
      (module) => module.default
    ),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  return <AdminServicePackage />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
