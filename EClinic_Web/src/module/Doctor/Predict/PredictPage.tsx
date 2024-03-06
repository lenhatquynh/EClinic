import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TestModel from "module/Expert/Home/section/components/TestModel"
import Head from "next/head"
import React from "react"

const PredictPage = () => {
  return (
    <>
      <Head>
        <title>Predict page</title>
      </Head>
      <MainHeadingLayout heading="Predict disease">
        <TestModel />
      </MainHeadingLayout>
    </>
  )
}

export default PredictPage
