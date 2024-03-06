import { Box, IconButton, Tooltip } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import { CustomInput } from "components/User/Input"
import { useSearchPostsBlogAd } from "hooks/query/blog/useBlog"
import useDebounce from "hooks/useDebounce"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import Link from "next/link"
import { useMemo, useState } from "react"
import { HiOutlinePencilSquare } from "react-icons/hi2"
import { useSelector } from "react-redux"
import { ROLE } from "shared/constant/constant"
import { combineName, dayformat, getDataPaginate } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { RootState } from "store/store"
import { IBlog } from "types/Blog"

const ListBlog = () => {
  const auth = useSelector((state: RootState) => state.auth)

  const [searchData, setSearchData] = useState("")
  const searchTextDebounce = useDebounce(searchData, 1500)
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 1,
    pageSize: 10
  })
  const { data, isLoading, isError, isRefetching } = useSearchPostsBlogAd(
    searchTextDebounce,
    pagination.pageIndex + 1,
    pagination.pageSize,
    []
  )
  const columns = useMemo<MRT_ColumnDef<IBlog>[]>(
    () => [
      {
        accessorKey: "id",
        header: "Id",
        size: 60,
        enableClickToCopy: true,
        Cell: ({ row }) => {
          return <p className="line-clamp-1 max-w-[120px]">{row.original.id}</p>
        }
      },
      {
        accessorFn: (row) => `${row.coverImage} ${row.title} ${row.updatedAt}`,
        header: "Post",
        size: 260,
        Cell: ({ row }) => {
          return (
            <div className="flex items-center space-x-2">
              <div className="relative flex-shrink-0 w-16 h-12">
                {row.original.coverImage && (
                  <ImageCustom
                    src={row.original.coverImage}
                    fill
                    alt="user-avatar"
                    className="object-cover rounded-md"
                  />
                )}
              </div>
              <div className="flex flex-col">
                <h4 className="text-base font-medium text-h1 line-clamp-1">
                  {row.original.title}
                </h4>
                <time className="text-xs text-disable">
                  {dayformat(row.original.updatedAt)}
                </time>
              </div>
            </div>
          )
        }
      },
      {
        accessorKey: "hashtags",
        header: "HashTags",
        Cell: ({ row }) => {
          return (
            <div className="flex flex-wrap gap-2">
              {row.original.hashtags.map((item, index) => (
                <Tag color={colorsProvider.secondary} key={index}>
                  {item.hashtagName}
                </Tag>
              ))}
            </div>
          )
        }
      },
      {
        accessorKey: "author",
        header: "Author",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 ">
              {combineName(
                row.original.author.firstName,
                row.original.author.lastName
              )}
            </p>
          )
        }
      },
      {
        accessorKey: "isActive",
        header: "Status",
        size: 60,
        Cell: ({ row }) => {
          return (
            <Tag
              color={
                row.original.isActive
                  ? colorsProvider.success
                  : colorsProvider.error
              }
            >
              {row.original.isActive ? "Active" : "Banned"}
            </Tag>
          )
        }
      }
    ],
    []
  )
  const paginationData = getDataPaginate(data)
  return (
    <>
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
              <IconButton>
                <Link
                  href={`/${
                    auth.user.role === ROLE.SUPPORTER ? "sup" : "admin"
                  }/blog/edit/${row.original.id}`}
                >
                  <HiOutlinePencilSquare />
                </Link>
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
    </>
  )
}

export default ListBlog
