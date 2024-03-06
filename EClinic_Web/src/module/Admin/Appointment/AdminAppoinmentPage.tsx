import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TableAPM from "module/Doctor/Appoinments/sections/TableAPM"
import Head from "next/head"
import React from "react"

const AdminAppoinmentPage = () => {
  return (
    <>
      <Head>
        <title>Overview</title>
      </Head>
      <MainHeadingLayout heading="Appointment">
        <TableAPM />
      </MainHeadingLayout>
    </>
  )
}

export default AdminAppoinmentPage
