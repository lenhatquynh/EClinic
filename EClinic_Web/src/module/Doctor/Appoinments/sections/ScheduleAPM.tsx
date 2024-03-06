import { Box, Skeleton } from "@mui/material"
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers"
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs"
import EmtyData from "components/Common/Empty"
import InputCustom from "components/Common/Input"
import Tag from "components/Common/Tag"
import dayjs from "dayjs"
import { useGetDoctorScheduleForUser } from "hooks/query/booking"
import { useGetDoctorProfilesQuery } from "hooks/query/profile/useProfile"
import useDebounce from "hooks/useDebounce"
import ProfileItem from "module/User/Profile/section/profile/components/ProfileItem"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { HiChevronLeft, HiChevronRight } from "react-icons/hi2"
import colorsProvider from "shared/theme/colors"
import { IProfileDoctor } from "types/Profile.type"
import ModelSchedule from "../components/ModelSchedule"
import ProfileDoctorItem from "../components/ProfileDoctorItem"
import { OverlayScrollbarsComponent } from "overlayscrollbars-react"

const ScheduleAPM = () => {
  const [doctorSelected, setdoctorSelected] = useState<IProfileDoctor | null>(
    null
  )
  const [date, setDate] = useState(dayjs())

  const [pagination, setPagination] = useState({
    pageIndex: 1,
    pageSize: 100
  })
  const [searchData, setSearchData] = useState("")
  const searchTextDebounce = useDebounce(searchData, 1500)
  const getDoctorSchedule = useGetDoctorScheduleForUser(
    date.format("YYYY-MM-DD").toString(),
    doctorSelected?.userID || ""
  )
  const { data, isLoading } = useGetDoctorProfilesQuery({
    searchText: searchTextDebounce,
    pageNumber: pagination.pageIndex,
    pageSize: pagination.pageSize
  })
  const [showModal, setShowModal] = useState(false)
  const changeDate = (step = 1) => {
    const newDate = dayjs(date).add(step, "days")
    setDate(newDate)
  }
  const handleAddShowDateOverride = () => {
    if (!doctorSelected) {
      toast.error("Please choose doctor")
    } else {
      setShowModal(true)
    }
  }
  return (
    <section className="flex flex-col background-primary ">
      <div className="grid grid-cols-5">
        <div className="col-span-2 mr-4">
          <h4 className="mb-2 text-sm font-semibold">Choose profile doctor</h4>
          <div className="flex mb-3 gap-x-2">
            <InputCustom
              onChange={(e) => setSearchData(e.target.value)}
              value={searchData}
              className="w-full md:max-w-[300px]"
              placeholder="Find a profile by keyword"
            />
          </div>
          <OverlayScrollbarsComponent
            defer
            options={{ scrollbars: { autoHide: "scroll" } }}
          >
            <ul className="flex flex-col gap-2 p-1 max-h-[600px]">
              {data?.data.data?.map((item, index) => (
                <ProfileDoctorItem
                  className={
                    doctorSelected?.profileID === item.profileID
                      ? "ring-2 bg-primary bg-opacity-5"
                      : ""
                  }
                  data={item}
                  key={index}
                  loading={false}
                  onClick={() => {
                    setdoctorSelected(item)
                  }}
                />
              ))}
              {isLoading &&
                Array(2)
                  .fill(0)
                  .map((_, index) => (
                    <ProfileItem
                      onClick={() => {}}
                      key={index}
                      loading={true}
                    />
                  ))}
            </ul>
          </OverlayScrollbarsComponent>

          {data?.data.data && !isLoading && data?.data.data?.length < 1 && (
            <EmtyData />
          )}
        </div>
        <div className=" col-span-3 border border-y-0 border-l-[2px] border-gray-300 border-solid border-r-0 ">
          <div className="px-4">
            <h4 className="mb-2 text-sm font-semibold">Add date overrides</h4>
            <button
              className="px-6 py-2 rounded-[40px] w-fit border border-solid border-gray-400 flex items-center justify-center focus-within:bg-gray-300 transition-all outline-none text-sm cursor-pointer bg-transparent mt-4 "
              onClick={() => handleAddShowDateOverride()}
            >
              Add a date override
            </button>
            <div className="w-full h-[2px] bg-gray-300 my-4"></div>
            <div className="flex flex-col gap-y-4">
              <div className="flex items-center mx-auto w-full max-w-[340px] justify-between">
                <HiChevronLeft
                  className="cursor-pointer"
                  onClick={() => changeDate(-1)}
                />

                <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <DatePicker
                    label="Custom input"
                    value={date}
                    onChange={(newValue) => {
                      if (newValue) setDate(newValue)
                    }}
                    renderInput={({ inputRef, InputProps }) => (
                      <Box sx={{ display: "flex", alignItems: "center" }}>
                        <time ref={inputRef}>
                          {date.format("dddd, DD MMM YYYY")}
                        </time>

                        {InputProps?.endAdornment}
                      </Box>
                    )}
                  />
                </LocalizationProvider>
                <HiChevronRight
                  className="cursor-pointer"
                  onClick={() => changeDate(1)}
                />
              </div>
              <div className="flex-1 mt-6 modal-filed">
                {getDoctorSchedule.isLoading && (
                  <div className="grid h-full grid-cols-4 gap-3 overflow-y-auto">
                    {Array(20)
                      .fill(0)
                      .map((_, index) => (
                        <Skeleton
                          key={index}
                          variant="rounded"
                          width={129}
                          height={32}
                        />
                      ))}
                  </div>
                )}
                {getDoctorSchedule.data ? (
                  <div className="grid h-full grid-cols-3 gap-3 overflow-y-auto">
                    {getDoctorSchedule.data.data.slots.map((item, index) => (
                      <Tag
                        key={index}
                        color={colorsProvider.primary}
                        className="w-full cursor-pointer"
                      >
                        <p
                          className={item.isBooking ? "line-through" : ""}
                        >{`${item.startTime} - ${item.endTime}`}</p>
                      </Tag>
                    ))}
                  </div>
                ) : (
                  !getDoctorSchedule.isLoading && (
                    <EmtyData
                      message={
                        doctorSelected
                          ? "No doctor's schedule"
                          : "Please choose doctor to see schedule"
                      }
                      className="flex-1"
                    />
                  )
                )}
              </div>
            </div>

            <ModelSchedule
              doctorID={doctorSelected?.userID || ""}
              show={showModal}
              onChangeModel={(value) => setShowModal(value)}
            />
          </div>
        </div>
      </div>
    </section>
  )
}
export default ScheduleAPM
