import { Button } from "@mui/material"
import { NextRouter } from "next/router"
import { Toast, toast } from "react-hot-toast"
type Props = {
  router: NextRouter
  url: string
  title: string
  labelButton: string
  t: Toast
}
export function ToastNavigate({ labelButton, router, title, url, t }: Props) {
  return (
    <div className="flex items-center gap-x-2">
      <p className="flex-shrink-0 font-medium text-h1">{title}</p>
      <Button
        color="primary"
        size="small"
        onClick={() => {
          toast.dismiss(t.id)
          router.push(url)
        }}
      >
        {labelButton}
      </Button>
    </div>
  )
}
