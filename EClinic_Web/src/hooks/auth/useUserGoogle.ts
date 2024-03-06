import { useGoogleLogin } from "@react-oauth/google"
import { useMutation } from "@tanstack/react-query"

import { useState } from "react"
import { toast } from "react-hot-toast"
import { getUserGoogle } from "services/google.service"

export default function useUserGoogle() {
  const [profile, setProfile] = useState<any>(null)
  const { mutate, error } = useMutation({
    mutationFn: (access_token: string) => getUserGoogle(access_token)
  })
  const action = useGoogleLogin({
    onSuccess: (codeResponse) => {
      toast.loading("Wait a minutes")
      return mutate(codeResponse.access_token, {
        onSuccess: (data: any) => {
          setProfile({
            ...data,
            access_token: codeResponse.access_token
          })
          toast.dismiss()
        }
      })
    },
    onError: (_) => {
      toast.dismiss()
      console.log("error")
    }
  })
  return { action, profile, error }
}
