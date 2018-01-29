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

namespace P1_TMurphy_Calc
{
    class ImageAdapter : BaseAdapter
    {
        Context context;
        int[] thumbIds = {
            Resource.Drawable.btn_top_ac,
            Resource.Drawable.btn_top_c,
            Resource.Drawable.btn_top_prc,
            Resource.Drawable.btn_rght_optr_div,
            Resource.Drawable.btn_num_7,
            Resource.Drawable.btn_num_8,
            Resource.Drawable.btn_num_9,
            Resource.Drawable.btn_rght_optr_mltp,
            Resource.Drawable.btn_num_4,
            Resource.Drawable.btn_num_5,
            Resource.Drawable.btn_num_6,
            Resource.Drawable.btn_rght_optr_sub,
            Resource.Drawable.btn_num_1,
            Resource.Drawable.btn_num_2,
            Resource.Drawable.btn_num_3,
            Resource.Drawable.btn_rght_optr_add,
            Resource.Drawable.btn_bttm_plsmns,
            Resource.Drawable.btn_num_0,
            Resource.Drawable.btn_bttm_dcpnt,
            Resource.Drawable.btn_rght_optr_eql,
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
            if(convertView == null) // if it's not recycled, initialize some attributes
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