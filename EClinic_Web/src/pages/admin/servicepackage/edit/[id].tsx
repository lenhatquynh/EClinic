import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"

const ServicePackageEdit = dynamic(
  () =>
    import("module/Admin/Edit/ServicePackageEdit").then(
      (module) => module.default
    ),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  const router = useRouter()
  if (router.query.id) {
    return <ServicePackageEdit />
  }
  return null
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
