import ImageCustom from "components/Common/ImageCustom"
import InputCustom from "components/Common/Input"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { CustomInput } from "components/User/Input"
import useConfirm from "context/ComfirmContext"
import dayjs from "dayjs"
import {
  useGetPaymentByIDQuery,
  useRefundTransactionMutation
} from "hooks/query/payment/usePayment"
import Head from "next/head"
import { useRouter } from "next/router"
import { useState } from "react"
import toast from "react-hot-toast"
import { combineName } from "shared/helpers/helper"
import { Payment, RefundTransaction } from "types/Payment"

const PaymentDetail = () => {
  const router = useRouter()
  const { data } = useGetPaymentByIDQuery(router.query.id! as string)
  const confirm = useConfirm()
  const payment: Payment = data?.data as Payment
  const [reasonValue, setReasonValue] = useState("")
  const refundTransactionMutation = useRefundTransactionMutation()
  const handleRefundTrans = async () => {
    if (confirm) {
      const choice = await confirm({
        title: "Refund for patient",
        content: "Are you sure you want to refund for this payment?"
      })
      if (!reasonValue.trim()) {
        toast.error("Please enter refund reason!")
        return
      }
      if (choice) {
        refundTransactionMutation.mutate(
          {
            paymentID: payment.paymentID,
            reason: reasonValue ? reasonValue : "Default refund!"
          } as RefundTransaction,
          {
            onSuccess: (data) => {
              if (data.isSuccess) {
                toast.success("Refund successfully!")
              } else {
                toast.error("Refund fail!")
              }
            },
            onError: () => {
              toast.error("Refund fail!")
            }
          }
        )
      }
    }
  }
  return (
    <>
      <Head>
        <title>Payment detail</title>
      </Head>
      <h3 className="m-3 text-xl font-medium text-black2">Payment Detail</h3>
      <div className="grid grid-cols-8 gap-6 m-3 mt-1 mr-7">
        <div className="col-span-8 background-primary">
          <div className="flex items-center justify-between w-full mb-3">
            <span className="text-base font-medium text-black2">Status</span>
            {payment?.isRefund ? (
              <Tag color="#FEAF02">Refund</Tag>
            ) : (
              <Tag color="#4fde98">Not Refund</Tag>
            )}
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">Author</span>
              <div className="flex items-center space-x-2">
                <div className="relative w-10 h-10 overflow-hidden rounded-full">
                  <ImageCustom
                    src={
                      payment?.author.avatar || "/images/avatars/avatar_1.jpg"
                    }
                    fill
                    alt="user-avatar"
                    className="object-cover"
                  />
                </div>
                <div className="flex flex-col">
                  <span className="text-sm font-medium text-h1">
                    {combineName(
                      payment?.author.firstName,
                      payment?.author.lastName
                    )}
                  </span>
                </div>
              </div>
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Payment Amount
              </span>
              <CustomInput
                name="amount"
                size="medium"
                value={payment?.paymentAmount + " VND"}
                disabled
              />
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Payment Time
              </span>
              {payment?.paymentTime && (
                <CustomInput
                  name="time"
                  size="medium"
                  value={dayjs(payment?.paymentTime).format(
                    "DD/MM/YYYY HH:mm:ss"
                  )}
                  disabled
                />
              )}
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Payment Type
              </span>
              <CustomInput
                name="payment"
                size="medium"
                value={payment?.paymentService || "Null"}
                disabled
              />
            </div>
          </div>
          {!payment?.isRefund && (
            <>
              <div className="flex flex-col justify-start mb-3">
                <div className="flex flex-col space-y-1">
                  <span className="text-sm font-medium text-black2">
                    Refund Reason
                  </span>
                  <InputCustom
                    className="!rounded-xl h-[56px] border-0 "
                    name="reason"
                    placeholder="Refund reason"
                    onChange={(e) => setReasonValue(e.target.value)}
                    value={reasonValue}
                  />
                </div>
              </div>
              <CustomButton
                kind="primary"
                type="submit"
                className="w-full ml-auto"
                isLoading={refundTransactionMutation.isLoading}
                onClick={() => handleRefundTrans()}
              >
                Refund for patient
              </CustomButton>
            </>
          )}
        </div>
      </div>
    </>
  )
}
export default PaymentDetail
