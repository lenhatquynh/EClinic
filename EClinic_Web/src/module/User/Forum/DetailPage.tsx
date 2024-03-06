import { LoadingArea } from "components/Common/Loading/LoadingIcon"
import { useGetPostbyIdQuery } from "hooks/query/forum/useForum"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import { useRouter } from "next/router"
import { useTranslation } from "react-i18next"
import { IBreadcrum } from "types/Base.type"
import DetailForum from "./section/detail"
import { CreateQuestion } from "./section/create"
import EmtyData from "components/Common/Empty"
const DetailPage = () => {
  const router = useRouter()
  const id = router.query.id as string
  const { data, isLoading, isError } = useGetPostbyIdQuery(id)
  const { t } = useTranslation(["base", "forum"])
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.forum"), href: "/forum" },
    { label: t("base:pages.detail") }
  ]
  if (router.isFallback) {
    return (
      <>
        <LoadingArea />
      </>
    )
  }
  if (isLoading) {
    return <LoadingArea />
  }
  if (isError) {
    return <EmtyData />
  }
  return (
    <>
      <Head>
        <title>Forum page</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <div className="grid grid-cols-3 gap-4 mt-6 md:gap-7">
          <div className="order-last col-span-3 space-y-4 md:order-first md:col-span-2">
            <DetailForum post={data.data} />
          </div>
          <div className="col-span-3 md:col-span-1">
            <CreateQuestion />
          </div>
        </div>
      </UserSecondaryLayout>
    </>
  )
}

export default DetailPage
