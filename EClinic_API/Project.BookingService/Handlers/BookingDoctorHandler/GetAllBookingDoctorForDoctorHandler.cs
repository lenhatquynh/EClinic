using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.BookingService.Dtos.BookingDoctorDtos;
using Project.BookingService.Dtos.Other;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingDoctorHandler
{
    public class GetAllBookingDoctorForDoctorHandler : IRequestHandler<GetAllBookingDoctorForDoctorQuery, ObjectResult>
    {
        private readonly IBookingDoctorRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllBookingDoctorForDoctorHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;
        public GetAllBookingDoctorForDoctorHandler(IConfiguration configuration, IBookingDoctorRepository repository, IMapper mapper, ILogger<GetAllBookingDoctorForDoctorHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }
        public async Task<ObjectResult> Handle(GetAllBookingDoctorForDoctorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingDoctor = await repository.GetAllBookingDoctor(x => x.BookingStatus == request.BookingStatus && x.DoctorID == Guid.Parse(request.userID));
                if (bookingDoctor == null)
                {
                    return ApiResponse.NotFound("Booking Doctor Not Found");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = bookingDoctor.Count;
                bookingDoctor = bookingDoctor
                    .OrderByDescending(x => x.BookingTime)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<string> listUserProifleID = bookingDoctor.Select(s => s.ProfileID.ToString()).ToList();
                List<string> listDoctorProifleID = bookingDoctor.Select(s => s.DoctorID.ToString()).ToList();
                GetDoctorAndUserProfileRequest ProfileRequest = new GetDoctorAndUserProfileRequest();
                ProfileRequest.DoctorProfileIDs.AddRange(listDoctorProifleID);
                ProfileRequest.UserProfileIDs.AddRange(listUserProifleID);
                var profileRes = await client.GetDoctorAndUserProfileAsync(ProfileRequest);
                if (profileRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var Proifles = profileRes.Profile;
                var bookingDoctorDtos = mapper.Map<List<BookingDoctorDTO>>(bookingDoctor);
                for (int i = 0; i < bookingDoctorDtos.Count(); i++)
                {
                    bookingDoctorDtos[i].DoctorProfile = new ProfileDtos();
                    bookingDoctorDtos[i].DoctorProfile.ProfileID = Guid.Parse(Proifles[i].DoctorProfileID);
                    bookingDoctorDtos[i].DoctorProfile.UserID = Guid.Parse(Proifles[i].DoctorUserID);
                    bookingDoctorDtos[i].DoctorProfile.FirstName = Proifles[i].DoctorFirstName;
                    bookingDoctorDtos[i].DoctorProfile.LastName = Proifles[i].DoctorLastName;
                    bookingDoctorDtos[i].DoctorProfile.Avatar = Proifles[i].DoctorAvatar;
                    bookingDoctorDtos[i].UserProfile = new ProfileDtos();
                    bookingDoctorDtos[i].UserProfile.ProfileID = Guid.Parse(Proifles[i].UserProfileID);
                    bookingDoctorDtos[i].UserProfile.UserID = Guid.Parse(Proifles[i].UserUserID);
                    bookingDoctorDtos[i].UserProfile.FirstName = Proifles[i].UserFirstName;
                    bookingDoctorDtos[i].UserProfile.LastName = Proifles[i].UserLastName;
                    bookingDoctorDtos[i].UserProfile.Avatar = Proifles[i].UserAvatar;
                    bookingDoctorDtos[i].Slot = new DoctorSlotDtos();
                    bookingDoctorDtos[i].Slot.SlotID = bookingDoctor[i].DoctorSchedule.ScheduleID;
                    bookingDoctorDtos[i].Slot.StartTime = bookingDoctor[i].DoctorSchedule.StartTime.ToString("HH:mm");
                    bookingDoctorDtos[i].Slot.EndTime = bookingDoctor[i].DoctorSchedule.EndTime.ToString("HH:mm");
                    bookingDoctorDtos[i].BookingCalendar = bookingDoctor[i].DoctorSchedule.DoctorCalendar.Time;
                }
                return ApiResponse.OK(bookingDoctorDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
