import classNames from "classnames"
import { useImageFile } from "hooks/useImageFile"
import Image from "next/image"
import {
  ChangeEvent,
  DetailedHTMLProps,
  InputHTMLAttributes,
  useEffect,
  useState
} from "react"
import { isImage } from "shared/helpers/helper"
import ImageCustom from "../ImageCustom"

interface UpdateCoverProps
  extends DetailedHTMLProps<
    InputHTMLAttributes<HTMLInputElement>,
    HTMLInputElement
  > {
  imageUrl: string | null
  // eslint-disable-next-line no-unused-vars
  onFileChange: (file: File | null) => void
  isError?: boolean
  isDelete?: boolean
}

export const UpdateCover = ({
  imageUrl = null,
  onFileChange,
  isError = false,
  isDelete = true,
  ...props
}: UpdateCoverProps) => {
  const { image, handleImageChange, setImage, handleRemoveImage } =
    useImageFile(imageUrl)

  const [error, setError] = useState<string | null>(null)

  const onImageChange = (e: ChangeEvent<HTMLInputElement>) => {
    setError(null)
    const files = e.target.files
    if (!files || files.length === 0) {
      // No files selected
      return
    }

    const fileCheck = files[0]
    if (isImage(fileCheck!)) {
      const file = handleImageChange(e)
      if (isImage(file!)) {
        onFileChange(file!)
      } else {
        setError("File is not an image type")
      }
    } else {
      setError("File is not an image type")
    }
  }

  useEffect(() => {
    if (typeof imageUrl === "string" || !imageUrl) {
      setImage(imageUrl)
    }
  }, [imageUrl])
  const clearImage = () => {
    setError(null)
    handleRemoveImage()
  }
  return (
    <div className="relative flex flex-col justify-center w-full gap-y-2">
      {image && isDelete && (
        <div
          className="absolute top-0 right-0 z-20 flex items-center justify-center w-6 h-6 text-white transition-all bg-black rounded-full cursor-pointer -translate-x-2/4 translate-y-2/4 bg-opacity-40 hover:bg-opacity-30 hover:scale-105"
          onClick={(e) => {
            e.stopPropagation()
            clearImage()
            onFileChange(null)
          }}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="currentColor"
            className="w-4 h-4"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M6 18L18 6M6 6l12 12"
            />
          </svg>
        </div>
      )}
      <label
        className={classNames(
          "relative flex flex-col items-center justify-center w-full h-72 p-3 transition-all border border-gray-400 border-dashed cursor-pointer bg-gray-50 rounded-xl hover:bg-gray-100 hover:border-gray-300",
          (isError || error) && "border-red-500 hover:border-red-500" // Add border color when there is an error
        )}
      >
        {image ? (
          <ImageCustom
            src={image}
            priority
            fill
            alt="avatar"
            className="object-contain rounded-xl"
          />
        ) : (
          <div className="flex flex-col items-center gap-x-3 md:flex-row">
            <div className="relative w-[200px] h-[170px]">
              <Image
                src={"/images/upload.svg"}
                fill
                alt="upload"
                className="object-cover "
              />
            </div>
            <div className="flex flex-col space-y-1">
              <h3 className="pt-1 text-2xl font-semibold tracking-wider text-h1">
                Drop or Select Image
              </h3>
              <p className="text-xs text-disable">
                Drop image here or click browse thorough your machine
              </p>
            </div>
          </div>
        )}

        <input
          {...props}
          type="file"
          className="opacity-0"
          onChange={(e) => onImageChange(e)}
        />
      </label>
      {isError && (
        <span className="text-xs text-red-500">Image is required.</span>
      )}
      {error && <span className="text-xs text-red-500 ">{error}</span>}
    </div>
  )
}
