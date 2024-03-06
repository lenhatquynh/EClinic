import {
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent
} from "@mui/material"
export type Option = {
  label: string
  value: string
}

interface SelectProps {
  options?: Option[]
  // eslint-disable-next-line no-unused-vars
  onSelectOption: (option: Option) => void
  className?: string
  placeholder?: string
  isLoading?: boolean
  value: string
  size?: "small" | "medium"
  error?: string
}

export const SelectCustom = ({
  options,
  onSelectOption,
  className,
  placeholder,
  value,
  isLoading = false,
  size = "small",
  error = ""
}: SelectProps) => {
  const handleChange = (event: SelectChangeEvent) => {
    const value = options!.find((item) => item.value == event.target.value)
    onSelectOption(value!)
  }

  return (
    <FormControl
      className={className}
      size={size === "small" ? "small" : "medium"}
      fullWidth
      error={!!error}
    >
      {placeholder && <InputLabel>{placeholder}</InputLabel>}
      <Select value={value} label={placeholder} onChange={handleChange}>
        {isLoading ? (
          <div>loading</div>
        ) : (
          options &&
          options.length > 0 &&
          options!.map((opt, index) => (
            <MenuItem key={index} value={opt.value}>
              {opt.label}
            </MenuItem>
          ))
        )}
      </Select>
      {!!error && <FormHelperText>{error}</FormHelperText>}
    </FormControl>
  )
}
