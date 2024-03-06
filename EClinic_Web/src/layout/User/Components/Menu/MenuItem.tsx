import classNames from "classnames"
import Link from "next/link"
import { useRouter } from "next/router"
import React, { ReactNode } from "react"
interface Props {
  children: ReactNode
  href: string
}

const MenuItem = ({ children, href }: Props) => {
  const { asPath } = useRouter()

  return (
    <Link href={href} passHref className="text-black">
      <p className="relative group">
        <span
          className={classNames(
            "md:text-sm lg:text-base group-hover:text-primary",
            asPath === href ? "text-primary" : ""
          )}
        >
          {children}
        </span>
        <span
          className={classNames(
            "absolute left-0 h-[3px] transition-all duration-300 bg-primary -bottom-1 group-hover:w-full",
            asPath === href ? "w-full" : "w-0"
          )}
        ></span>
      </p>
    </Link>
  )
}

export default MenuItem
