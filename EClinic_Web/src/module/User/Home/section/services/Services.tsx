import CustomButton from "components/User/Button"
import { useTranslation } from "react-i18next"
import CardService from "./components/card"
import { useRouter } from 'next/router';
import { useGetAllServicePackageQuery } from "hooks/query/service/useService"
import { ServicePackage } from "types/Service"

const Services = () => {
  const { t } = useTranslation("home")
  const router = useRouter();
  const { data } = useGetAllServicePackageQuery({
    pageNumber: 1,
    pageSize: 4
  })
  const servicePackages: ServicePackage[] = data?.data?.data as ServicePackage[]
  return (
    <section className="flex flex-col w-full ">
      <h3 className="text-center heading-section ">{t("service.title")}</h3>
      <div className="relative grid grid-cols-1 pt-8 gap-y-2 md:gap-x-4 md:grid-cols-4">
        {
          servicePackages?.map((servicePackage) => (
            <CardService key={servicePackage.servicePackageID} servicePackage={servicePackage} />
          ))
        }
      </div>
      <CustomButton kind="secondary" className="max-w-[200px] mx-auto mt-6" onClick={() => { router.push('/services'); }}>
        {t("service.btn")}
      </CustomButton>
    </section>
  )
}

export default Services
