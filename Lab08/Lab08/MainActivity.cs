using Android.App;
using Android.Widget;
using Android.OS;
using SALLab08;

namespace Lab08
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            //var ViewGroup = (Android.Views.ViewGroup)
            //    Window.DecorView.FindViewById(Android.Resource.Id.Content);
            //var MainLayout = ViewGroup.GetChildAt(0) as LinearLayout;
            //var HeaderImage = new ImageView(this);
            //HeaderImage.SetImageResource(Resource.Drawable.Xamarin_Diplomado_30);
            //MainLayout.AddView(HeaderImage);
            //var userNameTextView = new TextView(this);
            //userNameTextView.Text = GetString(Resource.String.UserName);
            //MainLayout.AddView(userNameTextView);
            ValidarUsuario();
        }

        public async void ValidarUsuario()
        {
            var UserNameValue = FindViewById<TextView>(Resource.Id.UserNameValue);
            var StatusValue = FindViewById<TextView>(Resource.Id.StatusValue);
            var TokenValue = FindViewById<TextView>(Resource.Id.TokenValue);

            string studentEmail = "xxxxxxxxxxxxxxxx";
            string password = "xxxxxxxxxxxxxx";
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

