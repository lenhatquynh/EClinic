export interface IRoom {
  roomID: string
  isClosed: boolean
  createdAt: string
  roomType: RoomType
  roomAuthor: ProfileChat
  chatMessage: Message
}

export interface RoomType {
  roomTypeID: string
  roomTypeName: string
}

export interface IMessage {
  myProfile: ProfileChat
  otherProfile: ProfileChat
  message: Message[]
}

export interface ProfileChat {
  userID: string
  firstName: string
  lastName: string
  avatar: string
}

export interface Message {
  chatMessageID: string
  userID: string
  content: string
  isImage: boolean
  type: number
  isMyChat: boolean
  createdAt: string
}
export type TypeMessage = "Text" | "Image"
