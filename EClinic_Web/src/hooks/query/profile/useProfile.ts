import { useMutation, useQuery } from "@tanstack/react-query"
import { QUERY_KEYS } from "shared/constant/constant"
import { IPaginationSearch } from "types/Pagination"
import {
  IProfile,
  IProfileDoctor,
  IProfileExpert,
  IProfileSupporter,
  IRelationShip
} from "types/Profile.type"
import { profileService } from "../../../services/profile.service"

export type CreateDoctorProfile = Omit<
  IProfileDoctor,
  "profileID" | "userID" | "specialization"
>
export type UpdateDoctorProfile = Omit<IProfileDoctor, "profileID">

export type CreateSupporterProfile = Omit<
  IProfileSupporter,
  "profileID" | "userID"
>
export type UpdateSupporterProfile = Omit<IProfileSupporter, "userID">

export type CreateExpertProfile = Omit<IProfileExpert, "profileID" | "userID">
export type UpdateExpertProfile = Omit<IProfileExpert, "userID">

export interface SearchDoctor {
  pageNumber: number
  pageSize: number
  searchText: string
  specializationID: string
  startPrice: number
  endPrice: number
}
export const useProfieId = (userId: string) => {
  const queryKey = [QUERY_KEYS.PROFILE, userId]
  const profileQuery = useQuery({
    queryKey,
    queryFn: () => profileService.GetUserProfilesByID(userId)
  })
  return profileQuery
}
export const useSearchFamlyProfilesQuery = (
  pageNumber: number,
  pageSize: number,
  searchText: string
) => {
  const queryKey = [QUERY_KEYS.PROFILE, pageNumber, pageSize, searchText]
  return useQuery({
    queryKey,
    queryFn: () =>
      profileService.searchFamlyProfiles(pageNumber, pageSize, searchText)
  })
}
export const useSimpleProfile = (userId: string) => {
  const queryKey = ["useSimpleProfile", userId]
  const profileQuery = useQuery({
    queryKey,
    queryFn: () => profileService.getSimpleProfile(userId)
  })
  return profileQuery
}

export const useGetUserMainProfilesByID = () => {
  const queryKey = [QUERY_KEYS.PROFILE]
  return useQuery({
    queryKey,
    queryFn: () => profileService.getUserMainProfilesByID()
  })
}
export const useAllRelationship = () => {
  const queryKey = ["AllRelationship"]
  const relationshipQuery = useQuery({
    queryKey,
    queryFn: () => profileService.getAllRelationshisp(),
    staleTime: Infinity
  })
  return relationshipQuery
}
export const useUpdateProfileMutation = () => {
  const updateProfileMutation = useMutation({
    mutationFn: (profile: IProfile & IRelationShip) =>
      profileService.updateUserProfile(profile)
  })
  return updateProfileMutation
}
export const useDeleteProfileMutation = () => {
  const deleteProfileMutation = useMutation({
    mutationFn: (profileId: string) =>
      profileService.deleteUserProfile(profileId)
  })
  return deleteProfileMutation
}
export const useCreateProfileMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: IProfile & IRelationShip) =>
      profileService.createUserProfile(profile)
  })
  return createProfileMutation
}

export const useGetBloodTypes = () => {
  const queryKey = ["blood"]
  return useQuery({
    queryKey,
    queryFn: () => profileService.getBloodTypes()
  })
}
//Doctor
export const useCreateProfileDoctorMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: CreateDoctorProfile) =>
      profileService.createDoctorProfile(profile)
  })
  return createProfileMutation
}
export const useUpdateProfileDoctorMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: UpdateDoctorProfile) =>
      profileService.updateDoctorProfile(profile)
  })
  return createProfileMutation
}
export const useGetDoctorProfilesQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.PROFILE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      profileService.getDoctorProfiles({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useSearchDoctorProfilesQuery = (data: SearchDoctor) => {
  const queryKey = [
    QUERY_KEYS.PROFILE,
    data.pageNumber,
    data.pageSize,
    data.searchText,
    data.specializationID,
    data.endPrice
  ]
  return useQuery({
    queryKey,
    queryFn: () => profileService.searchDoctorProfiles(data)
  })
}
export const useGetDoctorProfilesByIdQuery = (userID: string) => {
  const queryKey = [QUERY_KEYS.PROFILE, userID]
  return useQuery({
    queryKey,
    queryFn: () => profileService.getDoctorProfileById(userID)
  })
}
//Supporter
export const useCreateSupporterDoctorMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: CreateSupporterProfile) =>
      profileService.createSupporterProfile(profile)
  })
  return createProfileMutation
}
export const useUpdateSupporterMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: UpdateSupporterProfile) =>
      profileService.updateSupporterProfile(profile)
  })
  return createProfileMutation
}
export const useGetSupportersQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.PROFILE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      profileService.getSupporterProfiles({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useGetSupporterByIdQuery = (userID: string) => {
  const queryKey = [QUERY_KEYS.PROFILE, userID]
  return useQuery({
    queryKey,
    queryFn: () => profileService.getSupporterProfileById(userID)
  })
}
//Expert
export const useCreateExpertMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: CreateExpertProfile) =>
      profileService.createExpertProfile(profile)
  })
  return createProfileMutation
}
export const useUpdateExpertMutation = () => {
  const createProfileMutation = useMutation({
    mutationFn: (profile: UpdateExpertProfile) =>
      profileService.updateExpertProfile(profile)
  })
  return createProfileMutation
}
export const useGetExpertProfilesQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.PROFILE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      profileService.getExpertProfiles({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useGetExpertProfilesByIdQuery = (userID: string) => {
  const queryKey = [QUERY_KEYS.PROFILE, userID]
  return useQuery({
    queryKey,
    queryFn: () => profileService.getExpertProfileById(userID)
  })
}
export const useGetPatientProfilesQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.PROFILE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      profileService.getPatientProfiles({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
