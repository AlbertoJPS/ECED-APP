using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using ECED_APP.Fragments;
using Firebase;
using Firebase.Firestore;
using Java.Util;

namespace ECED_APP
{
    [Activity(Theme = "@style/EcedTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        FirebaseFirestore database;
        AndroidX.DrawerLayout.Widget.DrawerLayout drawerLayout;
        AndroidX.AppCompat.Widget.Toolbar mainToolbar;

        //EditText origem;
        //EditText destino;
        //Button testButton;

       
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

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            database = GetDatabase();

            

        }
        void ConnectViews()
        {
            drawerLayout = (AndroidX.DrawerLayout.Widget.DrawerLayout)FindViewById(Resource.Id.drawerLayout);
            mainToolbar = (AndroidX.AppCompat.Widget.Toolbar)FindViewById(Resource.Id.mainToolbar);
            SetSupportActionBar(mainToolbar);
            SupportActionBar.Title = "";
            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
            actionBar.SetDisplayHomeAsUpEnabled(true);


            //origem = (EditText)FindViewById(Resource.Id.origem);
            //destino = (EditText)FindViewById(Resource.Id.destino);
            //testButton = (Button)FindViewById(Resource.Id.testbutton);

            //testButton.Click += TestButton_Click;

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, new Fragment_EscolaHome(), "Fragment_EscolaHome");/*.Hide(Xstartpage);*/
            trans.Commit();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                //case Resource.Id.navEscolaHome:
                    
                //case Resource.Id.navPerfilResponsavel:

                //case Resource.Id.navPerfilAluno:

                //case Resource.Id.navEndereco:

                //case Resource.Id.navFalta:

                //case Resource.Id.navComunicado:

                //case Resource.Id.navBoletim:

                //case Resource.Id.navConfig:

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        //private void TestButton_Click(object sender, System.EventArgs e)
        //{
        //    HashMap doc = new HashMap();
        //    doc.Put("origem", origem.Text);
        //    doc.Put("destino", destino.Text);

        //    DocumentReference docRef = database.Collection("testAndroid").Document().Collection("subTestAndroid").Document();
        //    docRef.Set(doc);

        //}

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}


    }
}