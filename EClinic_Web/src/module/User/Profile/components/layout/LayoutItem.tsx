import React, { PropsWithChildren } from "react"
interface Props extends PropsWithChildren {
  label: string
}
const LayoutItem = ({ label, children }: Props) => {
  return (
    <div className="flex flex-col space-y-4 rounded-md background-primary">
      <h2 className="text-xl font-bold text-h1">{label}</h2>
      <div className="w-full">{children}</div>
    </div>
  )
}

export default LayoutItem
