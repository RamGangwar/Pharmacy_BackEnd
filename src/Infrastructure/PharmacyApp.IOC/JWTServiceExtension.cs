using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using PharmacyApp.Domain.JWT;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace PharmacyApp.IOC
{
    public static class JWTServiceExtension
    {
        public static void AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
        
           services.Configure<JWTSetting>(configuration.GetSection("JWTSettings") );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    //o.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        var accessToken = context.Request.Query["access_token"];

                    //        // If the request is for our hub...
                    //        var path = context.HttpContext.Request.Path;
                    //        if (!string.IsNullOrEmpty(accessToken) &&
                    //            (path.StartsWithSegments("/NotificationHub")))
                    //        {
                    //            // Read the token out of the query string
                    //            context.Token = accessToken;
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //};

                    //o.Events = new JwtBearerEvents()
                    //{
                    //    OnAuthenticationFailed = c =>
                    //    {
                    //        c.NoResult();
                    //        c.Response.StatusCode = 500;
                    //        c.Response.ContentType = "text/plain";
                    //        return c.Response.WriteAsync(c.Exception.ToString());
                    //    },
                    //    OnChallenge = context =>
                    //    {

                    //        context.HandleResponse();
                    //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //        context.Response.ContentType = "application/json";
                    //        var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                    //        return context.Response.WriteAsync(result);
                    //    },
                    //    OnForbidden = context =>
                    //    {
                    //        context.Response.StatusCode = 403;
                    //        context.Response.ContentType = "application/json";
                    //        var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                    //        return context.Response.WriteAsync(result);
                    //    },
                    //};
                });
        }
    }
}
