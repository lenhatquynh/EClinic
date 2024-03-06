import ChipCustom from "components/Common/Chip/Chip"
import EmtyData from "components/Common/Empty"
import { useGetAnwerByPostId } from "hooks/query/forum/useForum"
import HTMLReactParser from "html-react-parser"
import Info from "module/User/components/Info/Info"
import React from "react"
import { useTranslation } from "react-i18next"
type Props = {
  postId: string
}
const AnswerQuestion = ({ postId }: Props) => {
  const { t } = useTranslation(["base", "forum"])
  const { data, isError, isLoading } = useGetAnwerByPostId(postId)
  if (isLoading) {
    return (
      <EmtyData
        message="Not answered"
        urLImage="/images/conversation-img.png"
      />
    )
  }
  if (isError) {
    return (
      <EmtyData
        message="Not answered"
        urLImage="/images/conversation-img.png"
      />
    )
  }
  return (
    <>
      <div className="flex flex-col space-y-2">
        <h4 className="text-lg">{t("forum:anwers")}</h4>
        <div className="flex flex-col gap-y-2">
          <Info data={data.data.author} />
          <article className="entry-content ">
            {HTMLReactParser(data.data.content || "")}
          </article>
        </div>
      </div>
      <div className="flex items-center w-full gap-2 mt-4">
        <h4 className="text-lg ">{t("forum:tags")}:</h4>
        <ul className="flex items-center w-full space-x-4 overflow-auto list-none">
          {data.data.hashTags.map((tag) => (
            <li key={tag.hashtagID}>
              <ChipCustom label={tag.hashtagName} />
            </li>
          ))}
        </ul>
      </div>
    </>
  )
}

export default AnswerQuestion
