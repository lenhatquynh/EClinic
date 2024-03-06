import ImageCustom from "components/Common/ImageCustom"
import ModalSuccess from "components/Common/Modal/ModalSuccess"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import useConfirm from "context/ComfirmContext"
import { useUpdateStateBookingDoctorMutation } from "hooks/query/booking"
import { useRouter } from "next/router"
import { queryClient } from "pages/_app"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { QUERY_KEYS, STATUS_BOOKING } from "shared/constant/constant"
import { combineName, dayformat } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { IBookingDoctor } from "types/Booking"
interface Props {
  kind: number
  data: IBookingDoctor
}

const DoctorBookingItem = ({ kind, data }: Props) => {
  const router = useRouter()
  const [isSuccess, setIsSuccess] = useState(false)
  const confirm = useConfirm()
  const cancelBooking = useUpdateStateBookingDoctorMutation()
  const handleCancel = async () => {
    if (confirm) {
      const choice = await confirm({
        title: <h3 className="text-2xl text-error">Cancel Appointment</h3>,
        content: (
          <div className="flex flex-col gap-y-2w">
            <p>Are you sure you want to cancel your appoiment?</p>
            <p>
              Only <strong className="text-lg font-bold">50%</strong> of the
              funds will be returned to your account.
            </p>
          </div>
        ),
        btnAgree: "Yes, Cancel",
        btnDisagree: "Back"
      })
      if (choice) {
        cancelBooking.mutate(data.bookingID, {
          onSuccess() {
            toast.success("Cancel successfully")
            queryClient.refetchQueries([QUERY_KEYS.BOOKING.DOCTOR])
          },
          onError() {
            toast.error("Cancel failure")
          }
        })
      }
    }
  }
  return (
    <div className="flex flex-col p-4 bg-white rounded-lg shadow">
      <div className="relative w-48 h-48 mx-auto mb-3 md:hidden">
        <ImageCustom
          src={data.doctorProfile.avatar}
          fill
          alt="image doctor"
          className="object-cover rounded-lg"
        />
      </div>
      <div className="flex items-center top gap-x-4">
        <div className="relative hidden w-32 md:block h-28">
          <ImageCustom
            src={data.doctorProfile.avatar}
            fill
            alt="image doctor"
            className="object-cover rounded-lg"
          />
        </div>
        <div className="flex flex-col justify-between h-full">
          <h4 className="text-lg font-semibold">
            {combineName(
              data.doctorProfile.firstName,
              data.doctorProfile.lastName
            )}
          </h4>
          <div className="flex items-center gap-x-2">
            <span>
              {data.bookingType.toString() === "0"
                ? "Video Call"
                : "At the clinic"}
            </span>
            <span>-</span>
            <Tag
              color={
                kind === STATUS_BOOKING.UPCOMING
                  ? colorsProvider.pending
                  : kind === STATUS_BOOKING.DONE
                  ? colorsProvider.success
                  : colorsProvider.error
              }
              className="capitalize cursor-pointer"
            >
              {kind === STATUS_BOOKING.UPCOMING
                ? "Upcoming"
                : kind === STATUS_BOOKING.DONE
                ? "Done"
                : "Cancel"}
            </Tag>
          </div>
          <div className="flex items-center gap-2 text-disable">
            <span>{dayformat(data.bookingCalendar)}</span>
            <span>-</span>
            <span>{data.slot.startTime}</span>
          </div>
        </div>
        {Number(data.bookingType) === 0 && (
          <Tag
            color={colorsProvider.primary}
            className="cursor-pointer"
            onClick={() => {
              router.push(`/user/chat?roomId=${data.roomID}`)
            }}
          >
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
                d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z"
              />
            </svg>
          </Tag>
        )}
      </div>
      {kind !== STATUS_BOOKING.CANCEL && (
        <>
          <div className="h-[1px] w-full bg-gray-200 my-4"></div>
          <div className="flex items-center gap-x-4 justify-self-end">
            <CustomButton
              kind="secondary"
              onClick={() => {
                kind === STATUS_BOOKING.UPCOMING && handleCancel()
              }}
            >
              {kind === STATUS_BOOKING.DONE ? "Book Again" : "Cancel Appoiment"}
            </CustomButton>
            {/* {kind == STATUS_BOOKING.UPCOMING && (
              <CustomButton
                onClick={() => {
                  setShowModal(true)
                }}
              >
                Rechedule
              </CustomButton>
            )} */}
          </div>
        </>
      )}
      <ModalSuccess setIsSuccess={setIsSuccess} isSuccess={isSuccess}>
        <h1 className="text-4xl font-bold text-center text-black1">
          Rescheduling Success
        </h1>
        <p className="max-w-[400px] text-gray80 text-center mt-4">
          Appointment successfully changed. The doctor you selected will contact
          you.
        </p>
        <CustomButton className="mt-3" onClick={() => setIsSuccess(false)}>
          Close
        </CustomButton>
      </ModalSuccess>
    </div>
  )
}

export default DoctorBookingItem
