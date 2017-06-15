using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab12
{
    [Activity(Label = "Lab12", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class MainActivity : Activity
    {
        ValidacionComplex DataValidar;
        string studentEmail = "xxxxxxxxxxxxxx";
        string studentPassword = "xxxxxxxxxxxx";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var ListColors = FindViewById<ListView>(Resource.Id.listView1);
            ListColors.Adapter = new CustomAdapters.ColorAdapter(this, Resource.Layout.ListItem, 
                Resource.Id.textView1, Resource.Id.textView2, Resource.Id.imageView1);

            ValidarActividad();
        }

        private async void ValidarActividad()
        {
            string deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            var ResultadoText = FindViewById<TextView>(Resource.Id.ResultadoText);

            DataValidar = (ValidacionComplex)this.FragmentManager.FindFragmentByTag("DataValidar");
            if (DataValidar == null)
            {
                DataValidar = new ValidacionComplex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(DataValidar, "DataValidar");
                FragmentTransaction.Commit();

                SALLab12.ServiceClient ServiceClient = new SALLab12.ServiceClient();
                DataValidar.Result = await ServiceClient.ValidateAsync(studentEmail, studentPassword, deviceId);
            }
            ResultadoText.Text = DataValidar.ToString();
        }
    }
}

