import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import ChangePassword from "module/User/Profile/section/change-password"
import Head from "next/head"
import React from "react"

const ChangePasswordPage = () => {
  return (
    <>
      <Head>
        <title>Change password</title>
      </Head>
      <MainHeadingLayout heading="Change password">
        <div className="p-6 bg-white rounded-xl">
          <ChangePassword />
        </div>
      </MainHeadingLayout>
    </>
  )
}

export default ChangePasswordPage
