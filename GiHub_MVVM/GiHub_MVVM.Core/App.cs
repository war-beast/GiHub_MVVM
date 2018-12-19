using Android.App;
using Android.Content;
using Android.Preferences;
using GiHub_MVVM.Core.Helpers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.JsonLocalization;

namespace GiHub_MVVM.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // Register localization services
            var builder = new TextProviderBuilder();
            Mvx.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            Mvx.RegisterSingleton<IMvxTextProvider>(builder.TextProvider);

            // Construct custom application start object
            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            var appStart = Mvx.Resolve<IMvxAppStart>();

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            Mvx.RegisterSingleton<ISharedPreferences>(sharedPreferences);

            // register the appstart object
            RegisterAppStart(appStart);
        }
    }
}
