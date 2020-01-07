using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Resources.Helper;
using App1.Resources.Model;
using App1.Resources.Preferences;

namespace App1
{
    public class Fragment2 : Fragment
    {
        Database db;
        ListView lstViewData;
        List<History> listSource = new List<History>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View firstView = inflater.Inflate(Resource.Layout.fragment2, container, false);
            lstViewData = firstView.FindViewById<ListView>(Resource.Id.listView);
            db = new Database();

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);

            LoadData();

            lstViewData.ItemClick += (s, e) =>
            {
                //Toast.MakeText(this, listSource[e.Position].Id, ToastLength.Short).Show();

                if (listSource[e.Position].ProductQuantity > 0)
                {
                    History history = new History()
                    {
                        Id = listSource[e.Position].Id,
                        ProductId = listSource[e.Position].Id.ToString(),
                        ProductName = listSource[e.Position].ProductName,
                        ProductPrice = listSource[e.Position].ProductPrice,
                        ProductQuantity = listSource[e.Position].ProductQuantity,
                        ProductImage = listSource[e.Position].ProductImage,
                        UserEmail = ap.getUserKey()
                    };

                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this.Activity);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("Message");
                    alert.SetMessage("You want to Delete this Product To Cart");
                    alert.SetButton("Yes", (c, ev) =>
                    {

                        bool res = db.deleteProduct(Int32.Parse(listSource[e.Position].ProductId), history);
                        if (res)
                        {
                            LoadData();
                            Toast.MakeText(this.Activity, "Product Deleted Successfully", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(this.Activity, "Product Not Deleted", ToastLength.Short).Show();
                        }
                    });
                    alert.Show();
                }
                else
                {
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this.Activity);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("Alert");
                    alert.SetMessage("Product has 0 quantity");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        alert.Cancel();
                    });
                    alert.Show();
                }
            };
            return firstView;
        }

        private void LoadData()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            listSource.Clear();
            listSource = db.selectUserData(ap.getUserKey());
            if (listSource.Count > 0)
            {
                var adapter = new HistoryAdapter(this.Activity, listSource);
                lstViewData.Adapter = adapter;

            }
        }
    }
}