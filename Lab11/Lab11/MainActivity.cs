using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class MainActivity : Activity
    {
        Complex Data;
        ValidacionComplex DataValidar;
        int Counter = 0;
        string studentEmail = "xxxxxxxxxxxx";
        string studentPassword = "xxxxxxxxxxxxxx";
        protected override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.StartActivity).Click += (s, ev) =>
            {
                var ActivityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }
            if (bundle != null)
            {
                Counter = bundle.GetInt("CounterValue");
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }
            var ClickCounter = FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
            ClickCounter.Text += $"\n{Data.ToString()}";
            ClickCounter.Click += (s, ev) =>
            {
                Counter++;
                ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
                Data.Real++;
                Data.Imaginary++;
                ClickCounter.Text += $"\n{Data.ToString()}";
            };
            ValidarActividad();
        }

        private async void ValidarActividad()
        {
            string deviceId = Android.Provider.Settings.Secure
                .GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            var ResultadoText = FindViewById<TextView>(Resource.Id.ResultadoText);

            DataValidar = (ValidacionComplex)this.FragmentManager.FindFragmentByTag("DataValidar");
            if (DataValidar == null)
            {
                DataValidar = new ValidacionComplex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(DataValidar, "DataValidar");
                FragmentTransaction.Commit();

                SALLab11.ServiceClient ServiceClient = new SALLab11.ServiceClient();
                DataValidar.Result = await ServiceClient.ValidateAsync(studentEmail, studentPassword, deviceId);
            }
            ResultadoText.Text = DataValidar.ToString();
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            outState.PutInt("CounterValue", Counter);
            base.OnSaveInstanceState(outState);
        }
    }
}

