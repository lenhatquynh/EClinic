/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{ts,tsx}"],
  corePlugins: {
    preflight: false
  },
  theme: {
    fontFamily: {
      primary: ['"Lexend Deca"', "sans-serif"]
    },
    extend: {
      colors: {
        background: "#F6F7FB",
        primary: "#235EE8",
        primary1: "#E9EFFD",
        secondary: "#4FD8DE",
        error: "#D72755",
        h1: "#2E2C49",
        gray80: "#808191",
        black1: "#171822",
        grayPrimary: "#272B30",
        black2: "#394456",
        disable: "#878EA1",
        icon: "#828A9D",
        carbon: "#E4E9EF",
        snow: "#F9FAFC",
        pending: "#FEAF02",
        support: "#0C1A7B",
        success: "#07AB55"
      },
      boxShadow: {
        main: "rgba(145, 158, 171, 0.2) 0px 0px 2px 0px, rgba(145, 158, 171, 0.12) 0px 12px 24px -4px"
      }
    }
  },
  plugins: [require("@tailwindcss/line-clamp")]
}
