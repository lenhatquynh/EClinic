import UserSecondaryLayout from "layout/User/UserSecondaryLayout"
import Head from "next/head"
import { useTranslation } from "react-i18next"
import { IBreadcrum } from "types/Base.type"
import DetailDoctor from "./sections/DetailDoctor"

const DoctorDetailPage = () => {
  const { t } = useTranslation(["base", "ser"])

  const breadrums: IBreadcrum[] = [
    { label: t("base:pages.home"), href: "/" },
    { label: t("base:pages.doctors"), href: "/doctors" },
    { label: t("base:pages.detail") }
  ]
  return (
    <>
      <Head>
        <title>Doctor page</title>
      </Head>
      <UserSecondaryLayout breadrums={breadrums}>
        <section className="flex flex-col mt-6 gap-x-10">
          <DetailDoctor />
        </section>
      </UserSecondaryLayout>
    </>
  )
}

export default DoctorDetailPage
