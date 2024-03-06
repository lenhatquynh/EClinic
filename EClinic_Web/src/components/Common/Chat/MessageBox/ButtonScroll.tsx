import { motion } from "framer-motion"

const dropIn = {
  hidden: {
    y: "20vh",
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
    y: "20vh",
    opacity: 0
  }
}

interface Props {
  onClick: () => void
}

const ButtonScroll = ({ onClick }: Props) => {
  return (
    <motion.button
      onClick={onClick}
      variants={dropIn}
      initial="hidden"
      animate="visible"
      exit="exit"
      className="absolute flex items-center justify-center w-8 h-8 bg-white border-none rounded-full shadow outline-none cursor-pointer bottom-3 right-2/4 translate-x-2/4 active:ring-4"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        strokeWidth={2}
        stroke="currentColor"
        className="w-4 h-4"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M19.5 13.5L12 21m0 0l-7.5-7.5M12 21V3"
        />
      </svg>
    </motion.button>
  )
}

export default ButtonScroll
