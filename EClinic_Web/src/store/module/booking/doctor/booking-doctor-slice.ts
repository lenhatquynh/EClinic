import { createSlice } from "@reduxjs/toolkit"
import dayjs from "dayjs"
import { BOOKING_TYPE } from "shared/constant/constant"
import { Slot } from "types/Booking"
import { IProfile, IProfileDoctor, IRelationShip } from "types/Profile.type"
interface BookingDoctorState {
  method: string
  profile: (IProfile & IRelationShip) | null
  doctor: IProfileDoctor | null
  bookingTime: string
  bookingType: string
  slot: Slot | null
}

const initialState: BookingDoctorState = {
  method: "",
  profile: null,
  bookingTime: dayjs().format("YYYY-MM-DD").toString(),
  bookingType: BOOKING_TYPE.Online.toString(),
  doctor: null,
  slot: null
}

export const bookingDoctorSlice = createSlice({
  name: "booking-doctor",
  initialState,
  reducers: {
    methodChange: (state, action) => {
      state.method = action.payload
    },
    profileChange: (state, action) => {
      state.profile = action.payload
    },
    bookingTime: (state, action) => {
      state.bookingTime = action.payload
    },
    bookingTypeChange: (state, action) => {
      state.bookingType = action.payload
    },
    scheduleSlot: (state, action) => {
      state.slot = action.payload
    },
    doctorChange: (state, action) => {
      state.doctor = action.payload
    },
    resetBookingDoctor: () => {
      return initialState // Reset the state to its initial state
    }
  }
})
