import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import TableAPM from "module/Doctor/Appoinments/sections/TableAPM"
import TableAPMService from "module/Doctor/Appoinments/sections/TableAPMService"
import Head from "next/head"
import React, { useMemo } from "react"

const AppointmentPage = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `Booking doctor`,
        children: <TableAPM />
      },
      {
        key: 1,
        label: `Booking service`,
        children: <TableAPMService />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Appointment list</title>
      </Head>
      <MainHeadingLayout heading="Appointment list">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default AppointmentPage
