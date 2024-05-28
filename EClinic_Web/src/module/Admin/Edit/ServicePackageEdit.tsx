import { useGetServicePackageByIDForAdQuery } from "hooks/query/service/useService"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import { useRouter } from "next/router"
import React from "react"
import CreateServicePackage from "../ServicePackage/CreateServicePackage"

const ServiceEdit = () => {
  const router = useRouter()
  const { data, isLoading } = useGetServicePackageByIDForAdQuery(
    router.query.id! as string
  )
  if (isLoading) {
    return <p>Loading Service Package</p>
  }
  return (
    <MainHeadingLayout heading="Update Service Package">
      <Head>
        <title>Update Service Package</title>
      </Head>
      <CreateServicePackage
        labelForm="Update"
        servicePackage={data?.data}
        mode="update"
      />
    </MainHeadingLayout>
  )
}

export default ServiceEdit
