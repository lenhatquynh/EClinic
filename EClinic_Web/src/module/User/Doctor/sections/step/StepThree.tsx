import ImageCustom from "components/Common/ImageCustom"
import Payment from "components/Common/Payment"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { useDispatch, useSelector } from "react-redux"
import { BOOKING_TYPE, PAYMENT } from "shared/constant/constant"
import { combineName, dayformat, formatValueToVND } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { selectBookingDoctor } from "store/module/booking/doctor/booking-doctor-selector"
import { bookingDoctorSlice } from "store/module/booking/doctor/booking-doctor-slice"
import { PropsStep } from "./StepOne"
import { usePaymentBookingDoctorMutation } from "hooks/query/payment/usePayment"
import { RootState } from "store/store"
import { useRouter } from "next/router"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { useEffect } from "react"
interface Props extends PropsStep {}
const StepThree = ({ onBack }: Props) => {
  const { t } = useTranslation(["base"])

  const router = useRouter()
  const userId = useSelector((state: RootState) => state.auth.user.userId)
  const paymentMutation = usePaymentBookingDoctorMutation()
  const dispatch = useDispatch()
  const dataBooking = useSelector(selectBookingDoctor)
  const price =
    dataBooking.bookingType !== BOOKING_TYPE.Offline.toString()
      ? dataBooking.doctor?.price
      : Math.floor(dataBooking.doctor?.price! * 0.2)
  const onSubmit = () => {
    paymentMutation.mutate(
      {
        data: {
          userID: userId,
          profileID: dataBooking.profile?.profileID as string,
          doctorID: dataBooking.doctor?.userID as string,
          price: price?.toString() as string,
          bookingTime: dataBooking.bookingTime,
          bookingType: dataBooking.bookingType,
          scheduleID: dataBooking.slot?.slotID as string
        },
        type: dataBooking.method
      },
      {
        onSuccess(data) {
          router.push(data?.message as string)
        },
        onError() {
          toast.error("Booking doctor fail")
        }
      }
    )
  }
  useEffect(() => {
    dispatch(bookingDoctorSlice.actions.methodChange(PAYMENT.VNPAY.name))
  }, [])
  return (
    <>
      <div className="flex items-center gap-x-3">
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
      <div className="flex flex-col my-8  px-[30px] py-4">
        <h3 className="text-xl font-semibold text-black1">
          {t("base:booking.summary")}
        </h3>
        <Divider></Divider>
        <div className="flex flex-col gap-y-2">
          <div className="flex items-center gap-x-4">
            <div className="flex flex-col flex-1 gap-y-1">
              <h1 className="text-lg font-medium">
                {combineName(
                  dataBooking.profile?.firstName,
                  dataBooking.profile?.lastName
                )}
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
                    {dayformat(dataBooking.bookingTime.toString())}
                  </time>
                </div>
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
                        d="M12 6v6h4.5m4.5 0a9 9 0 11-18 0 9 9 0 0118 0z"
                      />
                    </svg>
                  </span>
                  <time className="text-base font-medium text-black2">
                    At {dataBooking.slot?.startTime}
                  </time>
                </div>
              </div>
            </div>
          </div>
        </div>
        <Divider></Divider>
        <div className="flex flex-col gap-y-2">
          <div className="flex items-center gap-x-4">
            <div className="relative w-[60px] h-[60px]">
              <ImageCustom
                fill
                src={dataBooking.doctor?.avatar}
                alt="avatar-image"
                className="object-cover rounded-md"
              />
            </div>
            <div className="flex flex-col gap-y-1">
              <h1 className="text-lg font-medium">
                {combineName(
                  dataBooking.doctor?.firstName,
                  dataBooking.doctor?.lastName
                )}
              </h1>
              <div className="flex items-center gap-x-2">
                <Tag
                  color={colorsProvider.success}
                  className="px-4 py-2 font-semibold rounded-lg"
                >
                  <div className="flex items-center gap-x-1">
                    <span>
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={1.5}
                        stroke="currentColor"
                        className="w-5 h-5"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m5.231 13.481L15 17.25m-4.5-15H5.625c-.621 0-1.125.504-1.125 1.125v16.5c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9zm3.75 11.625a2.625 2.625 0 11-5.25 0 2.625 2.625 0 015.25 0z"
                        />
                      </svg>
                    </span>
                    <span>
                      {dataBooking.doctor?.specialization.specializationName}
                    </span>
                  </div>
                </Tag>
                <Tag
                  color={colorsProvider.secondary}
                  className="px-4 py-2 font-semibold rounded-lg"
                >
                  <div className="flex items-center gap-x-1">
                    <span>
                      {dataBooking.bookingType ===
                      BOOKING_TYPE.Offline.toString()
                        ? "Offline"
                        : "Online"}
                    </span>
                  </div>
                </Tag>
              </div>
            </div>
            <div className="ml-auto text-base text-black1">
              {formatValueToVND(price || 0)}
            </div>
          </div>
        </div>
        <Divider></Divider>
        <div className="flex justify-between mt-3">
          <h3 className="text-base font-medium text-black1">
            {t("base:booking.total")}
          </h3>
          <span className="text-base text-black1">
            {formatValueToVND(price || 0)}
          </span>
        </div>
        <Divider></Divider>
        <div className="flex flex-col gap-y-2">
          <Payment
            methodSelected={dataBooking.method}
            onChangeMethod={(value) =>
              dispatch(bookingDoctorSlice.actions.methodChange(value))
            }
          />
        </div>
        <CustomButton
          className="mt-6"
          onClick={onSubmit}
          isLoading={paymentMutation.isLoading}
        >
          {t("base:booking.btn_pay")}
        </CustomButton>
      </div>
    </>
  )
}
const Divider = () => {
  return <div className="h-[1px] w-full bg-carbon my-5 "></div>
}
export default StepThree
