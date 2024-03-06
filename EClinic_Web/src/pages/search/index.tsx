import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
import React from "react"
const SearchPage = dynamic(() => import("module/User/Search/SearchPage"))

const Search: NextPageWithLayout = () => {
  return <SearchPage />
}
Search.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Search
