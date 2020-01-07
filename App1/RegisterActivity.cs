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
using Android.Widget;
using App1.Resources.Helper;
using App1.Resources.Model;

namespace App1
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : AppCompatActivity
    {
        Database db;
        EditText edtName, edtNumber, edtEmail, edtPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_register);

            db = new Database();

            // Create your application here
            Button buttonLogin = FindViewById<Button>(Resource.Id.btnLogin);
            buttonLogin.Click += Button_Click;

            Button buttonRegister = FindViewById<Button>(Resource.Id.btnRegister);
            buttonRegister.Click += Button2_Click;

            edtName = FindViewById<EditText>(Resource.Id.edtName);
            edtNumber = FindViewById<EditText>(Resource.Id.edtDepart);
            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Person person = new Person()
            {
                Name = edtName.Text,
                Number = edtNumber.Text,
                Email = edtEmail.Text,
                Password = edtPassword.Text
            };
            bool result = db.insertIntoTable(person);
            if (result)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Message");
                alert.SetMessage("Register Successfully");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Cancel();
                });
                alert.Show();
            }
        }
    }
}