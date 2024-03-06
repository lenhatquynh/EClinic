import { Skeleton } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import Tag from "components/Common/Tag"
import React from "react"
import { STATISTIC_STATUS } from "shared/constant/constant"
import { formatValueToVND } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { IStatictis } from "types/Base.type"
interface IProps {
  icon: string
  title: string
  isLoading?: boolean
  data?: IStatictis
  isMoney?: boolean
}
const Card = ({ icon, title, data, isLoading, isMoney = false }: IProps) => {
  return (
    <div className="flex items-center justify-between w-full px-6 py-4 bg-white rounded-lg shadow-main gap-x-6">
      <div className="flex flex-col flex-1 gap-y-2">
        {isLoading && <Skeleton variant="text" height={30} width={200} />}
        {data && (
          <span className="text-3xl font-semibold text-h1">
            {isMoney ? formatValueToVND(data.total) : data.total}
          </span>
        )}
        <h4 className="text-base text-disable">{title}</h4>
        {data && data.status === STATISTIC_STATUS.Decrease ? (
          <div className="flex items-center gap-x-3">
            <Tag color={colorsProvider.success}>
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
                  strokeLinejoin="round"
                  d="M2.25 18L9 11.25l4.306 4.307a11.95 11.95 0 015.814-5.519l2.74-1.22m0 0l-5.94-2.28m5.94 2.28l-2.28 5.941"
                />
              </svg>
            </Tag>
            <p className="text-xs text-disable">
              <strong className="font-semibold text-black1">
                +{data.percent}%
              </strong>{" "}
              than last month
            </p>
          </div>
        ) : data && data.status === STATISTIC_STATUS.Increase ? (
          <div className="flex items-center gap-x-3">
            <Tag color={colorsProvider.error}>
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
                  strokeLinejoin="round"
                  d="M2.25 6L9 12.75l4.286-4.286a11.948 11.948 0 014.306 6.43l.776 2.898m0 0l3.182-5.511m-3.182 5.51l-5.511-3.181"
                />
              </svg>
            </Tag>
            <p className="text-xs text-disable">
              <strong className="font-semibold text-black1">
                -{data.percent}%
              </strong>{" "}
              than last month
            </p>
          </div>
        ) : (
          <div className="flex items-center gap-x-3">
            <Tag color={colorsProvider.disable}>
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
                  strokeLinejoin="round"
                  d="M17.25 8.25L21 12m0 0l-3.75 3.75M21 12H3"
                />
              </svg>
            </Tag>
            <p className="text-xs text-disable">
              <strong className="font-semibold text-black1">
                {data?.percent || 0}%
              </strong>{" "}
              than last month
            </p>
          </div>
        )}
      </div>
      <div className="relative w-32 h-32 ">
        <ImageCustom src={icon} alt="icon" fill className="object-contain" />
      </div>
    </div>
  )
}
export default Card
