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
using SupportFragment = AndroidX.Fragment.App.Fragment;

namespace ECED_APP
{
    [Activity(Theme = "@style/EcedTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        FirebaseFirestore database;
        AndroidX.DrawerLayout.Widget.DrawerLayout drawerLayout;
        AndroidX.AppCompat.Widget.Toolbar mainToolbar;
        private SupportFragment currentFragment;

        private Fragment_Boletim fragment_Boletim;
        private Fragment_InfoEscola fragment_InfoEscola;
        private Fragment_PerfilResponsavel fragment_PerfilResponsavel;
        private Fragment_PerfilAluno fragment_PerfilAluno;
        private Fragment_Endereco fragment_Endereco;
        private Fragment_Falta fragment_Falta;
        private Fragment_Comunicado fragment_Comunicado;
        private Fragment_Configuracoes fragment_Configuracoes;

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
            SetContentView(Resource.Layout.Main_Activity);
            ConnectFragments();
            ConnectViews();
            database = GetDatabase();

            currentFragment = fragment_Boletim;

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

        }
        void ConnectFragments()
        {
            fragment_Boletim = new Fragment_Boletim();
            fragment_InfoEscola = new Fragment_InfoEscola();
            fragment_PerfilResponsavel = new Fragment_PerfilResponsavel();
            fragment_PerfilAluno = new Fragment_PerfilAluno();
            fragment_Endereco = new Fragment_Endereco();
            fragment_Falta = new Fragment_Falta();
            fragment_Comunicado = new Fragment_Comunicado();
            fragment_Configuracoes = new Fragment_Configuracoes();


            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, fragment_Configuracoes, "Fragment_Configuracoes");
            trans.Hide(fragment_Configuracoes);
            trans.Add(Resource.Id.fragmentContainer, fragment_Comunicado, "Fragment_Comunicado");
            trans.Hide(fragment_Comunicado);
            trans.Add(Resource.Id.fragmentContainer, fragment_Falta, "Fragment_Falta");
            trans.Hide(fragment_Falta);
            trans.Add(Resource.Id.fragmentContainer, fragment_Endereco, "Fragment_Endereco");
            trans.Hide(fragment_Endereco);
            trans.Add(Resource.Id.fragmentContainer, fragment_PerfilAluno, "Fragment_PerfilAluno");
            trans.Hide(fragment_PerfilAluno);
            trans.Add(Resource.Id.fragmentContainer, fragment_PerfilResponsavel, "Fragment_PerfilResponsavel");
            trans.Hide(fragment_PerfilResponsavel);
            trans.Add(Resource.Id.fragmentContainer, fragment_InfoEscola, "Fragment_InfoEscola");
            trans.Hide(fragment_InfoEscola);
            trans.Add(Resource.Id.fragmentContainer, fragment_Boletim, "Fragment_Boletim");
            trans.Commit();
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                
                case Resource.Id.navBoletim:
                    ViewFragments(fragment_Boletim);
                    return true;
                case Resource.Id.navPerfilResponsavel:
                    ViewFragments(fragment_PerfilResponsavel);
                    return true;
                case Resource.Id.navPerfilAluno:
                    ViewFragments(fragment_PerfilAluno);
                    return true;
                case Resource.Id.navEndereco:
                    ViewFragments(fragment_Endereco);
                    return true;
                case Resource.Id.navFalta:
                    ViewFragments(fragment_Falta);
                    return true;
                case Resource.Id.navComunicado:
                    ViewFragments(fragment_Comunicado);
                    return true;
                case Resource.Id.navInfoEscola:
                    ViewFragments(fragment_InfoEscola);
                    return true;
                case Resource.Id.navConfig:
                    ViewFragments(fragment_Configuracoes);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        private void ViewFragments(SupportFragment fragment)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Show(fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            currentFragment = fragment;

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