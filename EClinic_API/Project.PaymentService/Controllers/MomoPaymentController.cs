using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.PaymentService.Commands;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;

namespace Project.PaymentService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class MomoPaymentController : ControllerBase
    {
        private readonly IMediator mediator;

        public MomoPaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PaymentRequestForBookingPackage([FromQuery]BookingPackageDtos BookingPackageDtos)
        {
            return await mediator.Send(new GetPaymentURLForBookingPackageQuery(Data.PaymentService.Momo, BookingPackageDtos));
        }
        [HttpGet]
        public async Task<IActionResult> PaymentRequestForBookingDoctor([FromQuery] BookingDoctorDtos BookingDoctorDtos)
        {
            return await mediator.Send(new GetPaymentURLForBookingDoctorQuery(Data.PaymentService.Momo, BookingDoctorDtos));
        }
        [HttpGet]
        public async Task<IActionResult> PaymentReturnURl()
        {
            var queryParameters = HttpContext.Request.Query;
            var url = await mediator.Send(new CreatePaymentCommand(queryParameters, Data.PaymentService.Momo));
            return Redirect(url);
        }
    }
}
