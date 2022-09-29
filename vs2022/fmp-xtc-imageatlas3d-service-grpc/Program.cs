
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.56.0.  DO NOT EDIT!
//*************************************************************************************

using Microsoft.Extensions.Diagnostics.HealthChecks;
using XTC.FMP.MOD.ImageAtlas3D.App.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddGrpcHealthChecks()
                .AddCheck("ImageAtlas3D", () => HealthCheckResult.Healthy());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));

MyProgram.PreBuild(builder);
var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS,HEAD,PATCH");
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Max-Age", "100000");
    context.Response.Headers.Add("Access-Control-Expose-Headers", "Grpc-Status,Grpc-Message,Grpc-Encoding,Grpc-Accept-Encoding");
    if (context.Request.Method.ToUpper() == "OPTIONS")
    {
        return;
    }
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});
app.UseGrpcWeb();


app.MapGrpcService<DesignerService>().EnableGrpcWeb();

app.MapGrpcService<HealthyService>().EnableGrpcWeb();


app.MapGrpcHealthChecksService();
app.MapGet("/", () => "ImageAtlas3D");

IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

MyProgram.PreRun(app);
app.Run();

