import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import dynamic from "next/dynamic"
import { useMemo } from "react"
const DoctorBooking = dynamic(() => import("./components/DoctorBooking"), {
  ssr: false
})
const ServicesBooking = dynamic(() => import("./components/ServicesBooking"), {
  ssr: false
})
const AppointmentSchedule = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `Doctor`,
        children: <DoctorBooking />
      },
      {
        key: 1,
        label: `Services`,
        children: <ServicesBooking />
      }
    ],
    []
  )
  return (
    <>
      <MainHeadingLayout heading="Your Booking">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default AppointmentSchedule
