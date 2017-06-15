using Android.App;
using Android.Widget;
using Android.OS;
using SALLab03;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var Helper = new SharedProject.MySharedCode();
            //new AlertDialog.Builder(this)
            //    .SetMessage(Helper.GetFilePath("demo.dat"))
            //    .Show();
            Validate();
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }

        private async void Validate()
        {
            var serviceClient = new ServiceClient();
            string studenEmail = "xxxxxxxxxxxx";
            string password = "xxxxxxxxxxxxx";

            string myDevice = Android.Provider.Settings.Secure
                .GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var result = await serviceClient.ValidateAsync(studenEmail, password, myDevice);

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alert = builder.Create();
            alert.SetTitle("Resultado de la verificación");
            alert.SetIcon(Resource.Drawable.Icon);
            alert.SetMessage($"{result.Status}\n{result.Fullname}\n{result.Token}");
            alert.SetButton("Ok", (s, ev) => { });
            alert.Show();
        }
    }
}

