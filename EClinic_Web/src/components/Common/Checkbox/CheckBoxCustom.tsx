import { Checkbox, CheckboxProps, FormControlLabel } from "@mui/material"
import classNames from "classnames"
import { ChangeEvent } from "react"

interface Props extends CheckboxProps {
  checked?: boolean
  // eslint-disable-next-line no-unused-vars
  onChange?: (event: ChangeEvent<HTMLInputElement>) => void
  label: string
  disabled?: boolean
  className?: string
}

const CheckBoxCustom = ({
  checked = false,
  onChange,
  label,
  className = "",
  disableRipple = false,
  size = "medium",
  ...rest
}: Props) => {
  return (
    <FormControlLabel
      control={
        <Checkbox
          disableRipple={disableRipple}
          className={classNames("py-1", className)}
          checked={checked}
          onChange={onChange}
          size={size}
          {...rest}
        />
      }
      label={label}
    />
  )
}

export default CheckBoxCustom
