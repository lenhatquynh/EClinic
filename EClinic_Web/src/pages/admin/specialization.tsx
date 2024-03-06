import AdminLayout from "layout/Management/AdminLayout"
import AdminSpecializationPage from "module/Admin/Specialization/AdminSpecializationPage"
import { NextPageWithLayout } from "pages/page"

const Page: NextPageWithLayout = () => {
    return <AdminSpecializationPage />
}
Page.getLayout = (page) => {
    return <AdminLayout>{page}</AdminLayout>
}

export default Page
