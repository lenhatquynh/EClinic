import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import Head from "next/head"
import { useMemo } from "react"
import Overview from "./section/Overview"
import PredictionHistorys from "./section/components/PredictionHistorys"
import TestModel from "./section/components/TestModel"

const HomePage = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `Overview`,
        children: <Overview />
      },
      {
        key: 1,
        label: `Prediction history`,
        children: <PredictionHistorys />
      },
      {
        key: 2,
        label: `Test model`,
        children: (
          <>
            <TestModel />
          </>
        )
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
