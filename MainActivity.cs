using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase;
using Firebase.Firestore;
using Java.Util;

namespace ECED_APP
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        FirebaseFirestore database;
        EditText origem;
        EditText destino;
        Button testButton;

        void ConnectViews()
        {
            origem = (EditText)FindViewById(Resource.Id.origem);
            destino = (EditText)FindViewById(Resource.Id.destino);
            testButton = (Button)FindViewById(Resource.Id.testbutton);

            testButton.Click += TestButton_Click;
        }
        private void TestButton_Click(object sender, System.EventArgs e)
        {
            HashMap doc = new HashMap();
            doc.Put("origem", origem.Text);
            doc.Put("destino", destino.Text);

            DocumentReference docRef = database.Collection("testAndroid").Document().Collection("subTestAndroid").Document();
            docRef.Set(doc);
        }
        public FirebaseFirestore GetDatabase()
        {
            FirebaseFirestore database;
            var options = new FirebaseOptions.Builder()
                .SetProjectId("eced-e3031")
                .SetApplicationId("eced-e3031")
                .SetApiKey("AIzaSyCcS9iPYxmtL6mbjv9poP_Fk37uWgbYkl8")
                .SetDatabaseUrl("https://eced-e3031.firebaseio.com")
                .SetStorageBucket("eced-e3031.appspot.com")
                .Build();

            var app = FirebaseApp.InitializeApp(this, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //Xamarin.FormsGoogleMaps.Init(this, savedInstanceState); // inicia o Xamarin.Forms.GoogleMaps

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            database = GetDatabase();

        }
       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

      
    }
}