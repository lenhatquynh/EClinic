import { Chip } from "@mui/material"
import CustomButton from "components/User/Button"
import React, { PropsWithChildren } from "react"
import { caculateAge } from "shared/helpers/helper"
import { IProfileDoctor } from "types/Profile.type"

const InfoGeneral = ({ doctor }: { doctor: IProfileDoctor }) => {
  return (
    <div className="flex flex-col w-full h-full px-6 border border-r-0 border-gray-300 border-solid border-y-0">
      <h3 className="text-2xl mb-7">Thông tin chung</h3>
      <div className="flex flex-col space-y-6">
        <FieldInfo title="Giới tính">
          <p>{doctor.gender ? "Male" : "Female"}</p>
        </FieldInfo>
        <FieldInfo title="Tuổi">
          <p>{caculateAge(doctor.dateOfBirth)} tuổi</p>
        </FieldInfo>
        <FieldInfo title="Chuyên Khoa">
          <div className="flex gap-2">
            <Chip label="Thần kinh" color="info" variant="outlined" />
            <Chip
              label="Chấn thường chỉnh hình"
              color="info"
              variant="outlined"
            />
          </div>
        </FieldInfo>
      </div>
      <CustomButton kind="primary" className="w-full mt-7 rounded-xl">
        <div className="flex items-center gap-x-2">
          <svg
            width={24}
            height={24}
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M19 6H5C4.44772 6 4 6.44772 4 7V19C4 19.5523 4.44772 20 5 20H19C19.5523 20 20 19.5523 20 19V7C20 6.44771 19.5523 6 19 6ZM5 4C3.34315 4 2 5.34315 2 7V19C2 20.6569 3.34315 22 5 22H19C20.6569 22 22 20.6569 22 19V7C22 5.34315 20.6569 4 19 4H5Z"
              fill="white"
            />
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M10 12C9.44772 12 9 12.4477 9 13C9 13.5523 9.44772 14 10 14H17C17.5523 14 18 13.5523 18 13C18 12.4477 17.5523 12 17 12H10ZM7 16C6.44772 16 6 16.4477 6 17C6 17.5523 6.44772 18 7 18H13C13.5523 18 14 17.5523 14 17C14 16.4477 13.5523 16 13 16H7Z"
              fill="white"
            />
            <path
              fillRule="evenodd"
              clipRule="evenodd"
              d="M7 2C6.44772 2 6 2.44772 6 3V7C6 7.55228 6.44772 8 7 8C7.55228 8 8 7.55228 8 7V3C8 2.44772 7.55228 2 7 2ZM17 2C16.4477 2 16 2.44772 16 3V7C16 7.55228 16.4477 8 17 8C17.5523 8 18 7.55228 18 7V3C18 2.44772 17.5523 2 17 2Z"
              fill="white"
            />
          </svg>
          <span>Booking now</span>
        </div>
      </CustomButton>
    </div>
  )
}
interface Props extends PropsWithChildren {
  title: string
}
const FieldInfo = ({ title = "", children }: Props) => {
  return (
    <div className="flex">
      <h4 className="w-[160px] text-gray-600">{title}:</h4>
      <div className="flex-1">{children}</div>
    </div>
  )
}

export default InfoGeneral
