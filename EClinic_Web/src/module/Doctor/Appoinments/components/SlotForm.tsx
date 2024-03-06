import { Autocomplete, TextField } from "@mui/material"
import classNames from "classnames"
import TooltipIcon from "components/Common/ToolTipIcon"
import { HiOutlineTrash, HiPlus } from "react-icons/hi2"
import { Slot } from "types/Booking"

const timeSlots = Array.from(new Array(24 * 4)).map(
  (_, index) =>
    `${index < 10 ? "0" : ""}${Math.floor(index / 4)}:${
      index % 4 === 0
        ? "00"
        : index % 4 === 1
        ? "15"
        : index % 4 === 2
        ? "30"
        : "45"
    }`.replace(/^(\d{1}:(\d{2}))$/, "0$1") // Add leading zero if hour is single-digit
)
interface PropsSlotForm {
  slot?: Slot
  // eslint-disable-next-line no-unused-vars
  onChangeSlot: (type: "startTime" | "endTime", time: string) => void
  className?: string
  onCreate?: () => void
  onRemove: () => void
}
export const SlotForm = ({
  slot,
  onChangeSlot,
  className,
  onCreate,
  onRemove
}: PropsSlotForm) => {
  return (
    <div className={classNames("flex items-center", className)}>
      <div className="max-w-[120px] w-full">
        <Autocomplete
          disableClearable
          size="small"
          options={["", ...timeSlots]}
          value={slot?.startTime}
          sx={{ width: 120 }}
          onChange={(_, value) => onChangeSlot("startTime", value)}
          renderInput={(params) => <TextField {...params} label="Start" />}
        />
      </div>
      <span className="px-2">-</span>
      <div className="max-w-[120px] w-full">
        <Autocomplete
          disableClearable
          size="small"
          options={["", ...timeSlots]}
          value={slot?.endTime}
          sx={{ width: 120 }}
          onChange={(_, value) => onChangeSlot("endTime", value)}
          renderInput={(params) => <TextField {...params} label="End" />}
        />
      </div>
      <TooltipIcon title="Remove" className="ml-2" onClick={() => onRemove()}>
        <HiOutlineTrash />
      </TooltipIcon>
      {onCreate && (
        <TooltipIcon
          title="Add new interval"
          className="ml-2"
          onClick={() => onCreate()}
        >
          <HiPlus />
        </TooltipIcon>
      )}
    </div>
  )
}
