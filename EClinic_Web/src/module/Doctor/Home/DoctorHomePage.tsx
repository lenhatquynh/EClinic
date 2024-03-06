import { Calendar, dayjsLocalizer } from "react-big-calendar"
import {
  useGetAllBookingDoctorForDoctorQuery,
  useUpdateStateBookingDoctorDoneMutation
} from "hooks/query/booking"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import { BOOKING_TYPE, STATUS_BOOKING } from "shared/constant/constant"
import dayjs from "dayjs"
import "react-big-calendar/lib/css/react-big-calendar.css"
// add optional time zone support
import timezone from "dayjs/plugin/timezone"
import colorsProvider from "shared/theme/colors"
import ModalPrimary from "components/Common/Modal/ModalPrimary"
import { OverlayScrollbarsComponent } from "overlayscrollbars-react"
import CustomButton from "components/User/Button"
import { useState } from "react"
import { IBookingDoctor } from "types/Booking"
import ImageCustom from "components/Common/ImageCustom"
import { combineName, dayformat, hexToRGBA } from "shared/helpers/helper"
import Tag from "components/Common/Tag"
import { toast } from "react-hot-toast"
import useConfirm from "context/ComfirmContext"
import { useRouter } from "next/router"
dayjs.extend(timezone)
const localizer = dayjsLocalizer(dayjs)

const DoctorHomePage = () => {
  const router = useRouter()
  const confirm = useConfirm()
  const [show, setShow] = useState(false)
  const updateStatus = useUpdateStateBookingDoctorDoneMutation()
  const [eventSelected, setEventSelected] = useState<IBookingDoctor | null>(
    null
  )
  const { data, isLoading, refetch } = useGetAllBookingDoctorForDoctorQuery(
    1,
    100,
    STATUS_BOOKING.UPCOMING
  )
  if (isLoading) {
    return <p>Loading...</p>
  }
  const events = data?.data.data
    ? data.data.data.map((item) => {
        const newDate = dayjs(item.bookingCalendar).format("YYYY-MM-DD")
        const startTime = dayjs(`${newDate}T${item.slot.startTime}:00`).format(
          "ddd MMM DD YYYY HH:mm:ss [GMT]ZZ (Indochina Time)"
        )
        const endTime = dayjs(`${newDate}T${item.slot.endTime}:00`).format(
          "ddd MMM DD YYYY HH:mm:ss [GMT]ZZ (Indochina Time)"
        )
        const eventColor =
          Number(item.bookingType) === BOOKING_TYPE.Offline
            ? colorsProvider.primary
            : colorsProvider.success

        return {
          id: item.bookingID,
          title: Number(item.bookingType) === 0 ? "Online" : "Offline",
          start: new Date(startTime),
          end: new Date(endTime),
          color: eventColor,
          data: item
        }
      })
    : []
  const handleClickEvent = (data: IBookingDoctor) => {
    setEventSelected(data)
    setShow(true)
  }
  const handleUpdateState = async () => {
    if (eventSelected) {
      if (confirm) {
        const choice = await confirm({
          title: "Complete appointment",
          content: "Are you sure want to complete this appointment?"
        })
        if (choice) {
          updateStatus.mutate(eventSelected.bookingID, {
            onSuccess: () => {
              toast.success("This appointment has been completed")
              refetch()
            },
            onError: () => {
              toast.error("Something went wrong")
            }
          })
        }
      }
    }
  }
  return (
    <>
      <Head>
        <title>Overview</title>
      </Head>
      <MainHeadingLayout>
        <Calendar
          views={["day", "agenda", "work_week", "month"]}
          events={events}
          selectable
          localizer={localizer}
          defaultDate={new Date()}
          defaultView="month"
          style={{ height: "100vh" }}
          onSelectEvent={(event) => handleClickEvent(event.data)}
          eventPropGetter={(event) => ({
            style: {
              backgroundColor: hexToRGBA(event.color, 0.25),
              color: event.color
            }
          })}
        />
        <ModalPrimary show={show} onClose={() => setShow(false)}>
          <OverlayScrollbarsComponent
            defer
            options={{ scrollbars: { autoHide: "scroll" } }}
          >
            {eventSelected && (
              <div className="flex flex-col p-4 gap-y-3">
                <h1 className="text-2xl text-h1">Booking information</h1>
                <div className="flex items-center top gap-x-4">
                  <div className="relative w-32 h-32">
                    <ImageCustom
                      src={eventSelected?.userProfile.avatar}
                      fill
                      alt="image doctor"
                      className="object-contain rounded-lg"
                    />
                  </div>
                  <div className="flex flex-col justify-between h-full">
                    <h4 className="text-lg font-semibold">
                      {combineName(
                        eventSelected.userProfile.firstName,
                        eventSelected.userProfile.lastName
                      )}
                    </h4>
                    <div className="flex items-center gap-x-2">
                      <span>
                        {Number(eventSelected.bookingType) === 0
                          ? "Video Call"
                          : "At the clinic"}
                      </span>
                      <span>-</span>
                      <Tag
                        color={
                          eventSelected.bookingStatus ===
                          STATUS_BOOKING.UPCOMING
                            ? colorsProvider.pending
                            : eventSelected.bookingStatus ===
                              STATUS_BOOKING.DONE
                            ? colorsProvider.success
                            : colorsProvider.error
                        }
                        className="capitalize cursor-pointer"
                      >
                        {eventSelected.bookingStatus === STATUS_BOOKING.UPCOMING
                          ? "Upcoming"
                          : eventSelected.bookingStatus === STATUS_BOOKING.DONE
                          ? "Done"
                          : "Cancel"}
                      </Tag>
                    </div>
                    <div className="flex items-center gap-2 text-disable">
                      <span>{dayformat(eventSelected.bookingCalendar)}</span>
                      <span>-</span>
                      <span>{eventSelected.slot.startTime}</span>
                    </div>
                  </div>
                  {Number(eventSelected.bookingType) === 0 && (
                    <Tag
                      onClick={() => {
                        router.push(
                          `doctor/chats?roomId=${eventSelected.roomID}`
                        )
                      }}
                      color={colorsProvider.primary}
                      className="cursor-pointer"
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
              </div>
            )}
          </OverlayScrollbarsComponent>

          <div className="footer ">
            <div className="px-6">
              <CustomButton className="ml-auto" onClick={handleUpdateState}>
                Complete
              </CustomButton>
            </div>
          </div>
        </ModalPrimary>
      </MainHeadingLayout>
    </>
  )
}

export default DoctorHomePage
