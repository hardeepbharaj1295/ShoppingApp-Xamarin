using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using App1.Resources.Preferences;
using App1.Resources.Helper;
using Android.Util;
using App1.Resources.Model;

namespace App1
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        bool mBool = false;
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            db = new Database();

            loadData();
         

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            mBool = ap.getLoginKey();
            if (!mBool)
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
                Finish();
            }

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);



            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            FragmentHome aDifferentDetailsFrag = new FragmentHome();

            // The fragment will have the ID of Resource.Id.fragment_container.
            fragmentTx.Add(Resource.Id.frameLayout1, aDifferentDetailsFrag);

            // Commit the transaction.
            fragmentTx.Commit();
            

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }
        
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
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
                FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                FragmentHome aDifferentDetailsFrag = new FragmentHome();

                // The fragment will have the ID of Resource.Id.fragment_container.
                fragmentTx.Replace(Resource.Id.frameLayout1, aDifferentDetailsFrag);

                // Commit the transaction.
                fragmentTx.Commit();
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
                Intent intent = new Intent(this, typeof(productsActivity));
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
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

        public void loadData()
        {
            string[] name = { "Gaming Remotes", "Electronics", "Hand Tools", "Speakers", "Earings", "Watches" };
            string[] price = { "10", "10", "10", "10", "10", "10"};
            int[] quantity = { 10, 10, 10, 10, 10, 10 };
            int[] image = { Resource.Drawable.first , Resource.Drawable.second , Resource.Drawable.third , Resource.Drawable.fourth , Resource.Drawable.fifth , Resource.Drawable.sixth };

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);

            int res = db.selectCount();
            //Toast.MakeText(this, res.ToString(), ToastLength.Long).Show();
            bool data = ap.getProductKey();
            //if (!data)
            if(res==0)
            {
                for (int i = 0; i < 6; i++)
                {
                    Products product = new Products()
                    {
                        ProductName = name[i],
                        ProductPrice = price[i],
                        ProductQuantity = quantity[i],
                        ProductImage = image[i]
                    };
                    bool result = db.insertProducts(product);
                    Log.Info("Result : ", result.ToString());
                }
                ap.saveProductKey(true);
            }
        }
    }
}

