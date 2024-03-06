import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import SiderBar from "./section/siderbar"

const ProfilePage = () => {
  return (
    <>
      <Head>
        <title>Profile page</title>
        <link rel="icon" href="/images/favicon.png" />
      </Head>
      <UserSecondaryLayout breadrums={[]}>
        <SiderBar />
      </UserSecondaryLayout>
    </>
  )
}

export default ProfilePage
