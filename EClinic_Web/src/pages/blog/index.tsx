import UserLayout from "layout/User/UserLayout"
import { NextPageWithLayout } from "../page"
import dynamic from "next/dynamic"
const BlogPage = dynamic(() =>
  import("module/User/Blog/BlogPage").then((module) => module.default)
)
const Page: NextPageWithLayout = () => {
  return <BlogPage />
}
Page.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Page
