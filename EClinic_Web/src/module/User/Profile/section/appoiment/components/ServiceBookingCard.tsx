import ImageCustom from "components/Common/ImageCustom"
import ModalSuccess from "components/Common/Modal/ModalSuccess"
import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import useConfirm from "context/ComfirmContext"
import { useUpdateStateBookingServiceMutation } from "hooks/query/booking"
import { queryClient } from "pages/_app"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { QUERY_KEYS, STATUS_BOOKING } from "shared/constant/constant"
import { combineName, dayformat } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { BookingService } from "types/Booking"
interface Props {
  kind: number
  data: BookingService
}

const ServiceBookingCard = ({ kind, data }: Props) => {
  const confirm = useConfirm()
  const [isSuccess, setIsSuccess] = useState(false)
  const cancelBooking = useUpdateStateBookingServiceMutation()
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
            queryClient.refetchQueries([QUERY_KEYS.BOOKING.SERVICE])
          },
          onError() {
            toast.error("Cancel failure")
          }
        })
      }
    }
  }
  return (
    <div className="flex flex-col p-4 bg-white rounded-lg shadow gap-y-3">
      <div className="flex items-center top gap-x-4">
        <div className="relative w-32 h-28">
          <ImageCustom
            src={data.service.image}
            fill
            alt="image doctor"
            className="object-cover rounded-lg"
          />
        </div>
        <div className="flex flex-col justify-between gap-y-2">
          <h4 className="text-lg font-semibold">
            {data.service.servicePackageName}
          </h4>
          <div className="flex items-center gap-x-2">
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
            <span>{dayformat(data.bookingTime)}</span>
          </div>
        </div>
      </div>

      {/* <div className="flex flex-col gap-y-1 ">
        <span className="text-disable">For patients:</span>
        <div className="flex items-center">
          <div className="relative w-16 h-16">
            <ImageCustom
              src={data.profile.avatar}
              fill
              alt="image doctor"
              className="object-cover rounded-lg"
            />
          </div>
          <div className="flex flex-col ">
            <h4 className="text-base font-semibold">
              {combineName(data.profile.lastName, data.profile.firstName)}
            </h4>
            <span className="text-disable">{dayformat(data.bookingTime)}</span>
          </div>
        </div>
      </div> */}
      {kind !== STATUS_BOOKING.CANCEL && (
        <>
          <div className="h-[1px] w-full bg-gray-200 my-2"></div>
          <div className="flex items-center gap-x-4 justify-self-end">
            <CustomButton
              kind="secondary"
              onClick={() => kind === STATUS_BOOKING.UPCOMING && handleCancel()}
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
      {/* {kind === STATUS_BOOKING.UPCOMING && (
        <ModalPrimary show={showModal} onClose={() => setShowModal(false)}>
          <OverlayScrollbarsComponent
            defer
            options={{ scrollbars: { autoHide: "scroll" } }}
          >
            <Reschedule />
          </OverlayScrollbarsComponent>

          <div className="footer">
            <div className="flex justify-between px-6">
              <CustomButton kind="tertiary" onClick={() => setShowModal(false)}>
                Cancel
              </CustomButton>
              <CustomButton
                kind="primary"
                onClick={() => {
                  setShowModal(false)
                  setIsSuccess(true)
                }}
              >
                Reschedule
              </CustomButton>
            </div>
          </div>
        </ModalPrimary>
      )} */}

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

export default ServiceBookingCard
