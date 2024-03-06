import AdminLayout from "layout/Management/AdminLayout"
import dynamic from "next/dynamic"
import { useRouter } from "next/router"
import { NextPageWithLayout } from "pages/page"

const ServiceEdit = dynamic(
    () => import("module/Admin/Edit/ServiceEdit").then((module) => module.default),
    {
        ssr: false
    }
)
const Page: NextPageWithLayout = () => {
    const router = useRouter()
    if (router.query.id) {
        return <ServiceEdit />
    }
    return null
}
Page.getLayout = (page) => {
    return <AdminLayout>{page}</AdminLayout>
}

export default Page
