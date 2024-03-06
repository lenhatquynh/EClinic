import classNames from "classnames"
import InputCustom from "components/Common/Input/InputCustom"
import Spinner from "components/Common/Loading/LoadingIcon"
import TextAreaCustom from "components/Common/Textarea/TextAreaCustom"
import CustomButton from "components/User/Button"
import {
  CreatePostForum,
  useCreatePostMutation
} from "hooks/query/forum/useForum"
import { ToastNavigate } from "module/User/components/Toast/ToastNavigate"
import { useRouter } from "next/router"
import { ChangeEvent, useState } from "react"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { useSelector } from "react-redux"
import { routers } from "shared/constant/routers"
import { RootState } from "store/store"
import { ImageItem } from "types/Base.type"
import UploadImage from "./UploadImage"
type Props = {
  className?: string
}
type KeyCreatePost = keyof CreatePostForum
const CreateQuestion = ({ className = "" }: Props) => {
  const { t } = useTranslation("forum")
  const router = useRouter()
  const auth = useSelector((state: RootState) => state.auth)
  const [images, setImages] = useState<ImageItem[]>([])
  const [post, setPost] = useState<CreatePostForum>({
    content: "",
    images: [],
    title: ""
  })
  const createPostMutation = useCreatePostMutation()
  const handleChangePost = (type: KeyCreatePost, data: any) => {
    setPost((prevPost) => ({
      ...prevPost,
      [type]: data
    }))
  }
  const handleFileImageChange = (event: ChangeEvent<HTMLInputElement>) => {
    if (images.length < 4) {
      if (event.target.files !== null && event.target.files?.length > 0) {
        const currentFile = event.target.files[0]
        const url = URL.createObjectURL(currentFile)
        setImages([
          ...images,
          {
            file: currentFile,
            key: images.length.toString(),
            url
          }
        ])
        const imagesList = [...images.map((item) => item.file), currentFile]
        setPost((prevState) => ({
          ...prevState,
          images: imagesList
        }))
      }
    } else {
      toast.error("Up to 4 images")
    }
  }
  const removeImage = (key: string) => {
    const newImages = images.filter((item) => item.key !== key)
    setImages(newImages)
    setPost((prevState) => ({
      ...prevState,
      images: newImages.map((item) => item.file)
    }))
  }
  const handleSubmit = () => {
    if (post && auth.user.userId) {
      if (post.title.trim() && post.content.trim()) {
        createPostMutation.mutate(post, {
          onError: () => {
            toast.error("Add error")
          },
          onSuccess: () => {
            setImages([])
            setPost({ content: "", images: [], title: "" })
            toast.success("Create post successfully!")
          }
        })
      } else {
        toast.error("Please enter your title and content")
      }
    } else {
      toast((t) => (
        <ToastNavigate
          url={routers.signIn}
          t={t}
          labelButton="Login"
          router={router}
          title="Please login to create question"
        />
      ))
    }
  }
  return (
    <div
      className={classNames(
        "flex flex-col justify-start space-y-4 background-primary",
        className
      )}
    >
      <h3 className="text-xl">{t("questionTitleHeading")}</h3>
      <InputCustom
        className="max-w-[412px]"
        value={post.title}
        placeholder={t("inputTitle")}
        onChange={(e) => handleChangePost("title", e.target.value)}
      />
      <TextAreaCustom
        value={post.content}
        classCustom="max-w-full h-[185px] "
        className="resize-none"
        placeholder={t("textareaDesc")}
        onChange={(e) => handleChangePost("content", e.target.value)}
      />
      <UploadImage
        images={images}
        removeImage={removeImage}
        onChange={(value) => handleFileImageChange(value)}
      />
      <p>
        {t("desctiptionUpload.imagetitle")}{" "}
        <span className="text-sm text-gray-400">
          ({t("desctiptionUpload.note")})
        </span>
      </p>
      <CustomButton
        disabled={createPostMutation.isLoading}
        kind="primary"
        className="md:max-w-[182px] rounded-[4px]"
        onClick={handleSubmit}
      >
        {createPostMutation.isLoading ? <Spinner /> : t("btnUpload")}
      </CustomButton>
    </div>
  )
}

export default CreateQuestion
