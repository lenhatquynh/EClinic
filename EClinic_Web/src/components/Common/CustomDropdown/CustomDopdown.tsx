import { ClickAwayListener, Popper } from "@mui/material"
import { ReactNode, useState } from "react"

type IProps = {
  children: ReactNode
  dropdownMenu: ReactNode
}

const CustomDropdown = ({ children, dropdownMenu }: IProps) => {
  const [anchorEl, setAnchorEl] = useState<
    (EventTarget & HTMLDivElement) | null
  >(null)

  const handleClick = (e: any) => {
    setAnchorEl(anchorEl ? null : e.currentTarget)
  }

  const handleClickAway = () => {
    setAnchorEl(null)
  }

  const open = Boolean(anchorEl)
  return (
    <>
      <div
        className="h-full flex items-center justify-center"
        onClick={(e) => handleClick(e)}
      >
        {children}
      </div>
      {open && (
        <ClickAwayListener onClickAway={() => handleClickAway()}>
          <Popper
            className="z-20 right-0"
            open={open}
            placement="bottom"
            anchorEl={anchorEl}
          >
            {dropdownMenu}
          </Popper>
        </ClickAwayListener>
      )}
    </>
  )
}

export default CustomDropdown
