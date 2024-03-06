import EmtyData from "components/Common/Empty"
import PaginationCustom from "components/Common/Pagination"
import TabButton from "components/User/Booking/TabButton"
import { useGetAllBookingDoctorQuery } from "hooks/query/booking"
import { useState } from "react"
import { PAGE_SIZE } from "shared/constant/constant"
import { getDataPaginate } from "shared/helpers/helper"
import DoctorBookingItem from "./DoctorBookingItem"
export type KindAppoiment = "cancelled" | "completed" | "upcomming"
const DoctorBooking = () => {
  const [type, setType] = useState<number>(1)
  const [pageIndex, setPageIndex] = useState(1)

  const bookingData = useGetAllBookingDoctorQuery(pageIndex, PAGE_SIZE, type)
  if (bookingData.isError) {
    return (
      <>
        <EmtyData />
      </>
    )
  }
  const paginateData = getDataPaginate(bookingData.data)
  return (
    <div className="flex flex-col">
      <TabButton setType={setType} type={type} />
      <div className="grid grid-cols-2 gap-4"></div>
      {bookingData.isLoading && <p>loading.......</p>}
      {bookingData.data?.data && bookingData.data?.data.data.length > 0 ? (
        <div className="grid gap-4 md:grid-cols-2">
          {bookingData.data?.data.data.map((booking, index) => (
            <DoctorBookingItem data={booking} kind={type} key={index} />
          ))}
        </div>
      ) : (
        <EmtyData />
      )}
      <PaginationCustom
        onPageChange={(value) => setPageIndex(value)}
        pagination={paginateData}
        color="primary"
        className="pt-6 md:ml-auto md:w-fit"
        shape="rounded"
      />
    </div>
  )
}

export default DoctorBooking
