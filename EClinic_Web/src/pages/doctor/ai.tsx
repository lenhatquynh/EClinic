import DoctorLayout from "layout/Management/DoctorLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const PredictPage = dynamic(() => import("module/Doctor/Predict/PredictPage"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <PredictPage />
}
Page.getLayout = (page) => {
  return <DoctorLayout>{page}</DoctorLayout>
}

export default Page
