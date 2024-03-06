import styled from "@emotion/styled"
import { PropsWithChildren, ReactNode } from "react"

interface Props extends PropsWithChildren {
  heading: string
  elementRight?: ReactNode
}
const Wrapper = styled.div`
  .MuiPaper-root.MuiPaper-rounded.MuiPaper-elevation {
    box-shadow: none !important;
    padding-left: 0;
    padding-right: 0;
    .MuiToolbar-root {
      border-top: 1px solid #eeee;
    }
  }
`
const WrapperHeading = ({ heading, elementRight, children }: Props) => {
  return (
    <>
      <Wrapper className="flex flex-col w-full bg-white rounded-lg">
        <div className="flex items-center justify-between p-4">
          <h3 className="text-xl font-semibold text-gray80">{heading}</h3>
          {elementRight}
        </div>
        {children}
      </Wrapper>
    </>
  )
}

export default WrapperHeading
