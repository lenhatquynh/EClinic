import { createSlice } from "@reduxjs/toolkit"

export const chatsSlice = createSlice({
  name: "chats",
  initialState: {
    roomId: "",
    search: ""
  },
  reducers: {
    onShowChatRoom: (state, action) => {
      state.roomId = action.payload
    },
    onSearch: (state, action) => {
      state.search = action.payload
    }
  }
})
