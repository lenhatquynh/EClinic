import { IconButton, InputAdornment, OutlinedInput } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import { useSearchPostsBlog } from "hooks/query/blog/useBlog"
import { useSearchPostsForum } from "hooks/query/forum/useForum"
import { useSearchServicePackageQuery } from "hooks/query/service/useService"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { useTranslation } from "react-i18next"
import { IBreadcrum } from "types/Base.type"
import { IconSearch } from "../Home/section/banner/Search"
import ResultsSearch from "./sections/results"
const SearchPage = () => {
  const { t } = useTranslation(["home", "base"])
  const router = useRouter()
  const [searchValue, setSearchValue] = useState("")
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.search") }
  ]
  const ServicePackagesResult = useSearchServicePackageQuery({
    searchText: (router.query.keyword as string) || "",
    pageNumber: 1,
    pageSize: 4
  })
  const PostsForumResult = useSearchPostsForum(
    router.query.keyword as string,
    1,
    4,
    []
  )
  const PostsBlogResult = useSearchPostsBlog(
    router.query.keyword as string,
    1,
    4,
    []
  )
  useEffect(() => {
    if (router.query.keyword) {
      setSearchValue(router.query.keyword?.toString())
    }
  }, [router.query])
  return (
    <>
      <Head>
        <title>Search page</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <div className="flex flex-col items-center w-full pb-3 mx-auto bg-white">
          <div className="relative w-[560px] h-[250px]">
            <ImageCustom
              src={"/images/search-image.webp"}
              alt="search-image"
              fill
              className="object-cover"
              sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
            />
          </div>
          <h1 className="py-3 text-2xl font-semibold text-h1">
            {t("base:pages.search_page.title")}
          </h1>
          <div className="flex items-center justify-between h-11 md:h-[64px] w-full max-w-[652px] mb-4">
            <OutlinedInput
              onChange={(e) => setSearchValue(e.target.value)}
              placeholder={t("banner.search.placeholder")}
              className="h-full bg-[#FAFBFE] text-xs md:text-base rounded-none rounded-tl-lg rounded-bl-lg border-r-0 border-none outline-none"
              fullWidth
              value={searchValue}
              startAdornment={
                <InputAdornment position="start">
                  <IconSearch />
                </InputAdornment>
              }
            />
            <IconButton
              onClick={() => router.push(`/search?keyword=${searchValue}`)}
              aria-label="delete"
              className="text-white h-full w-14 md:w-[72px] flex items-center justify-center rounded-none rounded-tr-lg rounded-br-lg bg-primary hover:bg-primary "
            >
              <IconSearch />
            </IconButton>
          </div>
        </div>
        <div className="flex flex-col gap-y-4">
          {ServicePackagesResult.isLoading ? (
            "Loading"
          ) : (
            <ResultsSearch
              servicePackages={ServicePackagesResult.data?.data?.data}
              blogs={PostsBlogResult.data?.data?.data}
              posts={PostsForumResult.data?.data?.data}
            />
          )}
        </div>
      </UserSecondaryLayout>
    </>
  )
}

export default SearchPage
