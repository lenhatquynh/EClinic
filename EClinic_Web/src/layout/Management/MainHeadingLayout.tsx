import React, { PropsWithChildren } from "react"
interface Props extends PropsWithChildren {
  heading?: string
}
const MainHeadingLayout = ({ heading, children }: Props) => {
  return (
    <div className="flex flex-col flex-1 px-2 mt-5 md:px-4">
      {heading && (
        <h1 className="mb-2 text-2xl font-semibold text-black1">{heading}</h1>
      )}
      <div className="flex flex-col gap-y-[30px]">{children}</div>
    </div>
  )
}

export default MainHeadingLayout
