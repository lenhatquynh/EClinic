import InputCustom from "components/Common/Input"
import CustomButton from "components/User/Button"
import {
  useCreateProfileMutation,
  useDeleteProfileMutation,
  useSearchFamlyProfilesQuery,
  useUpdateProfileMutation
} from "hooks/query/profile/useProfile"
import useDebounce from "hooks/useDebounce"
import { useEffect, useState } from "react"
import { FieldValues } from "react-hook-form"
import { toast } from "react-hot-toast"
import { HiMagnifyingGlass } from "react-icons/hi2"
import { useSelector } from "react-redux"
import { PAGE_SIZE } from "shared/constant/constant"
import { RootState } from "store/store"
import { IProfile, IRelationShip } from "types/Profile.type"
import ProfileDisplay from "./components/ProfileDisplay/ProfileDisplay"
import ProfileItem from "./components/ProfileItem"
import Edit from "./components/form/Edit"
import { useTranslation } from "react-i18next"

type Action = "add" | "edit" | "view"
const Profile = () => {
  const { t } = useTranslation(["base"])

  const [profiles, setProfiles] = useState<(IProfile & IRelationShip)[]>([])
  const [ownerProfile, setOwnerProfile] = useState<
    (IProfile & IRelationShip) | undefined
  >(undefined)
  const [pageIndex, _] = useState(1)
  const [searchText, setSearchText] = useState("")
  const searchTextDebounce = useDebounce(searchText, 1500)
  const auth = useSelector((state: RootState) => state.auth)
  const [mode, setMode] = useState<Action>("view")

  //query
  const { data, status, isError, isLoading, refetch } =
    useSearchFamlyProfilesQuery(pageIndex, PAGE_SIZE, searchTextDebounce)
  const deleteProfileMutation = useDeleteProfileMutation()
  const updateProfileMutaiton = useUpdateProfileMutation()
  const createProfileMutaiton = useCreateProfileMutation()
  useEffect(() => {
    if (status === "success") {
      setProfiles(data.data.data)
      //default value for owner profiled to display first render
      const owner = data?.data.data.find((item) => {
        return item.userID === auth.user.userId
      })
      setOwnerProfile(owner)
      setMode("view")
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [status, data])
  if (isError) {
    return <p>Not found</p>
  }
  const handleSubmitForm = (value: FieldValues) => {
    if (mode === "edit") {
      updateProfileMutaiton.mutate(value as IProfile & IRelationShip, {
        onSuccess: (data) => {
          if (data.isSuccess) {
            toast.success("Update successfully!")
            refetch()
          } else {
            toast.error("Create error")
          }
        },
        onError: () => {
          toast.error("Create error")
        }
      })
    } else if (mode === "add") {
      createProfileMutaiton.mutate(
        { ...value, userID: auth.user.userId } as IProfile & IRelationShip,
        {
          onSuccess: (data) => {
            if (data.isSuccess) {
              toast.success("Add successfully")
              refetch()
            } else {
              toast.error("Add error")
            }
          },
          onError: (data: any) => {
            toast.error(data?.response?.data?.message || "Add error")
          }
        }
      )
    }
  }
  const handleChangeForm = (profileID: string | null, mode: Action) => {
    if (mode == "add") {
      setOwnerProfile(undefined)
    } else if (mode === "edit" || mode === "view") {
      const owner = data?.data.data.find((item) => {
        return item.profileID === profileID
      })
      setOwnerProfile(owner)
    }
    setMode(mode)
  }
  const handleDeleteProfile = (profileId: string) => {
    deleteProfileMutation.mutate(profileId, {
      onSuccess: (data) => {
        if (data.isSuccess) {
          toast.success("Delete successfully")
          refetch()
          setMode("view")
        } else {
          toast.error("Delete error")
        }
      },
      onError: () => {
        toast.error("Delete error")
      }
    })
  }

  return (
    <div className="flex flex-col w-full bg-white gap-y-3 md:flex-row">
      <div className="w-[340px] flex flex-col space-y-4">
        <InputCustom
          onChange={(e) => setSearchText(e.target.value)}
          icon={<HiMagnifyingGlass />}
          className="w-full md:max-w-[412px]"
          placeholder={t("base:pages.profileUser.find")}
        />
        <ul className="max-h-[600px] overflow-auto space-y-2">
          {profiles?.map((item, index) => (
            <ProfileItem
              data={item}
              key={index}
              loading={false}
              onClick={() => handleChangeForm(item.profileID, "view")}
            />
          ))}
          {isLoading &&
            Array(2)
              .fill(0)
              .map((_, index) => (
                <ProfileItem onClick={() => {}} key={index} loading={true} />
              ))}
        </ul>
        <CustomButton
          kind="primary"
          onClick={() => handleChangeForm(null, "add")}
        >
          <span className="text-base">
            {t("base:pages.profileUser.add-profile")}
          </span>
        </CustomButton>
      </div>
      <div className="flex-1 px-3 border md:px-6 md:ml-4 md:border-r-0 md:border-gray-200 md:border-dashed border-y-0">
        {(mode == "add" || mode === "edit") && (
          <Edit
            isLoading={
              createProfileMutaiton.isLoading ||
              deleteProfileMutation.isLoading ||
              updateProfileMutaiton.isLoading
            }
            labelForm={
              mode === "add"
                ? t("base:pages.profileUser.add")
                : t("base:pages.profileUser.edit")
            }
            onSubmit={handleSubmitForm}
            profile={ownerProfile}
            onDelete={handleDeleteProfile}
          />
        )}
        {mode == "view" && (
          <ProfileDisplay
            isLoading={updateProfileMutaiton.isLoading}
            profile={ownerProfile}
            onEdit={(profileId) => handleChangeForm(profileId, "edit")}
          />
        )}
      </div>
    </div>
  )
}

export default Profile
