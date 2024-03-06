import { Box, Chip, Drawer, TextField } from "@mui/material"
import TooltipIcon from "components/Common/ToolTipIcon"
import { CustomInput } from "components/User/Input"
import { useState } from "react"
import { HiOutlineEye, HiOutlineTrash } from "react-icons/hi2"
import { dayformat } from "shared/helpers/helper"
const DetailBooking = () => {
  const style = "border border-solid border-gray80"
  const [show, setShow] = useState(false)
  return (
    <>
      <div className="flex items-center gap-x-3">
        <TooltipIcon title="Delete" className={style}>
          <HiOutlineTrash />
        </TooltipIcon>
        <TooltipIcon
          title="Detail Booking"
          className={style}
          onClick={() => setShow(true)}
        >
          <HiOutlineEye />
        </TooltipIcon>
        <Drawer anchor="right" open={show} onClose={() => setShow(false)}>
          <Box
            sx={{
              width: 600
            }}
          >
            <div className="flex flex-col">
              <div className="flex flex-col gap-6 px-6 py-6 ">
                <h1 className="text-xl text-h1">Detail a booking</h1>
                <div className="flex flex-col gap-y-2">
                  <span className="text-base">Info </span>
                  <div className="grid grid-cols-2 gap-x-2">
                    <CustomInput
                      variant="filled"
                      label="Patient Name"
                      name="name"
                      value={"Patient name"}
                    />
                    <CustomInput
                      variant="filled"
                      label="Email"
                      name="email"
                      value={"Patient@gmail.com"}
                    />
                  </div>
                </div>
                <CustomInput
                  variant="filled"
                  label="Phone number"
                  name="Phone number"
                  value={"079534235123"}
                />
                <div className="flex flex-col gap-y-2">
                  <span className="text-base">Appointment Date </span>
                  <div className="flex items-center gap-3">
                    <Chip
                      color="info"
                      className="rounded-md min-w-[160px]"
                      label={dayformat("03 May 2023")}
                    />
                    <Chip
                      color="info"
                      className="rounded-md"
                      label={`10:00 - 11:00`}
                    />
                  </div>
                </div>
                <div className="flex flex-col gap-y-2">
                  <span className="text-base">Note </span>
                  <TextField
                    label="Multiline"
                    multiline
                    rows={4}
                    defaultValue="Default Value"
                    variant="filled"
                  />
                </div>
              </div>
            </div>
          </Box>
        </Drawer>
      </div>
    </>
  )
}

export default DetailBooking
