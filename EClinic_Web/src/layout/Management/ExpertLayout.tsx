import { useRouter } from "next/router"
import { PropsWithChildren, useMemo } from "react"
import { routers } from "shared/constant/routers"
import { logoutUser } from "store/module/auth/action-creators"
import { useAppDispatch } from "store/store"
import ManagmentLayout from "./ManagmentLayout"
import { ItemSidebar } from "./components/SideBar"

const ExpertLayout = ({ children }: PropsWithChildren) => {
  const dispatch = useAppDispatch()
  const router = useRouter()
  const logout = () => {
    router.push(routers.signIn).then(() => dispatch(logoutUser()))
  }
  const sidebars: ItemSidebar[] = useMemo(
    () => [
      {
        icon: (
          <>
            <svg
              width={18}
              height={20}
              viewBox="0 0 18 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M1.56836 1.66663H7.4476V7.91663H1.56836V1.66663Z"
                stroke="#5F666F"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M1.56836 12.0833H7.4476V18.3333H1.56836V12.0833Z"
                stroke="#5F666F"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M11.3671 1.66663H17.2463V7.91663H11.3671V1.66663Z"
                stroke="#5F666F"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M11.3671 12.0833H17.2463V18.3333H11.3671V12.0833Z"
                stroke="#5F666F"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </>
        ),
        title: "Overview",
        link: "/expert"
      },
      {
        link: "/expert/change-password",
        title: `Change password`,
        icon: (
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M15.75 5.25a3 3 0 013 3m3 0a6 6 0 01-7.029 5.912c-.563-.097-1.159.026-1.563.43L10.5 17.25H8.25v2.25H6v2.25H2.25v-2.818c0-.597.237-1.17.659-1.591l6.499-6.499c.404-.404.527-1 .43-1.563A6 6 0 1121.75 8.25z"
            />
          </svg>
        )
      },
      {
        title: `Logout`,
        icon: (
          <>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="w-6 h-6"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
              strokeWidth="2"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"
              />
            </svg>
          </>
        ),
        link: routers.signIn,
        onClick: () => logout()
      }
    ],
    []
  )
  return <ManagmentLayout sidebars={sidebars}>{children}</ManagmentLayout>
}

export default ExpertLayout
