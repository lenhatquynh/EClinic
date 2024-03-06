import { Chip, Skeleton } from "@mui/material"
import { useInfiniteQuery } from "@tanstack/react-query"
import classNames from "classnames"
import ChipCustom from "components/Common/Chip/Chip"
import InputCustom from "components/Common/Input"
import Spinner from "components/Common/Loading/LoadingIcon"
import CustomButton from "components/User/Button"
import { useState } from "react"
import { useTranslation } from "react-i18next"
import { AiOutlinePlus } from "react-icons/ai"
import { HiMagnifyingGlass } from "react-icons/hi2"
import { forumService } from "services/forum.service"
import { PAGE_SIZE, QUERY_KEYS } from "shared/constant/constant"
import { getDataPaginate } from "shared/helpers/helper"
import { IHashtag } from "types/Post"

type Props = {
  tagsActive: IHashtag[]
  // eslint-disable-next-line no-unused-vars
  onSearchChange: (value: string) => void
  // eslint-disable-next-line no-unused-vars
  onChangeHashTags: (value: IHashtag[]) => void
  isLoading: boolean
}
const Search = ({
  tagsActive,
  onSearchChange,
  onChangeHashTags,
  isLoading = false
}: Props) => {
  const { t } = useTranslation("forum")
  const [keyword, setKeyword] = useState("")
  const tagsQuery = useInfiniteQuery(
    [QUERY_KEYS.HASHTAG, PAGE_SIZE],
    async ({ pageParam = 1 }) => {
      const res = await forumService.getTagSortByCount(pageParam, PAGE_SIZE)
      return res
    },
    {
      getNextPageParam: (lastPage) =>
        getDataPaginate(lastPage).HasNext
          ? getDataPaginate(lastPage).PageIndex + 1
          : undefined
    }
  )
  const handleSearchBykeyword = (hashtagCurrent: IHashtag) => {
    const checkExits = tagsActive.some(
      (item) => item.hashtagID === hashtagCurrent.hashtagID
    )
    let newHashTags = [] as IHashtag[]
    if (!checkExits) {
      newHashTags = [...tagsActive, hashtagCurrent]
    } else {
      newHashTags = tagsActive.filter(
        (item) => item.hashtagID !== hashtagCurrent.hashtagID
      )
    }
    onChangeHashTags(newHashTags)
  }
  return (
    <div className="flex flex-col space-y-3">
      <div className="flex gap-x-1">
        <InputCustom
          onChange={(e) => setKeyword(e.target.value)}
          icon={<HiMagnifyingGlass />}
          className="w-full md:max-w-[412px]"
          placeholder={t("search.input")}
        />
        <CustomButton
          kind="primary"
          disabled={isLoading}
          className="!rounded-[5px]"
          onClick={() => onSearchChange(keyword)}
        >
          {isLoading ? <Spinner /> : "Search"}
        </CustomButton>
      </div>
      <div className="w-full px-4 py-5 background-primary">
        <h3 className="text-xl">{t("search.heading")}</h3>
        <ul className="flex flex-wrap items-center w-full gap-4 mt-3 overflow-auto list-none">
          {tagsQuery.data?.pages.map((page) =>
            page.data.data.map((item, index) => {
              const check = tagsActive.some(
                (tag) => tag.hashtagID === item.hashtagID
              )
              return (
                <li key={index}>
                  <ChipCustom
                    className={check ? "bg-primary text-white" : ""}
                    label={item.hashtagName}
                    onClick={() => handleSearchBykeyword(item)}
                  />
                </li>
              )
            })
          )}

          {tagsQuery.isLoading &&
            new Array(3).fill(null).map((item, index) => (
              <li key={index}>
                <Skeleton variant="rounded" width={100} height={30} />
              </li>
            ))}
          <Chip
            onClick={() => {
              tagsQuery.hasNextPage && tagsQuery.fetchNextPage()
            }}
            label="Load more"
            icon={<AiOutlinePlus className="text-base" />}
            clickable
            className={classNames(
              "!bg-opacity-25 rounded-md font-semibold",
              !tagsQuery.hasNextPage && "hidden"
            )}
            color="primary"
          />
        </ul>
      </div>
    </div>
  )
}

export default Search
