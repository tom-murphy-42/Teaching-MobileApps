using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android.Provider;
using System;
using System.Collections.Generic;

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

            bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            ReplaceDisplay();

            // Build our gridview, and assign functions to each button
            var gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += GridView_ItemClick;

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }

        private void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            if (e.Position == 0) // "ADD RED"
            {
                AddRed();
            }
            else if (e.Position == 1) // "ADD GREEN"
            {
                AddGreen();
            }
            else if (e.Position == 2) // "ADD BLUE"
            {
                AddBlue();
            }
            else if (e.Position == 3) // "TINT"
            {
                Tint();
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
                Blur();
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
                Pixelate();
            }
            else if (e.Position == 15) // "FLIP HORIZONTALLY"
            {
                FlipHorizontally();
            }
            else if (e.Position == 16) // "FLIP VERTICALLY"
            {
                FlipVertically();
            }
            else if (e.Position == 17) // "ROTATE 90 DEGREES"
            {
                Rotate90();
            }
            else if (e.Position == 18) // "REVERT"
            {
                Toast.MakeText(this.ApplicationContext, "Image reset.", ToastLength.Short).Show();
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
            ReplaceDisplay();
        }

        private void TempHolder() //
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

        private void ReplaceDisplay() //
        {
            if (copyBitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
                //bitmap = null;
                //copyBitmap = null;
            }
        }

        private void AddRed() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method AddRed() has been called.", ToastLength.Short).Show();
            //this code removes all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = 255;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void AddGreen() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method AddGreen() has been called.", ToastLength.Short).Show();
            //this code removes all green from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = 255;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void AddBlue() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method AddBlue() has been called.", ToastLength.Short).Show();
            //this code removes all blue from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = 255;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void RemoveRed() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method RemoveRed() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method RemoveGreen() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method RemoveBlue() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method NegateRed() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method NegateGreen() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method NegateBlue() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method AddRandomNoise() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method Woodgrain() has been called.", ToastLength.Short).Show();
            //this code adds random noise to each pixel in a picture (+/- 10 to rgb), 
            // however in a woodgrain pattern because of how random is declared
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
            //Toast.MakeText(this.ApplicationContext, "Method HighContrast() has been called.", ToastLength.Short).Show();
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
            //Toast.MakeText(this.ApplicationContext, "Method Grayscale() has been called.", ToastLength.Short).Show();
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

        private void Tint() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method Tint() has been called.", ToastLength.Short).Show();
            //this code subtracts 25 from each pixel's rgb values
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.R.GetHashCode().ToString()), -25));
                    c.G = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.G.GetHashCode().ToString()), -25));
                    c.B = Convert.ToByte(NoiseHelper(Convert.ToInt32(c.B.GetHashCode().ToString()), -25));
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        // ---- Advanced Tasks ----

        private void FlipHorizontally() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method FlipHorizontally() has been called.", ToastLength.Short).Show();
            //this code horizontally flips all of the pixels in an image
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    copyBitmap.SetPixel(bitmap.Width - (i + 1), j, c);
                }
            }
        }

        private void FlipVertically() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method FlipVertically() has been called.", ToastLength.Short).Show();
            //this code vertically flips all of the pixels in an image
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    copyBitmap.SetPixel(i, bitmap.Height - (j+1), c);
                }
            }
        }

        private void Rotate90() //
        {
            //set new width to previous height, set new height to previous width
            copyBitmap = Android.Graphics.Bitmap.CreateBitmap(bitmap.Height, bitmap.Width, Android.Graphics.Bitmap.Config.Argb8888);
            //Toast.MakeText(this.ApplicationContext, "Rotate90() has been called.", ToastLength.Short).Show();
            //this code rotates an image 90 degrees (to the right)
            for (int i = 0; i < bitmap.Width; i++)
            {
                Stack<Android.Graphics.Color> vertPixels = new Stack<Android.Graphics.Color>();
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    vertPixels.Push(c);
                }
                for (int j = 0; j < bitmap.Height; j++)
                {
                    copyBitmap.SetPixel(j, i, vertPixels.Pop());
                }
            }
        }

        private void Blur() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method Blur() has been called.", ToastLength.Short).Show();
            //this code blurs each pixel with it's immediate neighbors (L & R, U & D), 1 time(s)
            Android.Graphics.Bitmap prevBitmap;
            for (int blurCount = 0; blurCount < 1; blurCount++)
            {
                prevBitmap = copyBitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        //fetch our pixel
                        int p = bitmap.GetPixel(i, j);
                        Android.Graphics.Color c = new Android.Graphics.Color(p);

                        if (i == 0) // we're at left border, fetch only right pixel
                        {
                            int r = bitmap.GetPixel(i + 1, j);
                            Android.Graphics.Color cR = new Android.Graphics.Color(r);

                            Android.Graphics.Color[] cr = new Android.Graphics.Color[] { c, cR };
                            c = BlurHelper(cr);
                        }
                        else if (i == bitmap.Width - 1) // we're at right border, fetch only left pixel
                        {
                            int l = bitmap.GetPixel(i - 1, j);
                            Android.Graphics.Color cL = new Android.Graphics.Color(l);

                            Android.Graphics.Color[] lc = new Android.Graphics.Color[] { cL, c };
                            c = BlurHelper(lc);
                        }
                        else // otherwise, fetch both left and right pixels
                        {
                            int l = bitmap.GetPixel(i - 1, j);
                            Android.Graphics.Color cL = new Android.Graphics.Color(l);
                            int r = bitmap.GetPixel(i + 1, j);
                            Android.Graphics.Color cR = new Android.Graphics.Color(r);

                            Android.Graphics.Color[] lcr = new Android.Graphics.Color[] { cL, c, cR };
                            c = BlurHelper(lcr);
                        }

                        if (j == 0) // we're at top border, fetch only down pixel
                        {
                            int d = bitmap.GetPixel(i, j + 1);
                            Android.Graphics.Color cD = new Android.Graphics.Color(d);

                            Android.Graphics.Color[] cd = new Android.Graphics.Color[] { c, cD };
                            c = BlurHelper(cd);
                        }
                        else if (j == bitmap.Height - 1) // we're at bottom border, fetch only up pixel
                        {
                            int u = bitmap.GetPixel(i, j - 1);
                            Android.Graphics.Color cU = new Android.Graphics.Color(u);

                            Android.Graphics.Color[] uc = new Android.Graphics.Color[] { cU, c };
                            c = BlurHelper(uc);
                        }
                        else // otherwise, fetch both up and down pixels
                        {
                            int u = bitmap.GetPixel(i, j - 1);
                            Android.Graphics.Color cU = new Android.Graphics.Color(u);
                            int d = bitmap.GetPixel(i, j + 1);
                            Android.Graphics.Color cD = new Android.Graphics.Color(d);

                            Android.Graphics.Color[] ucd = new Android.Graphics.Color[] { cU, c, cD };
                            c = BlurHelper(ucd);
                        }
                        copyBitmap.SetPixel(i, j, c);
                    }
                }
            }
        }

        private Android.Graphics.Color BlurHelper(Android.Graphics.Color[] pix) //
        {
            int r, g, b;
            r = g = b = 0;
            foreach (Android.Graphics.Color i in pix)
            {
                r += Android.Graphics.Color.GetRedComponent(i);
                g += Android.Graphics.Color.GetGreenComponent(i);
                b += Android.Graphics.Color.GetBlueComponent(i);
            }
            r /= pix.Length;
            g /= pix.Length;
            b /= pix.Length;
            Android.Graphics.Color blrdPxl = new Android.Graphics.Color(pix[0]);
            blrdPxl.R = Convert.ToByte(r);
            blrdPxl.G = Convert.ToByte(g);
            blrdPxl.B = Convert.ToByte(b);
            return blrdPxl;
        }

        private void Pixelate() //
        {
            //Toast.MakeText(this.ApplicationContext, "Method Pixelate() has been called.", ToastLength.Short).Show();
            //this code changes pixels to be identical in blocks
            int d = 5; //distance, for pixelation
            for (int i = 0; i < bitmap.Width; i+=d)
            {
                for (int j = 0; j < bitmap.Height; j+=d)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    for (int k = 0; k < d; k++)
                    {
                        if (i + k < bitmap.Width)
                        {
                            for (int l = 0; l < d; l++)
                            {
                                if (j + l < bitmap.Height)
                                {
                                    copyBitmap.SetPixel(i + k, j + l, c);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

