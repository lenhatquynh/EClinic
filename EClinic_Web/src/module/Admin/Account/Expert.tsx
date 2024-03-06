import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import CreateAccountExpert from "module/Doctor/components/CreateAccountExpert"
import ListExpert from "module/Doctor/components/ListExpert"
import Head from "next/head"
import { useMemo } from "react"

const Expert = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `All Expert`,
        children: <ListExpert />
      },
      {
        key: 1,
        label: `Create account`,
        children: <CreateAccountExpert labelForm="Create Account Expert" />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Export account</title>
      </Head>
      <MainHeadingLayout heading="Account Experts">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default Expert
