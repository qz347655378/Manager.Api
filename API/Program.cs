using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using Serilog.Sinks.MSSqlServer;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var columnOptionsSection = Configuration.GetSection("Serilog:ColumnOptions");
            var sinkOptionsSection = Configuration.GetSection("Serilog:SinkOptions");
            var connectionStringName = Configuration.GetConnectionString("ManagerConnection");
            Log.Logger = new LoggerConfiguration()
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
                });

        private static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json").Build();
                return builder;
            }
        }
    }
}
