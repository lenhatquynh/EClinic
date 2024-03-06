import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Typography
} from "@mui/material"
import { HiChevronDown } from "react-icons/hi2"

const Booking = () => {
  return (
    <div>
      <Accordion className="shadow-none border border-solid border-[#EAEAEA]">
        <AccordionSummary expandIcon={<HiChevronDown />}>
          <Typography>Đặt lịch tư vấn online</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse
            malesuada lacus ex, sit amet blandit leo lobortis eget.
          </Typography>
        </AccordionDetails>
      </Accordion>
    </div>
  )
}

export default Booking
