import CustomButton from "components/User/Button"
import Image from "next/image"
import React from "react"
import { useTranslation } from "react-i18next"
import { useRouter } from "next/router"

const Appoiment = () => {
  const { t } = useTranslation("home")
  const router = useRouter()
  return (
    <section className="flex flex-col-reverse md:flex-row items-center justify-between mt-28 md:mt-[180px]">
      <div className="w-[300px] h-[300px] md:w-[570px] md:h-[570px] relative flex-shrink-0">
        <Image
          alt="image-appoiment"
          src="/images/image-1.png"
          fill
          sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
        />
      </div>
      <div className="flex flex-col space-y-3 text-center md:space-y-7 max-w-[700px]">
        <h3 className="heading-section ">{t("appoiment.title")}</h3>
        <p className="desc-section">{t("appoiment.desc")}</p>
        <CustomButton
          kind="secondary"
          className="mx-auto md:mx-0max-w-[200px]"
          onClick={() => {
            router.push("/doctors")
          }}
        >
          {t("appoiment.btn")}
        </CustomButton>
      </div>
    </section>
  )
}

export default Appoiment
