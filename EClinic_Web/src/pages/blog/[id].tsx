import { QueryClient, dehydrate } from "@tanstack/react-query"
import UserLayout from "layout/User/UserLayout"
import { GetStaticPaths, GetStaticProps } from "next"
import { blogService } from "services/blog.service"
import { QUERY_KEYS } from "shared/constant/constant"
import { NextPageWithLayout } from "../page"
import dynamic from "next/dynamic"
const BlogDetailPage = dynamic(() => import("module/User/Blog/BlogDetailPage"))
const Page: NextPageWithLayout = () => {
  return <BlogDetailPage />
}
Page.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export const getStaticPaths: GetStaticPaths = async () => {
  let paths: any = []
  try {
    const data = await blogService.searchPosts("", 1, 10, [])
    paths = data?.data.data.map((post) => {
      return {
        params: {
          id: post.id
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
  await queryClient.prefetchQuery([QUERY_KEYS.BLOG.POST, id], () =>
    blogService.getPostById(id)
  )
  return {
    props: {
      dehydratedState: dehydrate(queryClient)
    }
  }
}
export default Page
