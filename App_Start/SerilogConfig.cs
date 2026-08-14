using System.Web;
using Serilog;

namespace Condominio.Web.App_Start
{
    public static class SerilogConfig
    {
        public static void Configurar()
        {
            var rutaLogs = System.IO.Path.Combine(
                HttpRuntime.AppDomainAppPath, "Logs", "log-.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    path: rutaLogs,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
