using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace PharmacyApp.Application.Exception
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var body = contextFeature.Error.StackTrace + " , " + contextFeature.Error.InnerException.Message.ToString();

                        var json = JsonConvert.SerializeObject(contextFeature);
                        Log.Error(json);
                        // await email.SendEmail(toemail, body, subject, ccemail);

                    }
                });
            });
        }
    }
}
