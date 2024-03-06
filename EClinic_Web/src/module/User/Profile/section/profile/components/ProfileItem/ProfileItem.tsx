import { Skeleton } from "@mui/material"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import Tag from "components/Common/Tag"
import { RELATIONSHIPS } from "shared/constant/constant"
import { dayformat } from "shared/helpers/helper"
import { IProfile, IRelationShip } from "types/Profile.type"
interface Props {
  data?: IProfile & IRelationShip
  onClick: () => void
  className?: string
  loading: boolean
}

const ProfileItem = ({ data, onClick, loading = false, className }: Props) => {
  return (
    <>
      <li
        className={classNames(
          "flex px-2 py-2 transition-all rounded-md cursor-pointer gap-x-3 hover:bg-gray-100",
          className
        )}
        onClick={onClick}
      >
        {loading && <Skeleton variant="rounded" width={64} height={64} />}
        {data && (
          <div className="relative w-20 h-20 overflow-hidden rounded-md">
            <ImageCustom
              src={(data.avatar as string) || "/images/default.jpeg"}
              alt={data.lastName}
              sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
              fill
              classNameImage="rounded-md"
            />
          </div>
        )}

        <div className="flex flex-col space-y-[2px] flex-1">
          {loading && <Skeleton variant="rounded" width={40} height={16} />}

          {data && (
            <Tag
              color={
                data.relationshipName === RELATIONSHIPS.ME
                  ? "#07AB55"
                  : "#235EE8"
              }
            >
              {data.relationshipName}
            </Tag>
          )}
          {loading && (
            <>
              <Skeleton variant="text" sx={{ fontSize: "1rem" }} width={115} />
              <Skeleton
                variant="text"
                sx={{ fontSize: "0.75rem" }}
                width={115}
              />
            </>
          )}
          {data && (
            <>
              <span className="text-base font-medium max-w-[200px] line-clamp-1">
                {data.firstName + " " + data.lastName}
              </span>
              <time className="text-xs text-gray-400">
                {dayformat(data.dateOfBirth)}
              </time>
            </>
          )}
        </div>
      </li>
    </>
  )
}

export default ProfileItem
