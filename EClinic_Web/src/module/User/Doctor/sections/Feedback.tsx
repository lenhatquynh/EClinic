import { Checkbox, FormControlLabel, Rating } from "@mui/material"
import ImageCustom from "components/Common/ImageCustom"
import TextAreaCustom from "components/Common/Textarea/TextAreaCustom"
import CustomButton from "components/User/Button"
import React from "react"

const Feedback = () => {
  return (
    <>
      <div className="flex flex-col">
        <h1 className="mb-3 text-xl font-semibold text-h1">
          Patients Feedback for Dr. Jacob Jones
        </h1>
        <span className="text-disable">129 results</span>
      </div>
      <div className="flex flex-col w-full gap-y-6">
        {Array(5)
          .fill(0)
          .map((_, index) => (
            <FeedbackItem key={index} />
          ))}
      </div>
      <CustomButton className="mx-auto w-fit">Load more</CustomButton>
      <CreateFeedBack />
    </>
  )
}
const FeedbackItem = () => {
  return (
    <div className="flex items-start w-full gap-x-6">
      <div className="relative flex-shrink-0 w-12 h-12">
        <ImageCustom
          src={"/images/avatars/avatar_1.jpg"}
          fill
          alt="avatar"
          className="object-cover rounded-full"
        />
      </div>
      <div className="flex flex-col gap-y-1">
        <div className="flex justify-between">
          <h3 className="text-base text-h1">Delowar Hussen</h3>
          <Rating readOnly size="small" value={3} />
        </div>
        <p className="text-sm text-disable">20/5/2022</p>
        <p>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Optio, iste!
          Eaque reprehenderit illo repudiandae ipsum nisi provident velit
          deleniti sint eligendi beatae labore error, cumque id vel vitae.
          Doloribus, explicabo!
        </p>
      </div>
    </div>
  )
}
const CreateFeedBack = () => {
  return (
    <div className="flex flex-col gap-y-3">
      <h2 className="text-lg font-semibold text-h1 ">
        Share your experience here
      </h2>
      <TextAreaCustom
        classCustom="max-w-full h-[185px]"
        className="resize-none"
        placeholder={"Write your experience here"}
      />
      <div className="bg-background rounded-[10px] py-6 px-5">
        <div className="flex items-center justify-between">
          <div className="flex flex-col gap-y-2">
            <p className="text-gray-500">How overall you rate</p>
            <span className="text-lg font-semibold text-h1">
              M. Hasan Nayeem
            </span>
          </div>
          <div className="flex flex-col gap-y-2">
            <Rating value={3} />
            <span className="text-base text-black2">Quiet Satisfy</span>
          </div>
        </div>
      </div>
      <FormControlLabel
        control={<Checkbox />}
        label="Keep this feedback publicly anonymous"
      />
      <CustomButton>Submit</CustomButton>
    </div>
  )
}

export default Feedback
