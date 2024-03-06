import { Box, IconButton, Tooltip } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import { Option, SelectCustom } from "components/Common/Select/SelectCustom"
import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import useConfirm from "context/ComfirmContext"
import {
  useGetAllBookingServicerForAdQuery,
  useUpdateStateBookingServiceDoneMutation
} from "hooks/query/booking"
import { MRT_PaginationState, type MRT_ColumnDef } from "material-react-table"
import { queryClient } from "pages/_app"
import { useMemo, useState } from "react"
import { toast } from "react-hot-toast"
import { AiOutlineFileDone } from "react-icons/ai"
import { useSelector } from "react-redux"
import { QUERY_KEYS, STATUS_BOOKING } from "shared/constant/constant"
import { dayformat, getDataPaginate } from "shared/helpers/helper"
import { RootState } from "store/store"
import { BookingService as IBookingService } from "types/Booking"
const statusBookingOptions: Option[] = Object.entries(STATUS_BOOKING).map(
  ([key, value]) => ({
    label: key,
    value: value.toString()
  })
)

const TableAPMService = () => {
  const role = useSelector((state: RootState) => state.auth.user.role)
  const confirm = useConfirm()
  const doneBooking = useUpdateStateBookingServiceDoneMutation()
  const [statusFilter, setStatusFilter] = useState(statusBookingOptions[0])
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 1,
    pageSize: 10
  })
  const { data, isLoading, isError, isRefetching } =
    useGetAllBookingServicerForAdQuery(
      pagination.pageIndex + 1,
      pagination.pageSize,
      Number(statusFilter.value)
    )
  //should be memoized or stable
  const columns = useMemo<MRT_ColumnDef<IBookingService>[]>(
    () => [
      {
        accessorKey: "service", //access nested data with dot notation
        header: "Service",
        enableSorting: false,
        Cell: ({ row }) => {
          const data = row.original.service
          // return <Info data={row.original.userProfile as Author} />
          return (
            <div className="flex items-center space-x-3">
              <div className="relative h-14 md:w-11 w-14 md:h-11">
                {data && (
                  <ImageCustom
                    src={data.image || "/images/sample.png"}
                    fill
                    alt="image"
                    className="object-cover rounded-full"
                  />
                )}
              </div>
              <div className="flex flex-col text-[#9A9FA5] text-sm md:text-[10px] font-medium">
                <span className="text-black">{data.servicePackageName}</span>
              </div>
            </div>
          )
        }
      },
      {
        accessorKey: "profile", //access nested data with dot notation
        header: "Patient profile",
        enableSorting: false,
        Cell: ({ row }) => {
          const data = row.original.profile
          // return <Info data={row.original.userProfile as Author} />
          return (
            <div className="flex items-center space-x-3">
              <div className="relative h-14 md:w-11 w-14 md:h-11">
                {data && (
                  <ImageCustom
                    src={data.avatar || "/images/sample.png"}
                    fill
                    alt="image"
                    className="object-cover rounded-full"
                  />
                )}
              </div>
              <div className="flex flex-col text-[#9A9FA5] text-sm md:text-[10px] font-medium">
                <span className="text-black">
                  {data && <>{data.firstName + " " + data.lastName}</>}
                </span>
              </div>
            </div>
          )
        }
      },
      {
        accessorKey: "appoinmentTime",
        header: "Appointment Date",
        Cell: ({ row }) => {
          return (
            <div className="flex flex-col gap-y-2">
              <time dateTime={row.original.appoinmentTime}>
                {dayformat(row.original.appoinmentTime)}
              </time>
            </div>
          )
        }
      },
      {
        accessorKey: "bookingStatus",
        header: "Status",
        Cell: ({ cell }) => {
          return cell.getValue() === 1 ? (
            <Tag color="#FEAF02">Upcoming</Tag>
          ) : cell.getValue() === 2 ? (
            <Tag color="#4FD8DE">Done</Tag>
          ) : (
            <Tag color="#D72755">Cancelled</Tag>
          )
        }
      }
    ],
    []
  )
  const paginationData = getDataPaginate(data)
  const handleDone = async (bookingId: string) => {
    if (confirm) {
      const choice = await confirm({
        title: <h3 className="text-2xl text-error">Done Appointment</h3>,
        content: (
          <div className="flex flex-col gap-y-2w">
            <p>Are you sure you want to complete this appoiment?</p>
          </div>
        ),
        btnAgree: "Yes",
        btnDisagree: "Back"
      })
      if (choice) {
        doneBooking.mutate(bookingId, {
          onSuccess() {
            toast.success("Completed appointment")
            queryClient.refetchQueries([QUERY_KEYS.BOOKING.SERVICE])
          },
          onError() {
            toast.error("Completed failure")
          }
        })
      }
    }
  }
  return (
    <TableCustom
      pagination={pagination}
      onPaginationChange={setPagination}
      columns={columns}
      data={data?.data?.data ?? []}
      rowCount={paginationData.TotalCount ?? 0}
      pageCount={paginationData.TotalPages ?? 0}
      isLoading={isLoading}
      isError={isError}
      isRefetching={isRefetching}
      enableRowActions={true}
      renderRowActions={({ row }) => {
        if (row.original.bookingStatus.toString() === "1") {
          return (
            <Box sx={{ display: "flex", gap: "1rem" }}>
              <Tooltip arrow placement="left" title="Click to complete booking">
                <IconButton onClick={() => handleDone(row.original.bookingID)}>
                  <AiOutlineFileDone />
                </IconButton>
              </Tooltip>
            </Box>
          )
        }
      }}
      renderTopToolbarCustomActions={() => (
        <SelectCustom
          size="small"
          className="max-w-[140px]"
          value={statusFilter.value}
          options={statusBookingOptions}
          onSelectOption={(value) => setStatusFilter(value)}
        />
      )}
    />
  )
}
export default TableAPMService
