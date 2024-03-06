import { Box, IconButton, Tooltip } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom/ImageCustom"
import TableCustom from "components/Common/Table/TableCustom"
import { CustomInput } from "components/User/Input"
import { useSearchServicePackageForAdQuery } from "hooks/query/service/useService"
import useDebounce from "hooks/useDebounce"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import { useRouter } from "next/router"
import { useMemo, useState } from "react"
import { HiOutlinePencilSquare } from "react-icons/hi2"
import { getDataPaginate } from "shared/helpers/helper"
import { ServicePackage } from "types/Service"
const ListServicePackage = () => {
  const router = useRouter()
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 0,
    pageSize: 10
  })
  const [searchData, setSearchData] = useState("")
  const searchTextDebounce = useDebounce(searchData, 1000)

  const { data, isLoading, isError, isRefetching } =
    useSearchServicePackageForAdQuery({
      searchText: searchTextDebounce,
      pageNumber: pagination.pageIndex + 1,
      pageSize: pagination.pageSize
    })
  const paginationData = getDataPaginate(data)
  const columns = useMemo<MRT_ColumnDef<ServicePackage>[]>(
    () => [
      {
        accessorKey: "servicePackageID",
        header: "Id",
        size: 160,
        enableClickToCopy: true,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[160px]">
              {row.original.servicePackageID}
            </p>
          )
        }
      },
      {
        accessorKey: "servicePackageName",
        header: "Name",
        size: 340,
        Cell: ({ row }) => {
          return (
            <div className="flex items-center space-x-2 max-w-[340px]">
              <div className="relative w-10 h-10 overflow-hidden rounded-full">
                <ImageCustom
                  src={row.original.image || "/images/avatars/avatar_1.jpg"}
                  fill
                  alt="service-package-image"
                  className="object-cover"
                />
              </div>
              <span className="text-sm font-medium text-h1">
                {row.original.servicePackageName}
              </span>
            </div>
          )
        }
      },
      {
        accessorKey: "price",
        header: "Price",
        Cell: ({ row }) => {
          return <p className="line-clamp-1">{row.original.price} VND</p>
        }
      },
      {
        accessorKey: "discount",
        header: "Discount",
        Cell: ({ row }) => {
          return <p className="line-clamp-1">{row.original.discount} %</p>
        }
      },
      {
        accessorKey: "createdAt",
        header: "Discount Price",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1">
              {row.original.price * (1 - row.original.discount / 100)} VND
            </p>
          )
        }
      },
      {
        accessorKey: "totalOrder",
        header: "Total Order",
        Cell: ({ row }) => {
          return <p className="line-clamp-1">{row.original.totalOrder}</p>
        }
      },
      {
        accessorKey: "estimatedTime",
        header: "Estimate Time",
        Cell: ({ row }) => {
          return <p className="line-clamp-1">{row.original.estimatedTime}</p>
        }
      },
      {
        accessorKey: "isActive",
        header: "Status",
        size: 110,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[110px]">
              {row.original.isActive ? "Active" : "InActive"}
            </p>
          )
        }
      },
      {
        accessorKey: "serviceItems",
        header: "Services",
        size: 300,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[300px]">
              {row.original.serviceItems.map(
                (item, index) =>
                  item.serviceName +
                  (index + 1 === row.original.serviceItems.length ? "" : ",")
              )}
            </p>
          )
        }
      }
    ],
    []
  )
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
      renderRowActions={({ row }) => (
        <Box sx={{ display: "flex", gap: "1rem" }}>
          <Tooltip arrow placement="left" title="Edit">
            <IconButton
              onClick={() =>
                router.push(
                  `/admin/servicepackage/edit/${row.original.servicePackageID}`
                )
              }
            >
              <HiOutlinePencilSquare />
            </IconButton>
          </Tooltip>
        </Box>
      )}
      renderTopToolbarCustomActions={() => (
        <CustomInput
          placeholder="Search data here"
          className="max-w-[300px] w-full"
          onChange={(e) => setSearchData(e.target.value)}
        />
      )}
    />
  )
}

export default ListServicePackage
