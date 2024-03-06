import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import ListPatient from "module/Doctor/components/ListPatient"
import Head from "next/head"

const Patient = () => {
  return (
    <>
      <Head>
        <title>Patient account</title>
      </Head>
      <MainHeadingLayout heading="Account Patient">
        <ListPatient />
      </MainHeadingLayout>
    </>
  )
}

export default Patient
