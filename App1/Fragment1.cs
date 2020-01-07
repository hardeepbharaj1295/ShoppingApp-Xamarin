using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Resources.Helper;
using App1.Resources.Model;
using App1.Resources.Preferences;

namespace App1
{
    public class Fragment1 : Fragment
    {
        Database db;
        ListView lstViewData;
        List<Products> listSource = new List<Products>();
        EditText inputSearch;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            View firstView = inflater.Inflate(Resource.Layout.fragment1, container, false);

            lstViewData = firstView.FindViewById<ListView>(Resource.Id.listView);
            inputSearch = firstView.FindViewById<EditText>(Resource.Id.inputSearch);
            db = new Database();

            inputSearch.TextChanged += InputSearch_TextChanged;

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
                        ProductId = listSource[e.Position].Id.ToString(),
                        ProductName = listSource[e.Position].ProductName,
                        ProductPrice = Int32.Parse(listSource[e.Position].ProductPrice),
                        ProductQuantity = 1,
                        ProductImage = listSource[e.Position].ProductImage,
                        UserEmail = ap.getUserKey()
                    };
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this.Activity);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("Message");
                    alert.SetMessage("You want to Add this Product To Cart");
                    alert.SetButton("Yes", (c, ev) =>
                    {

                        bool res = db.buyProduct(listSource[e.Position].Id, history);
                        if (res)
                        {
                            LoadData();
                            Toast.MakeText(this.Activity, "Product Added Successfully", ToastLength.Short).Show();
                            Intent intent = new Intent(this.Activity, typeof(productsActivity));
                            StartActivity(intent);
                        }
                        else
                        {
                            Toast.MakeText(this.Activity, "Product Not Added", ToastLength.Short).Show();
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
            listSource.Clear();
            listSource = db.selectTable();
            var adapter = new ProductsAdapter(this.Activity, listSource);
            lstViewData.Adapter = adapter;
        }

        private void InputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //get the text from Edit Text            
            var searchText = inputSearch.Text;
            
            //Compare the entered text with List  
            List<Products> list = (from items in listSource
                               where items.ProductName.Contains(searchText)
                               select items).ToList<Products>();

            // bind the result with adapter  
            lstViewData.Adapter = new ProductsAdapter(this.Activity, list);
        }
    }
}