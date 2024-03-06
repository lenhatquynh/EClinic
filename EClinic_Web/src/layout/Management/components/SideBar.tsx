import styled from "@emotion/styled"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import Link from "next/link"
import { useRouter } from "next/router"
import { ReactNode, useState } from "react"
import colorsProvider from "shared/theme/colors"
export type ItemSidebar = {
  icon?: ReactNode
  title?: string
  link?: string
  onClick?: () => void
  subItem?: ItemSidebar[]
}
export type Props = {
  items: ItemSidebar[]
}
const StyleLink = styled(Link)`
  position: relative;
  ~ .expand::before {
    content: "";
    position: absolute;
    height: 16px;
    width: 4px;
    left: 0px;
    top: 50%;
    transform: translateY(-50%);
    background-color: ${colorsProvider.primary};
    border-radius: 0px 1.998px 1.998px 0px;
  }
`

const SideBar = ({ items }: Props) => {
  const { asPath } = useRouter()
  const [expandITem, setExpandITem] = useState<null | number>(null)
  const classItem =
    "flex items-center text-xs gap-x-4 px-4 py-2 font-medium mb-1 text-gray80 cursor-pointer"
  const classActive =
    "bg-primary bg-opacity-[0.08] rounded-xl !text-primary expand"
  const classHover =
    "hover:bg-primary hover:bg-opacity-10 hover:text-primary transition-all rounded-[4px]"
  return (
    <nav className="flex flex-col items-center py-6 w-[220px] flex-shrink-0 bg-white border border-dashed border-gray-200 border-y-0 border-l-0 border-r px-3 rounded-none ">
      <Link
        href="/"
        className="relative scale-90 md:scale-100 w-[150px] h-9 mb-11 "
      >
        <ImageCustom
          priority
          src={"/images/logo.png"}
          fill
          alt="Eclinic"
          className="object-contain cursor-pointer"
        />
      </Link>
      <div className="flex flex-col w-full">
        {items.map((item, index) => {
          if (item.subItem) {
            return (
              <div
                key={index}
                className={classNames(classItem, "flex flex-col")}
                onClick={() =>
                  setExpandITem(index === expandITem ? null : index)
                }
              >
                <div className="flex items-center justify-between w-full">
                  <div className="flex items-center gap-x-5">
                    <span className="w-6 h-6">{item.icon}</span>
                    <span>{item.title}</span>
                  </div>
                  <span
                    className={classNames(
                      "w-5 h-5 transition-all",
                      index === expandITem && "rotate-90 "
                    )}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth={1.5}
                      stroke="currentColor"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M8.25 4.5l7.5 7.5-7.5 7.5"
                      />
                    </svg>
                  </span>
                </div>
                {index === expandITem && (
                  <ul className="flex flex-col w-full py-3 mt-2 list-disc rounded-sm">
                    {item.subItem.map((subItem) => {
                      return (
                        <StyleLink
                          href={subItem.link || asPath}
                          key={subItem.link}
                          className={classNames(
                            classHover,
                            classItem,
                            asPath === subItem.link && classActive,
                            "pl-4"
                          )}
                        >
                          <li
                            className="pl-6"
                            onClick={() => setExpandITem(index)}
                          >
                            {subItem?.title}
                          </li>
                        </StyleLink>
                      )
                    })}
                  </ul>
                )}
              </div>
            )
          } else {
            return (
              <StyleLink
                href={item.link ? item.link : asPath}
                key={index}
                className={classNames(
                  classItem,
                  classHover,
                  asPath === item.link && classActive
                )}
                onClick={item.onClick}
              >
                <span className="w-6 h-6">{item.icon}</span>
                <span>{item.title}</span>
              </StyleLink>
            )
          }
        })}
      </div>
    </nav>
  )
}

export default SideBar
