export interface IServerResponse<T> {
  isSuccess: boolean
  message?: string
  data: T
}
