using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.CommunicateService.Dtos.RoomTypeDtos;
using Project.CommunicateService.Queries;
using Project.CommunicateService.Repository.RoomTypeRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomTypeHandlers
{
    public class GetAllRoomTypeHandler : IRequestHandler<GetAllRoomTypeQuery, ObjectResult>
    {
        private readonly IRoomTypeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllRoomTypeHandler> logger;

        public GetAllRoomTypeHandler(IRoomTypeRepository repository, IMapper mapper, ILogger<GetAllRoomTypeHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetAllRoomTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var RoomTypes = await repository.GetAllAsync();
                var RoomTypeDtos = mapper.Map<List<RoomTypeDto>>(RoomTypes);
                return ApiResponse.OK<List<RoomTypeDto>>(RoomTypeDtos);
            }
            catch(Exception ex) 
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
