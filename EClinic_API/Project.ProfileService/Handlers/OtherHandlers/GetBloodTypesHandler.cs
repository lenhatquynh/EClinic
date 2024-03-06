using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.ProfileService.Queries;

namespace Project.ProfileService.Handlers.OtherHandlers
{
    public class GetBloodTypesHandler : IRequestHandler<GetBloodTypesQuery, ObjectResult>
    {
        public async Task<ObjectResult> Handle(GetBloodTypesQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            List<string> bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return ApiResponse.OK<List<string>>(bloodTypes);
        }
    }
}
