import { AxiosResponse } from "axios"
import axiosClient from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import { IStatictis } from "types/Base.type"
import {
  BookingSchedule,
  BookingService as BookingType,
  CreateScheduleDoctor,
  IBookingDoctor,
  UpdateScheduleDoctor
} from "types/Booking"
import { IServerResponse } from "types/server/IServerResponse"

class BookingService {
  async getAllBookingPackageForUser(
    pageNumber: number,
    pageSize: number,
    status: number
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.SERVICE}/GetAllBookingPackageForUser?BookingStatus=${status}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<BookingType[]>>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async getStatisticsOverview() {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.BOOKING}/getStatisticsOverview`
      )
      return res.data as IServerResponse<IStatictis>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async getAllBookingDoctorForUser(
    pageNumber: number,
    pageSize: number,
    status: number
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.DOCTOR}/GetAllBookingDoctorForUser?BookingStatus=${status}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBookingDoctor[]>>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async GetAllBookingDoctorForDoctor(
    pageNumber: number,
    pageSize: number,
    status: number
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.DOCTOR}/GetAllBookingDoctorForDoctor?BookingStatus=${status}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBookingDoctor[]>>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async GetAllBookingDoctorForAd(
    pageNumber: number,
    pageSize: number,
    status: number
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.DOCTOR}/GetAllBookingDoctorForAd?BookingStatus=${status}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBookingDoctor[]>>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async getAllBookingPackageForAD(
    pageNumber: number,
    pageSize: number,
    status: number
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BOOKING.SERVICE}/GetAllBookingPackageForAD?BookingStatus=${status}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<BookingType[]>>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async updateBookingPackageStatusCancel(bookingPackageId: string) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BOOKING.SERVICE}/updateBookingPackageStatusCancel?bookingPackageID=${bookingPackageId}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async updateBookingPackageStatusDone(bookingPackageId: string) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BOOKING.SERVICE}/updateBookingPackageStatusDone?bookingPackageID=${bookingPackageId}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async updateBookingDoctorStatusCancel(bookingDoctorId: string) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BOOKING.DOCTOR}/updateBookingDoctorStatusCancel?BookingID=${bookingDoctorId}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async updateBookingDoctorStatusDone(bookingDoctorId: string) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BOOKING.DOCTOR}/updateBookingDoctorStatusDone?BookingID=${bookingDoctorId}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async getDoctorScheduleForUser(date: string, doctorId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.BOOKING.DOCTOR_SCHEDULE}/getDoctorScheduleForUser`,
      {
        params: {
          date,
          doctorId
        }
      }
    )
    return res.data as IServerResponse<BookingSchedule>
  }
  async createDoctorSchedule(data: CreateScheduleDoctor) {
    try {
      const res: AxiosResponse = await axiosClient.post(
        `${URL_API.BOOKING.DOCTOR_SCHEDULE}/CreateDoctorSchedule`,
        data
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
  async updateDoctorSchedule(data: UpdateScheduleDoctor) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BOOKING.DOCTOR_SCHEDULE}/UpdateDoctorSchedule`,
        data
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BookingService ~ error:", error)
    }
  }
}
export const bookingService = new BookingService()
