import { useGetBlogPostbyForAdIdQuery } from "hooks/query/blog/useBlog"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import { useRouter } from "next/router"
import CreateBlog from "./CreateBlog"

const BlogEdit = () => {
  const router = useRouter()
  const { data, isLoading } = useGetBlogPostbyForAdIdQuery(
    router.query.id! as string
  )
  if (isLoading) {
    return <p>Loading</p>
  }
  return (
    <MainHeadingLayout heading="Edit Blog">
      <Head>
        <title>Update Blog</title>
      </Head>
      <CreateBlog labelForm="Update Blog" post={data?.data} mode="update" />
    </MainHeadingLayout>
  )
}

export default BlogEdit
