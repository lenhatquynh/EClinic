import Image from "next/image"
import Search from "./Search"
import { useTranslation } from "react-i18next"
const Banner = () => {
  const { t } = useTranslation("home")
  return (
    <section className="banner-wrapper ">
      <div className="relative page-container">
        <div className="grid grid-cols-2 pt-10">
          <div className="md:mt-20">
            <h1 className="font-bold text-4xl md:text-5xl lg:text-[70px] md:leading-tight text-h1">
              {t("banner.title")}
            </h1>
            <h3 className="text-xs leading-snug md:text-[20px] text-[#7B7E90] md:leading-tight  mt-4">
              {t("banner.description")}
            </h3>
          </div>
          <div
            className="relative flex-shrink-0 w-[230px] h-[230px] md:w-[400px]
md:h-[400px] lg:w-[600px] lg:h-[600px]"
          >
            <Image
              alt="banner"
              src="/images/banner.png"
              fill
              sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
            />
          </div>
        </div>
        <Search />
      </div>
    </section>
  )
}

export default Banner
