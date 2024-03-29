using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Core31.Library.Services.Redis;
using WebNet6.Middlewares;
using WebNet6.Hubs;
using Core31.Library.Services.RabbitMQ;
using Core31.Library.Models.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebNet6.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Core31.Library.Services.System;
using Core31.Library.Authentications.JwtTestUser;
using Core31.Library;
using Core31.Library.Services.User;
using Net6.Library;
using Net6.Library.Services.MessageBoard;
using System.Diagnostics;
using WebNet6.EventListeners;
using Microsoft.Extensions.Logging;

namespace WebNet6
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSetting>(Configuration);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                // c.SwaggerDoc("v1", new OpenApiInfo
                // {
                //     Version = "v1",
                //     Title = "Swagger",
                //     Description = "A simple example ASP.NET Core Web API",
                // });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IRedisService>(sp => new RedisService(Environment.GetEnvironmentVariable("RedisConnectString")));


            services.AddMemoryCache();
            services.AddSignalR();

            var rabbitMQServiceParas = new RabbitMQServiceParas(
                            hostName: Environment.GetEnvironmentVariable("RabbitMQHostName"),
                            userName: Environment.GetEnvironmentVariable("RabbitMQUserName"),
                            password: Environment.GetEnvironmentVariable("RabbitMQPassword"),
                            queueName: Environment.GetEnvironmentVariable("RabbitMQQueueName")
                        );

            services.AddSingleton<RabbitMQServiceParas>(rabbitMQServiceParas);
            services.AddSingleton<IRabbitMQPublishService, RabbitMQPublishService>();
            services.AddSingleton(sp => new RabbitMQSubscribeService(sp, rabbitMQServiceParas, RabbitMQHub.Subscribe));

            services.AddDbContext<TodoItemContext>(options =>
                options.UseMySQL(Environment.GetEnvironmentVariable("MySQLConnectString"))
            );

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(2, 1);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<SwaggerVersion>();

            services.AddSingleton<ISystemService, SystemService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IJwtTestUserAuth, JwtTestUserAuth>();
            services.AddSingleton(new Core31LibraryParas(
                jwtTestUserKey: Environment.GetEnvironmentVariable("JwtTestUserAuthKey"))
            );

            services.AddScoped<IUserService, UserInMemoryService>();


            services.AddSingleton(new Net6Setting(Environment.GetEnvironmentVariable("PostgresConnectionString")));
            services.AddScoped<IMessageBoardService, MessageBoardService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime lifetime,
            IApiVersionDescriptionProvider apiVersionprovider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                    foreach (var description in apiVersionprovider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseMiddleware<HandleExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });

                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapHub<RabbitMQHub>("/RabbitMQHub");
            });

            var diagnosticSource = app.ApplicationServices.GetRequiredService<DiagnosticListener>();
            var badRequestListener = new BadRequestEventListener(diagnosticSource, (badRequestExceptionFeature) =>
            {
                using var logScope = app.ApplicationServices.CreateScope();
                var logger = logScope.ServiceProvider.GetService<ILogger<BadRequestEventListener>>();
                logger.LogError(badRequestExceptionFeature.Error, "Bad request received");
            });

            if (Environment.GetEnvironmentVariable("IsRabbitMQSubscribeServiceRun") == "true")
            {
                lifetime.ApplicationStarted.Register(() =>
                {
                    app.ApplicationServices.GetService<RabbitMQSubscribeService>();
                });
            }
        }
    }
}
