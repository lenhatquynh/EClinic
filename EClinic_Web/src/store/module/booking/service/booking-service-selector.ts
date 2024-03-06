import { createSelector } from "reselect"
import { RootState } from "store/store"

// Select the booking service slice from the Redux state
const selectBookingService = (state: RootState) => state.bookingService

// Create individual selectors for each state property
export const selectMethod = createSelector(
  [selectBookingService],
  (bookingService) => bookingService.method
)

export const selectProfile = createSelector(
  [selectBookingService],
  (bookingService) => bookingService.profile
)

export const selectServicePackage = createSelector(
  [selectBookingService],
  (bookingService) => bookingService.servicePackage
)

export const selectAppointmentTime = createSelector(
  [selectBookingService],
  (bookingService) => bookingService.appoinmentTime
)
