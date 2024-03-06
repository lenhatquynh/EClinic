import { createSelector } from "reselect"
import { RootState } from "store/store"

// Select the booking-doctor slice from the Redux state
export const selectBookingDoctor = (state: RootState) => state.bookingDoctor

// Create individual selectors for each state property
export const selectProfileWithDoctor = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.profile
)

export const selectBookingTime = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.bookingTime
)

export const selectBookingType = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.bookingType
)

export const selectSlotBooking = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.slot
)

export const selectDoctor = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.doctor
)
export const selectMethodDoctor = createSelector(
  [selectBookingDoctor],
  (bookingDoctor) => bookingDoctor.method
)
