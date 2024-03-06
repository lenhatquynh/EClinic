import { Skeleton } from "@mui/material"
import classNames from "classnames"
interface Props {
  kind?: "large" | "medium"
}
const CardForumLoading = ({ kind = "medium" }: Props) => {
  return (
    <div
      className={classNames(
        "grid w-full rounded-md overflow-hidden",
        kind === "large" && "md:grid-cols-8 md:gap-x-4",
        kind === "medium" && "grid-cols-1 gap-y-4 "
      )}
    >
      <div
        className={classNames(
          "relative rounded-md overflow-hidden",
          kind === "large" && "col-span-5 h-[200px] md:h-[245px]",
          kind === "medium" && "col-span-5 h-[140px] md:h-[200px]"
        )}
      >
        <Skeleton variant="rounded" className="absolute w-full h-full" />
      </div>
      <div className={classNames(kind === "large" && "space-y-4 col-span-3")}>
        <h4 className="text-[22px] text-[#304050] font-semibold line-clamp-2">
          <Skeleton variant="text" />
        </h4>
        <div className="flex items-center space-x-3">
          <div className="relative h-14 md:w-11 w-14 md:h-11">
            <Skeleton variant="circular" className="w-full h-full" />
          </div>
          <div className="max-w-[160px] w-full flex flex-col">
            <Skeleton variant="text" className="w-1/3" />
            <Skeleton variant="text" className="w-2/4" />
            <Skeleton variant="text" className="w-full" />
          </div>
        </div>
      </div>
    </div>
  )
}

export default CardForumLoading
