using Android.Graphics;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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