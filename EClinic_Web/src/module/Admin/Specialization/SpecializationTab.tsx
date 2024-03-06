import { yupResolver } from "@hookform/resolvers/yup"
import { Box, IconButton, Tooltip } from "@mui/material"
import classNames from "classnames"
import TableCustom from "components/Common/Table/TableCustom"
import CustomButton from "components/User/Button"
import { CustomInput } from "components/User/Input"
import InputField from "components/User/Input/InputField"
import useConfirm from "context/ComfirmContext"
import {
  CreateSpecialization,
  useCreateSpecializationMutation,
  useSearchSpecializationQuery,
  useUpdateSpecializationMutation
} from "hooks/query/service/useService"
import useDebounce from "hooks/useDebounce"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import { useMemo, useState } from "react"
import { FieldValues, useForm } from "react-hook-form"
import toast from "react-hot-toast"
import { HiOutlinePencilSquare } from "react-icons/hi2"
import { getDataPaginate } from "shared/helpers/helper"
import { Specialization } from "types/Service"
import * as yup from "yup"

const schema = yup.object({
  specializationName: yup
    .string()
    .trim()
    .required("Please enter specialization name")
})
const SpecializationTab = () => {
  const [mode, setMode] = useState("create")
  const [spec, setSpec] = useState<Specialization>({
    specializationID: "",
    specializationName: ""
  })
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 1,
    pageSize: 10
  })
  const [searchData, setSearchData] = useState("")
  const searchTextDebounce = useDebounce(searchData, 1000)
  const { data, isLoading, isError, isRefetching, refetch } =
    useSearchSpecializationQuery({
      searchText: searchTextDebounce,
      pageNumber: pagination.pageIndex + 1,
      pageSize: pagination.pageSize
    })
  const paginationData = getDataPaginate(data)
  const confirm = useConfirm()
  const createSpecialization = useCreateSpecializationMutation()
  const updateSpecialization = useUpdateSpecializationMutation()
  const {
    handleSubmit,
    control,
    reset,
    setValue,
    formState: { errors }
  } = useForm({
    mode: "onSubmit",
    resolver: yupResolver(schema),
    defaultValues: {
      specializationID: spec.specializationID,
      specializationName: spec.specializationName
    } as Specialization
  })
  const handleUpdate = (spec: Specialization) => {
    setMode("update")
    setSpec(spec)
    setValue("specializationName", spec.specializationName)
  }
  const onSubmit = async (value: FieldValues) => {
    if (mode === "update") {
      if (confirm) {
        const choice = await confirm({
          title: "Update Specialization",
          content: "Are you sure want to update this Specialization?"
        })
        if (choice) {
          updateSpecialization.mutate(
            {
              ...value,
              specializationID: spec.specializationID
            } as Specialization,
            {
              onSuccess: (data) => {
                refetch()
                if (data.isSuccess) {
                  toast.success("Update a Specialization successfully")
                  resetForm()
                  setMode("create")
                } else {
                  toast.error("Update a Specialization fail")
                }
              },
              onError: () => {
                toast.error("Update a Specialization fail")
              }
            }
          )
        }
      }
    } else {
      if (confirm) {
        const choice = await confirm({
          title: "Create Specialization",
          content: "Are you sure want to create this Specialization?"
        })
        if (choice) {
          createSpecialization.mutate(
            {
              ...value
            } as CreateSpecialization,
            {
              onSuccess: (data) => {
                refetch()
                if (data?.isSuccess) {
                  toast.success("Create a Specialization successfully")
                  resetForm()
                } else {
                  toast.error("Create a Specialization fail")
                }
              },
              onError: () => {
                toast.error("Create a Specialization fail")
              }
            }
          )
        }
      }
    }
  }
  const resetForm = () => {
    reset({
      specializationName: ""
    })
  }
  const columns = useMemo<MRT_ColumnDef<Specialization>[]>(
    () => [
      {
        accessorKey: "specializationID",
        header: "Id",
        size: 160,
        enableClickToCopy: true,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[160px]">
              {row.original.specializationID}
            </p>
          )
        }
      },
      {
        accessorKey: "specializationName",
        header: "Name",
        size: 180,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[180px]">
              {row.original.specializationName}
            </p>
          )
        }
      }
    ],
    []
  )
  return (
    <div className="flex flex-col gap-y-4">
      <form
        className="flex items-center max-w-2xl gap-x-3 background-primary"
        onSubmit={handleSubmit(onSubmit)}
      >
        <InputField
          size="small"
          control={control}
          name="specializationName"
          label={"Specialization name"}
        />

        <CustomButton
          color={mode === "create" ? "primary" : "secondary"}
          kind="primary"
          type="submit"
          className={classNames(
            "w-fit",
            mode === "create" ? "bg-primary" : "bg-secondary text-white"
          )}
          isLoading={createSpecialization.isLoading}
        >
          {mode === "create" ? "Create" : "Update"}
        </CustomButton>
      </form>
      <TableCustom
        pagination={pagination}
        onPaginationChange={setPagination}
        columns={columns}
        data={data?.data?.data ?? []}
        rowCount={paginationData.TotalCount ?? 0}
        pageCount={paginationData.TotalPages ?? 0}
        isLoading={isLoading}
        isError={isError}
        isRefetching={isRefetching}
        renderRowActions={({ row }) => (
          <Box sx={{ display: "flex", gap: "1rem" }}>
            <Tooltip arrow placement="left" title="Edit">
              <IconButton
                onClick={() =>
                  handleUpdate({
                    specializationID: row.original.specializationID,
                    specializationName: row.original.specializationName
                  } as Specialization)
                }
              >
                <HiOutlinePencilSquare />
              </IconButton>
            </Tooltip>
          </Box>
        )}
        renderTopToolbarCustomActions={() => (
          <CustomInput
            placeholder="Search data here"
            className="max-w-[300px] w-full"
            onChange={(e) => setSearchData(e.target.value)}
          />
        )}
      />
    </div>
  )
}

export default SpecializationTab
