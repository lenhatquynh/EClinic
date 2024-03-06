import { useGetAllServicePackageQuery } from "hooks/query/service/useService"
import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"
import { ServicePackage } from "types/Service"
const ServicesDetailPage = dynamic(
  () => import("module/User/Services/ServicesDetailPage").then((module) => module.default),
  {
    ssr: false
  }
)

const ServicesDetail: NextPageWithLayout = () => {
  const router = useRouter()
  const { data } = useGetAllServicePackageQuery({
    pageNumber: 1,
    pageSize: 4
  })
  const servicePackages: ServicePackage[] = data?.data?.data as ServicePackage[]
  if (router.query.id) {
    return <ServicesDetailPage servicePackages={servicePackages} />
  }
  return null
}
ServicesDetail.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default ServicesDetail
