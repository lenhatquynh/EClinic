import dynamic from "next/dynamic"
import { Footer } from "./Components"
import { useSelector } from "react-redux"
import { RootState } from "store/store"
import { useEffect } from "react"
import { useRouter } from "next/router"
import { ROLE } from "shared/constant/constant"
import { DEFAULT_ROUTER } from "shared/constant/constant"
const Header = dynamic(
  () => import("./Components").then((module) => module.Header),
  {
    ssr: false
  }
)
export interface IUserLayout extends React.ComponentPropsWithoutRef<"div"> {
  justify?: "items-center" | "items-start"
  footer?: boolean
}
const pathDefaultByRole = (role: string) =>
  role === ROLE.ADMIN
    ? DEFAULT_ROUTER.ADMIN
    : role === ROLE.DOCTOR
    ? DEFAULT_ROUTER.DOCTOR
    : role === ROLE.EXPERT
    ? DEFAULT_ROUTER.EXPERT
    : DEFAULT_ROUTER.SUPPORTER
const UserLayout: React.FC<IUserLayout> = ({
  children,
  footer = true,
  justify = "items-center",
  ...divProps
}) => {
  const router = useRouter()
  const user = useSelector((state: RootState) => state.auth)

  useEffect(() => {
    if (user) {
      if (user.user.role !== "User" && user.user.role) {
        console.log(pathDefaultByRole(user.user.role))

        router.push(pathDefaultByRole(user.user.role))
      }
    }
  }, [user.user])
  return (
    <>
      <div
        {...divProps}
        className={`min-h-screen flex flex-col ${justify} relative`}
      >
        <Header />
        <div className="flex flex-col flex-1 w-full">{children}</div>
        {footer && <Footer />}
      </div>
    </>
  )
}

export default UserLayout
