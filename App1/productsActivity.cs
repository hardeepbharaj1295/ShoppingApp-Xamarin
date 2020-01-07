using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Support.V4.Widget;
using App1.Resources.Preferences;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class productsActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        ViewPager viewpager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.products);

            viewpager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            //SupportActionBar.SetIcon(Resource.Drawable.Icon);

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
           // SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            if (viewpager.Adapter == null)
            {
                setupViewPager(viewpager);


            }
            else
            {
                viewpager.Adapter.NotifyDataSetChanged();
            }
            

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);

            tabLayout.SetupWithViewPager(viewpager);
        }
        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new Fragment1(), "Products");
            adapter.AddFragment(new Fragment2(), "History");
            viewPager.Adapter = adapter;
            viewpager.Adapter.NotifyDataSetChanged();
            //viewpager.OffscreenPageLimit(4);
        }


        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Create a new fragment and a transaction.
                //FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                //FragmentHome aDifferentDetailsFrag = new FragmentHome();

                //// The fragment will have the ID of Resource.Id.fragment_container.
                //fragmentTx.Replace(Resource.Id.frameLayout1, aDifferentDetailsFrag);

                //// Commit the transaction.
                //fragmentTx.Commit();
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
                OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                // Create a new fragment and a transaction.
                // FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                // Fragment1 aDifferentDetailsFrag = new Fragment1();

                // // The fragment will have the ID of Resource.Id.fragment_container.
                //// fragmentTx.Replace(Resource.Id.frameLayout1, aDifferentDetailsFrag);

                // // Commit the transaction.
                // fragmentTx.Commit();
                //Intent intent = new Intent(this, typeof(productsActivity));
                //StartActivity(intent);
                //Finish();
            }


            else if (id == Resource.Id.nav_slideshow)
            {
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);
                ap.saveLoginKey(false, "");
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
                Finish();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

    }
    class Adapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();


        public Adapter(V4FragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(V4Fragment fragment, String title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);


        }

        public override V4Fragment GetItem(int position)
        {
            return fragments[position];

        }

        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }
    }
}