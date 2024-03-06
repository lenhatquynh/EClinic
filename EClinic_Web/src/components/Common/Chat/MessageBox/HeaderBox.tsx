import { IconButton, Skeleton, Tooltip } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import { HiOutlineLockClosed } from "react-icons/hi"
import {
  HiOutlineInformationCircle,
  HiOutlineVideoCamera
} from "react-icons/hi2"
import { useSelector } from "react-redux"
import { ROLE } from "shared/constant/constant"
import { combineName } from "shared/helpers/helper"
import { RootState } from "store/store"
import { ProfileChat } from "types/Chat"
interface IProps {
  toggleInfo: () => void
  author?: ProfileChat
  isLoading: boolean
  isLoseRoom: boolean
  handleCall: () => void
  handleClose: () => void
}
export const HeaderBox = ({
  toggleInfo,
  author,
  isLoseRoom,
  isLoading = false,
  handleCall,
  handleClose
}: IProps) => {
  const role = useSelector((state: RootState) => state.auth.user.role)
  return (
    <div className="flex justify-between px-5 py-3 border border-t-0 border-gray-200 border-solid border-x-0">
      <div className="flex items-center space-x-2">
        {isLoading && (
          <Skeleton
            variant="circular"
            width={40}
            height={40}
            className="flex-shrink-0"
          />
        )}
        {author && (
          <div className="relative w-10 h-10 overflow-hidden rounded-full">
            <ImageCustom
              src={author.avatar}
              fill
              alt="user-avatar"
              className="object-cover"
            />
          </div>
        )}
        <div className="flex flex-col justify-between">
          {isLoading && (
            <>
              <Skeleton variant="text" width={100} />
              {/* <Skeleton variant="text" width={30} /> */}
            </>
          )}
          {author && (
            <>
              <span className="text-base font-medium text-h1">
                {combineName(author.firstName, author.lastName)}
              </span>
              {/* <span className="text-sm text-green-500">Online</span> */}
            </>
          )}
        </div>
      </div>
      <div className="flex items-center gap-x-2">
        {(role === ROLE.SUPPORTER || role === ROLE.DOCTOR) && !isLoseRoom && (
          <>
            <Tooltip
              title="Close this room"
              placement="top"
              onClick={handleClose}
            >
              <IconButton>
                <span className="text-3xl text-red-500">
                  <HiOutlineLockClosed />
                </span>
              </IconButton>
            </Tooltip>
          </>
        )}
        {role === ROLE.DOCTOR && (
          <Tooltip title="Call video" placement="top" onClick={handleCall}>
            <IconButton>
              <span className="text-3xl text-secondary">
                <HiOutlineVideoCamera />
              </span>
            </IconButton>
          </Tooltip>
        )}

        <Tooltip title="View profile" placement="top">
          <IconButton onClick={toggleInfo}>
            <span className="text-3xl text-disable">
              <HiOutlineInformationCircle />
            </span>
          </IconButton>
        </Tooltip>
      </div>
    </div>
  )
}
