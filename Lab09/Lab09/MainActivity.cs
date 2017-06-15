using Android.App;
using Android.Widget;
using Android.OS;
using SALLab09;

namespace Lab09
{
    [Activity(Label = "Lab09", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            ValidarRegistro();
        }

        private async void ValidarRegistro()
        {
            var UserNameValue = FindViewById<TextView>(Resource.Id.UserNameValue);
            var StatusValue = FindViewById<TextView>(Resource.Id.StatusValue);
            var TokenValue = FindViewById<TextView>(Resource.Id.TokenValue);

            string studentEmail = "xxxxxxxxxxxxxx";
            string password = "xxxxxxxxxxxxxxxxx";
            string deviceID = Android.Provider.Settings.Secure.GetString(ContentResolver,
                Android.Provider.Settings.Secure.AndroidId);

            ServiceClient serviceClient = new ServiceClient();
            ResultInfo Result = await serviceClient.ValidateAsync(studentEmail, password, deviceID);
            UserNameValue.Text = Result.Fullname;
            StatusValue.Text = Result.Status.ToString();
            TokenValue.Text = Result.Token;
        }
    }
}

