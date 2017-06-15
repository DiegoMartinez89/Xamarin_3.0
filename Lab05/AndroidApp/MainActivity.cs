using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SALLab05;

namespace AndroidApp
{
    [Activity(Label = "PhoneApp", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var TextResultado = FindViewById<TextView>(Resource.Id.textResultado);

            CallButton.Enabled = false;
            var TranslatedNumber = string.Empty;

            RegistrarLaboratorio(TextResultado);

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
                    //Crear un intento para marcar el número de teléfono
                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);
                    CallIntent.SetData(Android.Net.Uri.Parse($"tel: {TranslatedNumber}"));
                    StartActivity(CallIntent);
                });
                CallDialog.SetNegativeButton("Cancelar", delegate { });
                //Mostrar el cuadro de dialogo al usuario y esperar una respuesta
                CallDialog.Show();
            };
        }

        private /*async*/ void RegistrarLaboratorio(TextView textResultado)
        {
            //string email = "xxxxxxxxxxxxx";
            //string password = "xxxxxxxxxxxxxxxx";
            //string deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver,
            //    Android.Provider.Settings.Secure.AndroidId);
            //ServiceClient serviceClient = new ServiceClient();
            //var result = await serviceClient.ValidateAsync(email, password, deviceId);
            //var resultado = $"{result.Status}\n{result.Fullname}\n{result.Token}";
            var resultado = $"Success\nDiego Armando Martínez Rodríguez\nMS-28-7-10-35-370-182208-5\nXamarinDiplomado3.0-Lab05";
            textResultado.Text = resultado;
        }
    }
}

