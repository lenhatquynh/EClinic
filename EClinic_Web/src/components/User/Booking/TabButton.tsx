/* eslint-disable no-unused-vars */
import classNames from "classnames"
import { STATUS_BOOKING } from "shared/constant/constant"

interface ServicesBookingProps {
  setType: (type: number) => void
  type: number
}

const TabButton: React.FC<ServicesBookingProps> = ({ setType, type }) => {
  return (
    <div className="flex items-center mb-6 gap-x-3">
      {Object.keys(STATUS_BOOKING).map((item, index) => (
        <button
          key={index}
          className={classNames(
            "flex items-center justify-center px-4 py-[10px] outline-none border-none focus:ring-4 rounded-md cursor-pointer normal-case bg-opacity-10 transition-all",
            type === index + 1 && `ring-4 ring-opacity-60 `,
            index === STATUS_BOOKING.UPCOMING
              ? "text-success ring-success bg-success"
              : index === STATUS_BOOKING.DONE
              ? "text-error ring-error bg-error"
              : "text-pending ring-pending bg-pending "
          )}
          onClick={() => setType(index + 1)}
        >
          {item}
        </button>
      ))}
    </div>
  )
}

export default TabButton
