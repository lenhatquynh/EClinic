import styled from "@emotion/styled"
import classNames from "classnames"
import { AnimatePresence, motion } from "framer-motion"
import React from "react"
import { HiXMark } from "react-icons/hi2"
import Backdrop from "../Backdrop"
const ModalPrimaryWrapper = styled(motion.div)`
  .footer {
    padding: 20px 0;
    border-top: 0.5px solid #cccc;
    margin-top: 12px;
  }
`
const dropIn = {
  hidden: {
    y: "-100vh",
    opacity: 0
  },
  visible: {
    y: "0",
    opacity: 1,
    transition: {
      duration: 0.1,
      type: "spring",
      damping: 25,
      stiffness: 500
    }
  },
  exit: {
    y: "100vh",
    opacity: 0
  }
}
interface ModalProps {
  show: boolean
  onClose: () => void
  children: React.ReactNode
  closeButton?: boolean
}

const ModalPrimary = ({
  show,
  onClose,
  children,
  closeButton = true
}: ModalProps) => {
  return (
    <>
      <AnimatePresence initial={false} onExitComplete={() => null} mode="wait">
        {show && (
          <Backdrop>
            <ModalPrimaryWrapper
              variants={dropIn}
              initial="hidden"
              animate="visible"
              exit="exit"
            >
              <div
                className={classNames(
                  "relative bg-white w-fit mx-auto shadow-lg z-50  transition-opacity transition-scale duration-300 rounded-[20px]"
                )}
              >
                {closeButton && (
                  <div className="absolute top-0 right-0 translate-x-1/3 -translate-y-1/3 z-20 h-[46px] w-[46px] rounded-full p-1 bg-white cursor-pointer">
                    <button
                      onClick={onClose}
                      className="bg-[#44444F] w-full h-full flex items-center justify-center border-none outline-none rounded-full cursor-pointer hover:bg-opacity-90 transition-all active:scale-90"
                    >
                      <HiXMark className="text-lg text-white" />
                    </button>
                  </div>
                )}
                {children}
              </div>
            </ModalPrimaryWrapper>
          </Backdrop>
        )}
      </AnimatePresence>
    </>
  )
}

export default ModalPrimary
