import classNames from "classnames"
import React from "react"
import ImageCustom from "../ImageCustom"
interface Props extends React.HTMLProps<HTMLDivElement> {
  message?: string
  classNameImage?: string
  urLImage?: string
}

const EmtyData = ({
  message = "No data available",
  className,
  classNameImage,
  urLImage = "/images/empty-data.png",
  ...props
}: Props) => {
  return (
    <div
      className={classNames(
        "flex flex-col items-center justify-center ",
        className
      )}
      {...props}
    >
      <div className={classNames("relative h-36 w-36", classNameImage)}>
        <ImageCustom
          src={urLImage}
          alt="data-empty"
          fill
          className="object-cover"
        />
      </div>
      <p className="text-disable">{message}</p>
    </div>
  )
}

export default EmtyData
