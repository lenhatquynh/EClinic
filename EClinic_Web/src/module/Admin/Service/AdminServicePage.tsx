import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import CreateService from "module/Admin/Service/CreateService"
import ListService from "./ListService"
import Head from "next/head"
import React, { useMemo } from "react"

const AdminServicePage = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `All service`,
        children: <ListService />
      },
      {
        key: 1,
        label: `Create service`,
        children: <CreateService />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Service item</title>
      </Head>
      <MainHeadingLayout heading="Service">
        <TabsCustom defaultTabIndex={0} tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default AdminServicePage
