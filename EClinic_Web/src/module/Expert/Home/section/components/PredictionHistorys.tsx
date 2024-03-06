import TableCustom from "components/Common/Table/TableCustom"
import Tag from "components/Common/Tag"
import { useGetAllHistory } from "hooks/query/ai"
import { MRT_ColumnDef, MRT_PaginationState } from "material-react-table"
import { useMemo, useState } from "react"
import { dayformat, getDataPaginate } from "shared/helpers/helper"
import colorsProvider from "shared/theme/colors"
import { PredictHistory } from "types/AI"

const PredictionHistorys = () => {
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 1,
    pageSize: 10
  })
  const { data, isLoading, isError, isRefetching } = useGetAllHistory(
    pagination.pageIndex + 1,
    pagination.pageSize
  )
  const columns = useMemo<MRT_ColumnDef<PredictHistory>[]>(
    () => [
      {
        accessorKey: "PredictID",
        header: "Id",
        size: 60,
        enableClickToCopy: true,
        Cell: ({ row }) => {
          return (
            <p className="line-clamp-1 max-w-[120px]">
              {row.original.PredictID}
            </p>
          )
        }
      },
      {
        accessorKey: "ModelName",
        header: "Model Name"
      },
      {
        accessorKey: "PredictTime",
        header: "Time prediction",
        Cell: ({ row }) => {
          return (
            <time dateTime={dayformat(row.original.PredictTime)}>
              {dayformat(row.original.PredictTime)}
            </time>
          )
        }
      },
      {
        accessorKey: "Result",
        header: "Result",
        Cell: ({ row }) => {
          const result = row.original.Result
          return (
            <Tag
              color={
                result === "Monkeypox"
                  ? colorsProvider.success
                  : result === "Chickenpox"
                  ? colorsProvider.error
                  : result === "Measles"
                  ? colorsProvider.primary
                  : result === "Normal"
                  ? colorsProvider.secondary
                  : colorsProvider.error
              }
            >
              {result}
            </Tag>
          )
        }
      }
    ],
    []
  )
  let paginationData
  if (data && data.headers["x-pagination"]) {
    paginationData = JSON.parse(
      data.headers["x-pagination"]
        ?.replace(/'/g, '"')
        .replace(/True/g, "true")
        .replace(/False/g, "false")
    )
  }
  return (
    <>
      <TableCustom
        pagination={pagination}
        onPaginationChange={setPagination}
        columns={columns}
        data={data?.data?.data ?? []}
        rowCount={paginationData?.TotalCount ?? 0}
        pageCount={paginationData?.TotalPages ?? 0}
        isLoading={isLoading}
        isError={isError}
        isRefetching={isRefetching}
        enableRowActions={false}
      />
    </>
  )
}

export default PredictionHistorys
