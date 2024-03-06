import EmtyData from "components/Common/Empty"
import PaginationCustom from "components/Common/Pagination"
import CardForum from "module/User/components/CardForum"
import CardForumLoading from "module/User/components/CardForum/Loading"
import { ReactNode } from "react"
import { IPagination } from "types/Pagination"
import { IPost } from "types/Post"
interface Props {
  title: string | ReactNode
  posts: IPost[] | undefined
  isLoading: boolean
  paginate: IPagination
  // eslint-disable-next-line no-unused-vars
  onPageIndexChange: (index: number) => void
}

const ListCardForum = ({
  title,
  isLoading,
  posts,
  paginate,
  onPageIndexChange
}: Props) => {
  return (
    <>
      <div className="flex flex-col w-full gap-y-4 ">
        <h4 className="text-xl "> {title}</h4>
        {posts?.length === 0 && <EmtyData />}
        <div className="grid grid-cols-1 gap-8 md:grid-cols-2">
          {isLoading &&
            Array(4)
              .fill(0)
              .map((_, index) => <CardForumLoading key={index} />)}
          {posts &&
            posts.map((post, index) => <CardForum key={index} post={post} />)}
        </div>
        <PaginationCustom
          onPageChange={(value) => onPageIndexChange(value)}
          pagination={paginate}
          color="primary"
          className="pt-6 md:ml-auto md:w-fit"
          shape="rounded"
        />
      </div>
    </>
  )
}

export default ListCardForum
