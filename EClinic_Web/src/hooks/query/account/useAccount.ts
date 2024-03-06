import { useMutation } from "@tanstack/react-query"
import { AxiosError } from "axios"
import { toast } from "react-hot-toast"
import { authService } from "services/auth.service"
export interface IChangePassowrd {
  oldPassword: string
  newPassword: string
  confirmPassword: string
}
export interface IResetPassowrd {
  email: string
  newPassword: string
  confirmPassword: string
}
export const useAccountChangePassowordMutation = () => {
  return useMutation({
    mutationFn: (value: IChangePassowrd) => authService.changePassword(value),
    onSuccess: () => toast.success("Change password successfully"),
    onError: (data) => {
      if (data instanceof AxiosError) {
        toast.error(data.response?.data.message)
      } else {
        toast.error("Update password Failed")
      }
    }
  })
}
export const useAccountResetPassowordMutation = () => {
  return useMutation({
    mutationFn: (value: IResetPassowrd) => authService.resetPassword(value)
  })
}
export const useChangeStatusMutation = () => {
  return useMutation({
    mutationFn: (userId: string) => authService.changeStatus(userId)
  })
}
