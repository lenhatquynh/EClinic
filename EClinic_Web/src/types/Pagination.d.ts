export interface IPagination {
  PageIndex: number
  PageSize: number
  TotalCount: number
  TotalPages: number
  HasPrevious: boolean
  HasNext: boolean
}
export interface IPaginationSearch {
  pageNumber: number
  pageSize: number
  searchText: string
}
export interface IPaginationSearchServicePackageFiltered
  extends IPaginationSearch {
  specializationIDs: string[]
}
export type IPaging = Omit<IPaginationSearch, "searchText">
