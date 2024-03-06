import CustomButton from "components/User/Button"
import Image from "next/image"
import { useTranslation } from "react-i18next"
import { dayformat } from "shared/helpers/helper"
import { IProfile, IRelationShip } from "types/Profile.type"
interface Props {
  profile?: IProfile & IRelationShip
  // eslint-disable-next-line no-unused-vars
  onEdit: (profileId: string) => void
  isLoading: boolean
}
const ProfileDisplay = ({ profile, onEdit, isLoading = false }: Props) => {
  const { t } = useTranslation("base")

  if (!profile) {
    return null
  } else {
    return (
      <>
        <div className="flex flex-col items-center justify-center gap-y-5 max-w-[460px] mx-auto">
          <div className="relative w-32 h-32 rounded-full shadow-sm">
            <Image
              src={(profile?.avatar as string) || "/images/default.jpeg"}
              fill
              alt={profile?.firstName}
              sizes="(max-width: 768px) 100vw,
                      (max-width: 1200px) 50vw,
                      33vw"
              className="object-cover border border-gray-200 border-solid rounded-full"
            />
          </div>
          <div className="self-start w-full space-y-4 ">
            <h3 className="text-lg font-bold text-h1">
              {t("pages.information")}
            </h3>
            <ProfileItem
              label={t("pages.profileUser.form.full_name")}
              content={profile?.firstName + " " + profile?.lastName}
            />
            <ProfileItem
              label={t("pages.profileUser.form.email")}
              content={profile?.email}
            />
            <ProfileItem
              label={t("pages.profileUser.form.phone_number")}
              content={profile?.phone}
            />
            <ProfileItem
              label={t("pages.profileUser.form.gender")}
              content={
                profile?.gender
                  ? t("pages.profileUser.form.feMale")
                  : t("pages.profileUser.form.male")
              }
            />
            <ProfileItem
              label={t("pages.profileUser.form.address")}
              content={profile?.address}
            />
            <ProfileItem
              label={t("pages.profileUser.form.date_of_birth")}
              content={dayformat(profile?.dateOfBirth)}
            />
            <ProfileItem
              label={t("pages.profileUser.form.blood_type")}
              content={profile?.bloodType}
            />
            <ProfileItem
              label={t("pages.profileUser.form.height")}
              content={profile?.height.toString()}
            />
            <ProfileItem
              label={t("pages.profileUser.form.weight")}
              content={profile?.weight.toString()}
            />
            <ProfileItem
              label={t("pages.profileUser.form.relationship")}
              content={profile?.relationshipName}
            />
          </div>
          <CustomButton
            isLoading={isLoading}
            kind="primary"
            className="self-end "
            onClick={() => {
              onEdit(profile?.profileID)
            }}
          >
            {t("pages.profileUser.change")}
          </CustomButton>
        </div>
      </>
    )
  }
}
const ProfileItem = ({ label = "", content = "--" }) => {
  return (
    <div className="flex justify-between">
      <span className="font-light">{label}</span>
      <span className="font-semibold">{content ?? "--"}</span>
    </div>
  )
}
export default ProfileDisplay
