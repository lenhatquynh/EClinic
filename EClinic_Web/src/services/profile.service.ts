import { AxiosResponse } from "axios"
import axiosClient from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import {
  IProfile,
  IProfileDoctor,
  IProfileExpert,
  IProfileSupporter,
  Profile
} from "types/Profile.type"
import { IServerResponse } from "types/server/IServerResponse"
import { IRelationShip } from "./../types/Profile.type.d"
import { IPaginationSearch } from "types/Pagination"
import {
  CreateDoctorProfile,
  CreateExpertProfile,
  CreateSupporterProfile,
  SearchDoctor,
  UpdateDoctorProfile,
  UpdateExpertProfile,
  UpdateSupporterProfile
} from "hooks/query/profile/useProfile"

class ProfileService {
  async GetUserProfilesByID(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetUserProfilesById?UserID=${userId}`
    )
    return res.data as IServerResponse<(IProfile & IRelationShip)[]>
  }
  async searchFamlyProfiles(
    pageNumber: number,
    pageSize: number,
    searchText: string
  ) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/SearchFamlyProfiles?SearchText=${searchText}`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<(IProfile & IRelationShip)[]>>
  }
  async getUserMainProfilesByID() {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetUserMainProfilesByID`
    )
    return res.data as IServerResponse<IProfile & IRelationShip>
  }
  async getSimpleProfile(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetSimpleProfile?UserID=${userId}`
    )
    return res.data as IServerResponse<any>
  }
  async getAllRelationshisp() {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.RELATIONSHIPS}/GetAllRelationship`
    )
    return res.data as IServerResponse<IRelationShip[]>
  }
  async updateUserProfile(profile: IProfile & IRelationShip) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(profile)) {
      if (key !== "userID" && key !== "relationshipName") {
        formData.append(key, value)
      }
    }
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.PROFILE}/UpdateUserProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async createUserProfile(profile: IProfile & IRelationShip) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(profile)) {
      if (key !== "relationshipName" && key !== "profieID") {
        formData.append(key, value)
      }
    }
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.PROFILE}/CreateUserProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async deleteUserProfile(profileId: string) {
    const res: AxiosResponse = await axiosClient.delete(
      `${URL_API.PROFILE}/DeleteUserProfile?ProfileID=${profileId}`
    )
    return res.data as IServerResponse<null>
  }
  async getBloodTypes() {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE_OTHER}/GetBloodTypes`
    )
    return res.data as IServerResponse<string[]>
  }
  // Doctor
  async getDoctorProfiles(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetDoctorProfiles?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IProfileDoctor[]>>
  }
  async searchDoctorProfiles(data: SearchDoctor) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/SearchDoctorProfiles`,
      {
        params: data,
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IProfileDoctor[]>>
  }
  async getDoctorProfileById(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetDoctorProfileById?UserID=${userId}`
    )
    return res.data as IServerResponse<IProfileDoctor>
  }
  async createDoctorProfile(data: CreateDoctorProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.post(
      `${URL_API.PROFILE}/CreateDoctorProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateDoctorProfile(data: UpdateDoctorProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      if (key === "workEnd") {
        formData.append(key, value || "")
      } else {
        formData.append(key, value)
      }
    }
    const res = await axiosClient.put(
      `${URL_API.PROFILE}/UpdateDoctorProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  // Supporter
  async getSupporterProfiles(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetSupporterProfiles?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IProfileSupporter[]>>
  }
  async getSupporterProfileById(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetEmployeeProfileById?UserID=${userId}`
    )
    return res.data as IServerResponse<IProfileSupporter>
  }
  async createSupporterProfile(data: CreateSupporterProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.post(
      `${URL_API.PROFILE}/CreateSupporterProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateSupporterProfile(data: UpdateSupporterProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.put(
      `${URL_API.PROFILE}/UpdateSupporterProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  // Expert
  async getExpertProfiles(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetExpertProfiles?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IProfileExpert[]>>
  }
  async getExpertProfileById(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetExpertProfileById?UserID=${userId}`
    )
    return res.data as IServerResponse<IProfileExpert>
  }
  async createExpertProfile(data: CreateExpertProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.post(
      `${URL_API.PROFILE}/CreateExpertProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateExpertProfile(data: UpdateExpertProfile) {
    const formData = new FormData()
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value)
    }
    const res = await axiosClient.put(
      `${URL_API.PROFILE}/UpdateExpertProfile`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<null>
  }
  async getPatientProfiles(data: IPaginationSearch) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetUserProfiles?SearchText=${data.searchText}`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<Profile[]>>
  }
  async getPatientProfileById(userId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PROFILE}/GetUserProfileById?UserID=${userId}`
    )
    return res.data as IServerResponse<Profile>
  }
}

export const profileService = new ProfileService()
