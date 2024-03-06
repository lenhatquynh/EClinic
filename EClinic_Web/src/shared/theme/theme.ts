import { createTheme } from "@mui/material/styles"

// THIS OBJECT SHOULD BE SIMILAR TO ../tailwind.config.js
const themeConstants = {
  paper: "#FAFAFB",
  primary: {
    main: "#235EE8"
  },
  secondary: {
    main: "#4FD8DE"
  },
  breakpoints: {
    xs: 0,
    mb: 350,
    sm: 640,
    md: 768,
    lg: 1280,
    xl: 1536
  }
}

// Check here for more configurations https://material-ui.com/customization/default-theme/
const theme = createTheme({
  typography: {
    fontFamily: `"Lexend Deca", sans-serif`
  },
  palette: {
    primary: themeConstants.primary,
    secondary: themeConstants.secondary,
    background: { paper: themeConstants.paper }
  },
  breakpoints: {
    values: themeConstants.breakpoints
  }
})

export { theme }
