import styled from "@emotion/styled"
import { HTMLAttributes, ReactNode } from "react"
import { hexToRGBA } from "shared/helpers/helper"
interface Props extends HTMLAttributes<HTMLDivElement> {
  children: ReactNode
  color?: string
}

const TagWrapper = styled.div`
  width: fit-content;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: ${(props) => hexToRGBA(props.color, 10)};
  color: ${(props) => props.color};
  font-size: 14px;
  padding: 4px 12px;
  border-radius: 6px;
`
const Tag = ({ color = "235EE8", children, ...props }: Props) => {
  return (
    <TagWrapper color={color} {...props}>
      <>{children}</>
    </TagWrapper>
  )
}

export default Tag
