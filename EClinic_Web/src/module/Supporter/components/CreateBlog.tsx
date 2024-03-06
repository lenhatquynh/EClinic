import { yupResolver } from "@hookform/resolvers/yup"
import {
  Box,
  Chip,
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
  TextField
} from "@mui/material"
import { useQueryClient } from "@tanstack/react-query"
import { AxiosError } from "axios"
import SwitchCustom from "components/Common/IOSSwitch"
import Spinner from "components/Common/Loading/LoadingIcon"
import { UpdateCover } from "components/Common/UpLoadImage"
import CustomButton from "components/User/Button"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import {
  CreatePostBlog,
  UpdatePostBlog,
  useCreateBlogPostMutation,
  useCreateHashtagBlogMutation,
  useGetAllHashTag,
  useUpdateBlogPostMutation
} from "hooks/query/blog/useBlog"
import dynamic from "next/dynamic"
import { useEffect, useState } from "react"
import { FieldValues, useForm } from "react-hook-form"
import { toast } from "react-hot-toast"
import { QUERY_KEYS } from "shared/constant/constant"
import { HashTagBlog, IBlog } from "types/Blog"
import * as yup from "yup"
const Editor = dynamic(() => import("components/Common/Editor/Editor"), {
  ssr: false,
  loading: () => <Spinner />
})
const ITEM_HEIGHT = 48
const ITEM_PADDING_TOP = 8
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250
    }
  }
}
const schema = yup.object({
  title: yup.string().trim().required("Please enter title"),
  content: yup.string().trim().required("Please enter content"),
  metaTitle: yup.string().trim().required("Please enter meta title"),
  metaDescription: yup
    .string()
    .trim()
    .required("Please enter meta description"),
  metaKeywords: yup.string().trim().required("Please enter meta keywords"),
  hashtags: yup
    .array()
    .min(1, "Please choose at least one hashtag")
    .required("Please choose hashtag")
})
interface Props {
  labelForm: string
  post?: IBlog
  mode?: "update" | "create"
}
const CreateBlog = ({ labelForm, post, mode = "create" }: Props) => {
  const queryClient = useQueryClient()
  const [newHashtag, setNewHashtag] = useState<string>("")
  const confirm = useConfirm()
  const hashTags = useGetAllHashTag()
  const createPost = useCreateBlogPostMutation()
  const updatePost = useUpdateBlogPostMutation()
  const createHashtag = useCreateHashtagBlogMutation()
  const {
    handleSubmit,
    control,
    watch,
    reset,
    setValue,
    formState: { errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: post
  })
  const hashTagSelected = watch("hashtags")
  const watchDesc = watch("content")
  const watchCoverImage = watch("coverImage")
  console.log("CreateBlog ~ watchCoverImage:", watchCoverImage)
  const watchIsActive = watch("isActive", post?.isActive ? true : false)
  const onFileChange = (file: File | null) => {
    setValue("coverImage", file)
  }
  const onSubmit = async (value: FieldValues) => {
    const newHashTags =
      value.hashtags.length > 0
        ? value.hashtags.map((item: HashTagBlog) => item.id)
        : []
    if (mode === "update") {
      if (confirm) {
        const choice = await confirm({
          title: "Update blog",
          content: "Are you sure you want to update this blog?"
        })
        if (choice) {
          updatePost.mutate(
            {
              ...value,
              hashtagId: newHashTags
            } as UpdatePostBlog,
            {
              onSuccess: (data) => {
                if (data?.isSuccess) {
                  toast.success("Update successfully")
                  queryClient.invalidateQueries([QUERY_KEYS.BLOG.POST])
                } else {
                  toast.error("Update error")
                }
              },
              onError: () => {
                toast.error("Update error")
              }
            }
          )
        }
      }
    } else {
      if (!watchCoverImage) {
        toast.error("Please upload cover image")
        return
      }
      createPost.mutate(
        {
          ...value,
          hashtagId: newHashTags
        } as CreatePostBlog,
        {
          onSuccess: (data) => {
            if (data?.isSuccess) {
              toast.success("Create a post successfully")
              resetForm()
              queryClient.invalidateQueries([QUERY_KEYS.BLOG.POST])
            } else {
              toast.error("Add error")
            }
          },
          onError: () => {
            toast.error("Add error")
          }
        }
      )
    }
  }
  const resetForm = () => {
    reset({
      title: "",
      content: "",
      coverImage: "",
      hashtags: [],
      isActive: false,
      metaDescription: "",
      metaKeywords: "",
      metaTitle: ""
    })
  }
  const handleCreateHashTag = () => {
    if (newHashtag) {
      createHashtag.mutate(newHashtag, {
        onSuccess: () => {
          setNewHashtag("")
          queryClient.invalidateQueries([QUERY_KEYS.BLOG.HASHTASH])
          toast.success("Create hashtag success")
        },
        onError: (error: unknown) => {
          const data = error as AxiosError<{ message: string }>
          toast.error(data?.response?.data?.message || "Create fail")
        }
      })
    }
  }
  useEffect(() => {
    if (post === undefined) {
      resetForm()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [post])
  const handleChange = (event: SelectChangeEvent<string[]>) => {
    const {
      target: { value }
    } = event
    let newHashtags: HashTagBlog[] = []
    if (typeof value === "string") {
      newHashtags =
        hashTags.data?.data.data.filter((item) => {
          if (item.id === value) {
            return item
          }
        }) || []
    } else {
      newHashtags =
        hashTags.data?.data.data.filter((item) => {
          if (value.some((has) => has === item.id)) {
            return item
          }
        }) || []
    }
    setValue("hashtags", newHashtags)
  }
  const getHashtagName = (hashtagID: string) => {
    const selectedTag = hashTags.data?.data.data.find(
      (tag) => tag.id === hashtagID
    )
    return selectedTag ? selectedTag.hashtagName : ""
  }
  return (
    <form className="grid grid-cols-8 gap-6" onSubmit={handleSubmit(onSubmit)}>
      <div className="col-span-5 background-primary">
        <h3 className="pb-4 text-lg font-medium">{labelForm}</h3>
        <div className="flex flex-col justify-start">
          <div className="flex flex-col space-y-5">
            <InputField
              size="medium"
              label="Post title"
              control={control}
              name="title"
            />
            <div className="flex flex-col gap-y-2">
              <span className="text-gray-500">Cover</span>
              <UpdateCover
                isDelete={false}
                onFileChange={onFileChange}
                imageUrl={watchCoverImage || null}
              />
            </div>

            <div className="flex flex-col gap-y-2">
              <span className="text-gray-500">Content</span>
              <Editor
                onChange={(data: string) => {
                  setValue("content", data)
                }}
                value={watchDesc}
              />
            </div>
          </div>
        </div>
      </div>

      <div className="col-span-3">
        <div className="flex flex-col py-8 space-y-6 background-primary">
          <div className="flex items-center justify-between w-full">
            <span className="text-base font-medium text-black2">Publish</span>
            <SwitchCustom
              checked={watchIsActive}
              onChange={() => setValue("isActive", !watchIsActive)}
            />
          </div>
          <div className="flex flex-col gap-y-1">
            <div className="flex w-full gap-x-1">
              <TextField
                value={newHashtag}
                onChange={(e) => setNewHashtag(e.target.value)}
                size="small"
                helperText="Please enter your new hashtag here"
              />
              <CustomButton
                onClick={handleCreateHashTag}
                kind="primary"
                className="max-h-10 min-h-[24px]"
                isLoading={createHashtag.isLoading}
                disabled={!newHashtag}
              >
                Create
              </CustomButton>
            </div>
            <FormControl>
              <InputLabel>Hashtags</InputLabel>
              <Select
                multiple
                value={hashTagSelected?.map((item) => item.id) || []}
                onChange={handleChange}
                error={!!errors.hashtags}
                input={<OutlinedInput label="Hashtags" />}
                renderValue={(selected) => (
                  <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                    {selected.map((value) => (
                      <Chip key={value} label={getHashtagName(value)} />
                    ))}
                  </Box>
                )}
                MenuProps={MenuProps}
              >
                {hashTags.data?.data.data.map((name, index) => (
                  <MenuItem key={index} value={name.id}>
                    {name.hashtagName}
                  </MenuItem>
                ))}
              </Select>
              <FormHelperText error>{errors.hashtags?.message}</FormHelperText>
            </FormControl>
          </div>
          <InputField
            size="medium"
            label="Meta title"
            control={control}
            name="metaTitle"
          />
          <InputField
            size="medium"
            multiline
            rows={3}
            label="Meta description"
            control={control}
            name="metaDescription"
          />
          <InputField
            size="medium"
            label="Meta keywords"
            control={control}
            name="metaKeywords"
          />
        </div>
        <div className="flex w-full mt-4 space-x-5">
          <CustomButton
            kind="primary"
            type="submit"
            className="w-full "
            isLoading={createPost.isLoading || updatePost.isLoading}
          >
            {mode === "create" ? "Post" : "Update blog"}
          </CustomButton>
        </div>
      </div>
    </form>
  )
}
export default CreateBlog
