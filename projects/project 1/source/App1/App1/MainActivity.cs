using Android.App;
using Android.Widget;
using Android.OS;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.calcButton);
            // button.AfterTextChanged += CalculateInput;
            // button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += CalculateInput;

            // Get our textfield from the layout resource, 
            // so we can pull values from it

        }

        private void CalculateInput(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            // throw new System.NotImplementedException();

            // https://forums.xamarin.com/discussion/71735/how-to-display-a-message-box-or-alert-message-in-c-xamarin-android
            Toast.MakeText(this.ApplicationContext, "Let's try to calc stuff..", ToastLength.Short).Show();
        }
    }
}

