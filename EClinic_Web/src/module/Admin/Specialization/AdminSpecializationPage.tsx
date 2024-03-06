import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import React from "react"
import SpecializationTab from "./SpecializationTab"
const AdminSpecializationPage = () => {
    return (
        <>
            <Head>
                <title>Specialization</title>
            </Head>
            <MainHeadingLayout heading="Specialization">
                <SpecializationTab />
            </MainHeadingLayout>
        </>
    )
}

export default AdminSpecializationPage
