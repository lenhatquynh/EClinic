import { PropsWithChildren } from "react"

const Field = ({ children }: PropsWithChildren) => {
  return (
    <div className="flex flex-col items-start gap-y-[6px] mb-6 w-full">
      {children}
    </div>
  )
}

export default Field
