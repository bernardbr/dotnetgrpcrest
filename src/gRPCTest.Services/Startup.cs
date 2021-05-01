using Grpc.HealthCheck;
using gRPCTest.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace gRPCTest.Services
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<HealthCheckService>();
                endpoints.MapGet("/", async context =>
                {
                    await context
                        .Response
                        .WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGrpc();

            services
                .AddHealthChecks();

            services
                .AddGrpcHttpApi();

            services.AddSingleton<HealthServiceImpl>();
            services.AddSingleton<HealthCheckService>();

            services.AddHostedService<StatusService>();

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                });

            services
                .AddGrpcSwagger();
        }
    }
}
