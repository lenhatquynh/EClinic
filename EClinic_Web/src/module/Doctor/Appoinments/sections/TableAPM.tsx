import { Box, IconButton, Tooltip } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import { Option, SelectCustom } from "components/Common/Select/SelectCustom"
import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import { useGetAllBookingDoctorForAdQuery } from "hooks/query/booking"
import { MRT_PaginationState, type MRT_ColumnDef } from "material-react-table"
import { useRouter } from "next/router"
import { useMemo, useState } from "react"
import { useSelector } from "react-redux"
import { ROLE, STATUS_BOOKING } from "shared/constant/constant"
import { dayformat, getDataPaginate } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { RootState } from "store/store"
import { IBookingDoctor } from "types/Booking"
const statusBookingOptions: Option[] = Object.entries(STATUS_BOOKING).map(
  ([key, value]) => ({
    label: key,
    value: value.toString()
  })
)

const TableAPMService = () => {
  const role = useSelector((state: RootState) => state.auth.user.role)
  const router = useRouter()
  const [statusFilter, setStatusFilter] = useState(statusBookingOptions[0])
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 0,
    pageSize: 10
  })
  const { data, isLoading, isError, isRefetching } =
    useGetAllBookingDoctorForAdQuery(
      pagination.pageIndex + 1,
      pagination.pageSize,
      Number(statusFilter.value)
    )
  //should be memoized or stable
  const columns = useMemo<MRT_ColumnDef<IBookingDoctor>[]>(
    () => [
      {
        accessorKey: "userProfile", //access nested data with dot notation
        header: "Patient name",
        enableSorting: false,
        Cell: ({ row }) => {
          const data = row.original.userProfile
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
        accessorKey: "slot",
        header: "Appointment Date",
        Cell: ({ row }) => {
          return (
            <div className="flex flex-col gap-y-2">
              <time dateTime={row.original.bookingTime}>
                {dayformat(row.original.bookingTime)}
              </time>
              <time className="text-gray80">
                {row.original.slot.startTime} - {row.original.slot.endTime}
              </time>
            </div>
          )
        }
      },
      {
        accessorKey: "bookingType",
        header: "Kind",
        Cell: ({ row }) => {
          return (
            <div>
              {row.original.bookingType.toString() === "0" ? (
                <Tag color={colorsProvider.primary}>Online</Tag>
              ) : (
                <Tag color={colorsProvider.secondary}>Offline</Tag>
              )}
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
      enableRowActions={!(role === ROLE.ADMIN || role === ROLE.SUPPORTER)}
      renderRowActions={({ row }) => {
        if (row.original.bookingType.toString() === "0") {
          return (
            <Box sx={{ display: "flex", gap: "1rem" }}>
              <Tooltip arrow placement="left" title="Go to chat">
                <IconButton
                  onClick={() =>
                    router.push(
                      `/admin/accounts/doctor/edit/${row.original.price}`
                    )
                  }
                >
                  <Tag
                    color={colorsProvider.primary}
                    className="cursor-pointer"
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
                        d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z"
                      />
                    </svg>
                  </Tag>
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
