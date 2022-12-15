using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Diagnostics;
using RentElectroScooter.UI.ViewModels;
using RentElectroScooter.UI.Services;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using System.Text;
using RentElectroScooter.UI.Views;
using RentElectroScooter.UI.Views.Pages;

namespace RentElectroScooter.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var a = Assembly.GetExecutingAssembly();
        using var appsettingsStream = a.GetManifestResourceStream("RentElectroScooter.UI.appsettings.json");
        var builder = MauiApp.CreateBuilder();

        builder.Configuration.AddJsonStream(appsettingsStream);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Logging.AddSerilog(SetupLogger(builder.Configuration), dispose: true);
        builder.Services.AddSingleton<MainPage>()
            .AddTransient<UserProfilePage>()
            .AddScoped<SignInContentView>()
            .AddScoped<RegisterContentView>()
            .AddScoped<MainPageVM>()
            .AddScoped<UserVM>();

        builder.Services.AddSingleton<Session>()
            .AddScoped<ElectroScooterService>()
            .AddScoped<UserService>();

        builder.Services.AddTransient(services => services.GetService<ILoggerProvider>().CreateLogger(string.Empty));

        Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
        Routing.RegisterRoute(nameof(ESAdditionalInfoPage), typeof(ESAdditionalInfoPage));

        return builder.Build();
    }

    private static Serilog.ILogger SetupLogger(IConfiguration configuration)
    {
        var flushInterval = new TimeSpan(0, 1, 0);
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", GetLogLevel(configuration["Logging:LogLevel:Microsoft"]))
            .WriteTo.File(Path.Combine(FileSystem.AppDataDirectory, "log.txt"), flushToDiskInterval: flushInterval,
                encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day);
#if DEBUG
        loggerConfig.MinimumLevel.Debug();
#else
        loggerConfig.MinimumLevel.Warning();
#endif

        return loggerConfig.CreateLogger();
    }

    private static LogEventLevel GetLogLevel(string logLevel) => logLevel switch
    {
        "Debug" => LogEventLevel.Debug,
        "Information" => LogEventLevel.Information,
        "Error" => LogEventLevel.Error,
        "Fatal" => LogEventLevel.Fatal,
        "Warning" => LogEventLevel.Warning,
        _ => LogEventLevel.Verbose,
    };
}
