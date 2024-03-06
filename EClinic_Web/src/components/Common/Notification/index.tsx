import { Badge } from "@mui/material"
import React from "react"
import { MdNotificationsNone } from "react-icons/md"

const Notification = () => {
  return (
    <>
      <Badge badgeContent={4} color="primary">
        <MdNotificationsNone className="text-2xl cursor-pointer" />
      </Badge>
    </>
  )
}

export default Notification
