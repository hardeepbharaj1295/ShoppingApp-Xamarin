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

namespace App1.Resources.Model
{
    public class ViewHolders : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtPrice { get; set; }
        public TextView txtQuantity { get; set; }
    }

    class HistoryAdapter : BaseAdapter
    {
        private Activity activity;
        private List<History> listPerson;

        public HistoryAdapter(Activity activity, List<History> listPerson)
        {
            this.activity = activity;
            this.listPerson = listPerson;
        }

        public override int Count
        {
            get { return listPerson.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.history_list, parent, false);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtView_Name);
            var txtPrice = view.FindViewById<TextView>(Resource.Id.txtView_Price);
            var txtQuantity = view.FindViewById<TextView>(Resource.Id.txtView_Quantity);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imageView1);
            txtName.Text = "Product Name : " + listPerson[position].ProductName;
            txtPrice.Text = "Product Price : " + listPerson[position].ProductPrice.ToString();
            txtQuantity.Text = "Product Quantity : " + listPerson[position].ProductQuantity.ToString();
            imageView.SetImageResource(listPerson[position].ProductImage);
            return view;
        }
    }
}