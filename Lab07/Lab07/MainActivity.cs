using Android.App;
using Android.Widget;
using Android.OS;
using SALLab07;

namespace Lab07
{
    [Activity(Label = "Lab07", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button ValidarButton = FindViewById<Button>(Resource.Id.ValidarButton);
            EditText CorreoText = FindViewById<EditText>(Resource.Id.CorreoText);
            EditText PasswordText = FindViewById<EditText>(Resource.Id.PasswordText);
            TextView ResultadoText = FindViewById<TextView>(Resource.Id.ResultadoTextView);

            ValidarButton.Click += async (s, ev) =>
            {
                string deviceID = Android.Provider.Settings.Secure.GetString(ContentResolver, 
                    Android.Provider.Settings.Secure.AndroidId);
                ServiceClient serviceClient = new ServiceClient();
                ResultInfo Resultado = await serviceClient.ValidateAsync(CorreoText.Text, 
                    PasswordText.Text, deviceID);
                if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    string resultado = $"{Resultado.Status} {Resultado.Fullname} {Resultado.Token}";
                    var Builder = new Notification.Builder(this).SetContentTitle("Validación de actividad")
                        .SetContentText(resultado).SetSmallIcon(Resource.Drawable.Icon);

                    Builder.SetCategory(Notification.CategoryMessage);

                    var ObjectNotification = Builder.Build();
                    var Manager = GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
                    Manager.Notify(0, ObjectNotification);

                }
                else
                {
                    string resultado = $"{Resultado.Status}\n{Resultado.Fullname}\n{Resultado.Token}";
                    ResultadoText.Text = resultado;
                }
            };               
        }
    }
}

