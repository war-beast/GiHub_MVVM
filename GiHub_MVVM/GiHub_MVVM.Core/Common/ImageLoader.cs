using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GiHub_MVVM.Core.Common
{
    public class ImageLoader : IDisposable
    {
        Bitmap imageBitmap = null;

        public ImageLoader()
        {

        }

        public void Dispose()
        {
            if(imageBitmap != null)
                imageBitmap.Dispose();
        }

        public Bitmap GetImageBitmapFromUrl(string url)
        {
            byte[] imageBytes = null;

            using (var webClient = new HttpClient())
            {
                try
                {
                    Task.Run(async () => {
                        var response = await webClient.GetStreamAsync("http://somesite.com");
                        response.Read(imageBytes, 0, (int)response.Length);
                    });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }


    }
}