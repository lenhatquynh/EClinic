import { useMutation, useQuery } from "@tanstack/react-query"
import { QUERY_KEYS } from "shared/constant/constant"
import {
  IPaginationSearch,
  IPaginationSearchServicePackageFiltered,
  IPaging
} from "types/Pagination"
import { serviceService } from "../../../services/service.service"
import { Service, ServicePackage, Specialization } from "types/Service"

export type CreateServiceItem = Omit<
  Service,
  "serviceID" | "createdAt" | "updatedAt" | "specialization"
> & {
  specializationID: string
}
export type UpdateService = Omit<
  Service,
  "createdAt" | "updatedAt" | "specialization"
> & {
  specializationID: string
}
export type CreateServicePackageItem = Omit<
  ServicePackage,
  "servicePackageID" | "totalOrder" | "createdAt" | "updatedAt" | "serviceItems"
> & {
  serviceItemIds: string[]
}
export type UpdateServicePackage = Omit<
  ServicePackage,
  "totalOrder" | "createdAt" | "updatedAt" | "serviceItems"
> & {
  serviceItemIds: string[]
}
export type CreateSpecialization = Omit<Specialization, "specializationID">
//Service package
export const useSearchServicePackageForAdQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.SERVICE_PACKAGE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.searchServicePackageForAd({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useSearchServicePackageQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.SERVICE_PACKAGE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.searchServicePackage({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      })
  })
}
export const useSearchServicePackageFilteredQuery = (
  data: IPaginationSearchServicePackageFiltered
) => {
  const queryKey = [
    QUERY_KEYS.SERVICE_PACKAGE,
    data.pageNumber,
    data.pageSize,
    data.searchText,
    data.specializationIDs
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.searchServicePackageFiltered({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText,
        specializationIDs: data.specializationIDs
      }),
    keepPreviousData: true
  })
}
export const useGetAllServicePackageQuery = (data: IPaging) => {
  const queryKey = [QUERY_KEYS.SERVICE_PACKAGE, data.pageNumber, data.pageSize]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.getAllServicePackage({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize
      })
  })
}
export const useGetServicePackageByIDForAdQuery = (
  servicePackageId: string
) => {
  const queryKey = [QUERY_KEYS.SERVICE_PACKAGE, servicePackageId]
  return useQuery({
    queryKey,
    queryFn: () => serviceService.getServicePackageByIDForAd(servicePackageId)
  })
}
export const useGetServicePackageByIDQuery = (servicePackageId: string) => {
  const queryKey = [QUERY_KEYS.SERVICE_PACKAGE, servicePackageId]
  return useQuery({
    queryKey,
    queryFn: () => serviceService.getServicePackageByID(servicePackageId)
  })
}
export const useCreateServicePackageMutation = () => {
  const createServicePackageMutation = useMutation({
    mutationFn: (servicePackage: CreateServicePackageItem) =>
      serviceService.createServicePackage(servicePackage)
  })
  return createServicePackageMutation
}
export const useUpdateServicePackageMutation = () => {
  const updateServicePackageMutation = useMutation({
    mutationFn: (servicePackage: UpdateServicePackage) =>
      serviceService.updateServicePackage(servicePackage)
  })
  return updateServicePackageMutation
}
//Service
export const useGetAllServiceForAdQuery = (data: IPaging) => {
  const queryKey = [QUERY_KEYS.SERVICE, data.pageNumber, data.pageSize]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.getAllServiceForAd({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize
      })
  })
}
export const useSearchServiceForAdQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.SERVICE,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.searchServiceForAd({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useGetServiceByIDForAdQuery = (serviceId: string) => {
  const queryKey = [QUERY_KEYS.SERVICE, serviceId]
  return useQuery({
    queryKey,
    queryFn: () => serviceService.getServiceByIDForAd(serviceId)
  })
}
export const useCreateServiceMutation = () => {
  const createServiceMutation = useMutation({
    mutationFn: (service: CreateServiceItem) =>
      serviceService.createService(service)
  })
  return createServiceMutation
}
export const useUpdateServiceMutation = () => {
  const updateServiceMutation = useMutation({
    mutationFn: (service: UpdateService) =>
      serviceService.updateService(service)
  })
  return updateServiceMutation
}
//Specialization
export const useGetAllSpecializationQuery = (data: IPaging) => {
  const queryKey = [QUERY_KEYS.SPECIALIZATION, data.pageNumber, data.pageSize]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.getAllSpecialization({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize
      })
  })
}
export const useSearchSpecializationQuery = (data: IPaginationSearch) => {
  const queryKey = [
    QUERY_KEYS.SPECIALIZATION,
    data.pageNumber,
    data.pageSize,
    data.searchText
  ]
  return useQuery({
    queryKey,
    queryFn: () =>
      serviceService.searchSpecialization({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize,
        searchText: data.searchText
      }),
    keepPreviousData: true
  })
}
export const useCreateSpecializationMutation = () => {
  const createSpecializationMutation = useMutation({
    mutationFn: (specialization: CreateSpecialization) =>
      serviceService.createSpecialization(specialization)
  })
  return createSpecializationMutation
}
export const useUpdateSpecializationMutation = () => {
  const updateSpecializationMutation = useMutation({
    mutationFn: (specialization: Specialization) =>
      serviceService.updateSpecialization(specialization)
  })
  return updateSpecializationMutation
}
