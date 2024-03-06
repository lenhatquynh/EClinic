import { Slider } from "@mui/material"
import CheckBoxCustom from "components/Common/Checkbox"
import InputCustom from "components/Common/Input"
import PaginationCustom from "components/Common/Pagination"
import CustomButton from "components/User/Button"
import { useSearchDoctorProfilesQuery } from "hooks/query/profile/useProfile"
import { useGetAllSpecializationQuery } from "hooks/query/service/useService"
import useDebounce from "hooks/useDebounce"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import { ChangeEvent, useEffect, useState } from "react"
import { useTranslation } from "react-i18next"
import { HiMagnifyingGlass } from "react-icons/hi2"
import { formatValueToVND, getDataPaginate } from "shared/helpers/helper"
import { IBreadcrum } from "types/Base.type"
import FilterBar from "../components/filterBar/FilterBar"
import DoctorList from "./sections/DoctorList"

const DoctorPage = () => {
  const marks = [
    {
      value: 0,
      label: "0₫"
    },
    {
      value: 2500000,
      label: "2.500.000₫"
    },
    {
      value: 5000000,
      label: "5.000.000₫"
    }
  ]

  const [loadIndex, setLoadIndex] = useState(6)
  const { t } = useTranslation(["base", "ser"])
  const [searchData, setSearchData] = useState({
    searchText: "",
    price: 0,
    specializationID: ""
  })
  const searchTextDebounce = useDebounce(searchData.searchText, 1500)
  const searchPriceDebounce = useDebounce(searchData.price, 1500)
  const specializations = useGetAllSpecializationQuery({
    pageNumber: 1,
    pageSize: 100
  })
  const [showFilter, setShowFilter] = useState(false)

  const [pagination, setPagination] = useState({
    pageIndex: 1,
    pageSize: 10
  })

  const { data, isLoading } = useSearchDoctorProfilesQuery({
    searchText: searchTextDebounce,
    pageNumber: pagination.pageIndex,
    pageSize: pagination.pageSize,
    endPrice: searchPriceDebounce,
    startPrice: 0,
    specializationID: searchData.specializationID
  })
  const paginateData = getDataPaginate(data)

  useEffect(() => {
    if (window.innerWidth > 768) {
      setShowFilter(true)
    }
  }, [])
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.doctors") }
  ]
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchData((pveState) => ({
      ...pveState,
      specializationID: event.target.checked ? event.target.name : ""
    }))
  }
  return (
    <>
      <Head>
        <title>Doctor page</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <div className="grid grid-cols-3 gap-2 mt-6 md:mt-10 md:gap-7 md:hidden">
          <div className="col-span-3 md:hidden">
            <CustomButton
              kind="secondary"
              size="small"
              className="h-10  mx-auto w-[140px] rounded-[3px]"
              onClick={() => setShowFilter(!showFilter)}
            >
              <div className="flex items-center gap-2">
                <span>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    strokeWidth="1.5"
                    stroke="currentColor"
                    className="w-6 h-6"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M10.5 6h9.75M10.5 6a1.5 1.5 0 11-3 0m3 0a1.5 1.5 0 10-3 0M3.75 6H7.5m3 12h9.75m-9.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-3.75 0H7.5m9-6h3.75m-3.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-9.75 0h9.75"
                    />
                  </svg>
                </span>
                <span>Bộ lọc</span>
              </div>
            </CustomButton>
          </div>
        </div>
        <div className="flex mt-6 md:mt-10 ">
          <FilterBar
            show={showFilter}
            onClose={() => setShowFilter(!showFilter)}
          >
            <div className="space-y-2">
              <h3>{t("ser:input_search")}</h3>
              <InputCustom
                value={searchData.searchText}
                onChange={(e) =>
                  setSearchData((prev) => ({
                    ...prev,
                    searchText: e.target.value
                  }))
                }
                icon={<HiMagnifyingGlass />}
                className="w-full md:max-w-[412px]"
                placeholder={"Search by doctor's name"}
              />
            </div>
            <div className="space-y-2">
              <h3>{t("ser:specialist.label")}</h3>
              <div className="flex flex-col ">
                {specializations.data?.data.data.map((item, index) => {
                  if (index < loadIndex) {
                    return (
                      <CheckBoxCustom
                        key={item.specializationID}
                        name={item.specializationID}
                        label={item.specializationName}
                        checked={
                          searchData.specializationID === item.specializationID
                        }
                        onChange={handleChange}
                      />
                    )
                  }
                })}
                {specializations.data?.data.data.length &&
                  loadIndex < specializations.data?.data.data.length && (
                    <button
                      className="py-1 text-white border-none rounded shadow-sm outline-none cursor-pointer bg-primary"
                      onClick={() => setLoadIndex(loadIndex + 5)}
                    >
                      {t("ser:loadMore")}
                    </button>
                  )}
              </div>
            </div>
            <div className="space-y-2">
              <h3>{t("ser:range.label")}</h3>
              <div className="w-[95%]">
                <Slider
                  getAriaValueText={formatValueToVND}
                  max={5000000}
                  marks={marks}
                  step={10000}
                  size="medium"
                  value={searchData.price}
                  onChange={(e, value) =>
                    setSearchData((prev) => ({
                      ...prev,
                      price: value as number
                    }))
                  }
                  valueLabelDisplay="auto"
                />
              </div>
            </div>
          </FilterBar>
          <div className="flex-1 md:ml-16">
            <DoctorList isLoading={isLoading} data={data?.data.data} />
            <PaginationCustom
              onPageChange={(value) =>
                setPagination({ ...pagination, pageIndex: value })
              }
              pagination={paginateData}
              color="primary"
              className="pt-6 md:ml-auto md:w-fit"
              shape="rounded"
            />
          </div>
        </div>
      </UserSecondaryLayout>
    </>
  )
}

export default DoctorPage
