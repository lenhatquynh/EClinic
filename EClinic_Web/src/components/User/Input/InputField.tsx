import { BaseTextFieldProps, TextField, TextFieldProps } from "@mui/material"
import { Control, Controller } from "react-hook-form"

type Props = BaseTextFieldProps &
  TextFieldProps & {
    name: string
    label: string
    control: Control<any>
  }

const InputField = ({
  name,
  label,
  fullWidth = true,
  size = "small",
  control,
  className,
  ...props
}: Props) => {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field: { onChange, value }, fieldState: { error } }) => (
        <TextField
          helperText={error ? error.message : null}
          error={!!error}
          onChange={(value) => onChange(value.target.value)}
          value={value || ""}
          className={className}
          label={label}
          fullWidth={fullWidth}
          size={size}
          {...props}
        />
      )}
    />
  )
}

export default InputField
