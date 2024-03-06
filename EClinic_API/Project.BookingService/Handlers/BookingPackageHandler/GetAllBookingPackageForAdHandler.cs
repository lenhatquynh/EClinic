using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.BookingService.Dtos.BookingPackageDTOs;
using Project.BookingService.Dtos.Other;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingPackageHandler
{
    public class GetAllBookingPackageForAdHandler : IRequestHandler<GetAllBookingPackageForAdQuery, ObjectResult>
    {
        private readonly IBookingPackageRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllBookingPackageForAdHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;
        public GetAllBookingPackageForAdHandler(IConfiguration configuration, IBookingPackageRepository repository, IMapper mapper, ILogger<GetAllBookingPackageForAdHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
            serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
        }

        public async Task<ObjectResult> Handle(GetAllBookingPackageForAdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingPackage = await repository.GetAllAsync(x =>x.BookingStatus == request.BookingStatus);
                if (bookingPackage == null)
                {
                    return ApiResponse.NotFound("Booking Package Not Found");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = bookingPackage.Count;
                bookingPackage = bookingPackage
                    .OrderByDescending(x => x.BookingTime)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<string> listProifleID = bookingPackage.Select(s => s.ProfileID.ToString()).ToList();
                GetAllUserProfileRequest ProfileRequest = new GetAllUserProfileRequest();
                ProfileRequest.ProfileIDs.AddRange(listProifleID);
                var profileRes = await client.GetAllUserProfileAsync(ProfileRequest);
                if (profileRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var Proifles = profileRes.Profile;
                List<string> listServicePackageID = bookingPackage.Select(s => s.ServicePackageID.ToString()).ToList();
                GetAllServicePackageRequest ServiceRequest = new GetAllServicePackageRequest();
                ServiceRequest.ServicePackageIDs.AddRange(listServicePackageID);
                var serviceRes = await serviceClient.GetAllServicePackageAsync(ServiceRequest);
                if (serviceRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var ServicePackages = serviceRes.ServicePackage;
                var bookingPackageDtos = mapper.Map<List<BookingPackageDTO>>(bookingPackage);
                for (int i = 0; i < bookingPackageDtos.Count(); i++)
                {
                    bookingPackageDtos[i].Profile = new ProfileDtos();
                    bookingPackageDtos[i].Profile.ProfileID = Guid.Parse(Proifles[i].ProfileID);
                    bookingPackageDtos[i].Profile.UserID = Guid.Parse(Proifles[i].UserID);
                    bookingPackageDtos[i].Profile.FirstName = Proifles[i].FirstName;
                    bookingPackageDtos[i].Profile.LastName = Proifles[i].LastName;
                    bookingPackageDtos[i].Profile.Avatar = Proifles[i].Avatar;
                    bookingPackageDtos[i].Service = new ServiceInformation();
                    bookingPackageDtos[i].Service.ServicePackageID = Guid.Parse(ServicePackages[i].ServicePackageID);
                    bookingPackageDtos[i].Service.ServicePackageName = ServicePackages[i].ServicePackageName;
                    bookingPackageDtos[i].Service.Image = ServicePackages[i].Image;
                }
                return ApiResponse.OK(bookingPackageDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
