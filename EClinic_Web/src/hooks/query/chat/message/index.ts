import { useMutation, useQuery } from "@tanstack/react-query"
import { chatService } from "services/chat.service"
import { QUERY_KEYS } from "shared/constant/constant"

export const useGetAllMessageOfRoomQuery = (
  pageNumber: number,
  pageSize: number,
  roomId: string
) => {
  const queryKey = [QUERY_KEYS.CHAT.MESSAGE, pageNumber, pageSize, roomId]
  return useQuery({
    queryKey,
    queryFn: () => chatService.getAllMessageOfRoom(pageNumber, pageSize, roomId)
  })
}
export const useCreateChatMessage = () =>
  useMutation({
    mutationFn: ({ roomId, content }: { roomId: string; content: string }) =>
      chatService.createMessage(roomId, content)
  })
export const useCreateChatMessageFile = () =>
  useMutation({
    mutationFn: ({ roomId, file }: { roomId: string; file: File }) =>
      chatService.createMessageFile(roomId, file)
  })
