import styled from "@emotion/styled"
import { FormControlLabel, Switch, SwitchProps } from "@mui/material"
import React from "react"
const IOSSwitch = styled((props: SwitchProps) => (
  <Switch focusVisibleClassName=".Mui-focusVisible" disableRipple {...props} />
))(() => ({
  width: 38,
  height: 22,
  padding: 0,
  "& .MuiSwitch-switchBase": {
    padding: 0,
    margin: 2,
    transitionDuration: "300ms",
    "&.Mui-checked": {
      transform: "translateX(16px)",
      color: "#fff",
      "& + .MuiSwitch-track": {
        backgroundColor: "#235EE8",
        opacity: 1,
        border: 0
      },
      "&.Mui-disabled + .MuiSwitch-track": {
        opacity: 0.5
      }
    },
    "&.Mui-focusVisible .MuiSwitch-thumb": {
      color: "#235EE8",
      border: "6px solid #fff"
    },
    "&.Mui-disabled .MuiSwitch-thumb": {
      color: "#878EA1"
    },
    "&.Mui-disabled + .MuiSwitch-track": {
      opacity: 0.7
    }
  },
  "& .MuiSwitch-thumb": {
    boxSizing: "border-box",
    width: 18,
    height: 18
  },
  "& .MuiSwitch-track": {
    borderRadius: 26 / 2,
    backgroundColor: "#E9E9EA",
    opacity: 1,
    transition: "0.25s linear all"
  }
}))
const SwitchCustom = ({ ...props }: SwitchProps) => {
  return (
    <>
      <IOSSwitch value={props.checked || false} {...props} />
    </>
  )
}

export default SwitchCustom
