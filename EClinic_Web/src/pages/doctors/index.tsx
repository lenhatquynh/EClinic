import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const DoctorPage = dynamic(() => import("module/User/Doctor/DoctorPage"))

const Doctors: NextPageWithLayout = () => {
  return <DoctorPage />
}
Doctors.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Doctors
