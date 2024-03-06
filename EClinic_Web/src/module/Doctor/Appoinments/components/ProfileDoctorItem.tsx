import { Skeleton } from "@mui/material"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import Tag from "components/Common/Tag"
import { dayformat } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { IProfileDoctor } from "types/Profile.type"
interface Props {
  data?: IProfileDoctor
  onClick: () => void
  className?: string
  loading: boolean
}

const ProfileDoctorItem = ({
  data,
  onClick,
  loading = false,
  className
}: Props) => {
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

          {data && <Tag color={colorsProvider.success}>{data.title}</Tag>}
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
              <span className="text-base font-medium">
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

export default ProfileDoctorItem
