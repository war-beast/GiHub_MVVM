using Android.App;
using Android.Content;
using Android.Preferences;
using MvvmCross.Platform;

namespace GiHub_MVVM.Core.Common
{
    public static class AppSettings
    {
        private static int defaultCount = 10;
        static ISharedPreferences sharedPreferences;

        private static int _repoListCount;
        public static int RepoListCount
        {
            get
            {                
                _repoListCount = sharedPreferences.GetInt("repoListCount", defaultCount);
                return _repoListCount;
            }
            set
            {
                if (_repoListCount != value)
                {
                    _repoListCount = value;
                    ISharedPreferencesEditor preferencesEditor = sharedPreferences.Edit();
                    preferencesEditor.PutInt("repoListCount", _repoListCount);
                    preferencesEditor.Commit();
                }
            }
        }

        static AppSettings()
        {
            sharedPreferences = Mvx.Resolve<ISharedPreferences>();
        }
    }
}