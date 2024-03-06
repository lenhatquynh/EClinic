import { AxiosResponse } from "axios"
import axiosClient from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import { Service, ServicePackage, Specialization } from "types/Service"
import { IServerResponse } from "types/server/IServerResponse"
import {
  IPaginationSearch,
  IPaginationSearchServicePackageFiltered,
  IPaging
} from "types/Pagination"
import {
  CreateServiceItem,
  CreateServicePackageItem,
  CreateSpecialization,
  UpdateService,
  UpdateServicePackage
} from "hooks/query/service/useService"

class ServiceService {
  //service package
  async searchServicePackageForAd(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE_PACKAGE}/SearchServicePackageForAd?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<ServicePackage[]>>
  }
  async searchServicePackage(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE_PACKAGE}/SearchServicePackage?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<ServicePackage[]>>
  }
  async searchServicePackageFiltered(
    data: IPaginationSearchServicePackageFiltered
  ) {
    let url = `${URL_API.SERVICE_PACKAGE}/SearchServicePackageFiltered?`

    if (data.searchText) {
      url += `SearchText=${encodeURIComponent(data.searchText)}`
    }

    if (data.specializationIDs.length > 0) {
      for (const specializationID of data.specializationIDs) {
        url += `&SpecializationIDs=${encodeURIComponent(specializationID)}`
      }
    }

    const res: AxiosResponse = await axiosClient.get(url, {
      headers: {
        PageNumber: data.pageNumber,
        PageSize: data.pageSize
      }
    })

    return res as AxiosResponse<IServerResponse<ServicePackage[]>>
  }

  async getAllServicePackage(data: IPaging) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE_PACKAGE}/GetAllServicePackage`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<ServicePackage[]>>
  }
  async getServicePackageByIDForAd(servicePackageId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE_PACKAGE}/GetServicePackageByIDForAd?servicePackageID=${servicePackageId}`
    )
    return res.data as IServerResponse<ServicePackage>
  }
  async getServicePackageByID(servicePackageId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE_PACKAGE}/GetServicePackageByID?servicePackageID=${servicePackageId}`
    )
    return res.data as IServerResponse<ServicePackage>
  }
  async createServicePackage(data: CreateServicePackageItem) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(data)) {
        if (key === "serviceItemIds") {
          value.forEach((hash: string) => {
            formData.append(key, hash)
          })
        } else {
          formData.append(key, value)
        }
      }
      const res: AxiosResponse = await axiosClient.post(
        `${URL_API.SERVICE_PACKAGE}/CreateServicePackage`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("Service Package Service ~ error:", error)
    }
  }
  async updateServicePackage(data: UpdateServicePackage) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(data)) {
        if (key === "serviceItemIds") {
          value.forEach((hash: string) => {
            formData.append(key, hash)
          })
        } else {
          formData.append(key, value)
        }
      }
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.SERVICE_PACKAGE}/UpdateServicePackage`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("Service Package Service ~ error:", error)
    }
  }
  //service
  async getAllServiceForAd(data: IPaging) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE}/GetAllServiceForAd`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<Service[]>>
  }
  async searchServiceForAd(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE}/SearchServiceForAd?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<Service[]>>
  }
  async getServiceByIDForAd(serviceId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SERVICE}/GetServiceByIDForAd?serviceID=${serviceId}`
    )
    return res.data as IServerResponse<Service>
  }
  async createService(data: CreateServiceItem) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.post(
      `${URL_API.SERVICE}/CreateService`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateService(data: UpdateService) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.put(
      `${URL_API.SERVICE}/UpdateService`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  //specialization
  async getAllSpecialization(data: IPaging) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.SPECIALIZATION}/GetAllSpecialization`,
        {
          headers: {
            PageNumber: data.pageNumber,
            PageSize: data.pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<Specialization[]>>
    } catch (error) {
      console.log("getAllSpecialization ~ error:", error)
    }
  }
  async searchSpecialization(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.SPECIALIZATION}/SearchSpecialization?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<Specialization[]>>
  }
  async createSpecialization(data: CreateSpecialization) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.post(
      `${URL_API.SPECIALIZATION}/CreateSpecialization`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateSpecialization(data: Specialization) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.put(
      `${URL_API.SPECIALIZATION}/UpdateSpecialization`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
}

export const serviceService = new ServiceService()
