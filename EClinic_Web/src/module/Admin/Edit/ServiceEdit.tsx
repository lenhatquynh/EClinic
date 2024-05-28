import { useGetServiceByIDForAdQuery } from "hooks/query/service/useService"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import CreateService from "module/Admin/Service/CreateService"
import Head from "next/head"
import { useRouter } from "next/router"
import React from "react"

const ServiceEdit = () => {
  const router = useRouter()
  const { data, isLoading } = useGetServiceByIDForAdQuery(
    router.query.id! as string
  )
  if (isLoading) {
    return <p>Loading Service</p>
  }
  return (
    <MainHeadingLayout heading="Update Service">
      <Head>
        <title>Update Service</title>
      </Head>
      <CreateService service={data?.data} mode="update" />
    </MainHeadingLayout>
  )
}

export default ServiceEdit
