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
using SALLab10;

namespace Lab10
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon")]
    public class ValidarActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Validar);

            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetIcon(Resource.Drawable.Icon);
            //ActionBar.SetDisplayUseLogoEnabled(true);

            var ButtonValidar = FindViewById<Button>(Resource.Id.ValidarButton);

            ButtonValidar.Click += ButtonValidar_Click;
        }

        private async void ButtonValidar_Click(object sender, EventArgs e)
        {
            var TextResultado = FindViewById<TextView>(Resource.Id.textResultado);
            var TextEmail = FindViewById<EditText>(Resource.Id.EmailValidarText);
            var TextPassword = FindViewById<EditText>(Resource.Id.PasswordValidarText);
            string deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver,
                Android.Provider.Settings.Secure.AndroidId);
            ServiceClient serviceClient = new ServiceClient();
            var result = await serviceClient.ValidateAsync(TextEmail.Text, TextPassword.Text, deviceId);
            var resultado = $"{result.Status}\n{result.Fullname}\n{result.Token}";
            TextResultado.Text = resultado;
        }
    }
}