import Tag from "components/Common/Tag"
import { CustomInput } from "components/User/Input"
import dayjs from "dayjs"
import { useGetRefundByIDQuery } from "hooks/query/payment/usePayment"
import Head from "next/head"
import { useRouter } from "next/router"
import { getCurrentDate } from "shared/helpers/helper"
import { Refund } from "types/Payment"

const RefundDetail = () => {
  const router = useRouter()
  const { data } = useGetRefundByIDQuery(router.query.id! as string)
  const refund: Refund = data?.data as Refund

  return (
    <>
      <Head>
        <title>Refund detail</title>
      </Head>
      <h3 className="m-3 text-xl font-medium text-black2">Refund Detail</h3>
      <div className="grid grid-cols-8 gap-6 m-3 mt-1 mr-7">
        <div className="col-span-8 background-primary">
          <div className="flex items-center justify-between w-full mb-3">
            <span className="text-base font-medium text-black2">Status</span>
            <Tag color="#FEAF02">Refund</Tag>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Refund Amount
              </span>
              <CustomInput
                size="medium"
                value={refund?.refundAmount + " VND"}
                disabled
              />
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Refund Time
              </span>
              <CustomInput
                size="medium"
                value={dayjs(refund?.refundTime).format("DD/MM/YYYY HH:mm:ss")}
                disabled
              />
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Refund Type
              </span>
              <CustomInput
                size="medium"
                value={refund?.paymentService || "Null"}
                disabled
              />
            </div>
          </div>
          <div className="flex flex-col justify-start mb-3">
            <div className="flex flex-col space-y-1">
              <span className="text-sm font-medium text-black2">
                Refund Reason
              </span>
              <CustomInput
                size="medium"
                value={refund?.refundReason}
                disabled
              />
            </div>
          </div>
        </div>
      </div>
    </>
  )
}
export default RefundDetail
