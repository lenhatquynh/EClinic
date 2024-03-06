import React, { PropsWithChildren } from "react"
interface Props extends PropsWithChildren {
  title: string
}
const Field = ({ title = "", children }: Props) => {
  return (
    <div className="flex flex-col ">
      <h3 className="mb-4 text-2xl text-h1">{title}</h3>
      <div>{children}</div>
    </div>
  )
}

export default Field
