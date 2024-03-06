import { useQuery } from "@tanstack/react-query"
import { chatService } from "services/chat.service"
import { QUERY_KEYS } from "shared/constant/constant"

export const useGetAllNewRoomQuery = (pageNumber: number, pageSize: number) => {
  const queryKey = [
    QUERY_KEYS.CHAT.ROOM,
    pageNumber,
    pageSize,
    QUERY_KEYS.CHAT.NEW_ROOM
  ]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllNewRoom(pageNumber, pageSize)
  })
}
export const useGetAllRoomOfUserQuery = (
  pageNumber: number,
  pageSize: number
) => {
  const queryKey = [QUERY_KEYS.CHAT.ROOM, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllRoomOfUser(pageNumber, pageSize),
    keepPreviousData: true
  })
}
export const useGetAllRoomOfSupporterQuery = (
  pageNumber: number,
  pageSize: number
) => {
  const queryKey = [QUERY_KEYS.CHAT.ROOM, pageNumber, pageSize, "Supporter"]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllRoomOfSupporter(pageNumber, pageSize)
  })
}
export const useGetAllRoomOfDoctorQuery = (
  pageNumber: number,
  pageSize: number
) => {
  const queryKey = [QUERY_KEYS.CHAT.ROOM, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllRoomOfDoctor(pageNumber, pageSize)
  })
}
export const useGetAllRoomTypeQuery = () => {
  const queryKey = [QUERY_KEYS.CHAT.ROOM_TYPE]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllRoomType()
  })
}
