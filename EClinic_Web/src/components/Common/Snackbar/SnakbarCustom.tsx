import { Alert, Grow, Snackbar, SnackbarProps } from "@mui/material"
import { useEffect, useState } from "react"
interface Props extends SnackbarProps {
  isOpen: boolean
  title: string
  state: "success" | "error"
}
const SnakbarCustom = ({
  isOpen = false,
  title,
  state = "success",
  autoHideDuration = 10000,
  ...rest
}: Props) => {
  const [open, setOpen] = useState(false)

  const handleClose = () => {
    setOpen(false)
  }
  useEffect(() => {
    setOpen(isOpen)
  }, [isOpen, title, state])

  return (
    <Snackbar
      {...rest}
      autoHideDuration={autoHideDuration}
      open={open}
      onClose={handleClose}
      TransitionComponent={Grow}
    >
      <Alert
        className="shadow-[rgba(0,_0,_0,_0)_0px_0px_0px_0px,_rgba(0,_0,_0,_0)_0px_0px_0px_0px,_rgba(81,_41,_10,_0.1)_-6px_8px_10px_0px,_rgba(81,_41,_10,_0.2)_0px_2px_2px_0px] rounded-[8px] bg-white"
        variant="standard"
        severity={state}
        sx={{ width: "100%" }}
        onClose={handleClose}
      >
        {title}
      </Alert>
    </Snackbar>
  )
}

export default SnakbarCustom
