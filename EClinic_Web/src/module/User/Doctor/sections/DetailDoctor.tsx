import ImageCustom from "components/Common/ImageCustom"
import ModalPrimary from "components/Common/Modal/ModalPrimary"
import ModalSuccess from "components/Common/Modal/ModalSuccess"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { useGetDoctorProfilesByIdQuery } from "hooks/query/profile/useProfile"
import { useRouter } from "next/router"
import { OverlayScrollbarsComponent } from "overlayscrollbars-react"
import { useEffect, useState } from "react"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { useSelector } from "react-redux"
import { combineName, formatValueToVND } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { selectSlotBooking } from "store/module/booking/doctor/booking-doctor-selector"
import { bookingDoctorSlice } from "store/module/booking/doctor/booking-doctor-slice"
import { RootState, useAppDispatch } from "store/store"
import About from "./About"
import { StepOne } from "./step/StepOne"
import StepThree from "./step/StepThree"
import { StepTwo } from "./step/StepTwo"
import { DetailDoctorModalWrapper } from "./styles"
import classNames from "classnames"
const DetailDoctor = () => {
  const { t } = useTranslation(["base"])

  const [isActive, setIsActive] = useState(false)
  const [status, setStatus] = useState("")
  const isLogin = useSelector((state: RootState) => state.auth.isLoggedIn)
  const slot = useSelector(selectSlotBooking)
  const dispatch = useAppDispatch()
  const router = useRouter()
  const { data, isLoading } = useGetDoctorProfilesByIdQuery(
    router.query.id! as string
  )
  const [showModal, setShowModal] = useState(false)
  const [currentStep, setCurrentStep] = useState<number>(1)
  const onChangeStep = (step: typeof currentStep) => {
    setCurrentStep(step)
  }
  useEffect(() => {
    if (showModal === false) {
      setCurrentStep(1)
    }
  }, [showModal])
  useEffect(() => {
    if (data?.isSuccess) {
      dispatch(bookingDoctorSlice.actions.doctorChange(data.data))
    }
  }, [data?.isSuccess, showModal])
  useEffect(() => {
    if (router.query.bookingId && router.query.status) {
      setTimeout(() => {
        setIsActive(true)
        console.log("setTimeout ~ router.query.status:", router.query.status)
        setStatus(router.query.status as string)
      }, 1500)
    }
  }, [router.query.bookingId, router.query.status])
  if (isLoading) {
    return <p>Loading</p>
  }
  if (!data?.data) {
    return null
  }
  return (
    <div className="flex flex-col">
      <ModalPrimary
        show={showModal}
        onClose={() => {
          dispatch(bookingDoctorSlice.actions.resetBookingDoctor())
          setShowModal(false)
        }}
      >
        <OverlayScrollbarsComponent
          defer
          options={{ scrollbars: { autoHide: "scroll" } }}
        >
          <DetailDoctorModalWrapper>
            {currentStep === 1 ? (
              <StepOne />
            ) : currentStep === 2 ? (
              <StepTwo onBack={() => onChangeStep(1)} />
            ) : (
              <StepThree onBack={() => onChangeStep(2)} />
            )}
          </DetailDoctorModalWrapper>
        </OverlayScrollbarsComponent>

        <div className="footer ">
          <div className="flex justify-between px-6">
            <CustomButton kind="tertiary" onClick={() => setShowModal(false)}>
              {t("base:booking.btn_cancel")}
            </CustomButton>
            {currentStep !== 3 && (
              <CustomButton
                kind="primary"
                onClick={() => {
                  slot
                    ? onChangeStep(currentStep + 1)
                    : toast.error("Please choose a slot")
                }}
              >
                {t("base:booking.btn_ct")}
              </CustomButton>
            )}
          </div>
        </div>
      </ModalPrimary>
      <div className="grid grid-cols-3 gap-8">
        <div className="flex flex-col items-center justify-center col-span-1 gap-y-5 background-primary h-fit">
          <div className="flex flex-col items-center justify-center gap-y-2">
            <div className="relative w-[168px] h-[168px]">
              <ImageCustom
                fill
                src={data?.data.avatar || "/images/sample.png"}
                alt="avatar-image"
                className="object-cover"
              />
            </div>
            <h1 className="text-lg font-semibold">
              {data.data.title +
                "." +
                combineName(data?.data.firstName, data?.data.lastName)}
            </h1>
            <div className="flex items-center gap-x-2">
              <span>Price:</span>
              <span>{formatValueToVND(data.data.price)}</span>
            </div>
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
                <span>{data.data.specialization.specializationName}</span>
              </div>
            </Tag>
          </div>
          <p className="text-sm leading-normal text-center text-disable">
            {data.data.content}
          </p>
          <CustomButton
            kind="primary"
            className="w-[190px] rounded-xl"
            onClick={() => {
              if (isLogin) {
                setShowModal(true)
              } else {
                toast.error("You need to login to booking")
              }
            }}
          >
            {t("base:booking.btn")}
          </CustomButton>
        </div>
        <div className="col-span-2 background-primary">
          <About data={data.data.description} />
        </div>
      </div>
      <ModalSuccess
        isSuccess={isActive}
        setIsSuccess={() => setIsActive(false)}
        imageUrl={
          status === "success"
            ? "/images/success-image.png"
            : "/images/fail.png"
        }
      >
        <h1
          className={classNames(
            "text-4xl font-bold text-center ",
            status === "success" ? "text-black1" : " text-error"
          )}
        >
          {status === "success" ? "Congratulations" : "Payment failed"}
        </h1>
        <p className="mt-5 text-sm font-light text-center text-black2">
          {status === "success"
            ? "Your Appointment Request has been Successful!"
            : "Your appointment request has failed!"}
        </p>
        {status === "success" ? (
          <CustomButton
            className="mt-3"
            onClick={() => router.push("/doctors")}
          >
            Make a New Appointment
          </CustomButton>
        ) : (
          <CustomButton
            className="mt-3"
            onClick={() => {
              router.replace(`/doctors/${router.query.id}`, undefined, {
                shallow: true
              })
              setIsActive(false)
              setStatus("")
            }}
          >
            Booking again
          </CustomButton>
        )}
      </ModalSuccess>
    </div>
  )
}
export default DetailDoctor
