using Microsoft.AspNetCore.Mvc;
using MiniBook.Server.Shared.Strings;
using System.Net;



namespace MiniBook.Server.Shared
{
    public static class ApiResultExtensions
    {

        public static IActionResult ErrorResult(this ControllerBase controller, ErrorCode errorCode, HttpStatusCode statusCode)
        {
            return JsonResult(new ApiResponse<object>((int)errorCode, ErrorResources.ResourceManager.GetString(errorCode.ToString())), statusCode);
        }
        public static IActionResult ErrorResult(this ControllerBase controller , ErrorCode errorCode)
        {
            return JsonResult(new ApiResponse<object>((int)errorCode, ErrorResources.ResourceManager.GetString(errorCode.ToString())), HttpStatusCode.BadRequest);
        }
        public static IActionResult OkResult<T>(this ControllerBase controller, T result)
        {
            return JsonResult(new ApiResponse<T>(result));
        }
        public static IActionResult OkResult(this ControllerBase controller)
        {
            return JsonResult(new ApiResponse<object>(true));
        }
        public static IActionResult OkResult(this ControllerBase controller, object result) 
        {
            return JsonResult(new ApiResponse<object>(result));
        }
        private static IActionResult JsonResult(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiJsonResult(result, statusCode);
        }
    }
}
