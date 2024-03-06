import { Container } from "@mui/material"
import dynamic from "next/dynamic"
const BreadcrumsCustom = dynamic(
  () => import("components/Common/Breadcrums").then((module) => module.default),
  {
    ssr: false
  }
)
import { PropsWithChildren } from "react"
import { IBreadcrum } from "types/Base.type"

interface Props extends PropsWithChildren {
  breadrums: IBreadcrum[]
}

const UserSecondaryLayout = ({ breadrums, children }: Props) => {
  return (
    <Container className="page-container mb-20 flex-1 pt-16 md:pt-[72px] flex flex-col">
      <div className="mt-1 mb-1 md:mb-3 md:mt-3">
        <BreadcrumsCustom items={breadrums} />
      </div>
      {children}
    </Container>
  )
}

export default UserSecondaryLayout
