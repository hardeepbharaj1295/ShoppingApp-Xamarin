using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Java.Lang;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace App1
{
    class PagerAdapter : FragmentPagerAdapter
    {
        private string[] Titles;

        public PagerAdapter(Android.Support.V4.App.FragmentManager fm, string[] titles)
            : base(fm)
        {
            Titles = titles;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {

            return new Java.Lang.String(Titles[position]);
        }

        public override int Count
        {
            get { return Titles.Length; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return new Fragment1();
                case 1:
                    return new Fragment2();
                case 2:
                default:
                    return new Fragment1();
            }
        }
    }
}