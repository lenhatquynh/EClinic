import classNames from "classnames"
import React, { PropsWithChildren } from "react"
interface Props
  extends React.DetailedHTMLProps<
      React.LabelHTMLAttributes<HTMLLabelElement>,
      HTMLLabelElement
    >,
    PropsWithChildren {}

const Label = ({ ...props }: Props) => {
  return (
    <label
      className={classNames(
        "text-sm font-normal text-gray-600 cursor-pointer",
        props.className
      )}
      htmlFor={props.htmlFor}
      {...props}
    >
      {props.children}
    </label>
  )
}

export default Label
