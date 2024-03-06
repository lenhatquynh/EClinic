import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"

const BlogEdit = dynamic(
  () =>
    import("module/Supporter/components/EditBlog").then(
      (module) => module.default
    ),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  const router = useRouter()
  if (router.query.id) {
    return <BlogEdit />
  }
  return null
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
