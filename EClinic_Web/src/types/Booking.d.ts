export interface BookingService {
  bookingID: string
  profile: ProfileBooking
  price: number
  bookingTime: string
  service: Service
  appoinmentTime: string
  bookingStatus: number
}
export interface IBookingDoctor {
  bookingID: string
  doctorProfile: ProfileBooking
  userProfile: ProfileBooking
  price: string
  bookingTime: string
  bookingCalendar: string
  bookingType: string
  bookingStatus: number
  slot: Slot
  roomID: string
}
export interface ProfileBooking {
  profileID: string
  userID: string
  firstName: string
  lastName: string
  avatar: string
}

interface Service {
  servicePackageID: string
  servicePackageName: string
  image: string
}
export interface BookingSchedule {
  calenderID: string
  time: string
  slots: Slot[]
}

export interface Slot {
  slotID: string
  startTime: string
  endTime: string
  isBooking: boolean
}
export interface CreateScheduleDoctor {
  doctorID: string
  time: string
  slots: Slot[]
}
export interface UpdateScheduleDoctor {
  calenderID: string
  slots: Slot[]
}
