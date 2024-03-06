import {
  FormControl,
  FormControlProps,
  FormHelperText,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput
} from "@mui/material"
import { useState } from "react"
import { Control, useController } from "react-hook-form"
import { HiOutlineEye, HiOutlineEyeSlash } from "react-icons/hi2"

interface Props extends FormControlProps {
  name: string
  label: string
  control: Control<any>
  errorMessage?: string
}

const CustomInputPassword = ({
  name,
  label,
  errorMessage,
  fullWidth = true,
  size = "small",
  control,
  ...props
}: Props) => {
  const [showPassword, setShowPassword] = useState(false)
  const { field } = useController({
    control,
    name,
    defaultValue: ""
  })
  const handleClickShowPassword = () => setShowPassword((show) => !show)
  return (
    <FormControl
      variant="outlined"
      size={size}
      fullWidth={fullWidth}
      {...props}
    >
      <InputLabel htmlFor="outlined-adornment-password">{label}</InputLabel>
      <OutlinedInput
        {...field}
        id="outlined-adornment-password"
        type={showPassword ? "text" : "password"}
        label={label}
        endAdornment={
          <InputAdornment position="end">
            <IconButton
              aria-label="toggle password visibility"
              onClick={handleClickShowPassword}
              edge="end"
            >
              {showPassword ? <HiOutlineEye /> : <HiOutlineEyeSlash />}
            </IconButton>
          </InputAdornment>
        }
      />
      {errorMessage && (
        <FormHelperText className="text-error">{errorMessage}</FormHelperText>
      )}
    </FormControl>
  )
}

export default CustomInputPassword
