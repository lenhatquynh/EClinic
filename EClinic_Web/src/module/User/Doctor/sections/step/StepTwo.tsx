import { Tooltip } from "@mui/material"
import EmtyData from "components/Common/Empty"
import InputCustom from "components/Common/Input"
import { Option, SelectCustom } from "components/Common/Select/SelectCustom"
import { useSearchFamlyProfilesQuery } from "hooks/query/profile/useProfile"
import useDebounce from "hooks/useDebounce"
import ProfileItem from "module/User/Profile/section/profile/components/ProfileItem"
import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { BOOKING_TYPE, PAGE_SIZE } from "shared/constant/constant"
import {
  selectBookingType,
  selectProfileWithDoctor
} from "store/module/booking/doctor/booking-doctor-selector"
import { bookingDoctorSlice } from "store/module/booking/doctor/booking-doctor-slice"
import { PropsStep } from "./StepOne"
import { useTranslation } from "react-i18next"

export const StepTwo = ({ onBack }: PropsStep) => {
  const { t } = useTranslation(["base"])
  const dispatch = useDispatch()
  const [searchData, setSearchData] = useState("")
  const profile = useSelector(selectProfileWithDoctor)
  const bookingType = useSelector(selectBookingType)
  const searchTextDebounce = useDebounce(searchData, 1000)
  const profiles = useSearchFamlyProfilesQuery(1, PAGE_SIZE, searchTextDebounce)
  useEffect(() => {
    if (profiles.isSuccess) {
      console.log("useEffect ~ profiles.isSuccess:", profiles.isSuccess)
      dispatch(
        bookingDoctorSlice.actions.profileChange(profiles.data?.data.data[0])
      )
    }
  }, [profiles.isSuccess])
  return (
    <>
      <div className="flex items-center gap-x-3">
        <button
          className="h-[50px] w-[50px] rounded-full shadow flex items-center justify-center cursor-pointer outline-none border-none"
          onClick={() => onBack && onBack()}
        >
          <svg
            width={13}
            height={20}
            viewBox="0 0 13 20"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M11.6267 16.7721C12.3049 17.3945 12.3502 18.4488 11.7278 19.127C11.1054 19.8052 10.0511 19.8504 9.37294 19.2281L0.801512 11.3622C0.0812647 10.7013 0.0818923 9.56521 0.802869 8.90504L9.3743 1.05661C10.0532 0.434993 11.1074 0.481413 11.729 1.16029C12.3507 1.83916 12.3042 2.89342 11.6254 3.51503L4.39489 10.1356L11.6267 16.7721Z"
              fill="#403ECC"
            />
          </svg>
        </button>
        <span>{t("base:booking.btn_black")}</span>
      </div>
      <div className="flex justify-between my-8 ">
        <div className="flex flex-col gap-y-2">
          <h3 className="text-2xl text-h1">{t("base:booking.add")}</h3>
        </div>
      </div>
      <div className="mb-4 modal-filed">
        <div className="flex items-center gap-x-1">
          <span className="label">{t("base:booking.select_payment")}</span>
          <Tooltip
            title={
              <ul className="flex flex-col list-disc">
                <li>If you choose online, you have to pay 100%.</li>
                <li>If you choose offline, you must deposit 20%</li>
              </ul>
            }
            placement="top"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9 5.25h.008v.008H12v-.008z"
              />
            </svg>
          </Tooltip>
        </div>
        <SelectCustom
          size="small"
          className="max-w-[160px]"
          value={bookingType.toString()}
          options={
            Object.keys(BOOKING_TYPE).map((item, index) => ({
              label: item,
              value: index.toString()
            })) as Option[]
          }
          onSelectOption={(value) => {
            dispatch(bookingDoctorSlice.actions.bookingTypeChange(value.value))
          }}
        />
      </div>
      <div className="modal-filed ">
        <span className="label">{t("base:booking.select_pro")}</span>
        <div className="flex mb-3 gap-x-2">
          <InputCustom
            onChange={(e) => setSearchData(e.target.value)}
            value={searchData}
            className="w-full md:max-w-[300px]"
            placeholder={t("base:booking.input_search")}
          />
        </div>
        <ul className="max-h-[600px] overflow-auto gap-2 grid grid-cols-2">
          {profiles.data?.data.data?.map((item, index) => (
            <ProfileItem
              className={
                profile?.profileID === item.profileID
                  ? "border border-primary border-solid bg-primary bg-opacity-5"
                  : ""
              }
              data={item}
              key={index}
              loading={false}
              onClick={() =>
                dispatch(bookingDoctorSlice.actions.profileChange(item))
              }
            />
          ))}
          {profiles.isLoading &&
            Array(2)
              .fill(0)
              .map((_, index) => (
                <ProfileItem onClick={() => {}} key={index} loading={true} />
              ))}
        </ul>
        {profiles.data?.data.data && profiles.data?.data.data?.length < 1 && (
          <EmtyData />
        )}
      </div>
    </>
  )
}
