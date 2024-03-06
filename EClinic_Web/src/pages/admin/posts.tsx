import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const PostFormPage = dynamic(
  () => import("module/Admin/PostForum/PostFormPage"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <PostFormPage />
}
Page.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}

export default Page
