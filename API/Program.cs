using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    //  AddFilter() ���˵�ָ���������־
                    builder.AddFilter("System", LogLevel.Warning);
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                    builder.ClearProviders();//�����Ĭ�ϵ���־���
                    builder.AddSerilog();//ע��Serilog
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
