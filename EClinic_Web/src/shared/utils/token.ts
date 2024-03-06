import Cookies from "js-cookie"
class Token {
  objCookies = {}

  saveToken = (access_token: string, refresh_token: string) => {
    if (access_token && refresh_token) {
      Cookies.set(
        process.env.NEXT_PUBLIC_ACCESS_TOKEN_NAME as string,
        access_token,
        {
          expires: 1
        }
      )
      Cookies.set(
        process.env.NEXT_PUBLIC_REFRESH_TOKEN_NAME as string,
        refresh_token,
        {
          expires: 30
        }
      )
    } else {
      Cookies.remove(process.env.NEXT_PUBLIC_ACCESS_TOKEN_NAME as string, {
        ...this.objCookies,
        path: "/",
        domain: process.env.COOKIE_DOMAIN
      })
      Cookies.remove(process.env.NEXT_PUBLIC_REFRESH_TOKEN_NAME as string, {
        ...this.objCookies,
        path: "/",
        domain: process.env.COOKIE_DOMAIN
      })
    }
  }

  getToken = () => {
    const access_token = Cookies.get(
      process.env.NEXT_PUBLIC_ACCESS_TOKEN_NAME as string
    )
    const refresh_token = Cookies.get(
      process.env.NEXT_PUBLIC_REFRESH_TOKEN_NAME as string
    )
    return {
      access_token,
      refresh_token
    }
  }
  deleteToken = () => {
    Cookies.remove(process.env.NEXT_PUBLIC_ACCESS_TOKEN_NAME as string)
    Cookies.remove(process.env.NEXT_PUBLIC_REFRESH_TOKEN_NAME as string)
  }
}
export const token = new Token()
