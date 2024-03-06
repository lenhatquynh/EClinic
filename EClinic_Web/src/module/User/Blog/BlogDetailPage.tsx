import { Grid, SpeedDial, SpeedDialAction, Typography } from "@mui/material"
import { LoadingArea } from "components/Common/Loading/LoadingIcon"
import {
  useGetBlogPostbyIdQuery,
  useSearchPostsBlog
} from "hooks/query/blog/useBlog"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import Image from "next/image"
import { useRouter } from "next/router"
import { useTranslation } from "react-i18next"
import { AiOutlineInstagram } from "react-icons/ai"
import { HiOutlineShare } from "react-icons/hi2"
import { PAGE_SIZE } from "shared/constant/constant"
import { combineName, dayformat } from "shared/helpers/helper"
import { IBreadcrum } from "types/Base.type"
import BlogPostCard from "./components/BlogPostCard"
import EmtyData from "components/Common/Empty"
import ImageCustom from "components/Common/ImageCustom"
import colorsProvider from "shared/theme/colors"

const actions = [
  {
    icon: (
      <>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          xmlnsXlink="http://www.w3.org/1999/xlink"
          aria-hidden="true"
          role="img"
          className="MuiBox-root css-xe43za iconify iconify--eva"
          width="1em"
          height="1em"
          viewBox="0 0 24 24"
        >
          <g id="iconifyReact1530">
            <g id="iconifyReact1531">
              <path
                id="iconifyReact1532"
                fill="currentColor"
                d="M17 3.5a.5.5 0 0 0-.5-.5H14a4.77 4.77 0 0 0-5 4.5v2.7H6.5a.5.5 0 0 0-.5.5v2.6a.5.5 0 0 0 .5.5H9v6.7a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-6.7h2.62a.5.5 0 0 0 .49-.37l.72-2.6a.5.5 0 0 0-.48-.63H13V7.5a1 1 0 0 1 1-.9h2.5a.5.5 0 0 0 .5-.5Z"
              />
            </g>
          </g>
        </svg>
      </>
    ),
    name: "Facebook"
  },
  {
    icon: <AiOutlineInstagram />,
    name: "Instagram"
  }
]

const BlogDetailPage = () => {
  const router = useRouter()
  const { t } = useTranslation(["base", "forum"])
  const { data, isLoading, isError } = useGetBlogPostbyIdQuery(
    router.query.id as string
  )
  const postRelated = useSearchPostsBlog(
    "",
    1,
    4,
    data?.data.hashtags.map((has) => has.id) || []
  )
  if (router.isFallback || isLoading || postRelated.isLoading) {
    return (
      <>
        <LoadingArea />
      </>
    )
  }
  if (isError || postRelated.isError) {
    return <EmtyData />
  }
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.blog") }
  ]
  return (
    <>
      <Head>
        <title>{data.data.metaTitle}</title>
        <meta name="description" content={data.data.metaDescription}></meta>
        <meta name="keywords" content={data.data.metaKeywords}></meta>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <div className="flex-1 p-0">
          <div className="flex flex-col w-full h-full p-0 background-primary">
            <div className="w-full h-[648px] relative rounded-tr-2xl rounded-tl-2xl overflow-hidden">
              <ImageCustom
                fill
                src={data.data.coverImage || "/images/covers/cover_1.jpg"}
                alt="thumbnails"
                className="object-cover"
              />
              <div className="absolute inset-0 z-10 overflow-hidden backdrop-brightness-50 rounded-tr-2xl rounded-tl-2xl"></div>

              <h1 className="absolute top-0 z-20 p-20 text-5xl font-bold leading-snug text-snow">
                {data?.data.title}
              </h1>
              <div className="absolute bottom-0 z-20 flex items-center justify-between w-full p-20">
                <div className="flex items-center gap-x-3">
                  <div className="relative w-12 h-12 overflow-hidden rounded-full">
                    <Image
                      src={
                        data?.data.author.avatar ||
                        "/images/avatars/avatar_1.jpg"
                      }
                      fill
                      alt="avatart"
                    />
                  </div>
                  <div className="flex flex-col gap-y-1">
                    <h3 className="text-base font-bold leading-normal text-snow">
                      {combineName(
                        data?.data.author.firstName,
                        data?.data.author.lastName
                      )}
                    </h3>
                    <Typography
                      gutterBottom
                      variant="caption"
                      sx={{
                        color: colorsProvider.background,
                        display: "block",
                        cursor: "pointer"
                      }}
                    >
                      {dayformat(data?.data.updatedAt)}
                    </Typography>
                  </div>
                </div>
                <SpeedDial
                  direction="left"
                  ariaLabel="SpeedDial basic example"
                  icon={<HiOutlineShare />}
                >
                  {actions.map((action) => (
                    <SpeedDialAction
                      key={action.name}
                      icon={action.icon}
                      tooltipTitle={action.name}
                    />
                  ))}
                </SpeedDial>
              </div>
            </div>
            <div className="w-full my-8 mx-9 md:my-16">
              <div
                className="entry-content"
                // Prevent XSS Attack recommen from React Docs
                dangerouslySetInnerHTML={{
                  __html: data.data.content || ""
                }}
              ></div>
            </div>
          </div>

          <div className="flex flex-col">
            <h3 className="my-10 text-2xl font-semibold text-h1">
              Recent posts
            </h3>
            <Grid container spacing={3}>
              {postRelated.data?.data.data.map((post, index) => (
                <BlogPostCard key={post.id} post={post} index={index + 1} />
              ))}
            </Grid>
          </div>
        </div>
      </UserSecondaryLayout>
    </>
  )
}

export default BlogDetailPage
