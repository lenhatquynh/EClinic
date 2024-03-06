import { useMutation, useQuery } from "@tanstack/react-query"
import { bookingService } from "services/booking.service"
import { QUERY_KEYS } from "shared/constant/constant"
import { CreateScheduleDoctor, UpdateScheduleDoctor } from "types/Booking"

export const useGetAllBookingPackageForUserQuery = (
  pageNumber: number,
  pageSize: number,
  status: number
) => {
  const queryKey = [QUERY_KEYS.BOOKING.SERVICE, pageNumber, pageSize, status]
  return useQuery({
    queryKey,
    queryFn: () =>
      bookingService.getAllBookingPackageForUser(pageNumber, pageSize, status)
  })
}
export const useGetAllBookingDoctorQuery = (
  pageNumber: number,
  pageSize: number,
  status: number
) => {
  const queryKey = [QUERY_KEYS.BOOKING.DOCTOR, pageNumber, pageSize, status]
  return useQuery({
    queryKey,
    queryFn: () =>
      bookingService.getAllBookingDoctorForUser(pageNumber, pageSize, status)
  })
}
export const useGetAllBookingDoctorForDoctorQuery = (
  pageNumber: number,
  pageSize: number,
  status: number
) => {
  const queryKey = [QUERY_KEYS.BOOKING.DOCTOR, pageNumber, pageSize, status]
  return useQuery({
    queryKey,
    queryFn: () =>
      bookingService.GetAllBookingDoctorForDoctor(pageNumber, pageSize, status)
  })
}
export const useGetAllBookingDoctorForAdQuery = (
  pageNumber: number,
  pageSize: number,
  status: number
) => {
  const queryKey = [QUERY_KEYS.BOOKING.DOCTOR, pageNumber, pageSize, status]
  return useQuery({
    queryKey,
    queryFn: () =>
      bookingService.GetAllBookingDoctorForAd(pageNumber, pageSize, status)
  })
}
export const useGetAllBookingServicerForAdQuery = (
  pageNumber: number,
  pageSize: number,
  status: number
) => {
  const queryKey = [QUERY_KEYS.BOOKING.SERVICE, pageNumber, pageSize, status]
  return useQuery({
    queryKey,
    queryFn: () =>
      bookingService.getAllBookingPackageForAD(pageNumber, pageSize, status)
  })
}
export const useUpdateStateBookingServiceMutation = () =>
  useMutation({
    mutationFn: (bookingId: string) =>
      bookingService.updateBookingPackageStatusCancel(bookingId)
  })
export const useUpdateStateBookingServiceDoneMutation = () =>
  useMutation({
    mutationFn: (bookingId: string) =>
      bookingService.updateBookingPackageStatusDone(bookingId)
  })
export const useUpdateStateBookingDoctorMutation = () =>
  useMutation({
    mutationFn: (bookingId: string) =>
      bookingService.updateBookingDoctorStatusCancel(bookingId)
  })
export const useUpdateStateBookingDoctorDoneMutation = () =>
  useMutation({
    mutationFn: (bookingId: string) =>
      bookingService.updateBookingDoctorStatusDone(bookingId)
  })
export const useGetDoctorScheduleForUser = (date: string, doctorId: string) => {
  const queryKey = [QUERY_KEYS.BOOKING.DOCTOR, date, doctorId]
  return useQuery({
    queryKey,
    queryFn: () => bookingService.getDoctorScheduleForUser(date, doctorId),
    retry: 1
  })
}
export const useCreateDoctorScheduleMutation = () =>
  useMutation({
    mutationFn: (data: CreateScheduleDoctor) =>
      bookingService.createDoctorSchedule(data)
  })
export const useUpdateDoctorScheduleMutation = () =>
  useMutation({
    mutationFn: (data: UpdateScheduleDoctor) =>
      bookingService.updateDoctorSchedule(data)
  })
