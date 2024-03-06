import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import TabsCustom from "layout/Management/components/TabsCustom"
import Head from "next/head"
import React, { useMemo } from "react"
import CreateBlog from "../components/CreateBlog"
import ListBlog from "../components/ListBlog"

const ManageBlog = () => {
  const tabs = useMemo(
    () => [
      {
        key: 0,
        label: `Blog`,
        children: <ListBlog />
      },
      {
        key: 1,
        label: `Create a new post
        `,
        children: <CreateBlog labelForm="Create a new blog" />
      }
    ],
    []
  )
  return (
    <>
      <Head>
        <title>Blog</title>
      </Head>
      <MainHeadingLayout heading="Manage Blog">
        <TabsCustom tabs={tabs} />
      </MainHeadingLayout>
    </>
  )
}

export default ManageBlog
