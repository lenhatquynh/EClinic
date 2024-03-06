import { IProfileDoctor } from "types/Profile.type"
import CardDoctor, { CardDoctorSkeleton } from "../components/card/CardDoctor"
interface IProps {
  isLoading: boolean
  data?: IProfileDoctor[]
}
const DoctorList = ({ isLoading, data }: IProps) => {
  return (
    <>
      <div className="grid grid-cols-1 gap-4 md:gap-8 lg:grid-cols-2">
        {!isLoading
          ? data?.map((doctor) => (
              <CardDoctor doctor={doctor} key={doctor.userID} />
            ))
          : Array(6)
              .fill(0)
              .map((_, index) => <CardDoctorSkeleton key={index} />)}
      </div>
    </>
  )
}

export default DoctorList
