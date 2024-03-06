import CustomButton from "components/User/Button"
import CardForum from "module/User/components/CardForum"
import { useTranslation } from "react-i18next"

const Forum = () => {
  const { t } = useTranslation("home")

  return (
    <section className="flex flex-col w-full ">
      <h3 className="mb-3 text-center heading-section ">{t("forum.title")}</h3>
      <div className="grid grid-cols-1 gap-y-4 md:gap-x-6 md:grid-cols-3">
        {/* <CardForum />
        <CardForum />
        <CardForum /> */}
      </div>
      <div className="flex items-center mt-6 space-x-2">
        <CustomButton kind="primary" className="max-w-[200px] ">
          {t("forum.btn1")}
        </CustomButton>
        <CustomButton kind="tertiary" className="max-w-[200px] space-x-2 ">
          <span>{t("forum.btn2")}</span>
          <span>
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
                d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3"
              />
            </svg>
          </span>
        </CustomButton>
      </div>
    </section>
  )
}

export default Forum
