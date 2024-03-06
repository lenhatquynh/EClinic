import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { IUser } from "types/User"
import { IAuthState } from "../../../types/Auth"

const initialState: IAuthState = {
  isLoggedIn: false,
  user: { role: "", userId: "" }
}

const authSlice = createSlice({
  name: "auth",
  initialState: initialState,
  reducers: {
    deleteAuthenticate: (state) => {
      state.isLoggedIn = false
      state.user = { role: "", userId: "" }
    },
    authenticate: (state, action: PayloadAction<IUser>) => {
      state.isLoggedIn = true
      state.user = action.payload
    }
  }
})

export const { deleteAuthenticate, authenticate } = authSlice.actions
export default authSlice.reducer
