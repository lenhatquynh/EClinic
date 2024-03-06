// @mui
import { Avatar, Card, CardContent, Grid, Typography } from "@mui/material"
import { alpha, styled } from "@mui/material/styles"
import { motion } from "framer-motion"
// utils
import SvgColor from "components/Common/svg-color/index"
import Link from "next/link"
import { dayformat } from "shared/helpers/helper"
import { IBlog } from "types/Blog"
import Tag from "components/Common/Tag"
import colorsProvider from "shared/theme/colors"
// ----------------------------------------------------------------------

const StyledCardMedia = styled("div")({
  position: "relative",
  paddingTop: "calc(100% * 3 / 4)"
})

const StyledTitle = styled(Link)({
  height: 44,
  overflow: "hidden",
  WebkitLineClamp: 2,
  display: "-webkit-box",
  WebkitBoxOrient: "vertical"
})

const StyledAvatar = styled(Avatar)(({ theme }) => ({
  zIndex: 9,
  width: 32,
  height: 32,
  position: "absolute",
  left: theme.spacing(3),
  bottom: theme.spacing(-2)
}))

const StyledCover = styled("img")({
  top: 0,
  width: "100%",
  height: "100%",
  objectFit: "cover",
  position: "absolute"
})

// ----------------------------------------------------------------------

interface IProps {
  post: IBlog
  index: number
}
export default function BlogPostCard({ post, index }: IProps) {
  const latestPostLarge = index === 0
  const latestPost = index === 1 || index === 2

  return (
    <Grid
      component={motion.div}
      whileHover={{ scale: latestPostLarge || latestPost ? 1.05 : 1.1 }}
      item
      xs={12}
      sm={latestPostLarge ? 12 : 6}
      md={latestPostLarge ? 6 : 3}
    >
      <Card className="shadow rounded-xl !p-0 relative">
        <StyledCardMedia
          sx={{
            ...((latestPostLarge || latestPost) && {
              pt: "calc(100% * 4 / 3)",
              "&:after": {
                top: 0,
                content: "''",
                width: "100%",
                height: "100%",
                position: "absolute",
                bgcolor: (theme) => alpha(theme.palette.grey[900], 0.72)
              }
            }),
            ...(latestPostLarge && {
              pt: {
                xs: "calc(100% * 4 / 3)",
                sm: "calc(100% * 3 / 4.66)"
              }
            })
          }}
        >
          <SvgColor
            color="paper"
            src="/images/shape-avatar.svg"
            sx={{
              width: 80,
              height: 36,
              zIndex: 9,
              bottom: -15,
              position: "absolute",
              color: "background.paper",
              ...((latestPostLarge || latestPost) && { display: "none" })
            }}
          />
          <StyledAvatar
            alt={post.author.firstName + " " + post.author.lastName}
            src={post.author.avatar}
            sx={{
              ...((latestPostLarge || latestPost) && {
                zIndex: 9,
                top: 24,
                left: 24,
                width: 40,
                height: 40
              })
            }}
          />

          <StyledCover alt={post.title} src={post.coverImage} />
        </StyledCardMedia>

        <CardContent
          sx={{
            pt: 4,
            ...((latestPostLarge || latestPost) && {
              bottom: 0,
              width: "100%",
              position: "absolute"
            })
          }}
        >
          <Typography gutterBottom variant="caption" className="text-disable">
            {dayformat(post.updatedAt)}
          </Typography>

          <StyledTitle
            href={`/blog/${post.id}`}
            className="hover:underline"
            sx={{
              ...(latestPostLarge && { typography: "h5", height: 60 }),
              ...((latestPostLarge || latestPost) && {
                color: "common.white"
              })
            }}
          >
            {post.title}
          </StyledTitle>
          <div className="flex items-center gap-2 mt-4">
            {post.hashtags.slice(0, 3).map((item, index) => (
              <Tag color={colorsProvider.gray80} key={index}>
                {item.hashtagName}
              </Tag>
            ))}
          </div>
        </CardContent>
      </Card>
    </Grid>
  )
}
