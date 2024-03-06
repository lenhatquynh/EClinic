import Image from "next/image"
import Link from "next/link"
import { useState } from "react"
import Navbar from "../Nav"

const Menu = () => {
  const [show, setShow] = useState(false)
  const handlerNav = () => {
    setShow(!show)
  }
  return (
    <>
      <div className="flex items-center justify-between px-5 lg:max-w-[1440px]  w-full h-full mx-auto">
        <Link
          href="/"
          className="relative scale-90 md:scale-100 w-[150px] h-9 md:hidden "
        >
          <Image
            src={"/images/logo.png"}
            fill
            sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
            priority
            alt="Eclinic"
            className="object-contain cursor-pointer"
          />
        </Link>
        <Navbar show={show} onClose={handlerNav} />
        {/* Humberger */}
        <span className="cursor-pointer md:hidden" onClick={() => handlerNav()}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25H12"
            />
          </svg>
        </span>
      </div>
    </>
  )
}

export default Menu
