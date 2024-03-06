using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Dtos.BookingPackageDTOs;
using Project.BookingService.Dtos.Other;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingPackageHandler
{
    public class GetBookingPackageByIDHandler : IRequestHandler<GetBookingPackageByIDQuery, ObjectResult>
    {
        private readonly IBookingPackageRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetBookingPackageByIDHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;

        public GetBookingPackageByIDHandler(IConfiguration configuration, IBookingPackageRepository repository, IMapper mapper, ILogger<GetBookingPackageByIDHandler> logger)
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

        public async Task<ObjectResult> Handle(GetBookingPackageByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingPackage = await repository.GetAsync(x => x.BookingID == request.BookingPackageID);
                if (bookingPackage == null)
                {
                    return ApiResponse.NotFound("Booking Package Not Found");
                }
                var Proifles = await client.GetPatientProfileAsync(new GetPatientProfileRequest { ProfileID = bookingPackage.ProfileID.ToString() });
                if (Proifles == null)
                {
                    return ApiResponse.InternalServerError();
                }
                List<string> listServicePackageID = new List<string>();
                listServicePackageID.Add(bookingPackage.ServicePackageID.ToString());
                GetAllServicePackageRequest ServiceRequest = new GetAllServicePackageRequest();
                ServiceRequest.ServicePackageIDs.AddRange(listServicePackageID);
                var serviceRes = await serviceClient.GetAllServicePackageAsync(ServiceRequest);
                if (serviceRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var ServicePackages = serviceRes.ServicePackage;
                var bookingPackageDtos = mapper.Map<BookingPackageDetail>(bookingPackage);

                bookingPackageDtos.Profile = new ProfileDetail();
                bookingPackageDtos.Profile.ProfileID = Guid.Parse(Proifles.ProfileID);
                bookingPackageDtos.Profile.UserID = Guid.Parse(Proifles.UserID);
                bookingPackageDtos.Profile.FirstName = Proifles.FirstName;
                bookingPackageDtos.Profile.LastName = Proifles.LastName;
                bookingPackageDtos.Profile.Avatar = Proifles.Avatar;
                bookingPackageDtos.Profile.Gender = Proifles.Gender;
                bookingPackageDtos.Profile.DateOfBirth = DateTime.Parse(Proifles.DateOfBirth);
                bookingPackageDtos.Profile.Address = Proifles.Address;
                bookingPackageDtos.Profile.Email = Proifles.Email;
                bookingPackageDtos.Profile.Phone = Proifles.Phone;
                bookingPackageDtos.Profile.BloodType = Proifles.BloodType;
                bookingPackageDtos.Profile.Height = Proifles.Height;
                bookingPackageDtos.Profile.Weight = Proifles.Weight;
                bookingPackageDtos.Profile.RelationshipName = Proifles.Relationship;
                bookingPackageDtos.Service = new ServiceInformation();
                bookingPackageDtos.Service.ServicePackageID = Guid.Parse(ServicePackages[0].ServicePackageID);
                bookingPackageDtos.Service.ServicePackageName = ServicePackages[0].ServicePackageName;
                bookingPackageDtos.Service.Image = ServicePackages[0].Image;
                return ApiResponse.OK(bookingPackageDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
