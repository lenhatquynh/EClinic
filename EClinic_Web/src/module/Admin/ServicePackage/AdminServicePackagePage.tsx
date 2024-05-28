import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import CreateServicePackage from "./CreateServicePackage"
import ListServicePackage from "./ListServicePackage"
import Head from "next/head"
import React, { useMemo } from "react"

const AdminServicePackagePage = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `All service package`,
        children: <ListServicePackage />
      },
      {
        key: 1,
        label: `Create service package`,
        children: <CreateServicePackage labelForm="Create" />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Service Package</title>
      </Head>
      <MainHeadingLayout heading="Service Package">
        <TabsCustom defaultTabIndex={0} tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default AdminServicePackagePage
