import { Skeleton } from "@mui/material"
import Chip from "@mui/material/Chip"
import FormControl from "@mui/material/FormControl"
import { useGetAllHashTagQuery } from "hooks/query/forum/useForum"
type Props = {
  hashTags: string[]
  // eslint-disable-next-line no-unused-vars
  handleChange: (value: string[]) => void
}
export default function MultipleSelect({ hashTags, handleChange }: Props) {
  const getAllHashTagQuery = useGetAllHashTagQuery()

  const handleChangeValue = (id: string) => {
    const checkExits = hashTags.some((item) => item === id)
    let newHashTags = []
    if (!checkExits) {
      newHashTags = [...hashTags, id]
    } else {
      newHashTags = hashTags.filter((item) => item !== id)
    }
    handleChange(newHashTags)
  }

  return (
    <FormControl sx={{ width: "100%" }}>
      <>
        <div className="flex flex-wrap gap-3">
          {getAllHashTagQuery.isLoading && (
            <Skeleton variant="rounded" width={"100%"} height={50} />
          )}
          {getAllHashTagQuery.data?.data.map((hashtag) => {
            const check = hashTags.some((item) => item === hashtag.hashtagID)
            return (
              <Chip
                key={hashtag.hashtagID}
                variant={check ? "filled" : "outlined"}
                color="primary"
                label={hashtag.hashtagName}
                clickable
                className="rounded-md"
                onClick={() => handleChangeValue(hashtag.hashtagID)}
              />
            )
          })}
        </div>
      </>
    </FormControl>
  )
}
