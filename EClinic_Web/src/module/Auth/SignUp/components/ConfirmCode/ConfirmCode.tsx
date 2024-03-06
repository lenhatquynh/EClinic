import ImageCustom from "components/Common/ImageCustom"
import CustomButton from "components/User/Button"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { MdKeyboardBackspace } from "react-icons/md"
import VerificationInput from "react-verification-input"

interface Props {
  isLoading: boolean
  handleSencode: (code: string) => void
  handleBack: () => void
  handleResendCode: () => void
  email: string
  countError: number
}

const ConfirmCode = ({
  isLoading,
  handleSencode,
  email,
  handleBack,
  handleResendCode,
  countError
}: Props) => {
  const [otp, setOtp] = useState<string | null>(null)
  const handleConfirmCode = () => {
    if (otp && otp.length === 6) {
      handleSencode(otp)
    } else toast.error("Please enter OTP")
  }
  const handleReset = () => {
    setOtp(null)
    handleResendCode()
  }
  return (
    <>
      <div className="relative mx-auto background-primary w-full max-w-[600px] mt-6">
        <div
          className="absolute top-0 left-0 flex items-center p-2 transition-all translate-x-3 translate-y-3 rounded-lg cursor-pointer gap-x-2 hover:bg-gray-400 hover:bg-opacity-10"
          onClick={handleBack}
        >
          <MdKeyboardBackspace />
          <span>Back</span>
        </div>
        <div className="mx-auto text-center">
          <div className="relative mx-auto w-36 h-36">
            <ImageCustom
              src={"/images/mail-sent.png"}
              fill
              alt="image"
              priority
            />
          </div>
          <h1 className="text-2xl font-bold text-h1">Email Verification</h1>
          <div className="flex flex-col mt-4 ">
            <p className="text-sm font-medium text-gray-400">
              We have sent a code to your email
              <br />
              <strong className="font-semibold text-h1">{email}</strong>
            </p>
          </div>
          <div className="flex flex-row justify-center px-2 mt-5 text-center">
            <VerificationInput
              onChange={(value) => setOtp(value)}
              placeholder=""
              classNames={{
                character:
                  "text-3xl text-gray-600 bg-white flex items-center justify-center jus border border-gray-300 border-solid rounded"
              }}
            />
          </div>
          {countError === 5 && (
            <p className="mt-3 text-red-500">
              Please send code again{" "}
              <span
                className="underline cursor-pointer text-primary"
                onClick={handleReset}
              >
                Send
              </span>
            </p>
          )}
          <div className="flex flex-col items-center justify-center mt-6 gap-y-2">
            <CustomButton
              isLoading={isLoading}
              disabled={countError === 5}
              className="w-full "
              onClick={handleConfirmCode}
            >
              Verify OTP
            </CustomButton>
            <div className="flex flex-row items-center justify-center space-x-1 text-sm font-medium text-center text-gray-500">
              <p>Didn&apos;t recieve code?</p>{" "}
              <a
                onClick={handleReset}
                className="flex flex-row items-center text-blue-600 bg-opacity-0 border-none outline-none cursor-pointer"
              >
                Resend
              </a>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default ConfirmCode
