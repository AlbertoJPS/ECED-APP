using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.DrawerLayout.Widget;
using ECED_APP.Fragments;
using Firebase;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Io.OpenCensus.Tags;
using SupportFragment = AndroidX.Fragment.App.Fragment;
using Tag = Android.Nfc.Tag;

namespace ECED_APP
{
    [Activity(Label = "@string/app_name", Theme = "@style/EcedTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        
        private AndroidX.DrawerLayout.Widget.DrawerLayout drawerLayout;
        private AndroidX.AppCompat.Widget.Toolbar mainToolbar;
        private AppDrawerToggle drawerToggle;
        private ListView listView;
        private ArrayAdapter listAdapter;
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
        private static FirebaseFirestore database;
        private Button botaoBoletim;
        private string nomeAluno;
        private EditText aluno;
        private TextView materia;
        private TextView nome;
        private TextView nota1;
        private TextView nota2;
        private TextView nota3;
        private TextView turma;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main_Activity);
            GetDatabase();
            CreateFragmentsPages();

            currentFragment = fragment_Boletim;

            CreateNavigatorListView();
            ConnectNavigator();

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
        void ConnectNavigator()
        {

            drawerLayout = FindViewById<AndroidX.DrawerLayout.Widget.DrawerLayout>(Resource.Id.drawerLayout);
            mainToolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.mainToolbar);
            //listView = FindViewById<ListView>(Resource.Id.listNavView);
            //TextView = FindViewById<TextView>(Resource.Id.materiaBoletim);
            aluno = FindViewById<EditText>(Resource.Id.editTextBoletim); 
            botaoBoletim = FindViewById<Button>(Resource.Id.botaoBoletim);


            SetSupportActionBar(mainToolbar);

            drawerToggle = new AppDrawerToggle(this, drawerLayout, Resource.String.openDrawer, Resource.String.closeDrawer);

            SupportActionBar.Title = "";
            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;

            drawerLayout.AddDrawerListener(drawerToggle);

            actionBar.SetHomeButtonEnabled(true);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            drawerToggle.SyncState();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                //case Android.Resource.Id.Home:
                //  drawerLayout.CloseDrawer((int)GravityFlags.Left);

                case Android.Resource.Id.Home:
                    drawerLayout.CloseDrawer(listView);
                    drawerToggle.OnOptionsItemSelected(item);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        void CreateFragmentsPages()
        {
            fragment_Boletim = new Fragment_Boletim();
            fragment_InfoEscola = new Fragment_InfoEscola();
            fragment_PerfilResponsavel = new Fragment_PerfilResponsavel();
            fragment_PerfilAluno = new Fragment_PerfilAluno();
            fragment_Endereco = new Fragment_Endereco();
            fragment_Falta = new Fragment_Falta();
            fragment_Comunicado = new Fragment_Comunicado();
            fragment_Configuracoes = new Fragment_Configuracoes();

            var transition = SupportFragmentManager.BeginTransaction();

            transition.Add(Resource.Id.fragmentContainer, fragment_Configuracoes, "Fragment_Configuracoes");
            transition.Hide(fragment_Configuracoes);
            transition.Add(Resource.Id.fragmentContainer, fragment_Comunicado, "Fragment_Comunicado");
            transition.Hide(fragment_Comunicado);
            transition.Add(Resource.Id.fragmentContainer, fragment_Falta, "Fragment_Falta");
            transition.Hide(fragment_Falta);
            transition.Add(Resource.Id.fragmentContainer, fragment_Endereco, "Fragment_Endereco");
            transition.Hide(fragment_Endereco);
            transition.Add(Resource.Id.fragmentContainer, fragment_PerfilAluno, "Fragment_PerfilAluno");
            transition.Hide(fragment_PerfilAluno);
            transition.Add(Resource.Id.fragmentContainer, fragment_PerfilResponsavel, "Fragment_PerfilResponsavel");
            transition.Hide(fragment_PerfilResponsavel);
            transition.Add(Resource.Id.fragmentContainer, fragment_InfoEscola, "Fragment_InfoEscola");
            transition.Hide(fragment_InfoEscola);
            transition.Add(Resource.Id.fragmentContainer, fragment_Boletim, "Fragment_Boletim");

            transition.Commit();
        }
        void CreateNavigatorListView()
        {
            SetContentView(Resource.Layout.Main_Activity);
            listView = FindViewById<ListView>(Resource.Id.listNavView);

            menuList = new List<string>();
            menuList.Add("Acessar Boletim");
            menuList.Add("Perfil Responsável");
            menuList.Add("Perfil Aluno");
            menuList.Add("Meu Endereço");
            menuList.Add("Info Escola");
            menuList.Add("Relatar Ausência");
            menuList.Add("Fazer Comunicado");
            menuList.Add("Configurações");

            ArrayAdapter<string> listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, menuList);
            listView.Adapter = listAdapter;


            listView.ItemClick += ListView_ItemClick;

        }
        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int posicao = e.Position;
            string item = menuList[posicao];

            switch (item)
            {
                case "Acessar Boletim":
                    ViewFragments(fragment_Boletim);
                    return;
                case "Perfil Responsável":
                    ViewFragments(fragment_PerfilResponsavel);
                    return;
                case "Perfil Aluno":
                    ViewFragments(fragment_PerfilAluno);
                    break;
                case "Meu Endereço":
                    ViewFragments(fragment_Endereco);
                    break;
                case "Info Escola":
                    ViewFragments(fragment_InfoEscola);
                    break;
                case "Relatar Ausência":
                    ViewFragments(fragment_Falta);
                    break;
                case "Fazer Comunicado":
                    ViewFragments(fragment_Comunicado);
                    break;
                case "Configurações":
                    ViewFragments(fragment_Configuracoes);
                    break;
            }
        }
        private void ViewFragments(SupportFragment fragment)
        {
            var transition = SupportFragmentManager.BeginTransaction();
            transition.Hide(currentFragment);
            transition.Show(fragment);
            transition.AddToBackStack(null);
            transition.Commit();

            currentFragment = fragment;

            drawerLayout.CloseDrawer(listView);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void BotaoBoletim_Click(object sender, System.EventArgs e)
        {

            HashMap doc = new HashMap();
            doc.Put("origem", aluno.Text);
            nomeAluno = aluno.Text.ToUpper();

            DocumentReference reference = database.Collection(nomeAluno).Document("Boletim");
            reference.Get();



        }
        //public static async Task<Response> MostrarDados(Fragment_Boletim name, List<string> vetor)
        //{
        //    DocumentReference reference = database.Collection(name.NomeAluno).Document("Boletim");

        //    DocumentSnapshot snap = reference.addSnapshotListener(new EventListener<QuerySnapshot>() { });

        //}
        //public override void onEvent(QuerySnapshot snapshots,FirebaseFirestoreException e)
        //{

        //}

    }
}