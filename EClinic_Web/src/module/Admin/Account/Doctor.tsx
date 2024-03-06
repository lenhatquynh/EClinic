import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import CreateAccount from "module/Doctor/components/CreateAccount"
import ListDoctor from "module/Doctor/components/ListDoctor"
import Head from "next/head"
import React, { useMemo } from "react"

const Doctor = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `All Doctor`,
        children: <ListDoctor />
      },
      {
        key: 1,
        label: `Create account`,
        children: <CreateAccount labelForm="Create Account" />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Doctor account</title>
      </Head>
      <MainHeadingLayout heading="Account Doctor">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}
export default Doctor
