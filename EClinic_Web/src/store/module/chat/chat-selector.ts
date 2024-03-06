import { RootState } from "store/store"

export const roomIdChatSelector = (state: RootState) => state.chats.roomId
export const searchChatSelector = (state: RootState) => state.chats.search
