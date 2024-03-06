import { IconButton, Tooltip, TooltipProps } from "@mui/material"
import classNames from "classnames"
import { PropsWithChildren } from "react"

interface Props extends PropsWithChildren, Omit<TooltipProps, "children"> {}
const TooltipIcon = ({ children, placement = "top", ...props }: Props) => {
  return (
    <>
      <Tooltip
        placement={placement}
        {...props}
        className={classNames("rounded-md", props.className)}
      >
        <IconButton>{children}</IconButton>
      </Tooltip>
    </>
  )
}

export default TooltipIcon
