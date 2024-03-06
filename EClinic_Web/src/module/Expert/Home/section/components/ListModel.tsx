import { Box, IconButton, Tooltip } from "@mui/material"
import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import { useActiveModelMutation, useGetAllModelQuery } from "hooks/query/ai"
import { MRT_ColumnDef } from "material-react-table"
import { useMemo, useState } from "react"
import { HiOutlineLockClosed, HiOutlinePencilSquare } from "react-icons/hi2"
import colorsProvider from "shared/theme/colors"
import { Model } from "types/AI"
import ButtonTable from "./ButtonTable"
import UpdateModel from "./UpdateModel"
import WrapperHeading from "./WrapperHeading"
import { toast } from "react-hot-toast"
import useConfirm from "context/ComfirmContext"

const ListModel = () => {
  const confirm = useConfirm()
  const [actionModel, setActionModel] = useState({
    active: false,
    type: "edit" as "edit" | "create",
    value: null as Model | null
  })
  const activeModel = useActiveModelMutation()
  const { data, isLoading, isError, isRefetching, refetch } =
    useGetAllModelQuery()
  const columns = useMemo<MRT_ColumnDef<Model>[]>(
    () => [
      {
        accessorKey: "ModelID",
        header: "Id",
        enableClickToCopy: true,
        size: 60,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[60px]">{row.original.ModelID}</p>
          )
        }
      },
      {
        accessorKey: "ModelName",
        header: "Model Name",
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[180px]">
              {row.original.ModelName}
            </p>
          )
        }
      },
      {
        accessorKey: "Accuracy",
        header: "Accuracy",
        size: 60,
        Cell: ({ row }) => {
          return (
            <Tag color={colorsProvider.success}>{row.original.Accuracy}</Tag>
          )
        }
      },
      {
        accessorKey: "DeepLearning.DeepID",
        header: "Deep Learning",
        Cell: ({ row }) => {
          return (
            <Tag color={colorsProvider.support}>
              {row.original.DeepLearning.DeepName}
            </Tag>
          )
        }
      },
      {
        accessorKey: "MachineLearning.MachineID",
        header: "Machine Learning",
        Cell: ({ row }) => {
          return (
            <Tag color={colorsProvider.pending}>
              {row.original.MachineLearning.MachineName}
            </Tag>
          )
        }
      },
      {
        accessorKey: "IsActive",
        header: "Active",
        Cell: ({ row }) => (
          <Tag
            color={
              row.original.IsActive
                ? colorsProvider.success
                : colorsProvider.error
            }
          >
            {row.original.IsActive ? "Active" : "Banned"}
          </Tag>
        )
      }
    ],
    []
  )
  const handleActiveModel = async (value: Model) => {
    if (confirm) {
      const choice = await confirm({
        title: "Change status",
        content: "Are you sure you want to change status this model?"
      })
      if (choice) {
        activeModel.mutate(value.ModelID, {
          onSuccess() {
            toast.success("Change status completed")
            refetch()
          },
          onError() {
            toast.success("Change status fail")
          }
        })
      }
    }
  }
  return (
    <WrapperHeading
      heading="Model list"
      elementRight={
        <>
          <ButtonTable
            content={"Create a new Model"}
            onClick={() =>
              setActionModel((prevState) => ({
                ...prevState,
                type: "create",
                value: null,
                active: true
              }))
            }
          />
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
        renderRowActions={({ row }) => (
          <Box sx={{ display: "flex", gap: "1rem", paddingRight: 10 }}>
            <Tooltip arrow placement="left" title="Edit">
              <IconButton
                onClick={() => {
                  setActionModel({
                    active: true,
                    type: "edit",
                    value: row.original
                  })
                }}
              >
                <HiOutlinePencilSquare />
              </IconButton>
            </Tooltip>
            <Tooltip arrow placement="left" title="Change status">
              <IconButton onClick={() => handleActiveModel(row.original)}>
                <HiOutlineLockClosed />
              </IconButton>
            </Tooltip>
          </Box>
        )}
      />
      <UpdateModel
        onModalChange={(value) => {
          setActionModel((prevstate) => ({
            ...prevstate,
            active: value,
            value: value ? prevstate.value : null
          }))
        }}
        model={actionModel.value}
        show={actionModel.active}
      />
    </WrapperHeading>
  )
}

export default ListModel
