import ImageCustom from "components/Common/ImageCustom"

export const CommentAvatar = ({ avatar = "" }) => {
  return (
    <div className="relative w-8 h-8 mr-2 ">
      <ImageCustom
        src={avatar || "/images/sample.png"}
        fill
        alt="user-avatar"
        className="object-cover rounded-full"
      />
    </div>
  )
}
