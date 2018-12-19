using NUnit.Framework;
using MvvmCross.Test.Core;
using Moq;
using GiHub_MVVM.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Localization;
using GiHub_MVVM.Core.Helpers;
using System.Threading.Tasks;
using Android.Content;
using GiHub_MVVM.Core.Common;

namespace Tests
{
    [TestFixture]
    public class SettingTest : MvxIoCSupportingTest
    {
        IMvxNavigationService _navService;

        protected override void AdditionalSetup()
        {
            var navService = new Mock<IMvxNavigationService>();
            Ioc.RegisterSingleton<IMvxNavigationService>(navService.Object);

            var builder = new TextProviderBuilder();
            Ioc.RegisterSingleton<IMvxTextProvider>(builder.TextProvider);

            var settService = new Mock<ISettingsService>();
            settService.Setup(a => a.GetListCount()).Returns(10);//10 дефолтное значение, но можно и поменять и не зфбыть поменять в Assert в тестовом методе
            Ioc.RegisterSingleton<ISettingsService>(settService.Object);

            var pref = new Mock<ISharedPreferences>();
            pref.Setup(a => a.GetInt("repoListCount", 10)).Returns(3);//Второй параметр =10, потому что в классе AppSettings задано дефолтное значение 10
            Ioc.RegisterSingleton(pref.Object);
        }

        [Test]
        public void TestViewModel()
        {
            base.Setup();

            _navService = Mvx.Resolve<IMvxNavigationService>();
            SettingsViewModel settingsService = new SettingsViewModel(_navService);
            int count = settingsService.RepoCount;
            Assert.AreEqual(10, count);
        }

        [Test]
        public void TestGetPreferences()
        {
            base.Setup();
            int count = AppSettings.RepoListCount;
            Assert.AreEqual(3, count);
        }
    }
}