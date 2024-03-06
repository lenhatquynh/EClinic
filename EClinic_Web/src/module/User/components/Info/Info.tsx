import { Skeleton } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import { useTranslation } from "react-i18next"
import { Author } from "types/Post"
type Props = {
  data?: Author
  loading?: boolean
}
const Info = ({ data, loading = false }: Props) => {
  const { t } = useTranslation("forum")

  return (
    <div className="flex items-center space-x-3">
      <div className="relative h-14 md:w-11 w-14 md:h-11">
        {data && (
          <ImageCustom
            src={data.avatar || "/images/sample.png"}
            fill
            alt="image"
            className="object-cover rounded-full"
          />
        )}
        {loading && <Skeleton variant="circular" className="w-full h-full" />}
      </div>
      <div className="flex flex-col text-[#9A9FA5] text-sm md:text-[10px] font-medium">
        {loading && <Skeleton variant="text" width={30} />}
        {data && <span>{t("card.by")}</span>}
        <span className="text-black">
          {loading && <Skeleton variant="text" width={50} />}
          {data && <>BS. {data.firstName + " " + data.lastName}</>}
        </span>
      </div>
    </div>
  )
}

export default Info
