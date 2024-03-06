import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import TableAPMForDoctor from "./sections/TableAPMForDoctor"

const AppoinmentPage = () => {
  return (
    <>
      <Head>
        <title>Appointment page</title>
      </Head>
      <MainHeadingLayout heading="Appointment List">
        <TableAPMForDoctor />
      </MainHeadingLayout>
    </>
  )
}

export default AppoinmentPage
