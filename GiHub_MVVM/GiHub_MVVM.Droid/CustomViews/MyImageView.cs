using Android.Content;
using Android.Widget;
using System.ComponentModel;

namespace GiHub_MVVM.Droid.CustomViews
{
    public class MyImageView : ImageView, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Source { get; set; } = "";

        public void OnSourceChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyImageView(Context context) : base(context)
        {
            
        }
    }
}