

//namespace WebApplication3.Middleware
//{
//    public class SessionTimeoutMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public SessionTimeoutMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var session = context.Session;
//            if (session.Keys.Count = 0) // No active session
//            {
//                context.Response.Redirect("/Login"); // Redirect to login page
//                return;
//            }
//            await _next(context);
//        }
//    }
//}
