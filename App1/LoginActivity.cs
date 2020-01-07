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
using App1.Resources.Preferences;

namespace App1
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : AppCompatActivity
    {
        Database db;
        EditText edtEmail, edtPassword;
        bool mBool = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_login);
            //Create Database
            db = new Database();
            db.createDatabase();

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            mBool = ap.getLoginKey();
            if (mBool)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            }

            Button buttonLogin = FindViewById<Button>(Resource.Id.btnLogin);
            buttonLogin.Click += Button_Click;

            Button buttonRegister = FindViewById<Button>(Resource.Id.btnRegister);
            buttonRegister.Click += Button2_Click;

            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            bool result = db.loginIntoTable(edtEmail.Text, edtPassword.Text);
            if (result)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Message");
                alert.SetMessage("Login Successfully");
                alert.SetButton("OK", (c, ev) =>
                {
                    Context mContext = Android.App.Application.Context;
                    AppPreferences ap = new AppPreferences(mContext);
                    ap.saveLoginKey(true, edtEmail.Text);
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();
                });
                alert.Show();
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Alert");
                alert.SetMessage("Wrong Username/Password");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Cancel();
                });
                alert.Show();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }
    }
}