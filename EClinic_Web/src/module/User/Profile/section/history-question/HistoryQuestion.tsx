import { Chip, Skeleton } from "@mui/material"
import { useQueryClient } from "@tanstack/react-query"
import PaginationCustom from "components/Common/Pagination"
import Tag from "components/Common/Tag"
import Popover from "components/User/Popover"
import {
  useDeletePostbyIdMutaiton,
  useGetAnwerByPostId,
  useGetPostByUserId
} from "hooks/query/forum/useForum"
import Info from "module/User/components/Info/Info"
import Link from "next/link"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { HiOutlineDotsHorizontal } from "react-icons/hi"
import { QUERY_KEYS } from "shared/constant/constant"
import { dayformat } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { IPagination } from "types/Pagination"
import { IPost } from "types/Post"

const HistoryQuestion = () => {
  const [pageIndex, setPageIndex] = useState(1)

  const { data, isLoading } = useGetPostByUserId(pageIndex, 2)
  const paginateData = data?.headers["x-pagination"]
    ? (JSON.parse(data.headers["x-pagination"]) as IPagination)
    : {
        PageIndex: pageIndex,
        PageSize: 0,
        TotalCount: 0,
        TotalPages: 0,
        HasPrevious: false,
        HasNext: false
      }
  return (
    <div className="flex flex-col justify-start">
      <div className="grid w-full gap-6 md:grid-cols-2">
        {isLoading &&
          Array(2)
            .fill(0)
            .map((_, index) => <QuestionItemLoading key={index} />)}
        {data?.data.data.map((ques) => (
          <QuestionItem key={ques.id} ques={ques} />
        ))}
      </div>
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
const QuestionItem = ({ ques }: { ques: IPost }) => {
  const { data, isLoading } = useGetAnwerByPostId(ques.id)
  const queryClient = useQueryClient()
  const deletePostbyIdMutaiton = useDeletePostbyIdMutaiton()
  const handleDeltePost = () => {
    deletePostbyIdMutaiton.mutate(ques.id, {
      onSuccess: () => {
        queryClient.invalidateQueries([QUERY_KEYS.FORUM.POST])
        toast.success("Delete successfully!")
      },
      onError: () => {
        toast.error("Delete Failed!")
      }
    })
  }
  return (
    <div className="flex flex-col px-4 py-3 rounded-md shadow">
      <Link href={`/forum/${ques.id}`}>
        <div className="flex justify-between">
          {isLoading ? (
            <Skeleton variant="rounded" width={52} height={32} />
          ) : data?.data.id ? (
            <Tag color={colorsProvider.success}>Answer</Tag>
          ) : (
            <Tag color={colorsProvider.pending}>Watting</Tag>
          )}

          <span className="text-sm text-gray-400">
            {dayformat(ques.createdAt)}
          </span>
        </div>
        <h3 className="mt-4 font-normal line-clamp-2">{ques.title}</h3>
        <div className="w-full h-[1px] bg-gray-200 my-4"></div>
      </Link>
      <div className="flex items-center justify-between">
        <Info data={ques.author} />
        <Popover
          position="left"
          className="w-fit"
          buttonTrigger={
            <button
              className="inline-flex items-center p-2 text-sm font-medium text-center text-gray-400 bg-white border-none rounded-lg cursor-pointer hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-50"
              type="button"
            >
              <HiOutlineDotsHorizontal className="text-xl" />
            </button>
          }
        >
          <ul className="py-2 bg-white rounded-md shadow">
            <li
              className="px-4 py-2 text-sm text-red-500 transition-all cursor-pointer hover:bg-gray-200"
              onClick={() => handleDeltePost()}
            >
              Delete
            </li>
          </ul>
        </Popover>
      </div>
    </div>
  )
}
const QuestionItemLoading = () => {
  return (
    <div className="flex flex-col px-4 py-3 rounded-md shadow">
      <div className="flex justify-between">
        <Skeleton variant="rounded" width={52} height={32} />
        <Skeleton variant="text" width={62} />
      </div>
      <h3 className="mt-4 font-normal line-clamp-2">
        <Skeleton variant="text" />
      </h3>
      <div className="w-full h-[1px] bg-gray-200 my-4"></div>
      <Info loading={true} />
    </div>
  )
}
export default HistoryQuestion
