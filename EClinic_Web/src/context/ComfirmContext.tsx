import AlertCustom from "components/User/Alert"
import {
  createContext,
  PropsWithChildren,
  ReactNode,
  useCallback,
  useContext,
  useRef,
  useState
} from "react"
type Props = {
  open?: boolean
  title?: ReactNode
  content?: ReactNode
  btnAgree?: string
  btnDisagree?: string
}
// eslint-disable-next-line no-unused-vars
type ConfirmFn = (data: Props) => Promise<boolean>

const ConfirmDialog = createContext<ConfirmFn | null>(null)

export function ConfirmDialogProvider({ children }: PropsWithChildren) {
  const [state, setState] = useState<Props>()
  const fn = useRef<any>()
  const confirm = useCallback(
    (data: Props) => {
      return new Promise<boolean>((resolve) => {
        setState({ ...data, open: true })
        fn.current = (choice: boolean) => {
          resolve(choice)
          setState({ ...data, open: false })
        }
      })
    },
    [setState]
  )
  return (
    <ConfirmDialog.Provider value={confirm}>
      {children}
      <AlertCustom
        {...state}
        handleClose={() => fn.current?.(false)}
        onConfirm={() => fn.current?.(true)}
      />
    </ConfirmDialog.Provider>
  )
}

export default function useConfirm() {
  return useContext(ConfirmDialog)
}
