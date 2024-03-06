import { OverlayScrollbarsComponent } from "overlayscrollbars-react"
import React from "react"
import ImageCustom from "../ImageCustom"
import ModalPrimary from "./ModalPrimary"

interface ModalSuccessProps {
  isSuccess: boolean
  setIsSuccess: (value: boolean) => void
  children: React.ReactNode
  imageUrl?: string
}

const ModalSuccess: React.FC<ModalSuccessProps> = ({
  isSuccess,
  setIsSuccess,
  children,
  imageUrl = "/images/success-image.png"
}) => {
  return (
    <ModalPrimary show={isSuccess} onClose={() => setIsSuccess(false)}>
      <OverlayScrollbarsComponent
        defer
        options={{ scrollbars: { autoHide: "scroll" } }}
      >
        <div className="flex flex-col  max-w-[560px] w-full p-6">
          <div className="relative w-full h-[260px]">
            <ImageCustom
              priority
              src={imageUrl}
              alt="img-success"
              fill
              className="object-cover"
            />
          </div>
          {children}
        </div>
      </OverlayScrollbarsComponent>
    </ModalPrimary>
  )
}

export default ModalSuccess
