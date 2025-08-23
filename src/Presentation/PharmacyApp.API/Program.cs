using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PharmacyApp.Application;
using PharmacyApp.IOC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
//builder.Services.AddSignalR();


builder.Services.AddApplicationDependencyInjection();

builder.Services.AddMediatRDependencyInjection();
builder.Services.AddServiceDependency(builder);
builder.AddSerilogDependencyInjection();
builder.Services.AddEmailService(builder.Configuration);
//builder.Services.AddGEOLocation(builder.Configuration);
builder.Services.AddEncryptionService(builder.Configuration);
builder.Services.AddJWTService(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000")
                //.AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());
});

#region content Negotiation 
builder.Services.AddMvc(option => option.RespectBrowserAcceptHeader = true);
#endregion


builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PharmacyApp API",
        Version = "v1"
    });
    s.IgnoreObsoleteActions();
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

    string xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
    s.IncludeXmlComments(xmlpath);
});

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    NullValueHandling = NullValueHandling.Ignore
};




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("ApiCorsPolicy");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger().UseSwaggerUI(c =>
{
    {
        string swaggerjsonbasepath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerjsonbasepath}/swagger/v1/swagger.json", "PharmacyApp.API");
    }

});
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("API Working!");
    });
    endpoints.MapControllers();

});


app.Run();
