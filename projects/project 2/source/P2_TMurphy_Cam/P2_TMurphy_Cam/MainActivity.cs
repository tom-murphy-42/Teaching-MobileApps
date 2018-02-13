using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using System;

namespace P2_TMurphy_Cam
{
    [Activity(Label = "P2_TMurphy_Cam", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Used to track the file that we're manipulating between functions
        /// </summary>
        public static Java.IO.File _file;

        /// <summary>
        /// Used to track the directory that we'll be writing to between functions
        /// </summary>
        public static Java.IO.File _dir;

        /// Used to pass bitmap info between functions for manipulation, until I test pointers
        Android.Graphics.Bitmap bitmap;
        Android.Graphics.Bitmap copyBitmap;
        ImageView imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
            }
        }

        /// <summary>
        /// Apparently, some android devices do not have a camera.  To guard against this,
        /// we need to make sure that we can take pictures before we actually try to take a picture.
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// Creates a directory on the phone that we can place our images
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraExample");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //android.support.v4.content.FileProvider
            //getUriForFile(getContext(), "com.mydomain.fileprovider", newFile);
            //FileProvider.GetUriForFile

            //The line is a problem line for Android 7+ development
            //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            StartActivityForResult(intent, 0);
        }

        // <summary>
        // Called automatically whenever an activity finishes
        // </summary>
        // <param name = "requestCode" ></ param >
        // < param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //Make image available in the gallery
            /*
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            */

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;

            if (bitmap != null)
            {
                //AC: workaround for not passing actual files
                //bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
                //copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);

                //AddRandomNoise();
                //HighContrast();
                //TempHolder();
                //TempHolder2();
            }

            bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            if (bitmap != null)
            {
                copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            }

            if (copyBitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
                bitmap = null;
                copyBitmap = null;
            }

            var gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += GridView_ItemClick;

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }

        private void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position == 0) // "ADD RED"
            {
                Toast.MakeText(this.ApplicationContext, "Add Red.", ToastLength.Short).Show();
            }
            else if (e.Position == 1) // "ADD GREEN"
            {
                Toast.MakeText(this.ApplicationContext, "Add Green.", ToastLength.Short).Show();
            }
            else if (e.Position == 2) // "ADD BLUE"
            {
                Toast.MakeText(this.ApplicationContext, "Add Blue.", ToastLength.Short).Show();
            }
            else if (e.Position == 3) // "TINT"
            {
                Toast.MakeText(this.ApplicationContext, "Add Tint.", ToastLength.Short).Show();
            }
            else if (e.Position == 4) // "ADD RANDOM NOISE"
            {
                AddRandomNoise();
            }
            else if (e.Position == 5) // "REMOVE RED"
            {
                RemoveRed();
            }
            else if (e.Position == 6) // "REMOVE GREEN"
            {
                RemoveGreen();
            }
            else if (e.Position == 7) // "REMOVE BLUE"
            {
                RemoveBlue();
            }
            else if (e.Position == 8) // "GRAYSCALE"
            {
                Grayscale();
            }
            else if (e.Position == 9) // "BLUR"
            {
                Toast.MakeText(this.ApplicationContext, "Blur.", ToastLength.Short).Show();
            }
            else if (e.Position == 10) // "NEGATE RED"
            {
                NegateRed();
            }
            else if (e.Position == 11) // "NEGATE GREEN"
            {
                NegateGreen();
            }
            else if (e.Position == 12) // "NEGATE BLUE"
            {
                NegateBlue();
            }
            else if (e.Position == 13) // "HIGH CONTRAST"
            {
                HighContrast();
            }
            else if (e.Position == 14) // "PIXELATE"
            {
                Toast.MakeText(this.ApplicationContext, "Pixelate.", ToastLength.Short).Show();
            }
            else if (e.Position == 15) // "FLIP HORIZONTALLY"
            {
                Toast.MakeText(this.ApplicationContext, "Flip Horizontally.", ToastLength.Short).Show();
            }
            else if (e.Position == 16) // "FLIP VERTICALLY"
            {
                Toast.MakeText(this.ApplicationContext, "Flip Vertically.", ToastLength.Short).Show();
            }
            else if (e.Position == 17) // "ROTATE 90 DEGREES"
            {
                Toast.MakeText(this.ApplicationContext, "Rotate 90 Degrees.", ToastLength.Short).Show();
            }
            else if (e.Position == 18) // "POINTILLISM"
            {
                Toast.MakeText(this.ApplicationContext, "Pointillism.", ToastLength.Short).Show();
            }
            else if (e.Position == 19) // "WOODGRAIN EFFECT"
            {
                Woodgrain();
            }
            else // Error
            {
                // The grid item clicked does not match one of the 20 initialized: {0-19}
                Toast.MakeText(this.ApplicationContext, "AN ERROR HAS OCCURED.", ToastLength.Short).Show();
            }
        }

        private void TempHolder() //
        {
            Toast.MakeText(this.ApplicationContext, "Method AddRandomNoise() has been called.", ToastLength.Short).Show();
            //this code adds random noise to each pixel in a picture (+/- 50 to rgb)
            Random rnd = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    int randVal = rnd.Next(-50, 51);
                    c.R = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.R.GetHashCode().ToString()), randVal));
                    c.G = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.G.GetHashCode().ToString()), randVal));
                    c.B = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.B.GetHashCode().ToString()), randVal));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void TempHolder2() //
        {
            //if (copyBitmap != null)
            //{
            //    imageView.SetImageBitmap(copyBitmap);
            //    imageView.Visibility = Android.Views.ViewStates.Visible;
            //    bitmap = null;
            //    copyBitmap = null;
            //}
        }

        private void RemoveRed() //
        {
            Toast.MakeText(this.ApplicationContext, "Method RemoveRed() has been called.", ToastLength.Short).Show();
            //this code removes all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void RemoveGreen() //
        {
            Toast.MakeText(this.ApplicationContext, "Method RemoveGreen() has been called.", ToastLength.Short).Show();
            //this code removes all green from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void RemoveBlue() //
        {
            Toast.MakeText(this.ApplicationContext, "Method RemoveBlue() has been called.", ToastLength.Short).Show();
            //this code removes all blue from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void NegateRed() //
        {
            Toast.MakeText(this.ApplicationContext, "Method NegateRed() has been called.", ToastLength.Short).Show();
            //this code negates all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = Convert.ToByte(255 - Convert.ToInt32(c.R.GetHashCode().ToString()));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void NegateGreen() //
        {
            Toast.MakeText(this.ApplicationContext, "Method NegateGreen() has been called.", ToastLength.Short).Show();
            //this code negates all green from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = Convert.ToByte(255 - Convert.ToInt32(c.G.GetHashCode().ToString()));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void NegateBlue() //
        {
            Toast.MakeText(this.ApplicationContext, "Method NegateBlue() has been called.", ToastLength.Short).Show();
            //this code negates all blue from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = Convert.ToByte(255 - Convert.ToInt32(c.B.GetHashCode().ToString()));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void AddRandomNoise() //
        {
            Toast.MakeText(this.ApplicationContext, "Method AddRandomNoise() has been called.", ToastLength.Short).Show();
            //this code adds random noise to each pixel in a picture (+/- 10 to rgb)
            Random rnd = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    int randVal = rnd.Next(-10, 11);
                    c.R = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.R.GetHashCode().ToString()), randVal));
                    c.G = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.G.GetHashCode().ToString()), randVal));
                    c.B = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.B.GetHashCode().ToString()), randVal));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void Woodgrain() //
        {
            Toast.MakeText(this.ApplicationContext, "Method Woodgrain() has been called.", ToastLength.Short).Show();
            //this code also adds random noise to each pixel in a picture, except in a less random sequence
            //this idea came from Alec. I helped him fix his random noise function, we discovered 'Woodgrain'
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Random rnd = new Random();
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    int randVal = rnd.Next(-10, 11);
                    c.R = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.R.GetHashCode().ToString()), randVal));
                    c.G = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.G.GetHashCode().ToString()), randVal));
                    c.B = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.B.GetHashCode().ToString()), randVal));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private int NoiseHelper(int color, int rndVal) //Ensure the noise doesn't push pixel's value out of bounds
        {
            color += rndVal;
            if (color > 255) color = 255;
            if (color < 0) color = 0;
            return color;
        }

        private void HighContrast() //
        {
            Toast.MakeText(this.ApplicationContext, "Method HighContrast() has been called.", ToastLength.Short).Show();
            //this code rounds each pixel's colors to 0 or 255 to create a high contrast image
            Random rnd = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = Convert.ToByte(HighContrastHelper(Convert.ToInt32(c.R.GetHashCode().ToString())));
                    c.G = Convert.ToByte(HighContrastHelper(Convert.ToInt32(c.G.GetHashCode().ToString())));
                    c.B = Convert.ToByte(HighContrastHelper(Convert.ToInt32(c.B.GetHashCode().ToString())));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private int HighContrastHelper(int color) //
        {
            if (color < 128) color = 0;
            else color = 255;
            return color;
        }

        private void Grayscale() //
        {
            Toast.MakeText(this.ApplicationContext, "Method Grayscale() has been called.", ToastLength.Short).Show();
            //this code averages the values of each pixel to result in a grayscale image
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    int avgVal = (Convert.ToInt32(c.R.GetHashCode().ToString()) +
                        Convert.ToInt32(c.G.GetHashCode().ToString()) +
                        Convert.ToInt32(c.B.GetHashCode().ToString())) / 3;
                    c.R = c.G = c.B = Convert.ToByte(avgVal);
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        // ---- Advanced Tasks ----

        private void FlipHorizontally() //
        {
            Toast.MakeText(this.ApplicationContext, "Method FlipHorizontally() has been called.", ToastLength.Short).Show();
        }

        private void FlipVertically() //
        {
            Toast.MakeText(this.ApplicationContext, "Method FlipVertically() has been called.", ToastLength.Short).Show();
        }

        private void Rotate90() //
        {
            Toast.MakeText(this.ApplicationContext, "Method Rotate90() has been called.", ToastLength.Short).Show();
        }

        private void Blur() //
        {
            Toast.MakeText(this.ApplicationContext, "Method Blur() has been called.", ToastLength.Short).Show();
        }

        private void Pixelate() //
        {
            Toast.MakeText(this.ApplicationContext, "Method Pixelate() has been called.", ToastLength.Short).Show();
        }
    }
}

