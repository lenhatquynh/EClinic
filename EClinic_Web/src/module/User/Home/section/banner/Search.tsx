import { IconButton, InputAdornment, OutlinedInput } from "@mui/material"
import { useRouter } from "next/router"
import { useState } from "react"
import { useTranslation } from "react-i18next"
import { SearchWrapper } from "./Search.styles"

const Search = () => {
  const { t } = useTranslation("home")
  const [searchValue, setSearchValue] = useState("")
  const router = useRouter()
  const handleSubmit = () => {
    router.push(`/search?keyword=${searchValue}`)
  }
  return (
    <SearchWrapper className="absolute bottom-0 translate-y-2/4 w-full p-4  mx-auto bg-white rounded-xl md:p-8 shadow-[0px_1px_4px_rgba(0,_0,_0,_0.15)]">
      <div className="flex items-center justify-between space-x-2 md:space-x-4 h-11 md:h-[72px]">
        <OutlinedInput
          onChange={(e) => setSearchValue(e.target.value)}
          placeholder={t("banner.search.placeholder")}
          className="h-full bg-[#FAFBFE] text-xs md:text-base rounded-lg border-none outline-none"
          fullWidth
          startAdornment={
            <InputAdornment position="start">
              <IconSearch />
            </InputAdornment>
          }
        />
        <IconButton
          onClick={handleSubmit}
          aria-label="delete"
          className="text-white h-full w-14 md:w-[72px] flex items-center justify-center rounded-[10px] bg-primary hover:bg-primary"
        >
          <IconSearch />
        </IconButton>
      </div>
    </SearchWrapper>
  )
}
export const IconSearch = () => {
  return (
    <>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        strokeWidth="1.5"
        stroke="currentColor"
        className="w-5 h-5 md:w-9 md:h-9"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
        />
      </svg>
    </>
  )
}

export default Search
