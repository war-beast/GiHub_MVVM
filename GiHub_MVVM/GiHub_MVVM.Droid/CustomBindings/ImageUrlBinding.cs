using Android.Widget;
using GiHub_MVVM.Core.Common;
using GiHub_MVVM.Droid.CustomViews;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Platform.Platform;
using System;
using System.ComponentModel;
using System.Reflection;

namespace GiHub_MVVM.Droid.CustomBindings
{
    public class ImageUrlBinding : MvxPropertyInfoTargetBinding<MyImageView>
    {

        // used to figure out whether a subscription to MyPropertyChanged
        // has been made
        private bool _subscribed;

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public ImageUrlBinding(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo)
        {
        }

        // describes how to set MyProperty on MyView
        protected override void SetValueImpl(object target, object value)
        {
            var view = target as MyImageView;
            if (view == null) return;

            using (var avatarLoadr = new ImageLoader()) {
                var imageBitmap = avatarLoadr.GetImageBitmapFromUrl((string)value);
                view.SetImageBitmap(imageBitmap);
                view.Source = (string)value;
            }
        }

        // is called when we are ready to listen for change events
        public override void SubscribeToEvents()
        {
            var myView = View;
            if (myView == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - ImageView is null in ImageUrlBinding");
                return;
            }

            _subscribed = true;
            myView.PropertyChanged += HandleMyPropertyChanged;
        }

        private void HandleMyPropertyChanged(object sender, EventArgs e)
        {
            var myView = View;
            if (myView == null) return;

            FireValueChanged(myView.Source);
        }

        // clean up
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                var myView = View;
                if (myView != null && _subscribed)
                {
                    myView.PropertyChanged -= HandleMyPropertyChanged;
                    _subscribed = false;
                }
            }
        }
    }
}