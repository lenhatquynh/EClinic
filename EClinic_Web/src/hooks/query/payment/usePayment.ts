import { useMutation, useQuery } from "@tanstack/react-query"
import { QUERY_KEYS } from "shared/constant/constant"
import { IPaging } from "types/Pagination"
import {
  PaymentBookingDoctor,
  PaymentBookingService,
  RefundTransaction,
  TransactionQuery
} from "types/Payment"
import { paymentService } from "../../../services/payment.service"

//payment
export const useGetPaymentByIDQuery = (paymentID: string) => {
  const queryKey = [QUERY_KEYS.PAYMENT, paymentID]
  return useQuery({
    queryKey,
    queryFn: () => paymentService.getPaymentByID(paymentID)
  })
}
export const useGetAllPaymentTransactionQuery = (data: IPaging) => {
  const queryKey = [QUERY_KEYS.PAYMENT, data.pageNumber, data.pageSize]
  return useQuery({
    queryKey,
    queryFn: () =>
      paymentService.getAllPaymentTransaction({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize
      })
  })
}
export const usePaymentBookingServiceMutation = () => {
  return useMutation({
    mutationFn: (data: { data: PaymentBookingService; type: string }) =>
      paymentService.paymentBookingService(data.data, data.type)
  })
}
export const usePaymentBookingDoctorMutation = () => {
  return useMutation({
    mutationFn: (data: { data: PaymentBookingDoctor; type: string }) =>
      paymentService.paymentBookingDoctor(data.data, data.type)
  })
}
//refund
export const useGetRefundByIDQuery = (refundID: string) => {
  const queryKey = [QUERY_KEYS.PAYMENT, refundID]
  return useQuery({
    queryKey,
    queryFn: () => paymentService.getRefundByID(refundID)
  })
}
export const useGetAllRefundTransactionQuery = (data: IPaging) => {
  const queryKey = [QUERY_KEYS.PAYMENT, data.pageNumber, data.pageSize]
  return useQuery({
    queryKey,
    queryFn: () =>
      paymentService.getAllRefundTransaction({
        pageNumber: data.pageNumber,
        pageSize: data.pageSize
      })
  })
}
export const useRefundTransactionMutation = () => {
  const refundTransaction = useMutation({
    mutationFn: (refundTrans: RefundTransaction) =>
      paymentService.refundTransaction(refundTrans)
  })

  return refundTransaction
}
export const useGetTransactionQuery = (data: TransactionQuery) => {
  const queryKey = [
    QUERY_KEYS.PAYMENT,
    data.endTime,
    data.startTime,
    data.timeType
  ]
  return useQuery({
    queryKey,
    queryFn: () => paymentService.getTransactionQuery(data)
  })
}
