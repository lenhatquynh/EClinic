import { TableContainerProps } from "@mui/material"
import MaterialReactTable, {
  MRT_ColumnDef,
  MRT_PaginationState,
  MaterialReactTableProps
} from "material-react-table"

interface ReusableTableProps<T extends Record<string, any>>
  extends MaterialReactTableProps<T> {
  columns: MRT_ColumnDef<T>[]
  data: T[]
  tableContainerProps?: TableContainerProps
  pagination?: MRT_PaginationState
  rowCount?: number
  isLoading: boolean
  isError: boolean
  isRefetching: boolean
}
const TableCustom = <T extends Record<string, any>>({
  columns,
  data,
  pagination,
  rowCount,
  isLoading,
  isError,
  isRefetching,
  enableRowActions = true,
  tableContainerProps = { sx: { maxHeight: "600px" } },
  ...props
}: ReusableTableProps<T>) => {
  if (pagination) {
    return (
      <MaterialReactTable
        enableColumnResizing
        {...props}
        columns={columns}
        manualPagination
        enableStickyHeader
        enableTopToolbar
        enableGlobalFilter={false}
        muiTableContainerProps={tableContainerProps}
        enableRowActions={enableRowActions}
        data={data}
        rowCount={rowCount}
        state={{
          isLoading,
          pagination,
          showAlertBanner: isError,
          showProgressBars: isRefetching
        }}
        positionActionsColumn="last"
      />
    )
  }
  return (
    <MaterialReactTable
      {...props}
      enableColumnResizing
      columns={columns}
      enableRowActions
      enableStickyHeader
      enableTopToolbar
      enableGlobalFilter={false}
      muiTableContainerProps={tableContainerProps}
      data={data}
      state={{
        isLoading,
        showAlertBanner: isError,
        showProgressBars: isRefetching
      }}
      positionActionsColumn="last"
    />
  )
}

export default TableCustom
