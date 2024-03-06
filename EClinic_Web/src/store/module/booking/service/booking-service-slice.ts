import { createSlice } from "@reduxjs/toolkit"
import dayjs from "dayjs"
import { IProfile, IRelationShip } from "types/Profile.type"
import { ServicePackage } from "types/Service"
interface BookingServiceState {
  method: string
  profile: (IProfile & IRelationShip) | null
  servicePackage: ServicePackage | null
  appoinmentTime: string
}

const initialState: BookingServiceState = {
  method: "",
  profile: null,
  servicePackage: null,
  appoinmentTime: dayjs().format("YYYY-MM-DD").toString()
}

export const bookingServiceSlice = createSlice({
  name: "booking-service",
  initialState,
  reducers: {
    methodChange: (state, action) => {
      state.method = action.payload
    },
    profileChange: (state, action) => {
      state.profile = action.payload
    },
    servicePackageChange: (state, action) => {
      state.servicePackage = action.payload
    },
    appoinmentTimeChange: (state, action) => {
      state.appoinmentTime = action.payload
    },
    resetBookingService: () => {
      return initialState // Reset the state to its initial state
    }
  }
})
