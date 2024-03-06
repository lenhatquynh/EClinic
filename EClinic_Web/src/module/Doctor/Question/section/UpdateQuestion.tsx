// @ts-nocheck
import {
  Dialog,
  DialogActions,
  DialogContent,
  IconButton,
  Tooltip
} from "@mui/material"

import { useQueryClient } from "@tanstack/react-query"
import Editor from "components/Common/Editor/Editor"
import Field from "components/Common/Field/Field"
import ImageCustom from "components/Common/ImageCustom"
import InputCustom from "components/Common/Input"
import Label from "components/Common/Label/Label"
import Spinner from "components/Common/Loading/LoadingIcon"
import CustomButton from "components/User/Button"
import {
  CreateAnwserPost,
  useCreateAwnserPostForumMutation,
  useCreatetagMutation
} from "hooks/query/forum/useForum"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { AiOutlineQuestionCircle } from "react-icons/ai"
import { HiPencilSquare } from "react-icons/hi2"
import { QUERY_KEYS } from "shared/constant/constant"
import { combineName, dayformat, isQuillEmpty } from "shared/helpers/helper"
import { IPost } from "types/Post"
import ImageReview from "../components/ImageReview"
import MultipleSelectChip from "../components/SelectMutitple"
import { OverlayScrollbarsComponent } from "overlayscrollbars-react"
type Props = {
  post: IPost
}
const UpdateQuestion = ({ post }: Props) => {
  const queryClient = useQueryClient()
  const [anwerData, setAnerData] = useState<CreateAnwserPost>({
    postId: post.id,
    content: "",
    tags: []
  })
  const [open, setOpen] = useState(false)
  const [tagValue, setTagValue] = useState("")
  const createAwnserPostForumMutation = useCreateAwnserPostForumMutation()
  const createtagMutation = useCreatetagMutation()
  const handleClose = () => {
    setOpen(!open)
  }
  const handleChangeAnwerData = (key: keyof CreateAnwserPost, value) => {
    setAnerData((prevState) => ({
      ...prevState,
      [key]: value
    }))
  }

  const handleSubmit = () => {
    if (!isQuillEmpty(anwerData.content) && anwerData.tags.length > 0) {
      createAwnserPostForumMutation.mutate(anwerData, {
        onSuccess: () => {
          queryClient.invalidateQueries([QUERY_KEYS.FORUM.POST])
          toast.success("Create answer successfully")
          setOpen(!open)
        },
        onError: () => {
          toast.error("Create answer failed")
        }
      })
    } else {
      toast.error("Please fill out the information completely")
    }
  }
  const createTag = () => {
    createtagMutation.mutate(tagValue, {
      onSuccess: () => {
        queryClient.invalidateQueries([QUERY_KEYS.HASHTAG])
        setTagValue("")
      },
      onError: (error) => {
        toast.error(error.response.data.message || "Create tag failed")
      }
    })
  }
  return (
    <>
      <Tooltip title="Create your anwers">
        <IconButton variant="contained" onClick={() => setOpen(true)}>
          <HiPencilSquare />
        </IconButton>
      </Tooltip>

      <Dialog
        open={open}
        onClose={handleClose}
        fullWidth
        maxWidth="lg"
        scroll="paper"
      >
        <OverlayScrollbarsComponent
          defer
          options={{ scrollbars: { autoHide: "scroll" } }}
        >
          <DialogContent>
            <Field>
              <h3 className="text-2xl font-semibold">{post.title}</h3>
              <time className="text-xs text-gray-400">
                {dayformat(post.createdAt)}
              </time>
            </Field>
            <Field>
              <div className="flex gap-x-2">
                <div className="relative flex-shrink-0 w-6 h-6">
                  <ImageCustom
                    src={post.author.avatar}
                    fill
                    className="object-cover rounded-sm"
                  />
                </div>
                <span>
                  {combineName(post.author.firstName, post.author.lastName)}
                </span>
              </div>
              <h3 className="text-sm font-normal leading-relaxed text-gray-500 ">
                {post.content}
              </h3>
            </Field>
            <Field>
              <Label>Picture of disease symptoms</Label>
              <ImageReview images={post.image} />
            </Field>
            <Field>
              <Label htmlFor="content">
                <div className="flex items-center space-x-1">
                  <span> Fill your answer here</span>
                  <span>
                    <Tooltip
                      title="The answer must be clear and precise with specific evidence"
                      placement="top"
                    >
                      <IconButton size="small">
                        <AiOutlineQuestionCircle />
                      </IconButton>
                    </Tooltip>
                  </span>
                </div>
              </Label>
              <div className="w-full">
                <Editor
                  value={anwerData.content}
                  onChange={(value) => handleChangeAnwerData("content", value)}
                />
              </div>
            </Field>
            <Field>
              <Label htmlFor="title">
                <div className="flex items-center space-x-1">
                  <span> Tags for post </span>
                  <span>
                    <Tooltip title="Choose hashtag of the post" placement="top">
                      <IconButton size="small">
                        <AiOutlineQuestionCircle />
                      </IconButton>
                    </Tooltip>
                  </span>
                </div>
              </Label>
              <div className="flex mb-3 gap-x-1">
                <InputCustom
                  value={tagValue}
                  className="w-full md:max-w-[412px]"
                  placeholder="Create your tag"
                  onChange={(e) => setTagValue(e.target.value)}
                />
                <CustomButton
                  kind="primary"
                  className="!rounded-[5px]"
                  disabled={createtagMutation.isLoading}
                  onClick={createTag}
                >
                  {createtagMutation.isLoading ? <Spinner /> : "Add"}
                </CustomButton>
              </div>
              <MultipleSelectChip
                hashTags={anwerData.tags}
                handleChange={(value) =>
                  handleChangeAnwerData("tags", [...value])
                }
              />
            </Field>
          </DialogContent>
        </OverlayScrollbarsComponent>

        <DialogActions>
          <CustomButton kind="secondary" onClick={handleClose}>
            Close
          </CustomButton>
          <CustomButton
            disabled={createAwnserPostForumMutation.isLoading}
            kind="primary"
            onClick={handleSubmit}
          >
            {createAwnserPostForumMutation.isLoading ? <Spinner /> : "Save"}
          </CustomButton>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default UpdateQuestion
