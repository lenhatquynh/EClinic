import DrawerCustom from "components/User/Drawer"
import { PropsWithChildren } from "react"
interface Props extends PropsWithChildren {
  show: boolean
  onClose: () => void
}
const FilterBar = ({ show, onClose, children }: Props) => {
  return (
    <DrawerCustom
      show={show}
      onClose={onClose}
      className="md:max-w-[200px] lg:max-w-[290px] max-w-full px-4 md:px-0"
    >
      <div className="flex flex-col justify-start mt-10 md:mt-0 gap-y-6">
        {children}
      </div>
    </DrawerCustom>
  )
}

export default FilterBar
