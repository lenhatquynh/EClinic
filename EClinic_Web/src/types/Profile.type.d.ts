export interface Profile {
  profileID: string
  userID: string
  firstName: string
  lastName: string
  avatar: any
  gender: boolean
  dateOfBirth: string
  address: string
  email: string
  phone: string
  enabledAccount: boolean
}

export interface IProfile extends Profile {
  bloodType: string
  height: number
  weight: number
}
export interface IProfileDoctor extends Profile {
  title: string
  workStart: string
  workEnd: string
  description: string
  content: string
  specialization: Specialization
  specializationID: string
  price: number
  isActive: boolean
}
export interface IProfileSupporter extends Profile {
  workStart: string
  workEnd: string
  description: string
  workEnd: string
}
export interface IProfileExpert extends Profile {
  workStart: string
  workEnd: string
  description: string
}

export interface IRelationShip {
  relationshipID: string
  relationshipName: string
}
export interface Specialization {
  specializationID: string
  specializationName: string
}
