using Serilog;
using Serilog.Events;

namespace OA_Core.Api.Configuration
{
	public static class SerilogExtension
	{
		public static void AddSerilogApi(WebApplicationBuilder builder) 
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business Error"))
				.WriteTo.File($@"Logs\Log-.txt",
				rollingInterval: RollingInterval.Day,
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
				.CreateLogger();
			builder.Logging.ClearProviders();
			builder.Logging.AddSerilog();
			builder.Host.UseSerilog(Log.Logger);
		}
	}
}
