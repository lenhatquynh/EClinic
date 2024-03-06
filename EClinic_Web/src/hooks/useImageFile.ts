import { ChangeEvent, useState } from "react"

export const useImageFile = (imageUrl: string | null) => {
  const [image, setImage] = useState(imageUrl)
  const [temporaryUrl, setTemporaryUrl] = useState<string | null>(null)

  const handleImageChange = (event: ChangeEvent<HTMLInputElement>) => {
    if (event.target.files !== null && event.target.files?.length > 0) {
      const objectUrl = URL.createObjectURL(event.target.files[0])
      setTemporaryUrl(objectUrl)
      setImage(objectUrl)
      return event.target.files[0]
    }
  }
  const handleRemoveImage = () => {
    if (temporaryUrl) {
      console.log("handleRemoveImage ~ temporaryUrl:", temporaryUrl)
      URL.revokeObjectURL(temporaryUrl)
      setTemporaryUrl(null)
      setImage(null)
    }
  }

  return { image, setImage, handleImageChange, handleRemoveImage }
}
