import axios, { AxiosInstance, CancelTokenSource } from "axios"
import { VERSION } from "shared/constant/constant"
class HttpServer {
  instance: AxiosInstance
  cancelTokenSource: CancelTokenSource
  constructor() {
    /**
     * cancelTokenSource
     * This can be useful for cases such as when a user navigates away from a page before a request has completed, or when a user initiates a new request before a previous one has completed.
     */
    this.cancelTokenSource = axios.CancelToken.source()
    this.instance = axios.create({
      baseURL: `${process.env.NEXT_PUBLIC_API_MAIN_URL}:${process.env.NEXT_PUBLIC_API_URL_PORT_BASE}/api/v${VERSION}/`,
      timeout: 10000,
      headers: {
        "Content-Type": "application/json"
      },
      cancelToken: this.cancelTokenSource.token
    })
  }
}
const axiosServer = new HttpServer().instance

export default axiosServer
