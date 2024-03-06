import { TextField } from "@mui/material"

const TimePickerCustom = () => {
  return (
    <div className="max-w-[120px] w-full">
      <TextField
        size="small"
        label="Alarm clock"
        type="time"
        InputLabelProps={{
          shrink: true
        }}
        inputProps={{
          step: 300 // 5 min
        }}
      />
    </div>
  )
}

export default TimePickerCustom
