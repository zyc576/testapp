using Microsoft.AspNetCore.Builder;

namespace ZTunnel.Pmms.Api.Middleware
{
    public static class MiddlewareHelper
    {   
        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMildd>();
        }
    }
}
