﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidApp
{
    [Activity(Label = "@string/CallHistory")]
    public class CallHistoryActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetLogo(Resource.Drawable.Icon);
            ActionBar.SetDisplayUseLogoEnabled(true);

            // Create your application here
            var PhoneNumbers =
                Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
            this.ListAdapter =
                new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, PhoneNumbers);
        }
    }
}