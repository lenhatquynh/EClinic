import Tag from "components/Common/Tag"
import CustomButton from "components/User/Button"
import { useGetServicePackageByIDQuery } from "hooks/query/service/useService"
import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import Image from "next/image"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { useTranslation } from "react-i18next"
import { useDispatch, useSelector } from "react-redux"
import { bookingServiceSlice } from "store/module/booking/service/booking-service-slice"
import { IBreadcrum } from "types/Base.type"
import { ServicePackage } from "types/Service"
import CardService from "../Home/section/services/components/card/CardService"
import BookingModel from "./components/BookingModel"
import ModalSuccess from "components/Common/Modal/ModalSuccess"
import { RootState } from "store/store"
import { toast } from "react-hot-toast"
import classNames from "classnames"

interface Props {
  servicePackages?: ServicePackage[]
}
const ServicesDetailPage = ({ servicePackages }: Props) => {
  const isLogin = useSelector((state: RootState) => state.auth.isLoggedIn)
  const [status, setStatus] = useState("")

  const { t } = useTranslation(["base", "ser"])
  const dispatch = useDispatch()
  const [show, setShow] = useState(false)
  const [isActive, setIsActive] = useState(false)
  const router = useRouter()
  const { data, isLoading } = useGetServicePackageByIDQuery(
    router.query.id! as string
  )
  const servicePackage: ServicePackage = data?.data as ServicePackage
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
    return <p>Loading Service Package Detail!</p>
  }
  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.servies"), href: "/services" },
    { label: t("base:pages.detail") }
  ]
  const handleBooking = () => {
    if (isLogin) {
      setShow(true)
      dispatch(bookingServiceSlice.actions.servicePackageChange(data?.data))
    } else {
      toast.error("You need to login to booking")
    }
  }

  return (
    <>
      <Head>
        <title>Forum page</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <BookingModel
          show={show}
          onClose={() => {
            dispatch(bookingServiceSlice.actions.resetBookingService())
            setShow(false)
          }}
        />
        <section className="flex flex-col mt-6 gap-x-10">
          <div className="flex-1">
            <div className="flex-col md:flex-row flex w-full  md:h-[320px] gap-y-4 md:gap-x-8 ">
              <div className="relative w-full h-56 md:h-full md:w-2/4">
                <Image
                  src={servicePackage?.image || "/images/sample-2.png"}
                  alt="detail-service"
                  fill
                  className="object-cover rounded-md"
                />
              </div>
              <div className="flex flex-col justify-between flex-1">
                <div className="flex flex-col gap-y-4">
                  <h4 className="text-2xl font-semibold text-h1 line-clamp-2">
                    {servicePackage?.servicePackageName}
                  </h4>
                  <div className="flex w-[660px] flex-wrap">
                    {servicePackage?.serviceItems?.map((service) => (
                      <Tag key={service?.serviceID} className="mb-1 mr-1">
                        {service.serviceName}
                      </Tag>
                    ))}
                  </div>
                </div>
                <div className="mt-5 space-y-1 md:space-y-3 md:mt-0">
                  <div className="flex space-x-6">
                    <div className="flex flex-col md:gap-y-2">
                      <span className="text-lg font-semibold text-h1">
                        {(servicePackage?.price || 0) *
                          (1 - (servicePackage?.discount || 1) / 100)}{" "}
                        VND
                      </span>
                      <span className="text-sm font-light text-gray-400">
                        {t("ser:price")}
                      </span>
                    </div>
                    <div className="flex flex-col md:gap-y-2">
                      <span className="text-lg font-semibold text-h1">
                        {servicePackage?.totalOrder}
                      </span>
                      <span className="text-sm font-light text-gray-400">
                        {t("ser:totalSell")}
                      </span>
                    </div>
                  </div>
                  <CustomButton
                    kind="primary"
                    size="small"
                    className="w-full "
                    onClick={() => handleBooking()}
                  >
                    <span className="text-[10px] md:text-xs">
                      {t("ser:btnRegis")}
                    </span>
                  </CustomButton>
                </div>
              </div>
            </div>
            <h3 className="mt-8 text-2xl font-normal text-h1">
              {t("ser:inforDetail")}
            </h3>
            <p className="text-[#696974] text-sm leading-loose mt-4">
              {servicePackage?.description}
            </p>
          </div>
          <div className="mt-12 space-y-3">
            <h3 className="text-2xl font-semibold text-h1">
              {" "}
              {t("ser:specialService")}
            </h3>
            <div className="grid grid-cols-1 gap-1 md:grid-cols-4">
              {servicePackages?.map((item) => (
                <CardService
                  servicePackage={item}
                  key={item.servicePackageID}
                />
              ))}
            </div>
          </div>
        </section>
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
              onClick={() => router.push("/services")}
            >
              Make a New Appointment
            </CustomButton>
          ) : (
            <CustomButton
              className="mt-3"
              onClick={() => {
                router.replace(`/services/${router.query.id}`, undefined, {
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
      </UserSecondaryLayout>
    </>
  )
}

export default ServicesDetailPage
