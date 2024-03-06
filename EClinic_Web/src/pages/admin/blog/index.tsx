import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ManageBlog = dynamic(() => import("module/Admin/Blog/ManageBlog"), {
  ssr: false
})

const Create: NextPageWithLayout = () => {
  return <ManageBlog />
}
Create.getLayout = (page) => {
  return <AdminLayout>{page}</AdminLayout>
}
export default Create
