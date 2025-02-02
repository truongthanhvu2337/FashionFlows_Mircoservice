using Microsoft.AspNetCore.Builder;

namespace FashionFlows.BuildingBlock.Application.Middleware;

public static class ExceptionExtensions
{
    public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalException>();
    }
}

