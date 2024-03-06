import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import CreateAccount from "module/Doctor/components/CreateAccount"
import CreateAccountSupporter from "module/Doctor/components/CreateAccountSupporter"
import ListDoctor from "module/Doctor/components/ListDoctor"
import ListSupporter from "module/Doctor/components/ListSupporter"
import Head from "next/head"
import React, { useMemo } from "react"

const Supporter = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `All Supporter`,
        children: <ListSupporter />
      },
      {
        key: 1,
        label: `Create account`,
        children: (
          <CreateAccountSupporter labelForm="Create Account Supporter" />
        )
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Supporter account</title>
      </Head>
      <MainHeadingLayout heading="Account Supporters">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default Supporter
