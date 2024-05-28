import CustomButton from "components/User/Button"
import React from "react"
import { useTranslation } from "react-i18next"
import CardBlog from "./components/CardBlog/CardBlog"
import { useRouter } from "next/router"
import { useGetAllBlog } from "hooks/query/blog/useBlog"
import { IBlog } from "types/Blog"

const Blog = () => {
  const { t } = useTranslation("home")
  const router = useRouter()
  const { data } = useGetAllBlog(1, 4)
  const blogs: IBlog[] = data?.data?.data as IBlog[]
  return (
    <section className="flex flex-col w-full ">
      <h3 className="mb-3 text-center heading-section ">{t("blog.title")}</h3>
      <div className="relative grid grid-cols-1 pt-8 gap-y-2 md:gap-x-4 md:grid-cols-4">
        {blogs?.map((blog) => (
          <CardBlog key={blog.id} blog={blog} />
        ))}
      </div>
      <CustomButton
        kind="secondary"
        className="max-w-[200px] mx-auto mt-6"
        onClick={() => {
          router.push("/blog")
        }}
      >
        {t("blog.btn")}
      </CustomButton>
    </section>
  )
}

export default Blog
