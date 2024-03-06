import classNames from "classnames"
import Image from "next/image"
import { useRouter } from "next/router"
import React from "react"
import { ServicePackage } from "types/Service"

interface Props {
  className?: string
  servicePackage?: ServicePackage
}
const CardService = ({ className, servicePackage }: Props) => {
  const router = useRouter()
  return (
    <div
      className={classNames(
        className,
        "h-[290px] w-full p-3 flex justify-between flex-col space-y-4 bg-white rounded-2xl border border-solid border-[#E7ECF3]"
      )}
      onClick={() =>
        router.push(`/services/${servicePackage?.servicePackageID}`)
      }
    >
      <div>
        <div className="w-full h-[152px] relative">
          <Image
            alt="image-Appointment"
            src={servicePackage?.image || "/images/sample.png"}
            fill
            sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
            className="object-cover rounded-2xl"
          />
        </div>
        <h4 className="font-semibold text-[#3B3E44] text-[19px] mt-1">
          {servicePackage?.servicePackageName}
        </h4>
      </div>
      <div>
        <div className="flex justify-between w-full mb-2">
          <div className="flex items-center">
            <svg
              className="mr-1"
              width={17}
              height={16}
              viewBox="0 0 17 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M7.44285 2.11455C7.94295 1.61445 8.62123 1.3335 9.32847 1.3335H13.1667C14.2713 1.3335 15.1667 2.22893 15.1667 3.3335V7.17174C15.1667 7.87898 14.8858 8.55726 14.3857 9.05735L9.24759 14.1954C8.46654 14.9765 7.20021 14.9765 6.41916 14.1954L2.30478 10.081C1.52373 9.29999 1.52373 8.03366 2.30478 7.25262L7.44285 2.11455ZM9.32847 2.66683C8.97485 2.66683 8.63571 2.80731 8.38566 3.05735L3.24759 8.19543C2.98724 8.45578 2.98724 8.87788 3.24759 9.13823L7.36197 13.2526C7.62232 13.513 8.04443 13.513 8.30478 13.2526L13.4429 8.11454C13.6929 7.8645 13.8334 7.52536 13.8334 7.17174V3.3335C13.8334 2.96531 13.5349 2.66683 13.1667 2.66683H9.32847Z"
                fill="#F4BF59"
              />
              <path
                d="M2.89062 6.66644L3.83343 5.72363L10.7762 12.6664L9.83343 13.6093L2.89062 6.66644Z"
                fill="#F4BF59"
              />
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M10.5 6.66667C10.8682 6.66667 11.1667 6.36819 11.1667 6C11.1667 5.63181 10.8682 5.33333 10.5 5.33333C10.1318 5.33333 9.83333 5.63181 9.83333 6C9.83333 6.36819 10.1318 6.66667 10.5 6.66667ZM10.5 8C11.6046 8 12.5 7.10457 12.5 6C12.5 4.89543 11.6046 4 10.5 4C9.39543 4 8.5 4.89543 8.5 6C8.5 7.10457 9.39543 8 10.5 8Z"
                fill="#F4BF59"
              />
            </svg>
            <span>
              {" "}
              {(servicePackage?.price || 0) *
                (1 - (servicePackage?.discount || 1) / 100)}{" "}
              VND
            </span>
          </div>

          <div className="flex items-center line-through">
            <svg
              className="mr-1"
              width={17}
              height={16}
              viewBox="0 0 17 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M7.44285 2.11455C7.94295 1.61445 8.62123 1.3335 9.32847 1.3335H13.1667C14.2713 1.3335 15.1667 2.22893 15.1667 3.3335V7.17174C15.1667 7.87898 14.8858 8.55726 14.3857 9.05735L9.24759 14.1954C8.46654 14.9765 7.20021 14.9765 6.41916 14.1954L2.30478 10.081C1.52373 9.29999 1.52373 8.03366 2.30478 7.25262L7.44285 2.11455ZM9.32847 2.66683C8.97485 2.66683 8.63571 2.80731 8.38566 3.05735L3.24759 8.19543C2.98724 8.45578 2.98724 8.87788 3.24759 9.13823L7.36197 13.2526C7.62232 13.513 8.04443 13.513 8.30478 13.2526L13.4429 8.11454C13.6929 7.8645 13.8334 7.52536 13.8334 7.17174V3.3335C13.8334 2.96531 13.5349 2.66683 13.1667 2.66683H9.32847Z"
                fill="#F4BF59"
              />
              <path
                d="M2.89062 6.66644L3.83343 5.72363L10.7762 12.6664L9.83343 13.6093L2.89062 6.66644Z"
                fill="#F4BF59"
              />
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M10.5 6.66667C10.8682 6.66667 11.1667 6.36819 11.1667 6C11.1667 5.63181 10.8682 5.33333 10.5 5.33333C10.1318 5.33333 9.83333 5.63181 9.83333 6C9.83333 6.36819 10.1318 6.66667 10.5 6.66667ZM10.5 8C11.6046 8 12.5 7.10457 12.5 6C12.5 4.89543 11.6046 4 10.5 4C9.39543 4 8.5 4.89543 8.5 6C8.5 7.10457 9.39543 8 10.5 8Z"
                fill="#F4BF59"
              />
            </svg>
            <span className="opacity-40"> {servicePackage?.price} VND</span>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CardService
