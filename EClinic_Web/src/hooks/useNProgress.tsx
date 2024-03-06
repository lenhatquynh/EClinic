import { useRouter } from "next/router"
import NProgress from "nprogress"
import { useEffect } from "react"

// show progres when change route
NProgress.configure({
  minimum: 0.3,
  easing: "ease",
  speed: 500,
  showSpinner: false
})

export const useNProgress = () => {
  const router = useRouter()

  useEffect(() => {
    router.events.on("routeChangeStart", () => {
      NProgress.start()
    })

    router.events.on("routeChangeComplete", () => {
      NProgress.done()
    })
    router.events.on("routeChangeError", () => {
      NProgress.done()
    })
  }, [router.events])

  return true
}
