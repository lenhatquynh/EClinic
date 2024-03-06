import { useGetDoctorProfilesByIdQuery } from "hooks/query/profile/useProfile"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import CreateAccount from "module/Doctor/components/CreateAccount"
import Head from "next/head"
import { useRouter } from "next/router"
import React from "react"

const DoctorEdit = () => {
  const router = useRouter()
  const { data, isLoading } = useGetDoctorProfilesByIdQuery(
    router.query.id! as string
  )
  if (isLoading) {
    return <p>Loading</p>
  }
  return (
    <MainHeadingLayout heading="Edit Account">
      <Head>
        <title>Update Account</title>
      </Head>
      <CreateAccount
        labelForm="Update account"
        profile={data?.data}
        mode="update"
      />
    </MainHeadingLayout>
  )
}

export default DoctorEdit
