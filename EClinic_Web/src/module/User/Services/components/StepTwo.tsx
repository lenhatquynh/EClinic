import ImageCustom from "components/Common/ImageCustom"
import Payment from "components/Common/Payment"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import useConfirm from "context/ComfirmContext"
import { usePaymentBookingServiceMutation } from "hooks/query/payment/usePayment"
import { useDispatch, useSelector } from "react-redux"
import { PAYMENT } from "shared/constant/constant"
import { combineName, dayformat, formatValueToVND } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import {
  selectAppointmentTime,
  selectMethod,
  selectProfile,
  selectServicePackage
} from "store/module/booking/service/booking-service-selector"
import { bookingServiceSlice } from "store/module/booking/service/booking-service-slice"
import { RootState } from "store/store"
import { PropsStep } from "./StepOne"
import { useRouter } from "next/router"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { useEffect } from "react"
interface Props extends PropsStep {}
const StepTwo = ({ onBack }: Props) => {
  const { t } = useTranslation(["base"])

  const router = useRouter()
  const confirm = useConfirm()
  const paymentMutation = usePaymentBookingServiceMutation()
  const userID = useSelector((state: RootState) => state.auth.user.userId)
  const dispatch = useDispatch()
  const method = useSelector(selectMethod)
  const profile = useSelector(selectProfile)
  const servicePackage = useSelector(selectServicePackage)
  const appoinmentTime = useSelector(selectAppointmentTime)

  const onSubmit = async () => {
    if (confirm) {
      const choice = await confirm({
        title: "Booking service package",
        content: "Are you sure you want to booking for this service"
      })
      if (choice) {
        paymentMutation.mutate(
          {
            data: {
              appoinmentTime: appoinmentTime,
              price: servicePackage?.price.toString() as string,
              profileID: profile?.profileID as string,
              servicePackageId: servicePackage?.servicePackageID as string,
              userID
            },
            type:
              method === PAYMENT.MOMO.name
                ? PAYMENT.MOMO.name
                : PAYMENT.VNPAY.name
          },
          {
            onSuccess(data) {
              router.push(data?.message as string)
            },
            onError() {
              toast.error("Booking service fail")
            }
          }
        )
      }
    }
  }
  useEffect(() => {
    dispatch(bookingServiceSlice.actions.methodChange(PAYMENT.VNPAY.name))
  }, [])

  return (
    <>
      <div className="flex items-center gap-x-2">
        <button
          className="h-[50px] w-[50px] rounded-full shadow flex items-center justify-center cursor-pointer outline-none border-none"
          onClick={() => onBack && onBack()}
        >
          <svg
            width={13}
            height={20}
            viewBox="0 0 13 20"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M11.6267 16.7721C12.3049 17.3945 12.3502 18.4488 11.7278 19.127C11.1054 19.8052 10.0511 19.8504 9.37294 19.2281L0.801512 11.3622C0.0812647 10.7013 0.0818923 9.56521 0.802869 8.90504L9.3743 1.05661C10.0532 0.434993 11.1074 0.481413 11.729 1.16029C12.3507 1.83916 12.3042 2.89342 11.6254 3.51503L4.39489 10.1356L11.6267 16.7721Z"
              fill="#403ECC"
            />
          </svg>
        </button>
        <span>{t("base:booking.btn_black")}</span>
      </div>
      <div className="flex flex-col my-4 px-[24px]">
        <h3 className="text-xl font-semibold text-black1">
          {t("base:booking.summary")}
        </h3>
        <Divider></Divider>
        <div className="flex flex-col gap-y-2">
          <div className="flex items-center gap-x-4">
            <div className="relative w-[60px] h-[60px]">
              <ImageCustom
                fill
                src={profile?.avatar || "/images/sample.png"}
                alt="avatar-image"
                className="object-cover rounded-md"
              />
            </div>
            <div className="flex flex-col gap-y-1">
              <h1 className="text-base font-medium">
                {combineName(profile?.firstName, profile?.lastName)}
              </h1>
              <Tag color={colorsProvider.success}>
                <span>{profile?.relationshipName}</span>
              </Tag>
              <time className="text-xs text-gray-400">
                {dayformat(profile?.dateOfBirth)}
              </time>
            </div>
          </div>
        </div>
        <Divider></Divider>
        <div className="flex flex-col gap-y-2">
          <div className="flex items-center gap-x-4">
            <div className="flex items-center gap-x-4">
              <div className="flex flex-col flex-1 gap-y-1">
                <h1 className="text-base font-medium">
                  {servicePackage?.servicePackageName}
                </h1>
                <div className="flex items-center w-full gap-x-6">
                  <div className="flex items-center gap-x-1">
                    <span className="text-icon">
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={1.5}
                        stroke="currentColor"
                        className="w-6 h-6"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          d="M6.75 3v2.25M17.25 3v2.25M3 18.75V7.5a2.25 2.25 0 012.25-2.25h13.5A2.25 2.25 0 0121 7.5v11.25m-18 0A2.25 2.25 0 005.25 21h13.5A2.25 2.25 0 0021 18.75m-18 0v-7.5A2.25 2.25 0 015.25 9h13.5A2.25 2.25 0 0121 11.25v7.5m-9-6h.008v.008H12v-.008zM12 15h.008v.008H12V15zm0 2.25h.008v.008H12v-.008zM9.75 15h.008v.008H9.75V15zm0 2.25h.008v.008H9.75v-.008zM7.5 15h.008v.008H7.5V15zm0 2.25h.008v.008H7.5v-.008zm6.75-4.5h.008v.008h-.008v-.008zm0 2.25h.008v.008h-.008V15zm0 2.25h.008v.008h-.008v-.008zm2.25-4.5h.008v.008H16.5v-.008zm0 2.25h.008v.008H16.5V15z"
                        />
                      </svg>
                    </span>
                    <time className="text-base font-medium text-black2">
                      {dayformat(appoinmentTime.toString())}
                    </time>
                  </div>
                </div>
              </div>
            </div>
            <div className="ml-auto text-base text-black1">
              {formatValueToVND(servicePackage?.price || 0)}
            </div>
          </div>
        </div>
        <Divider></Divider>
        <div className="flex justify-between mt-2">
          <h3 className="text-base font-medium text-black1">
            {" "}
            {t("base:booking.total")}
          </h3>
          <span className="text-base text-black1">
            {formatValueToVND(servicePackage?.price || 0)}
          </span>
        </div>
        <Divider></Divider>
        <Payment
          methodSelected={method}
          onChangeMethod={(value) =>
            dispatch(bookingServiceSlice.actions.methodChange(value))
          }
        />
        <CustomButton
          className="mt-6"
          onClick={onSubmit}
          isLoading={paymentMutation.isLoading}
        >
          Confirm and Pay
        </CustomButton>
      </div>
    </>
  )
}
const Divider = () => {
  return <div className="h-[1px] w-full bg-carbon my-3.5 "></div>
}
export default StepTwo
