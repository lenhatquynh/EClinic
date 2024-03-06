export interface IBreadcrum {
  label: string
  href?: string
}
export type Position = "left" | "right" | "center"
export type Coords = {
  x: number
  y: number
  width: number
  height: number
}
export interface HashTag {
  hashtagID: string
  hashtagName: string
}
export interface ImageItem {
  key: string
  file: File
  url: string
}
export interface IStatictis {
  total: number
  status: number
  percent: number
}
