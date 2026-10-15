using Microsoft.Extensions.Logging;
using MeTracker.Repositories;
using MeTracker.Services;
using MeTracker.ViewModels;
using MeTracker.Views;

namespace MeTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            try
            {
                SQLitePCL.Batteries_V2.Init();
            }
            catch { }

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ILocationRepository, LocationRepository>();
            builder.Services.AddSingleton<ILocationTrackingService, LocationTrackingService>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainView>();

            return builder.Build();
        }
    }
}
