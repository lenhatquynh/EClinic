import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ServicesPage = dynamic(() => import("module/User/Services/ServicesPage"))
const Services: NextPageWithLayout = () => {
  return <ServicesPage />
}
Services.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Services
