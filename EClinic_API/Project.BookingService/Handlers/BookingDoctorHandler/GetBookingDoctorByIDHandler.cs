using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Dtos.BookingDoctorDtos;
using Project.BookingService.Dtos.Other;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingDoctorHandler
{
    public class GetBookingDoctorByIDHandler : IRequestHandler<GetBookingDoctorByIDQuery, ObjectResult>
    {
        private readonly IBookingDoctorRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetBookingDoctorByIDHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;

        public GetBookingDoctorByIDHandler(IConfiguration configuration,IBookingDoctorRepository repository, IMapper mapper, ILogger<GetBookingDoctorByIDHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetBookingDoctorByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var BookingDoctor = await repository.GetBookingDoctor(x => x.BookingID == request.BookingDoctorID);
                if (BookingDoctor == null)
                {
                    return ApiResponse.NotFound("Booking Not Found");
                }
                var profileRes = await client.GetPatientProfileAsync(new GetPatientProfileRequest { ProfileID = BookingDoctor.ProfileID.ToString()});
                if (profileRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var BookingDetail = mapper.Map<BookingDoctorDetail>(BookingDoctor);
                BookingDetail.Slot = new DoctorSlotDtos();
                BookingDetail.Slot.SlotID = BookingDoctor.ScheduleID;
                BookingDetail.Slot.StartTime = BookingDoctor.DoctorSchedule.StartTime.ToString("HH:mm");
                BookingDetail.Slot.EndTime = BookingDoctor.DoctorSchedule.EndTime.ToString("HH:mm");
                BookingDetail.Profile = new ProfileDetail();
                BookingDetail.Profile.ProfileID = Guid.Parse(profileRes.ProfileID);
                BookingDetail.Profile.UserID= Guid.Parse(profileRes.UserID);
                BookingDetail.Profile.FirstName = profileRes.FirstName;
                BookingDetail.Profile.LastName = profileRes.LastName;
                BookingDetail.Profile.Avatar = profileRes.Avatar;
                BookingDetail.Profile.Gender = profileRes.Gender;
                BookingDetail.Profile.DateOfBirth = DateTime.Parse(profileRes.DateOfBirth);
                BookingDetail.Profile.Address = profileRes.Address;
                BookingDetail.Profile.Email = profileRes.Email;
                BookingDetail.Profile.Phone = profileRes.Phone;
                BookingDetail.Profile.BloodType = profileRes.BloodType;
                BookingDetail.Profile.Height = profileRes.Height;
                BookingDetail.Profile.Weight = profileRes.Weight;
                BookingDetail.Profile.RelationshipName = profileRes.Relationship;

                return ApiResponse.OK(BookingDetail);
            }catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
