import classNames from "classnames"
import { PropsWithChildren } from "react"

interface Props extends PropsWithChildren {
  className?: string
  show: boolean
  onClose: () => void
}
const DrawerCustom = ({
  className,
  show = false,
  onClose,
  children
}: Props) => {
  return (
    <>
      {show && (
        <div
          className="fixed top-0 right-0 z-40 w-full h-full bg-black md:z-0 md:hidden md:relative md:w-0 md:h-0 bg-opacity-20"
          onClick={() => onClose()}
        ></div>
      )}
      <div
        className={classNames(
          "fixed z-50 md:z-0 md:relative left-0 top-0  md:h-auto h-full bg-white md:bg-transparent shadow-md md:shadow-none transition-all",
          show ? "" : "-translate-x-full md:translate-x-0",
          className
        )}
      >
        <span
          className="absolute top-0 right-0 p-2 cursor-pointer md:hidden"
          onClick={onClose}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>
        </span>
        {children}
      </div>
    </>
  )
}

export default DrawerCustom
