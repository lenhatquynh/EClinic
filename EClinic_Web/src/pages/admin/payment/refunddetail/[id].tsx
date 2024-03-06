import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"

const RefundDetail = dynamic(
    () => import("module/Admin/Payment/Detail/RefundDetail").then((module) => module.default),
    {
        ssr: false
    }
)
const Page: NextPageWithLayout = () => {
    const router = useRouter()
    if (router.query.id) {
        return <RefundDetail />
    }
    return null
}
Page.getLayout = (page) => {
    return <AdminLayout>{page}</AdminLayout>
}

export default Page
