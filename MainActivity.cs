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
using System.Collections.Generic;
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
        private List<string> menuList;
        private ListView listView;


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
            database = GetDatabase();
            ConnectNavigator();
            //CreateNavigatorListView();
            currentFragment = fragment_Boletim;







        }
        void ConnectNavigator()
        {
            drawerLayout = (AndroidX.DrawerLayout.Widget.DrawerLayout)FindViewById(Resource.Id.drawerLayout);
            mainToolbar = (AndroidX.AppCompat.Widget.Toolbar)FindViewById(Resource.Id.mainToolbar);
            SetSupportActionBar(mainToolbar);
            SupportActionBar.Title = "";
            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
            actionBar.SetDisplayHomeAsUpEnabled(true);

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        //void CreateNavigatorListView()
        //{
        //    fragment_Boletim = new Fragment_Boletim();
        //    fragment_InfoEscola = new Fragment_InfoEscola();
        //    fragment_PerfilResponsavel = new Fragment_PerfilResponsavel();
        //    fragment_PerfilAluno = new Fragment_PerfilAluno();
        //    fragment_Endereco = new Fragment_Endereco();
        //    fragment_Falta = new Fragment_Falta();
        //    fragment_Comunicado = new Fragment_Comunicado();
        //    fragment_Configuracoes = new Fragment_Configuracoes();

        //    var transition = SupportFragmentManager.BeginTransaction();
        //    transition.Add(Resource.Id.fragmentContainer, fragment_Configuracoes, "Fragment_Configuracoes");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_Comunicado, "Fragment_Comunicado");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_Falta, "Fragment_Falta");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_Endereco, "Fragment_Endereco");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_PerfilAluno, "Fragment_PerfilAluno");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_PerfilResponsavel, "Fragment_PerfilResponsavel");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_InfoEscola, "Fragment_InfoEscola");
        //    transition.Add(Resource.Id.fragmentContainer, fragment_Boletim, "Fragment_Boletim");
        //    transition.Commit();

        //    SetContentView(Resource.Layout.Main_Activity);
        //    listView = FindViewById<ListView>(Resource.Id.navView);

        //    menuList = new List<string>();
        //    menuList.Add("Acessar Boletim");
        //    menuList.Add("Info Escola");
        //    menuList.Add("Perfil Responsável");
        //    menuList.Add("Perfil Aluno");
        //    menuList.Add("Meu Endereço");
        //    menuList.Add("Relatar Ausência");
        //    menuList.Add("Fazer Comunicado");
        //    menuList.Add("Configurações");

        //    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, menuList);
        //    listView.Adapter = adapter;
           
        //    listView.ItemClick += (sender, e) =>
        //    {
        //        int posicao = e.Position;
        //        string valor = menuList[posicao];
        //        switch (posicao)
        //        {
        //            case 1:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_Boletim;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 2:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_PerfilResponsavel;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 3:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_PerfilAluno;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 4:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_Endereco;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 5:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_Falta;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 6:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_Comunicado;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 7:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_InfoEscola;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            case 8:
        //                transition.Hide(currentFragment);
        //                currentFragment = fragment_Configuracoes;
        //                ViewFragments(currentFragment);
        //                transition.Commit();
        //                return;
        //            default:
        //                return;

        //        }
        //    };

        //}
        //private void ViewFragments(SupportFragment fragment)
        //{
        //    var transition = SupportFragmentManager.BeginTransaction();
        //    transition.Show(fragment);
        //    transition.AddToBackStack(null);
        //    transition.Commit();

        //    currentFragment = fragment;
        //}



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