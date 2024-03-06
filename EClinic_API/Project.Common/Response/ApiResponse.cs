using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Common.Response
{
    public static class ApiResponse
    {
        public static ObjectResult OK<T>(T data)
        {
            return new ObjectResult("OK")
            {
                StatusCode = StatusCodes.Status200OK,
                Value = new DataResponse<T>().Success(data)
            };
        }
        public static ObjectResult OK(string message)
        {
            return new ObjectResult("OK")
            {
                StatusCode = StatusCodes.Status200OK,
                Value = new NoDataResponse().Success(message)
            };
        }
        public static ObjectResult Created(string message)
        {
            return new ObjectResult("Created")
            {
                StatusCode = StatusCodes.Status201Created,
                Value = new NoDataResponse().Success(message)
            };
        }
        public static ObjectResult Created<T>(T data)
        {
            return new ObjectResult("Created")
            {
                StatusCode = StatusCodes.Status201Created,
                Value = new DataResponse<T>().Success(data)
            };
        }
        public static ObjectResult BadRequest(string message)
        {
            return new ObjectResult("BadRequest")
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Value = new NoDataResponse().Fail(message)
            };
        }
        public static ObjectResult Unauthorized(string message = "You are not authorized to access.")
        {
            return new ObjectResult("Unauthorized")
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Value = new NoDataResponse().Fail(message)
            };
        }
        public static ObjectResult Forbidden(string message = "Permission denied.")
        {
            return new ObjectResult("Forbidden")
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Value = new NoDataResponse().Fail(message)
            };
        }
        public static ObjectResult NotFound(string message)
        {
            return new ObjectResult("NotFound")
            {
                StatusCode = StatusCodes.Status404NotFound,
                Value = new NoDataResponse().Fail(message)
            };
        }
        public static ObjectResult InternalServerError()
        {
            return new ObjectResult("Internal Server Error")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Value = new NoDataResponse().Fail("Internal Server Error")
            };

        }


    }
}
