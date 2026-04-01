using Microsoft.Extensions.Logging;

using ProductivityApp.ViewModels;
using ProductivityApp.Views;
using ProductivityApp.DAL;
using ProductivityApp.Services;

namespace ProductivityApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Singleton - Global static, it creates at once
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            // Transient, create it every single time when we navigate 
            // creates and then destroys
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();

            builder.Services.AddTransient<NewGoalPage>();
            builder.Services.AddTransient<NewGoalViewModel>();

            // Singleton service of the type specified in IGoalService
            // with an implementation type specified in GoalService
            builder.Services.AddSingleton<IGoalService, GoalService>();

            // Singleton service of the type specified in IGoalRepository
            // with an implementation type specified in GoalRepository
            builder.Services.AddSingleton<IGoalRepository, GoalRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
