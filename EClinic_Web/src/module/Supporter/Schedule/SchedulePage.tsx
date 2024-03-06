import Head from "next/head"
import React from "react"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import ScheduleAPM from "module/Doctor/Appoinments/sections/ScheduleAPM"

const SchedulePage = () => {
  return (
    <>
      <Head>
        <title>Schedule management</title>
      </Head>
      <MainHeadingLayout heading="Manage schedule doctor">
        <ScheduleAPM />
      </MainHeadingLayout>
    </>
  )
}

export default SchedulePage
