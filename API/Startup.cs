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
            // ֱ����Autofacע�������Զ���� 
            builder.RegisterModule(new Core.AutofacRegister());
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //���ÿ���
            services.AddCors(c => c.AddPolicy("any", builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            //������ݿ�֧��
            // services.AddScoped<DbContext,DAL.ManagerDbContext>();
            var connectionStr = Configuration.GetConnectionString("ManagerConnection");
            services.AddDbContextPool<ManagerDbContext>(options => options.UseSqlServer(connectionStr, c => c.MigrationsAssembly("Models")));

            //����Serilog.AspNetCore �µ���־���
            services.AddLogging(builder =>
            {
                var sinkOptionsSection = Configuration.GetSection("Serilog:SinkOptions");
                //  var columnOptionsSection = Configuration.GetSection("Serilog:ColumnOptions");
                builder.ClearProviders();
                //������־����
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mananger.API", Version = "v1", Description = "Manager���API" });
                foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml"))
                {
                    c.IncludeXmlComments(file, true);
                }

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //�������ͷ����
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //���xmlע���ĵ�
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath ?? string.Empty, "API.xml");
                c.IncludeXmlComments(xmlPath);


            });
            //������֤����
            //����Microsoft.AspNetCore.Authentication.JwtBearer
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
            //�������л� ����Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//�շ���������
                setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //����ѭ������
                setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //Ĭ�����ڸ�ʽ��
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
