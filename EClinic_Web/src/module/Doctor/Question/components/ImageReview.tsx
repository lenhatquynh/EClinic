import { Box, Modal } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import React, { useState } from "react"
import { BiShowAlt } from "react-icons/bi"
type Props = {
  images: string[]
}
const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 800,
  height: 600,
  boxShadow: 24
}
const ImageReview = ({ images }: Props) => {
  const [open, setOpen] = React.useState(false)
  const [imageReview, setImageReview] = useState("")
  const handleOpen = (image: string) => {
    setOpen(true)
    setImageReview(image)
  }
  const handleClose = () => setOpen(false)
  return (
    <>
      <div className="flex items-center gap-3">
        {images.map((item, index) => (
          <div
            className="relative w-[160px] h-[120px] rounded-md overflow-hidden "
            key={index}
          >
            <div className="absolute top-0 right-0 z-10 flex items-center justify-center w-full h-full transition-all bg-black opacity-0 bg-opacity-40 hover:opacity-100">
              <div
                className="flex items-center cursor-pointer gap-x-2"
                onClick={() => handleOpen(item)}
              >
                <BiShowAlt className="text-2xl opacity-80 fill-white" />
                <span className="text-xs text-white">Preview Image</span>
              </div>
            </div>
            <ImageCustom src={item} fill alt="post" className="object-cover" />
          </div>
        ))}
      </div>
      <Modal open={open} onClose={handleClose}>
        <Box sx={style}>
          <ImageCustom src={imageReview} fill alt="image-review" />
        </Box>
      </Modal>
    </>
  )
}

export default ImageReview
