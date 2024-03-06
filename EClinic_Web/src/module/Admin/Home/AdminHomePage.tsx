import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import React from "react"
import HeaderCard from "./sections/HeaderCard"
import ChartEarning from "./sections/ChartEarning"
import TableAPM from "module/Doctor/Appoinments/sections/TableAPM"

const AdminHomePage = () => {
  return (
    <>
      <Head>
        <title>Overview</title>
      </Head>
      <MainHeadingLayout heading="Overview ">
        <HeaderCard />
        <ChartEarning />
        <TableAPM />
      </MainHeadingLayout>
    </>
  )
}

export default AdminHomePage
