import ExpertLayout from "layout/Management/ExpertLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
import React from "react"
const HomePage = dynamic(() => import("module/Expert/Home/HomePage"))

const Page: NextPageWithLayout = () => {
  return <HomePage />
}
Page.getLayout = (page) => {
  return <ExpertLayout>{page}</ExpertLayout>
}

export default Page
