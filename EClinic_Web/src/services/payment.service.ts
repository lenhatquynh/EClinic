import { AxiosResponse } from "axios"
import axiosClient from "shared/axios/httpClient"
import { PAYMENT, URL_API } from "shared/constant/constant"
import { IServerResponse } from "types/server/IServerResponse"
import { IPaging } from "types/Pagination"
import {
  Payment,
  PaymentWithoutAuthor,
  Refund,
  RefundWithoutReason,
  RefundTransaction,
  TransactionQuery,
  Statistics,
  PaymentBookingService,
  PaymentBookingDoctor
} from "types/Payment"
import { IStatictis } from "types/Base.type"

class PaymentService {
  //Payment
  async getPaymentByID(paymentID: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/GetPaymentByID?PaymentID=${paymentID}`
    )
    return res.data as IServerResponse<Payment>
  }
  async getAllPaymentTransaction(data: IPaging) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/GetAllPaymentTransaction`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<PaymentWithoutAuthor[]>>
  }
  async paymentBookingService(data: PaymentBookingService, type: string) {
    if (type === PAYMENT.MOMO.name) {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.PAYMENT.MOMO}/PaymentRequestForBookingPackage`,
        {
          params: data
        }
      )
      return res.data as IServerResponse<string>
    } else if (type === PAYMENT.VNPAY.name) {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.PAYMENT.VNPAY}/PaymentRequestForBookingPackage`,
        {
          params: data
        }
      )
      return res.data as IServerResponse<string>
    }
  }
  async paymentBookingDoctor(data: PaymentBookingDoctor, type: string) {
    const paymentUrl =
      type === PAYMENT.MOMO.name
        ? `${URL_API.PAYMENT.MOMO}/PaymentRequestForBookingDoctor`
        : type === PAYMENT.VNPAY.name
        ? `${URL_API.PAYMENT.VNPAY}/PaymentRequestForBookingDoctor`
        : ""

    if (paymentUrl) {
      const res: AxiosResponse = await axiosClient.get(paymentUrl, {
        params: data
      })
      return res.data as IServerResponse<string>
    }
  }

  //Refund
  async getRefundByID(refundID: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/GetRefundByID?RefundID=${refundID}`
    )
    return res.data as IServerResponse<Refund>
  }
  async getStatisticsOverview() {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/GetStatisticsOverview`
    )
    return res.data as IServerResponse<IStatictis>
  }
  async getAllRefundTransaction(data: IPaging) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/GetAllRefundTransaction`,
      {
        headers: {
          PageNumber: data.pageNumber,
          PageSize: data.pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<RefundWithoutReason[]>>
  }
  async refundTransaction(refundTrans: RefundTransaction) {
    console.log(
      "🚀 ~ file: payment.service.ts:54 ~ PaymentService ~ refundTransaction ~ refundTrans:",
      refundTrans
    )
    const res = await axiosClient.post(
      `${URL_API.PAYMENT.TRANSACTION}/RefundTransaction`,
      refundTrans
    )
    return res.data as IServerResponse<null>
  }
  //Transaction
  async getTransactionQuery(query: TransactionQuery) {
    const res = await axiosClient.get(
      `${URL_API.PAYMENT.TRANSACTION}/getTransactionQuery`,
      {
        params: query
      }
    )
    return res.data as IServerResponse<Statistics[]>
  }
}

export const paymentService = new PaymentService()
