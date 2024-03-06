import { AxiosResponse } from "axios"
import { ModelAction } from "hooks/query/ai"
import { HttpClient } from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import {
  DeepLearning,
  MachineLearning,
  Model,
  PredictHistory,
  PredictModel
} from "types/AI"
import { IServerResponse } from "types/server/IServerResponse"

const http = new HttpClient(
  `${process.env.NEXT_PUBLIC_API_MAIN_URL}:${process.env.NEXT_PUBLIC_API_URL_PORT_BASE}`
).instance

class AiService {
  async getAllMachineLearning() {
    try {
      const res: AxiosResponse = await http.get(
        `${URL_API.AI.MachineLearning}/GetAll`
      )
      return res as AxiosResponse<IServerResponse<MachineLearning[]>>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async updateMachineLearning(data: MachineLearning) {
    try {
      const res: AxiosResponse = await http.put(
        `${URL_API.AI.MachineLearning}/Update`,
        {
          ...data
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async createMachineLearning(name: string) {
    try {
      const res: AxiosResponse = await http.post(
        `${URL_API.AI.MachineLearning}/Create?MachineName=${name}`
      )
      if (res.status === 200) {
        return res.data as IServerResponse<string>
      }
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async getAllDeepLearning() {
    try {
      const res: AxiosResponse = await http.get(
        `${URL_API.AI.DeepLearning}/GetAll`
      )
      return res as AxiosResponse<IServerResponse<DeepLearning[]>>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async updateDeepLearning(data: DeepLearning) {
    try {
      const res: AxiosResponse = await http.put(
        `${URL_API.AI.DeepLearning}/Update`,
        {
          ...data
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async createDeepLearning(name: string) {
    try {
      const res: AxiosResponse = await http.post(
        `${URL_API.AI.DeepLearning}/Create?DeepName=${name}`
      )
      if (res.status === 200) {
        return res.data as IServerResponse<string>
      }
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  //Model
  async getAllModel() {
    try {
      const res: AxiosResponse = await http.get(`${URL_API.AI.Model}/GetAll`)
      return res as AxiosResponse<IServerResponse<Model[]>>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async createModel(model: ModelAction) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(model)) {
        formData.append(
          key,
          typeof value === "number" ? value.toString() : value
        )
      }
      const res: AxiosResponse = await http.post(
        `${URL_API.AI.Model}/Create`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async updateModel(model: ModelAction) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(model)) {
        if (key === "file" && !value) {
          break
        } else {
          formData.append(
            key,
            typeof value === "number" ? value.toString() : value
          )
        }
      }
      const res: AxiosResponse = await http.put(
        `${URL_API.AI.Model}/Update`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async activeModel(ModelID: string) {
    try {
      const res: AxiosResponse = await http.put(
        `${URL_API.AI.Model}/Active?ModelID=${ModelID}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  //History
  async getAllHistory(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await http.get(
        `${URL_API.AI.History}/GetAll`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<PredictHistory[]>>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  //Detect
  async expertPredict(model: PredictModel) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(model)) {
        formData.append(key, value)
      }
      const res: AxiosResponse = await http.post(
        `${URL_API.AI.AIPredict}/ExpertPredict`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
  async doctorPredict(model: PredictModel) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(model)) {
        formData.append(key, value)
      }
      const res: AxiosResponse = await http.post(
        `${URL_API.AI.AIPredict}/DoctorPredict`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("AIgService ~ error:", error)
    }
  }
}
export const aiService = new AiService()
