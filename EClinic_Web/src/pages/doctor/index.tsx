import DoctorLayout from "layout/Management/DoctorLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
import React from "react"
const DoctorHomePage = dynamic(
  () => import("module/Doctor/Home/DoctorHomePage"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <DoctorHomePage />
}
Page.getLayout = (page) => {
  return <DoctorLayout>{page}</DoctorLayout>
}

export default Page
