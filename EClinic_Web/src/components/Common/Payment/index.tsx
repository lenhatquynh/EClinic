import React from "react"
import ImageCustom from "../ImageCustom"
import { PAYMENT } from "shared/constant/constant"
import classNames from "classnames"
import { useTranslation } from "react-i18next"
interface Props {
  methodSelected?: string
  onChangeMethod: (method: string) => void
}

const Payment = ({ methodSelected, onChangeMethod }: Props) => {
  const { t } = useTranslation(["base"])

  return (
    <>
      <div className="flex flex-col gap-y-2">
        <h3 className="text-lg text-black1">{t("base:booking.total")}</h3>
        <ul className="flex items-center gap-4">
          {Object.values(PAYMENT).map((method, index) => (
            <li
              onClick={() => onChangeMethod(method.name)}
              className={classNames(
                "relative px-3 py-[6px] border border-solid border-carbon cursor-pointer rounded-md flex items-center justify-center transition-all",
                methodSelected === method.name && "border-primary"
              )}
              key={index}
            >
              {methodSelected === method.name && (
                <span className="absolute top-0 right-0 translate-x-2/4 -translate-y-2/4">
                  <svg
                    className="w-5 h-5"
                    viewBox="0 0 20 20"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M9.99935 18.3333C14.5827 18.3333 18.3327 14.5833 18.3327 9.99999C18.3327 5.41666 14.5827 1.66666 9.99935 1.66666C5.41602 1.66666 1.66602 5.41666 1.66602 9.99999C1.66602 14.5833 5.41602 18.3333 9.99935 18.3333Z"
                      fill="#235EE8"
                      stroke="#E9EFFD"
                      strokeWidth="1.5"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                    <path
                      d="M6.66602 9.85833L9.02435 12.2167L13.7493 7.5"
                      stroke="white"
                      strokeWidth={2}
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                </span>
              )}
              <div className="relative w-9 h-9">
                <ImageCustom
                  src={method.image}
                  fill
                  alt="momo"
                  className="object-cover"
                />
              </div>
            </li>
          ))}
        </ul>
      </div>
    </>
  )
}

export default Payment
