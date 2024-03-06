import { IUser } from "./User"
export interface IAuthState {
  isLoggedIn: boolean
  user: IUser
}
export interface ILogin {
  userName: string
  password: string
}
export interface ISignupForm {
  userName: string
  firstName: string
  lastName: string
  dateOfBirth: string
  gender: boolean
  email: string
  password: string
  confirmPassword: string
}
