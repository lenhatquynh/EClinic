import UserLayout from "layout/User/UserLayout"
import HomePage from "module/User/Home/HomePage"
// import { GetServerSideProps } from "next"
import { NextPageWithLayout } from "./page"

// export const getServerSideProps: GetServerSideProps = async (context) => {
//   // const cookies = context.req.cookies
//   return {
//     props: {
//       test: ""
//     }
//   }
// }

const Home: NextPageWithLayout = () => {
  return <HomePage />
}
Home.getLayout = (page) => {
  return <UserLayout>{page}</UserLayout>
}
export default Home
