using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SALLab06;
using System.Collections.Generic;

namespace AndroidApp
{
    [Activity(Label = "PhoneApp", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> PhoneNumbers = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
                        
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetLogo(Resource.Drawable.Icon);
            ActionBar.SetDisplayUseLogoEnabled(true);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            var ValidarActividadButton = FindViewById<Button>(Resource.Id.ValidarActividadButton);

            CallButton.Enabled = false;
            var TranslatedNumber = string.Empty;

            TranslateButton.Click += (object sender, EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if(string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    //No hay número a llamar
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    //Hay un número telefonico a llamar
                    CallButton.Text = $"Llamar al {TranslatedNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += (object sender, EventArgs e) =>
            {
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al número {TranslatedNumber}");
                CallDialog.SetNeutralButton("Llamar", delegate
                {
                    PhoneNumbers.Add(TranslatedNumber);
                    CallHistoryButton.Enabled = true;
                    //Crear un intento para marcar el número de teléfono
                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);
                    CallIntent.SetData(Android.Net.Uri.Parse($"tel: {TranslatedNumber}"));
                    StartActivity(CallIntent);
                });
                CallDialog.SetNegativeButton("Cancelar", delegate { });
                //Mostrar el cuadro de dialogo al usuario y esperar una respuesta
                CallDialog.Show();
            };

            CallHistoryButton.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);
            };

            ValidarActividadButton.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(ValidarActivity));
                StartActivity(Intent);
            };
        }
    }
}

