import jwt_decode from "jwt-decode"
import { authService } from "services/auth.service"
import { token } from "shared/utils/token"
import { ITokenDecode } from "types/Token"
import { IUser } from "../../../types/User"
import { AppDispatch } from "./../../store"
import { authenticate, deleteAuthenticate } from "./auth-slice"
export const loginUser = (user: IUser) => async (dispatch: AppDispatch) => {
  dispatch(authenticate(user))
}

export const logoutUser = () => async (dispatch: AppDispatch) => {
  token.deleteToken()
  dispatch(deleteAuthenticate())
}

export const checkLogin = () => async (dispatch: AppDispatch) => {
  if (!token.getToken().access_token && token.getToken().refresh_token) {
    const result = await authService.refreshToken()
    if (result?.isSuccess) {
      token.saveToken(result.data!.accessToken, result.data!.refreshToken)
    } else {
      // token.deleteToken()
    }
  }
  const accessToken = token.getToken().access_token
  const isAuthenticated = !!accessToken
  if (isAuthenticated) {
    const payload = jwt_decode(accessToken) as ITokenDecode
    dispatch(
      authenticate({
        role: payload.role,
        userId: payload.UserID
      })
    )
  } else {
    dispatch(deleteAuthenticate())
  }
}
