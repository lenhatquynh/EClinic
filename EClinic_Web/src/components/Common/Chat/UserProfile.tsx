import { IconButton, Skeleton, Tooltip } from "@mui/material"
import { useInfiniteQuery } from "@tanstack/react-query"
import { AnimatePresence, motion } from "framer-motion"
import { useSimpleProfile } from "hooks/query/profile/useProfile"
import { PropsWithChildren } from "react"
import { HiOutlineXMark, HiPlus } from "react-icons/hi2"
import { QUERY_KEYS } from "shared/constant/constant"
import { combineName, getDataPaginate } from "shared/helpers/helper"
import ImageCustom from "../ImageCustom"
import { chatService } from "services/chat.service"
interface IProps {
  onClose: () => void
  userId: string
  roomId: string
}
const UserProfile = ({ onClose, userId, roomId }: IProps) => {
  const imageHistory = useInfiniteQuery(
    [QUERY_KEYS.CHAT.MESSAGE, 10, roomId, userId],
    async ({ pageParam = 1 }) => {
      const res = await chatService.getAllImageOfRoom(pageParam, 10, roomId)
      return res
    },
    {
      getNextPageParam: (lastPage) =>
        getDataPaginate(lastPage).HasNext
          ? getDataPaginate(lastPage).PageIndex + 1
          : undefined
    }
  )
  const { isLoading, data } = useSimpleProfile(userId)

  return (
    <AnimatePresence initial={false} onExitComplete={() => null} mode="wait">
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        exit={{ opacity: 0 }}
        transition={{ duration: 0.2 }}
        className="max-w-[300px] w-full py-5 relative flex flex-col space-y-[26px] "
      >
        <div className="absolute top-0 left-0 text-2xl text-gray-400 cursor-pointer translate-y-1/4 translate-x-1/4">
          <Tooltip title="Close" placement="top">
            <IconButton onClick={onClose}>
              <HiOutlineXMark />
            </IconButton>
          </Tooltip>
        </div>

        {/* top profile */}
        <div className="flex flex-col items-center gap-y-1.5">
          {isLoading && (
            <Skeleton
              variant="circular"
              width={80}
              height={80}
              className="flex-shrink-0"
            />
          )}
          {data && (
            <div className="relative w-20 h-20 overflow-hidden rounded-full">
              <ImageCustom
                src={data.data.avatar || "/images/avatars/avatar_2.jpg"}
                fill
                alt="user-avatar"
                className="object-cover"
              />
            </div>
          )}
          {isLoading && (
            <>
              <Skeleton variant="text" height={36} width={120} />
              <Skeleton variant="text" height={36} width={130} />
            </>
          )}
          {data && (
            <>
              <h3 className="text-base font-semibold text-h1">
                @{combineName(data?.data?.firstName, data?.data?.lastName)}
              </h3>
              <span className="font-light text-disable">30 Years, Male</span>
            </>
          )}
        </div>
        <div className="pb-6 border border-t-0 border-b border-gray-200 border-solid border-x-0">
          <div className="px-[22px] flex flex-col gap-y-3">
            <Field
              isLoading={isLoading}
              label="Email"
              value={data?.data?.email}
            />
            <Field isLoading={isLoading} label="Phone" value="(704) 555-0127" />
          </div>
        </div>
        <div className="flex flex-col px-[22px]">
          <FieldMain label="Shared Image">
            <div className="grid grid-cols-4 gap-2">
              {imageHistory.data?.pages.map((page) =>
                page?.data?.data?.map((img, index) => (
                  <div
                    className="relative overflow-hidden rounded-md w-14 h-14"
                    key={index}
                  >
                    <ImageCustom
                      src={img}
                      fill
                      alt="user-avatar"
                      className="object-cover"
                    />
                  </div>
                ))
              )}

              <Tooltip title="Load more" placement="top">
                <IconButton>
                  <HiPlus />
                </IconButton>
              </Tooltip>
            </div>
          </FieldMain>
        </div>
      </motion.div>
    </AnimatePresence>
  )
}
interface IFieldProps extends PropsWithChildren {
  label: string
  value?: string
  isLoading?: boolean
}
const Field = ({ label, value, isLoading = false }: IFieldProps) => {
  return (
    <div className="flex flex-col space-y-0.5">
      <span className="font-light text-disable ">{label}</span>
      {isLoading && <Skeleton variant="text" height={30} width={120} />}
      {value && <span className=" text-h1">{value}</span>}
    </div>
  )
}
const FieldMain = ({ label, children }: IFieldProps) => {
  return (
    <div className="flex flex-col space-y-2">
      <span className="font-medium text-disable ">{label}</span>
      {children}
    </div>
  )
}
export default UserProfile
