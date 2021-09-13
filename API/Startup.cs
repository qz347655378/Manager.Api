using API.Core.Autofac;
using API.Core.Exception;
using API.Core.Filters;
using API.Core.LogExtensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        //register autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacRegister());
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(c => c.AddPolicy("any",
                builder => builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            var connectionStr = Configuration.GetConnectionString("ManagerConnection");
            services.AddDbContextPool<ManagerDbContext>(options => options.UseSqlServer(connectionStr, c => c.MigrationsAssembly("Models")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manager.API", Version = "v1", Description = "Manager.API" });
                foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml"))
                {
                    c.IncludeXmlComments(file, true);
                }

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath ?? string.Empty, "API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(c => c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["JWTSetting:JWTIssuer"],
                    ValidAudience = Configuration["JWTSetting:JWTAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSetting:JWTKey"])),
                    ClockSkew = TimeSpan.Zero
                });
            services.AddControllers(options =>
            {
                options.Filters.Add<ActionFilter>();
            }).AddNewtonsoftJson(setup =>
            {

                //   setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//驼峰命名返回
                setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用
                //set default datetime format
                setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //默认日期格式化
                setup.SerializerSettings.ContractResolver = new NullToEmptyStringResolver();//替换null值为string.empty

            });

            services.AddMemoryCache(options => options.ExpirationScanFrequency = TimeSpan.FromMinutes(30));

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            //Add Task Schedule
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //关闭自带的参数校验
            services.Configure<ApiBehaviorOptions>(c =>
            {
                c.SuppressModelStateInvalidFilter = true;
            });

            //添加本地化支持
            services.AddLocalization();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpContextLog();
            app.UseExceptionHandle();
            app.UseIpFilter();

            app.UseSwagger();
            //Add Swagger Document
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));


            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();


            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //注册本地化
            var languageList = new[]
            {
                "zh","en"
            };

            app.UseRequestLocalization(c =>
            {
                c.SetDefaultCulture("zh");
                c.AddSupportedCultures(languageList);
                c.AddSupportedUICultures(languageList);
            });

            //Allow Corss-domain
            app.UseCors("any");
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireCors("any"));

        }
    }
}
