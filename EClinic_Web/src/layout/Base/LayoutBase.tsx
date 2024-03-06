import { ConfirmDialogProvider } from "context/ComfirmContext"
import React from "react"
import { Toaster } from "react-hot-toast"

const LayoutBase = ({ children }: React.PropsWithChildren) => {
  return (
    <ConfirmDialogProvider>
      <Toaster />
      <>{children}</>
    </ConfirmDialogProvider>
  )
}

export default LayoutBase
