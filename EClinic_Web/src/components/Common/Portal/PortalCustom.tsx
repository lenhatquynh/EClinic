import { useRef, useEffect, useState, ReactNode } from "react"
import { createPortal } from "react-dom"

interface PortalProps {
  children: ReactNode
  isActive: boolean
}

export const PortalCustom = (props: PortalProps) => {
  const ref = useRef<Element | null>(null)
  const [mounted, setMounted] = useState(false)

  useEffect(() => {
    ref.current = document.querySelector<HTMLElement>("body")
    setMounted(true)
  }, [])
  if (props.isActive === false) {
    return <>{props.children}</>
  }
  return mounted && ref.current
    ? createPortal(<>{props.children}</>, ref.current)
    : null
}
