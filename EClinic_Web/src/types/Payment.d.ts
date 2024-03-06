export interface Payment {
  paymentID: string
  paymentAmount: number
  paymentTime: string
  paymentService: string
  author: Author
  isRefund: boolean
}
export type PaymentWithoutAuthor = Omit<Payment, "author">
export interface Refund {
  refundID: string
  refundAmount: number
  refundTime: string
  refundReason: string
  paymentService: string
}
export type RefundWithoutReason = Omit<Refund, "refundReason">
export interface RefundTransaction {
  paymentID: string
  reason: string
}
export interface Author {
  userID: string
  firstName: string
  lastName: string
  avatar: string
}
export interface TransactionQuery {
  startTime: string
  endTime: string
  timeType: number
}
export interface Statistics {
  time: string
  totalAmount: number
}
export interface PaymentBookingService {
  userID: string
  profileID: string
  price: string
  servicePackageId: string
  appoinmentTime: string
}
export interface PaymentBookingDoctor {
  userID: string
  profileID: string
  doctorID: string
  price: string
  bookingTime: string
  bookingType: string
  scheduleID: string
}
