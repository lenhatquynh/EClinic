export interface Service {
  serviceID: string
  serviceName: any
  price: number
  estimatedTime: number
  isActive: boolean
  specialization: Specialization
  createdAt: string
  updatedAt: string
}
export interface Specialization {
  specializationID: string
  specializationName: string
}
export interface ServicePackage {
  servicePackageID: string
  servicePackageName: string
  description: string
  image: any
  price: number
  discount: number
  totalOrder: number
  estimatedTime: number
  isActive: boolean
  createdAt: string
  updatedAt: string
  serviceItems: Service[]
}
