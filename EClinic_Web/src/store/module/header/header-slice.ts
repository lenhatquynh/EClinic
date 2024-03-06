import { createSlice } from "@reduxjs/toolkit"

export const headerSlice = createSlice({
  name: "header",
  initialState: {
    zIndex: 50
  },
  reducers: {
    onChangeZIndex: (state, action) => {
      state.zIndex = action.payload
    }
  }
})
