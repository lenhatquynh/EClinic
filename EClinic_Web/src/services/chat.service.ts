import { AxiosResponse } from "axios"
import axiosClient from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import { IMessage, IRoom, RoomType } from "types/Chat"
import { IServerResponse } from "types/server/IServerResponse"
class ChatService {
  //room
  async getAllNewRoom(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.ROOM}/GetAllNewRoom`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IRoom[]>>
    } catch (error) {
      console.log("ChatService ~ error:", error)
    }
  }
  async getAllRoomOfUser(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.ROOM}/GetAllRoomOfUser`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IRoom[]>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async getAllRoomOfSupporter(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.ROOM}/GetAllRoomOfSupporter`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IRoom[]>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async CreateSupporterRoom(message: string) {
    try {
      const res: AxiosResponse = await axiosClient.post(
        `${URL_API.CHAT.ROOM}/CreateSupporterRoom?message=${message}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async getAllRoomOfDoctor(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.ROOM}/GetAllRoomOfDoctor`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IRoom[]>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async closeRoom(roomId: string) {
    try {
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.CHAT.ROOM}/closeRoom?roomId=${roomId}`
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  //chat message
  async getAllMessageOfRoom(
    pageNumber: number,
    pageSize: number,
    roomId: string
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.CHATMESSAGE}/GetAllMessageOfRoom?roomId=${roomId}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IMessage>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async getAllImageOfRoom(
    pageNumber: number,
    pageSize: number,
    roomId: string
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.CHATMESSAGE}/GetAllImageOfRoom?roomId=${roomId}`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<string[]>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  //Roomtype
  async getAllRoomType() {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.CHAT.RoomTypes}/GetAllRoomType`
      )
      return res as AxiosResponse<IServerResponse<RoomType[]>>
    } catch (error) {
      console.log("ChatServices ~ error:", error)
    }
  }
  async createMessage(roomId: string, content: string) {
    const res = await axiosClient.post(
      `${URL_API.CHAT.CHATMESSAGE}/CreateMessage`,
      {
        content,
        roomId
      }
    )
    return res.data as AxiosResponse<IServerResponse<string>>
  }
  async createMessageFile(roomId: string, file: File) {
    const res = await axiosClient.post(
      `${URL_API.CHAT.CHATMESSAGE}/CreateMessageFile`,
      {
        file,
        roomId
      },
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as AxiosResponse<IServerResponse<string>>
  }
}
export const chatService = new ChatService()
