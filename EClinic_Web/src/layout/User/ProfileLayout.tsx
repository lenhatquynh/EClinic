import SiderBar from "module/User/Profile/section/siderbar"
import { PropsWithChildren } from "react"
import UserLayout from "./UserLayout"
import UserSecondaryLayout from "./UserSecondaryLayout"

const ProfileLayout = ({ children }: PropsWithChildren) => {
  return (
    <UserLayout footer={false}>
      <UserSecondaryLayout breadrums={[]}>
        <SiderBar>{children}</SiderBar>
      </UserSecondaryLayout>
    </UserLayout>
  )
}

export default ProfileLayout
