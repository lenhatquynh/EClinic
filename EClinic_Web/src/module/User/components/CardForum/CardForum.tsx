import { Skeleton } from "@mui/material"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import { useGetAnwerByPostId } from "hooks/query/forum/useForum"
import Image from "next/image"
import Link from "next/link"
import React from "react"
import { useTranslation } from "react-i18next"
import { combineName } from "shared/helpers/helper"
import { IPost } from "types/Post"
interface Props {
  post: IPost
  kind?: "large" | "medium"
}
const CardForum = ({ kind = "medium", post }: Props) => {
  const { t } = useTranslation("forum")
  const anwerPost = useGetAnwerByPostId(post.id)
  return (
    <Link
      href={`/forum/${post.id}`}
      className={classNames(
        "grid w-full rounded-md overflow-hidden",
        kind === "large" && "md:grid-cols-8 md:gap-x-4",
        kind === "medium" && "grid-cols-1 gap-y-4 "
      )}
    >
      <div
        className={classNames(
          "relative  rounded-md overflow-hidden",
          kind === "large" && "col-span-5 h-[200px] md:h-[245px]",
          kind === "medium" && "col-span-5 h-[140px] md:h-[200px]"
        )}
      >
        <ImageCustom
          src={post.image[0]}
          fill
          alt="image"
          classNameImage="object-cover"
        />
      </div>
      <div className={classNames(kind === "large" && "space-y-4 col-span-3")}>
        <h4 className="text-[22px] text-[#304050] font-semibold line-clamp-2">
          {post.title}
        </h4>
        <div className="flex items-center space-x-3">
          <div className="relative h-14 md:w-11 w-14 md:h-11">
            {anwerPost.data && (
              <Image
                src={anwerPost.data.data.author.avatar}
                fill
                sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
                alt="image"
                className="object-cover rounded-full"
              />
            )}
            {anwerPost.isLoading && (
              <Skeleton variant="circular" className="w-full h-full " />
            )}
          </div>
          <div className="flex flex-col text-[#9A9FA5] text-sm md:text-[10px] font-medium">
            <span className="leading-snug">{t("card.by")} : </span>
            <strong className="text-xs font-semibold text-black">
              {anwerPost.data &&
                combineName(
                  anwerPost.data?.data.author.firstName,
                  anwerPost.data?.data.author.lastName
                )}
            </strong>
            <div className="flex items-center gap-1">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="#ef4444"
                className="w-4 h-4 stroke-red-500"
              >
                <path d="M11.645 20.91l-.007-.003-.022-.012a15.247 15.247 0 01-.383-.218 25.18 25.18 0 01-4.244-3.17C4.688 15.36 2.25 12.174 2.25 8.25 2.25 5.322 4.714 3 7.688 3A5.5 5.5 0 0112 5.052 5.5 5.5 0 0116.313 3c2.973 0 5.437 2.322 5.437 5.25 0 3.925-2.438 7.111-4.739 9.256a25.175 25.175 0 01-4.244 3.17 15.247 15.247 0 01-.383.219l-.022.012-.007.004-.003.001a.752.752 0 01-.704 0l-.003-.001z" />
              </svg>
              <span className="text-xs font-semibold">{post.likes} </span>
              <span>Cảm ơn</span>
            </div>
          </div>
        </div>
      </div>
    </Link>
  )
}

export default CardForum
