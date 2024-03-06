import { useSelector } from "react-redux"
import { Menu } from "../Menu"
import { RootState } from "store/store"

const Header = () => {
  const headerZIndex = useSelector((state: RootState) => state.header.zIndex)
  return (
    <header
      className={`fixed top-0 z-${headerZIndex} w-full h-16 md:h-[72px] bg-white border-b border-solid border-gray-200`}
    >
      <Menu />
    </header>
  )
}
export default Header
