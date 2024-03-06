import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ManageBlog = dynamic(() => import("module/Supporter/Blog/ManageBlog"), {
  ssr: false
})

const Create: NextPageWithLayout = () => {
  return <ManageBlog />
}
Create.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}
export default Create
