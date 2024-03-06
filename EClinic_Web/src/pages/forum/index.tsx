import UserLayout from "layout/User/UserLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ForumPageServer = dynamic(() => import("module/User/Forum/ForumPage"))
const Forum: NextPageWithLayout = () => {
  return <ForumPageServer />
}

Forum.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Forum
