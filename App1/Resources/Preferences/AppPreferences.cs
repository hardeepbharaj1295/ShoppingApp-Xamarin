using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Resources.Preferences
{
    class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String LOGIN_ACCESS_KEY = "LOGIN";
        private static String ID_ACCESS_KEY = "USER_ID";
        private static String PRODUCT_ACCESS_KEY = "PRODUCT";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveLoginKey(bool key, string id)
        {
            mPrefsEditor.PutBoolean(LOGIN_ACCESS_KEY, key);
            mPrefsEditor.PutString(ID_ACCESS_KEY, id);
            mPrefsEditor.Apply();
        }

        public void saveProductKey(bool key)
        {
            mPrefsEditor.PutBoolean(PRODUCT_ACCESS_KEY, key);
            mPrefsEditor.Apply();
        }

        public bool getLoginKey()
        {
            return mSharedPrefs.GetBoolean(LOGIN_ACCESS_KEY, false);
        }

        public bool getProductKey()
        {
            return mSharedPrefs.GetBoolean(PRODUCT_ACCESS_KEY, false);
        }

        public string getUserKey()
        {
            return mSharedPrefs.GetString(ID_ACCESS_KEY, "");
        }
    }
}