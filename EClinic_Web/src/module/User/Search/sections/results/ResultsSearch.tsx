import CardForum from "module/User/components/CardForum"
import CardBlog from "module/User/Home/section/blog/components/CardBlog/CardBlog"
import CardService from "../../../Home/section/services/components/card/CardService"
import Link from "next/link"
import React, { PropsWithChildren } from "react"
import { IBlog } from "types/Blog"
import { IPost } from "types/Post"
import { ServicePackage } from "types/Service"
interface IProps {
  servicePackages?: ServicePackage[]
  blogs?: IBlog[]
  posts?: IPost[]
}
const ResultsSearch = ({ servicePackages, blogs, posts }: IProps) => {
  return (
    <div className="mt-6 space-y-8">
      <LayoutItem label="Forum" link="/forum">
        {posts?.length === 0 && (
          <div className="text-lg font-semibold text-h1">
            No results were found!
          </div>
        )}
        {posts?.map((item) => (
          <CardForum key={item.id} post={item} />
        ))}
      </LayoutItem>
      <LayoutItem label="Daily blogs" link="/blog">
        {blogs?.length === 0 && (
          <div className="text-lg font-semibold text-h1">
            No results were found!
          </div>
        )}
        {blogs?.map((item) => (
          <CardBlog key={item.id} blog={item} />
        ))}
      </LayoutItem>
      <LayoutItem label="Service Package" link="/services">
        {servicePackages?.length === 0 && (
          <div className="text-lg font-semibold text-h1">
            No results were found!
          </div>
        )}
        {servicePackages?.map((item) => (
          <CardService key={item.servicePackageID} servicePackage={item} />
        ))}
      </LayoutItem>
    </div>
  )
}
interface LayoutItemProps extends PropsWithChildren {
  label: string
  link: string
}
const LayoutItem = ({ children, label, link }: LayoutItemProps) => {
  return (
    <div className="shadow-sm background-primary">
      <div className="flex items-center justify-between mb-4">
        <h3 className="text-2xl font-semibold text-h1">{label}</h3>
        <Link href={link} className="text-primary hover:underline">
          Xem tất cả
        </Link>
      </div>
      <div className="grid grid-cols-2 gap-6 lg:grid-cols-4">{children}</div>
    </div>
  )
}

export default ResultsSearch
