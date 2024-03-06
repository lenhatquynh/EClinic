import {
  Box,
  Chip,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
  Stack,
  Typography
} from "@mui/material"
import InputCustom from "components/Common/Input"
import PaginationCustom from "components/Common/Pagination"
import { useGetAllHashTag, useSearchPostsBlog } from "hooks/query/blog/useBlog"
import useDebounce from "hooks/useDebounce"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import { useState } from "react"
import { useTranslation } from "react-i18next"
import { HiMagnifyingGlass } from "react-icons/hi2"
import { PAGE_SIZE } from "shared/constant/constant"
import { getDataPaginate } from "shared/helpers/helper"
import { IBreadcrum } from "types/Base.type"
import { HashTagBlog } from "types/Blog"
import BlogPostCard from "./components/BlogPostCard"
export default function BlogPage() {
  const [pageIndex, setPageIndex] = useState(1)
  const hashTags = useGetAllHashTag()

  const [searchData, setSearchData] = useState({
    searchText: "",
    tags: [] as HashTagBlog[]
  })
  const searchTextDebounce = useDebounce(searchData.searchText, 1500)

  const { t } = useTranslation(["base", "forum"])
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.forum") }
  ]
  const postsBlog = useSearchPostsBlog(
    searchTextDebounce,
    pageIndex,
    PAGE_SIZE,
    searchData.tags.map((item) => item.id)
  )
  const paginateData = getDataPaginate(postsBlog.data)
  const handleChange = (event: SelectChangeEvent<string[]>) => {
    const {
      target: { value }
    } = event
    let newHashtags: HashTagBlog[] = []
    if (typeof value === "string") {
      newHashtags =
        hashTags.data?.data.data.filter((item) => {
          if (item.id === value) {
            return item
          }
        }) || []
    } else {
      newHashtags =
        hashTags.data?.data.data.filter((item) => {
          if (value.some((has) => has === item.id)) {
            return item
          }
        }) || []
    }
    setSearchData({ ...searchData, tags: newHashtags })
  }
  const getHashtagName = (hashtagID: string) => {
    const selectedTag = hashTags.data?.data.data.find(
      (tag) => tag.id === hashtagID
    )
    return selectedTag ? selectedTag.hashtagName : ""
  }
  return (
    <>
      <Head>
        <title>Blog</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4" gutterBottom>
            Blog
          </Typography>
        </Stack>

        <Stack
          mb={5}
          direction="row"
          alignItems="center"
          justifyContent="space-between"
        >
          <div className="flex gap-x-2">
            <InputCustom
              value={searchData.searchText}
              onChange={(e) =>
                setSearchData({ ...searchData, searchText: e.target.value })
              }
              icon={<HiMagnifyingGlass />}
              className="!w-[360px] h-[42px]"
              placeholder={t("forum:search.input")}
            />
            <FormControl
              size="small"
              className="max-w-[400px] min-w-[200px] w-full flex-1"
            >
              <InputLabel>Hashtags</InputLabel>
              <Select
                multiple
                value={searchData.tags?.map((item) => item.id) || []}
                onChange={handleChange}
                input={<OutlinedInput label="Hashtags" />}
                renderValue={(selected) => (
                  <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                    {selected.map((value) => (
                      <Chip key={value} label={getHashtagName(value)} />
                    ))}
                  </Box>
                )}
              >
                {hashTags.data?.data.data.map((name, index) => (
                  <MenuItem key={index} value={name.id}>
                    {name.hashtagName}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </div>
        </Stack>

        <Grid container spacing={3}>
          {postsBlog.data?.data.data.map((post, index) => (
            <BlogPostCard key={post.id} post={post} index={index} />
          ))}
        </Grid>
        <Stack>
          <PaginationCustom
            onPageChange={(value) => setPageIndex(value)}
            pagination={paginateData}
            color="primary"
            className="pt-6 md:ml-auto md:w-fit"
            shape="rounded"
          />
        </Stack>
      </UserSecondaryLayout>
    </>
  )
}
