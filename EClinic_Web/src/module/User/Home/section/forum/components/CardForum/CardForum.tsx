import Image from "next/image"
import React from "react"

const CardForum = () => {
  return (
    <div className="flex flex-col w-full space-y-3">
      <div className="relative w-full h-[200px]">
        <Image
          src={"/images/sample.png"}
          fill
          sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
          alt="image"
          className="object-cover"
        />
      </div>
      <h4 className="text-[22px] text-[#304050] font-semibold line-clamp-2">
        Chụp CT có ảnh hưởng đến liền sẹo hay không?
      </h4>
      <div className="flex items-center space-x-3">
        <div className="relative h-14 md:w-11 w-14 md:h-11">
          <Image
            src={"/images/sample.png"}
            fill
            sizes="(max-width: 768px) 100vw,
              (max-width: 1200px) 50vw,
              33vw"
            alt="image"
            className="object-cover rounded-full"
          />
        </div>
        <div className="flex flex-col space-y-1 text-[#9A9FA5] text-sm md:text-[10px] font-medium">
          <span>Được trả lời bởi</span>
          <span className="text-black">BS. Nguyễn Thanh Bình</span>
          <span>9 năm kinh nghiệm</span>
        </div>
      </div>
    </div>
  )
}

export default CardForum
