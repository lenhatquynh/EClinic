import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import ListBlog from "module/Supporter/components/ListBlog"
import Head from "next/head"

const ManageBlog = () => {
  return (
    <>
      <Head>
        <title>Blog</title>
      </Head>
      <MainHeadingLayout heading="Manage Blog">
        <ListBlog />
      </MainHeadingLayout>
    </>
  )
}

export default ManageBlog
