import { Tab, Tabs } from "@mui/material"
import { useRouter } from "next/router"
import {
  PropsWithChildren,
  SyntheticEvent,
  useEffect,
  useMemo,
  useState
} from "react"
import { routers } from "shared/constant/routers"
import { logoutUser } from "store/module/auth/action-creators"
import { useAppDispatch } from "store/store"
import LayoutItem from "../../components/layout"
import { TabsWrapper } from "./Tabs.style"
import Head from "next/head"
import classNames from "classnames"
import { useTranslation } from "react-i18next"
const SiderBar = ({ children }: PropsWithChildren) => {
  const { t, i18n } = useTranslation(["base"])

  const [open, setOpen] = useState(false)
  const [tabIndex, setTabIndex] = useState(0)
  const [tabTitle, setTabTitle] = useState("")
  const dispatch = useAppDispatch()
  const router = useRouter()

  const handleTabChange = (_: SyntheticEvent<Element, Event>, value: any) => {
    const selectedTab = tabs.find((tab) => tab.key === value)
    if (selectedTab) {
      router.push(selectedTab.slug as string)
    }
    setTabIndex(value)
    setTabTitle(selectedTab?.label || "")
  }
  const logout = () => {
    dispatch(logoutUser())
    router.push(routers.signIn)
  }

  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: t("base:pages.profile"),
        slug: "/user/my-profile"
      },
      {
        key: 1,
        label: t("base:pages.changePassword"),
        slug: "/user/change-password"
      },
      {
        key: 2,
        label: t("base:pages.historyQuestion"),
        slug: "/user/history-question"
      },
      {
        key: 3,
        label: t("base:pages.comunication"),
        slug: "/user/chat",
        layout: false
      },
      {
        key: 4,
        label: t("base:pages.scheduleApp"),
        slug: "/user/app-schedule"
      },
      {
        key: 5,
        slug: "/login",
        label: t("base:pages.logout"),
        onclick: () => logout()
      }
    ],
    [i18n.language]
  )
  useEffect(() => {
    if (router.pathname) {
      const currentTab = tabs.find((item) => router.pathname === item.slug)
      setTabIndex(currentTab?.key as number)
      setTabTitle(currentTab?.label || "")
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [router.pathname])
  return (
    <>
      <Head>
        <title>{tabTitle}</title>
      </Head>
      <div
        className="fixed left-0 z-50 p-2 bg-gray-100 rounded-md cursor-pointer opacity-40 top-16 md:hidden"
        onClick={() => setOpen(!open)}
      >
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
            d="M3.75 6.75h16.5M3.75 12H12m-8.25 5.25h16.5"
          />
        </svg>
      </div>
      <TabsWrapper>
        <Tabs
          orientation="vertical"
          value={tabIndex}
          onChange={handleTabChange}
          className={classNames(
            open && "-translate-x-full",
            "h-fit tab-wrapper "
          )}
        >
          {tabs.map((tab) => (
            <Tab
              onClick={tab?.onclick}
              label={tab.label}
              key={tab.key}
              value={tab.key}
            />
          ))}
        </Tabs>
        <div className="flex-1 md:ml-6">
          {tabTitle === tabs[3].label ? (
            children
          ) : (
            <LayoutItem label={tabTitle}>{children}</LayoutItem>
          )}
        </div>
      </TabsWrapper>
    </>
  )
}

export default SiderBar
