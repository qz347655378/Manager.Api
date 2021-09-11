using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.IO;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var columnOptionsSection = Configuration.GetSection("Serilog:ColumnOptions");
            var sinkOptionsSection = Configuration.GetSection("Serilog:SinkOptions");
            var connectionStringName = Configuration.GetConnectionString("ManagerConnection");
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: connectionStringName,
                    sinkOptionsSection: sinkOptionsSection,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        AutoCreateSqlTable = true,
                        TableName = "SysLog"
                    },
                    appConfiguration: Configuration,
                    columnOptionsSection: columnOptionsSection)
                .CreateLogger();
            //  Log.Logger = new LoggerConfiguration().ReadFrom.CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //此处不加UseSerilog,加了会把系统级别的日志写入
                });

        private static IConfiguration Configuration
        {
            get
            {
                //set configuation file
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json").Build();
                return builder;
            }
        }
    }
}
