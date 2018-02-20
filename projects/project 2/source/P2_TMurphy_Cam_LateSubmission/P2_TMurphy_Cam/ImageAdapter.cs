using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace P2_TMurphy_Cam
{
    class ImageAdapter : BaseAdapter
    {
        Context context;
        int[] thumbIds = {
            Resource.Drawable.btn_add_red,
            Resource.Drawable.btn_add_green,
            Resource.Drawable.btn_add_blue,
            Resource.Drawable.btn_tint,
            Resource.Drawable.btn_add_rnd_noise,
            Resource.Drawable.btn_remove_red,
            Resource.Drawable.btn_remove_green,
            Resource.Drawable.btn_remove_blue,
            Resource.Drawable.btn_grayscale,
            Resource.Drawable.btn_blur,
            Resource.Drawable.btn_negate_red,
            Resource.Drawable.btn_negate_green,
            Resource.Drawable.btn_negate_blue,
            Resource.Drawable.btn_high_contrast,
            Resource.Drawable.btn_pixelate,
            Resource.Drawable.btn_flip_horiz,
            Resource.Drawable.btn_flip_vert,
            Resource.Drawable.btn_rotate_90,
            Resource.Drawable.btn_revert,
            Resource.Drawable.btn_woodgrain,
        };

        public ImageAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get
            {
                return thumbIds.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imgView;
            if (convertView == null) // if it's not recycled, initialize some attributes
            {
                imgView = new ImageView(context);
                imgView.LayoutParameters = new GridView.LayoutParams(150, 150);
                imgView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imgView.SetPadding(8, 8, 8, 8);
            }
            else
            {
                imgView = (ImageView)convertView;
            }
            imgView.SetImageResource(thumbIds[position]);
            return imgView;
        }
    }
}