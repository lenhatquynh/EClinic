import { useQueries } from "@tanstack/react-query"
import Card from "./Card"
import { QUERY_KEYS } from "shared/constant/constant"
import { bookingService } from "services/booking.service"
import { authService } from "services/auth.service"
import { paymentService } from "services/payment.service"

const HeaderCard = () => {
  const results = useQueries({
    queries: [
      {
        queryKey: [QUERY_KEYS.BOOKING.BOOKING],
        queryFn: bookingService.getStatisticsOverview,
        staleTime: Infinity
      },
      {
        queryKey: ["AUTH"],
        queryFn: authService.getStatisticsOverview,
        staleTime: Infinity
      },
      {
        queryKey: [QUERY_KEYS.PAYMENT],
        queryFn: paymentService.getStatisticsOverview,
        staleTime: Infinity
      }
    ]
  })

  return (
    <div className="grid justify-around grid-cols-3 gap-x-6">
      <Card
        isLoading={results[0].isLoading}
        data={results[0].data?.data}
        icon="/images/calendar.svg"
        title="Appointment"
      />
      <Card
        icon="/images/people.svg"
        title="New Patients"
        data={results[1].data?.data}
        isLoading={results[1].isLoading}
      />
      <Card
        isMoney={true}
        icon="/images/creditcard.svg"
        title="Total Earnings"
        data={results[2].data?.data}
        isLoading={results[2].isLoading}
      />
    </div>
  )
}

export default HeaderCard
