import { Skeleton } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import CustomButton from "components/User/Button"
import Link from "next/link"
import { useTranslation } from "react-i18next"
import { combineName, formatValueToVND } from "shared/helpers/helper"
import { IProfileDoctor } from "types/Profile.type"

interface IProps {
  doctor: IProfileDoctor
}
const CardDoctor = ({ doctor }: IProps) => {
  const { t } = useTranslation(["base", "ser"])

  return (
    <div className="bg-white rounded-[10px] p-5 w-full border border-solid border-[#EAEAEA]">
      <div className="flex gap-4">
        <div className="relative w-[100px] h-[100px] ">
          <ImageCustom
            alt="avtar-doctor"
            src={doctor.avatar || "/images/sample.png"}
            fill
            className="object-cover rounded-md "
          />
        </div>
        <div className="flex flex-col flex-1 gap-y-2">
          <div className="flex justify-between ">
            <div className="flex flex-col">
              <span className="text-base font-semibold">
                {combineName(doctor.firstName, doctor.lastName)}
              </span>
              <div className="flex items-center gap-x-2">
                <svg
                  width={20}
                  height={20}
                  viewBox="0 0 20 20"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M14.2044 5H16.6736C18.0374 5 19.1429 6.11929 19.1429 7.5V15.8333C19.1429 17.214 18.0374 18.3333 16.6736 18.3333H3.50444C2.14073 18.3333 1.03522 17.214 1.03522 15.8333V7.5C1.03522 6.11929 2.14073 5 3.50444 5H5.97367C5.97367 3.61929 7.07918 2.5 8.44289 2.5H11.7352C13.0989 2.5 14.2044 3.61929 14.2044 5ZM5.56213 6.66667V16.6667H14.616V6.66667H5.56213ZM16.2621 6.66667V16.6667H16.6736C17.1282 16.6667 17.4967 16.2936 17.4967 15.8333V7.5C17.4967 7.03976 17.1282 6.66667 16.6736 6.66667H16.2621ZM3.50444 6.66667H3.91598V16.6667H3.50444C3.04987 16.6667 2.68137 16.2936 2.68137 15.8333V7.5C2.68137 7.03976 3.04987 6.66667 3.50444 6.66667ZM12.5583 5C12.5583 4.53976 12.1898 4.16667 11.7352 4.16667H8.44289C7.98832 4.16667 7.61982 4.53976 7.61982 5H12.5583Z"
                    fill="#92929D"
                  />
                </svg>

                <span className="text-[#92929D] text-sm">
                  {doctor.specialization.specializationName}
                </span>
              </div>
            </div>
            <Link href={`/doctors/${doctor.userID}`}>
              <CustomButton
                kind="primary"
                size="small"
                className="max-h-[32px] rounded-[10px]"
              >
                Booking
              </CustomButton>
            </Link>
          </div>
          <div className="flex items-center gap-6">
            <InfoItem content={doctor.title} label={t("ser:position")} />

            <InfoItem
              content={formatValueToVND(doctor.price)}
              label={t("ser:price")}
            />
          </div>
          <div className="w-full bg-gray-200 h-[1px]"></div>
          <p className="text-sm text-gray-500 line-clamp-3">{doctor.content}</p>
        </div>
      </div>
    </div>
  )
}
const InfoItem = ({
  label = "",
  content = ""
}: {
  label: string
  content: React.ReactNode
}) => {
  return (
    <div className="flex flex-col ">
      <span className="text-[#B5B5BE] font-normal text-xs">{label}</span>
      <span className="font-semibold">{content}</span>
    </div>
  )
}
export const CardDoctorSkeleton = () => {
  return (
    <div className="bg-white rounded-[10px] p-5 w-full border border-solid border-[#EAEAEA]">
      <div className="flex gap-4">
        <div className="relative w-[100px] h-[100px] overflow-hidden rounded-md">
          <Skeleton variant="rounded" className="w-full h-full" />
        </div>
        <div className="flex flex-col flex-1 gap-y-2">
          <div className="flex justify-between ">
            <div className="flex flex-col">
              <span className="text-base font-semibold">
                <Skeleton variant="text" />
              </span>
              <div className="flex items-center gap-x-2">
                <Skeleton variant="rounded" width={80} height={20} />
              </div>
            </div>
            <Skeleton variant="rounded" width={60} height={32} />
          </div>
          <div className="flex items-center gap-6">
            <Skeleton variant="rounded" width={60} height={10} />
            <Skeleton variant="rounded" width={60} height={10} />
            <Skeleton variant="rounded" width={60} height={10} />
          </div>
          <div className="w-full bg-gray-200 h-[1px]"></div>
          <p className="flex flex-col text-sm text-gray-500 gap-y-1">
            <Skeleton variant="text" />
            <Skeleton variant="text" />
          </p>
        </div>
      </div>
    </div>
  )
}
export default CardDoctor
