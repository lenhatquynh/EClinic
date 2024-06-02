import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import Head from "next/head"
import { useMemo } from "react"
import Overview from "./section/Overview"

const HomePage = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `Overview`,
        children: <Overview />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Overview</title>
      </Head>
      <MainHeadingLayout heading="Management ">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default HomePage
