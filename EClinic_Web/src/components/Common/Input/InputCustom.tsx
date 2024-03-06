import classNames from "classnames"
import { ChangeEvent, InputHTMLAttributes, ReactNode } from "react"

interface Props extends InputHTMLAttributes<HTMLInputElement> {
  icon?: ReactNode
  className?: string
}

const InputCustom = ({ icon, className, ...rest }: Props) => {
  return (
    <div
      className={classNames(
        "w-full px-2 py-2 flex items-center space-x-2 input-hover ",
        className
      )}
    >
      {icon && <span>{icon}</span>}
      <input
        type="text"
        className="w-full h-full text-sm bg-transparent border-none outline-none"
        {...rest}
      />
    </div>
  )
}

export default InputCustom
