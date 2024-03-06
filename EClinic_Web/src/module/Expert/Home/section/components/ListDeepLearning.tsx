import TableCustom from "components/Common/Table/TableCustom"
import {
  useCreateDeepLearningMutation,
  useGetAllDeepLearningQuery,
  useUpdateDeepLearningMutation
} from "hooks/query/ai"
import { MRT_ColumnDef, MaterialReactTableProps } from "material-react-table"
import { useMemo, useState } from "react"
import { toast } from "react-hot-toast"
import { DeepLearning } from "types/AI"
import WrapperHeading from "./WrapperHeading"
import ButtonTable from "./ButtonTable"
import InputCustom from "components/Common/Input"
import colorsProvider from "shared/theme/colors"
import { QUERY_KEYS } from "shared/constant/constant"
import { queryClient } from "pages/_app"

const ListDeepLearning = () => {
  const [isCreate, setIsCreate] = useState(false)
  const [valueInput, setValueInput] = useState("")
  const { data, isLoading, isError, isRefetching, refetch } =
    useGetAllDeepLearningQuery()
  const update = useUpdateDeepLearningMutation()
  const create = useCreateDeepLearningMutation()
  const columns = useMemo<MRT_ColumnDef<DeepLearning>[]>(
    () => [
      {
        accessorKey: "DeepID",
        header: "Id",
        enableClickToCopy: true,
        enableEditing: false,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[160px]">{row.original.DeepID}</p>
          )
        }
      },
      {
        accessorKey: "DeepName",
        header: "Name",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[260px]">
              {row.original.DeepName}
            </p>
          )
        }
      }
    ],
    []
  )
  const handleSaveRow: MaterialReactTableProps<DeepLearning>["onEditingRowSave"] =
    async ({ exitEditingMode, values }) => {
      update.mutate(values, {
        onSuccess: () => {
          toast.success("Update complete")
          exitEditingMode()
          refetch()
          queryClient.invalidateQueries({
            queryKey: [QUERY_KEYS.AI.Model]
          })
        },
        onError: () => {
          toast.error("An error occurred during the update")
        }
      })
    }
  const handleCreate = () => {
    if (valueInput) {
      create.mutate(valueInput, {
        onSuccess: () => {
          toast.success("Create complete")
          setIsCreate(false)
          setValueInput("")
          refetch()
        },
        onError: () => {
          toast.error("An error occurred during the create")
        }
      })
    } else {
      toast.error("Please fill value")
    }
  }
  return (
    <WrapperHeading
      heading="Deep learning"
      elementRight={
        <>
          {!isCreate ? (
            <ButtonTable
              content={"Create a new Deep learning"}
              onClick={() => setIsCreate(true)}
            />
          ) : (
            <div className="flex items-center w-2/3 gap-2">
              <InputCustom
                placeholder="Deep learning's name"
                value={valueInput}
                onChange={(e) => setValueInput(e.target.value)}
              />
              <div className="flex items-center gap-2">
                <ButtonTable content={"Create"} onClick={handleCreate} />
                <ButtonTable
                  content={"Cancel"}
                  color={colorsProvider.error}
                  onClick={() => setIsCreate(false)}
                />
              </div>
            </div>
          )}
        </>
      }
    >
      <TableCustom
        initialState={{ pagination: { pageSize: 5, pageIndex: 0 } }}
        columns={columns}
        data={data?.data.data ?? []}
        isLoading={isLoading}
        isError={isError}
        isRefetching={isRefetching}
        editingMode="row"
        enableEditing
        onEditingRowSave={handleSaveRow}
      />
    </WrapperHeading>
  )
}

export default ListDeepLearning
