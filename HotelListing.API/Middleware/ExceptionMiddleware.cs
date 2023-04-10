//using System.Net;

//namespace HotelListing.API.Middleware
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public ExceptionMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {

//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private Task HandleExceptionAsync(HttpContext context, Exception ex)
//        {
//            context.Response.ContentType = "application/json";
//            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
//        }
//    }

//    public class ErrorDetails
//    {
//        public string ErrorType { get; set; }
//        public string ErrorMessage { get; set; }
//    }
//}
