using Microsoft.Extensions.Configuration;
using System.Reflection;

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

		return builder.Build();
	}
}
