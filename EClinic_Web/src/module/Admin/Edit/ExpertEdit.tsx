import { useGetSupporterByIdQuery } from "hooks/query/profile/useProfile"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import CreateAccountExpert from "module/Doctor/components/CreateAccountExpert"
import Head from "next/head"
import { useRouter } from "next/router"

const ExpertEdit = () => {
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
      <CreateAccountExpert
        labelForm="Update account Expert"
        profile={data?.data}
        mode="update"
      />
    </MainHeadingLayout>
  )
}

export default ExpertEdit
