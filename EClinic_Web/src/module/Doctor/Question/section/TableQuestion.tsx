import ImageCustom from "components/Common/ImageCustom"
import TableCustom from "components/Common/Table/TableCustom"
import { useGetPostNoAnserForumQuery } from "hooks/query/forum/useForum"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import { useMemo, useState } from "react"
import { combineName, dayformat, getDataPaginate } from "shared/helpers/helper"
import { IPost } from "types/Post"
import UpdateQuestion from "./UpdateQuestion"
import Tag from "components/Common/Tag"
import colorsProvider from "shared/theme/colors"

export default function TableQuestion() {
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 1,
    pageSize: 10
  })

  const { data, isLoading, isError, isRefetching } =
    useGetPostNoAnserForumQuery(pagination.pageIndex + 1, pagination.pageSize)

  const columns = useMemo<MRT_ColumnDef<IPost>[]>(
    () => [
      {
        accessorKey: "title",
        header: "Title",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[120px]">{row.original.title}</p>
          )
        }
      },
      {
        accessorKey: "content",
        header: "Description",
        size: 400,
        Cell: ({ row }) => {
          return (
            <p className="flex items-center space-x-2 line-clamp-2">
              {row.original.content}
            </p>
          )
        }
      },
      {
        accessorKey: "author",
        header: "Author",
        Cell: ({ row }) => {
          return (
            <div className="flex items-center gap-x-2">
              <div className="relative w-8 h-8 rounded-full">
                <ImageCustom
                  src={row.original.author.avatar}
                  alt={row.original.author.firstName}
                  fill
                  sizes="(max-width: 768px) 50vw,
              (max-width: 1200px) 30vw,
              22vw"
                />
              </div>
              <span>
                {combineName(
                  row.original.author.lastName,
                  row.original.author.firstName
                )}
              </span>
            </div>
          )
        }
      },
      {
        accessorKey: "createdAt",
        header: "Created at",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 ">{dayformat(row.original.createdAt)}</p>
          )
        }
      },
      {
        accessorKey: "likes",
        header: "Total like"
      },
      {
        accessorKey: "isActive",
        header: "Status",
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
      renderRowActions={({ row }) => <UpdateQuestion post={row.original} />}
      // renderTopToolbarCustomActions={() => (
      //   <CustomInput
      //     placeholder="Search data here"
      //     className="max-w-[300px] w-full"
      //   />
      // )}
    />
  )
}
