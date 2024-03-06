import { AxiosResponse } from "axios"
import dayjs from "dayjs"
import { DEFAULT_ROUTER } from "shared/constant/constant"
import { IPagination } from "types/Pagination"
import { ITokenDecode } from "types/Token"

export const isProduction = () => {
  return process.env.NODE_ENV === "production"
}

export const isDevelopment = () => {
  return process.env.NODE_ENV !== "development"
}

export const isServer = () => {
  return typeof window === "undefined"
}

export const isBrowser = () => {
  return !isServer()
}
export const numberWithCommas = (x: any) => {
  return x?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}
export const routerByRole = (role: ITokenDecode["role"]) => {
  let url = "/"
  switch (role) {
    case "User":
      url = "/"
      break
    case "Admin":
      url = DEFAULT_ROUTER.ADMIN
      break
    case "Supporter":
      url = DEFAULT_ROUTER.SUPPORTER
      break
    case "Doctor":
      url = DEFAULT_ROUTER.DOCTOR
      break
    case "Expert":
      url = DEFAULT_ROUTER.EXPERT
      break
    default:
      break
  }
  return url
}
export const getBase64 = (file: any): Promise<string> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.readAsDataURL(file)
    reader.onload = () => resolve(reader.result as string)
    reader.onerror = (error) => reject(error)
  })
export const getCurrentDate = (date: string) => {
  return dayjs(date, "ddd, DD MMM YYYY HH:mm:ss [GMT]").toISOString()
}
export const combineName = (lastName?: string, firstName?: string) => {
  return lastName + " " + firstName
}
export const dayformat = (date?: string) => {
  return dayjs(date).format("DD MMM YYYY") || ""
}
export const caculateAge = (date: string) => {
  const dateInput = new Date(date)
  const currentDate = new Date()
  return currentDate.getFullYear() - dateInput.getFullYear()
}
export const getDataPaginate = (
  response: AxiosResponse | undefined = undefined,
  field?: string
) => {
  if (!field) {
    field = "x-pagination"
  }

  if (response?.headers && response.headers[field]) {
    try {
      return JSON.parse(response.headers[field] as string) as IPagination
    } catch (error) {
      console.error("Error parsing pagination data:", error)
    }
  }

  return {
    PageIndex: 1,
    PageSize: 10,
    TotalCount: 0,
    TotalPages: 0,
    HasPrevious: false,
    HasNext: false
  }
}

export function renderQuarterArray() {
  const intervals = []

  // Loop through each quarter-hour interval (15 minutes)
  for (let minute = 0; minute < 60; minute += 15) {
    const minuteFormatted = minute.toString().padStart(2, "0") // Add leading zero if necessary
    intervals.push(`0:${minuteFormatted}`)
  }

  // Loop through each hour of the day (24-hour format), starting from 1:00 AM
  for (let hour = 1; hour < 24; hour++) {
    const hourFormatted = hour.toString().padStart(2, "0") // Add leading zero if necessary

    // Loop through each quarter-hour interval (15 minutes)
    for (let minute = 0; minute < 60; minute += 15) {
      const minuteFormatted = minute.toString().padStart(2, "0") // Add leading zero if necessary
      intervals.push(`${hourFormatted}:${minuteFormatted}`)
    }
  }

  return intervals
}
export function hexToRGBA(hexCode = "", opacity = 1) {
  let hex = hexCode.replace("#", "")

  if (hex.length === 3) {
    hex = `${hex[0]}${hex[0]}${hex[1]}${hex[1]}${hex[2]}${hex[2]}`
  }

  const r = parseInt(hex.substring(0, 2), 16)
  const g = parseInt(hex.substring(2, 4), 16)
  const b = parseInt(hex.substring(4, 6), 16)

  /* Backward compatibility for whole number based opacity values. */
  if (opacity > 1 && opacity <= 100) {
    opacity = opacity / 100
  }

  return `rgba(${r},${g},${b},${opacity})`
}
export function isImage(file: File) {
  return /^image\//.test(file?.type)
}
export const stripHtmlTags = (htmlString: string) => {
  if (!htmlString) return ""
  return htmlString.replace(/<[^>]*>/g, "")
}
export const formatValueToVND = (value: number) => {
  const formattedValue = new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND"
  }).format(value)
  return formattedValue
}
export function isQuillEmpty(value: string) {
  if (
    value.replace(/<(.|\n)*?>/g, "").trim().length === 0 &&
    !value.includes("<img")
  ) {
    return true
  }
  return false
}
