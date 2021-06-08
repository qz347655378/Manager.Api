using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Text;

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

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 直接用Autofac注册我们自定义的 
            builder.RegisterModule(new Core.AutofacRegister());
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //设置跨域
            services.AddCors(c => c.AddPolicy("any", builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            //添加数据库支持
            // services.AddScoped<DbContext,DAL.ManagerDbContext>();
            var connectionStr = Configuration.GetConnectionString("ManagerConnection");
            services.AddDbContextPool<ManagerDbContext>(options => options.UseSqlServer(connectionStr, c => c.MigrationsAssembly("Models")));

            //配置Serilog.AspNetCore 新的日志组件
            services.AddLogging(builder =>
            {
                var sinkOptionsSection = Configuration.GetSection("Serilog:SinkOptions");
                //  var columnOptionsSection = Configuration.GetSection("Serilog:ColumnOptions");
                builder.ClearProviders();
                //配置日志参数
                Log.Logger = new LoggerConfiguration().WriteTo.MSSqlServer(connectionStr, new MSSqlServerSinkOptions
                {
                    SchemaName = "dbo",
                    TableName = "SysLog",
                    AutoCreateSqlTable = true
                },
                    sinkOptionsSection: sinkOptionsSection,
                    appConfiguration: Configuration
                ).CreateLogger();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mananger.API", Version = "v1", Description = "Manager框架API" });
                foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml"))
                {
                    c.IncludeXmlComments(file, true);
                }

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //添加请求头配置
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //添加xml注释文档
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath ?? string.Empty, "API.xml");
                c.IncludeXmlComments(xmlPath);


            });
            //配置认证服务
            //引用Microsoft.AspNetCore.Authentication.JwtBearer
            services.AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["JWTSetting:JWTIssuer"],
                    ValidAudience = Configuration["JWTSetting:JWTAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSetting:JWTKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            //配置序列化 引用Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//驼峰命名返回
                setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用
                setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //默认日期格式化
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
