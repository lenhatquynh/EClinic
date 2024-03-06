import { QueryClient, dehydrate } from "@tanstack/react-query"
import UserLayout from "layout/User/UserLayout"
import { GetStaticPaths, GetStaticProps } from "next"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"
import { profileService } from "services/profile.service"
import { PAGE_SIZE, QUERY_KEYS } from "shared/constant/constant"
const DoctorDetailPage = dynamic(
  () => import("module/User/Doctor/DoctorDetailPage")
)

const Doctors: NextPageWithLayout = () => {
  const router = useRouter()
  if (router.query) {
    return <DoctorDetailPage />
  } else {
    return null
  }
}
Doctors.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export const getStaticPaths: GetStaticPaths = async () => {
  let paths: any = []
  try {
    const data = await profileService.getDoctorProfiles({
      pageNumber: 1,
      pageSize: PAGE_SIZE,
      searchText: ""
    })
    paths = data.data.data.map((doctor) => {
      return {
        params: {
          id: doctor.userID
        }
      }
    })
  } catch (error) {
    console.log("constgetStaticPaths:GetStaticPaths= ~ error:", error)
  }
  return {
    paths,
    fallback: true
  }
}
export const getStaticProps: GetStaticProps = async (context) => {
  const queryClient = new QueryClient()

  const id = context?.params?.id as string
  await queryClient.prefetchQuery([QUERY_KEYS.PROFILE, id], () =>
    profileService.getDoctorProfileById(id)
  )
  return {
    props: {
      dehydratedState: dehydrate(queryClient)
    }
  }
}
export default Doctors
