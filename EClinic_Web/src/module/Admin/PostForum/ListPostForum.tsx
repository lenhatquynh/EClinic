import { IconButton, Tooltip } from "@mui/material"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import ImageCustom from "components/Common/ImageCustom"
import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import { CustomInput } from "components/User/Input"
import useConfirm from "context/ComfirmContext"
import { useGetAllPostForumQuery } from "hooks/query/forum/useForum"
import useDebounce from "hooks/useDebounce"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import ViewDetailPost from "module/Supporter/components/ViewDetailPost"
import { useMemo, useState } from "react"
import { toast } from "react-hot-toast"
import { AiOutlineEye } from "react-icons/ai"
import { MdOutlineDelete } from "react-icons/md"
import { forumService } from "services/forum.service"
import { QUERY_KEYS } from "shared/constant/constant"
import { combineName, dayformat, getDataPaginate } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { IPost } from "types/Post"

export default function ListPostForum() {
  const [open, setOpen] = useState(false)
  const [postSelected, setPostSelected] = useState<IPost | null>(null)
  const queryClient = useQueryClient()
  const deletePostMutation = useMutation(forumService.deletePostByID)
  const confirm = useConfirm()
  const [searchData, setSearchData] = useState("")
  const searchTextDebounce = useDebounce(searchData, 1500)
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 0,
    pageSize: 10
  })
  const { data, isLoading, isError, isRefetching } = useGetAllPostForumQuery(
    pagination.pageIndex + 1,
    pagination.pageSize,
    searchTextDebounce
  )

  const deletePost = async (postId: string) => {
    if (confirm) {
      const choice = await confirm({
        title: "Delete of post?",
        content: "Are you sure you want to delete this post?"
      })
      if (choice) {
        deletePostMutation.mutate(postId, {
          onSuccess: (data) => {
            queryClient.invalidateQueries([QUERY_KEYS.FORUM.POST])
            toast.success(data.message as string)
          },
          onError: () => {
            toast.error("Change of post failed")
          }
        })
      }
    }
  }
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
        accessorKey: "author",
        header: "Author",
        Cell: ({ row }) => {
          return (
            <div className="flex items-center gap-x-2">
              <div className="relative flex-shrink-0 w-8 h-8 rounded-full">
                <ImageCustom
                  src={row.original.author.avatar}
                  alt={row.original.author.firstName}
                  fill
                  className="rounded-full"
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
          <div className="flex items-center flex-shrink-0 ">
            <Tooltip title="View detail question">
              <IconButton
                onClick={() => {
                  setPostSelected(row.original)
                  setOpen(true)
                }}
              >
                <AiOutlineEye />
              </IconButton>
            </Tooltip>
            <Tooltip arrow placement="left" title="Delete this post">
              <IconButton onClick={() => deletePost(row.original.id)}>
                <MdOutlineDelete />
              </IconButton>
            </Tooltip>
          </div>
        )}
        renderTopToolbarCustomActions={() => (
          <CustomInput
            value={searchData}
            onChange={(e) => setSearchData(e.target.value)}
            placeholder="Search data here"
            className="max-w-[300px] w-full"
          />
        )}
      />
      {postSelected && (
        <ViewDetailPost
          post={postSelected}
          open={open}
          onClose={() => setOpen(!open)}
        />
      )}
    </>
  )
}
