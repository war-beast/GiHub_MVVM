using System;
using Android.App;
using Android.Views.InputMethods;

namespace GiHub_MVVM.Droid.Activities
{
    public static class ActivityExtensions
    {
        public static void HideSoftKeyboard(this Activity activity)
        {
            if (activity.CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Android.Content.Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);

            activity.CurrentFocus.ClearFocus();
        }
    }
}
