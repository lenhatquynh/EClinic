import ChangeLanguage from "components/Common/ChangeLanguage"
import CustomButton from "components/User/Button"
import DrawerCustom from "components/User/Drawer"
import Image from "next/image"
import Link from "next/link"
import { useTranslation } from "react-i18next"
import { useSelector } from "react-redux"
import { RootState } from "store/store"
import { MenuItem } from "../Menu"
import UserAvatar from "../UserAvatar/UserAvatar"
import ImageCustom from "components/Common/ImageCustom"
interface Props {
  show: boolean
  onClose: () => void
}
const Navbar = ({ show = false, onClose }: Props) => {
  const { t } = useTranslation(["home", "base"])

  const MENU = [
    {
      title: t("base:pages.profile"),
      href: "/user/my-profile"
    }
  ]
  const auth = useSelector((state: RootState) => state.auth)
  return (
    <DrawerCustom
      show={show}
      onClose={onClose}
      className="flex flex-col items-center space-y-5 md:flex-row md:space-y-0 md:justify-between w-[260px] md:w-full px-5 py-4 md:p-0"
    >
      <div className="flex flex-col items-center justify-between w-full space-y-5 md:space-x-8 md:space-y-0 md:flex-row md:justify-start">
        <Link href="/" className="relative scale-90 md:scale-100 w-[130px] h-9">
          <ImageCustom
            src={"/images/logo.png"}
            fill
            alt="Eclinic"
            className="cursor-pointer"
            priority
          />
        </Link>
        <ul className="flex flex-col items-center space-y-6 list-none md:flex-row md:space-y-0 md:space-x-10">
          <MenuItem href="/forum">{t("banner.menu.Portal")}</MenuItem>
          <MenuItem href="/services">{t("banner.menu.Services")}</MenuItem>
          <MenuItem href="/doctors">{t("banner.menu.Doctor")}</MenuItem>
          <MenuItem href="/blog">{t("banner.menu.Blog")}</MenuItem>
        </ul>
      </div>
      <div className="flex flex-col items-center justify-start w-full h-auto space-x-0 space-y-3 md:space-x-4 md:justify-start md:space-y-0 md:h-full md:flex-row md:w-auto">
        <ChangeLanguage />
        {auth.isLoggedIn ? (
          <>
            <UserAvatar menu={MENU} />
          </>
        ) : (
          <>
            <Link
              href="/sign-up"
              passHref
              className="w-full no-underline md:w-auto"
            >
              <CustomButton kind="secondary" className="w-full md:w-auto">
                {t("banner.button.signup")}
              </CustomButton>
            </Link>
            <Link
              href="/sign-in"
              passHref
              className="w-full no-underline md:w-auto"
            >
              <CustomButton kind="primary" className="w-full md:w-[120px]">
                {t("banner.button.signin")}
              </CustomButton>
            </Link>
          </>
        )}
      </div>
    </DrawerCustom>
  )
}

export default Navbar
