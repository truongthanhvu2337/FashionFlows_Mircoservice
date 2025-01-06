using Microsoft.AspNetCore.Builder;

namespace PersonalScheduling.BuildingBlock.Application.Middleware;

public static class ExceptionExtensions
{
    public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalException>();
    }
}

