import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import Head from "next/head"
import React, { useMemo } from "react"
import ListPayment from "./ListPayment"
import ListRefund from "./ListRefund"

const AdminPaymentPage = () => {
    const tabs = useMemo(
        () => [
            {
                key: 0,
                label: `List Payment`,
                children: <ListPayment />
            },
            {
                key: 1,
                label: `List Refund`,
                children: <ListRefund />
            }
        ],
        []
    )
    return (
        <>
            <Head>
                <title>Payment</title>
            </Head>
            <MainHeadingLayout heading="Payment statistic">
                <TabsCustom defaultTabIndex={0} tabs={tabs} />
            </MainHeadingLayout>
        </>
    )
}

export default AdminPaymentPage
