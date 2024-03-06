import { useGetSupporterByIdQuery } from "hooks/query/profile/useProfile"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import CreateAccountSupporter from "module/Doctor/components/CreateAccountSupporter"
import Head from "next/head"
import { useRouter } from "next/router"

const DoctorEdit = () => {
  const router = useRouter()
  const { data, isLoading } = useGetSupporterByIdQuery(
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
      <CreateAccountSupporter
        labelForm="Update account supporter"
        profile={data?.data}
        mode="update"
      />
    </MainHeadingLayout>
  )
}

export default DoctorEdit
