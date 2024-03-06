using Grpc.Core;
using MediatR;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Protos;
using Project.IdentityService.Queries;

namespace Project.IdentityService.Service
{
    public class UserDataService : Protos.UserService.UserServiceBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<UserDataService> logger;

        public UserDataService(IMediator mediator, ILogger<UserDataService> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new CreateUserCommand(request.Email, request.Role, request.Enabled));
                var res = new CreateUserResponse();
                if (result == Guid.Empty)
                {
                    res.IsSuccess = false;
                    return res;
                }
                res.IsSuccess = true;
                res.UserID = result.ToString();
                return res;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                var res = new CreateUserResponse();
                return res;
            }
        }

        public override async Task<GetAllUserWithRoleResponse> GetAllUserWithRole(GetAllUserWithRoleRequest request, ServerCallContext context)
        {
            try
            {
                var User = await mediator.Send(new GetAllUserWithRoleQuery(request.Role));
                GetAllUserWithRoleResponse getAllUser = new GetAllUserWithRoleResponse();
                List<GetUserRoleResponse> GetUserWithRole = new List<GetUserRoleResponse>();
                foreach (var item in User)
                {
                    GetUserWithRole.Add(new GetUserRoleResponse { UserID = item.UserID.ToString(), Enabled = item.Enabled });
                }
                getAllUser.User.AddRange(GetUserWithRole);
                return getAllUser;
            }
            catch
            {
                return new GetAllUserWithRoleResponse();
            }
        }

        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            try
            {
                var UserID = Guid.Parse(request.UserID);
                var result = await mediator.Send(new GetUserEnabledQuery(UserID));
                GetUserResponse getUser = new GetUserResponse { Enabled = Convert.ToBoolean(result) };
                return getUser;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            try
            {
                var userID = Guid.Parse(request.UserID);
                var result = await mediator.Send(new UpdateUserCommand(userID, request.Enabled,request.Email));
                var res = new UpdateUserResponse();
                if (!result) { throw new Exception("Update User Error"); }
                res.IsSuccess = result;
                return res;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                var res = new UpdateUserResponse();
                return res;
            }
        }
    }
}
