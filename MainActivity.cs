using Android.App;
using Android.Content;
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
using System.Collections.Generic;
using SupportFragment = AndroidX.Fragment.App.Fragment;


namespace ECED_APP
{
    [Activity(Label = "@string/app_name", Theme = "@style/EcedTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        
        AndroidX.DrawerLayout.Widget.DrawerLayout drawerLayout;
        AndroidX.AppCompat.Widget.Toolbar mainToolbar;
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



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main_Activity);
            DBConection.GetDatabase();
            CreateFragmentsPages();
            currentFragment = fragment_Boletim;
            CreateNavigatorListView();
            ConnectNavigator();







        }
       

        void ConnectNavigator()
        {

            drawerLayout = FindViewById<AndroidX.DrawerLayout.Widget.DrawerLayout>(Resource.Id.drawerLayout);
            mainToolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar> (Resource.Id.mainToolbar);
            listView = FindViewById<ListView>(Resource.Id.listNavView);

            SetSupportActionBar(mainToolbar);

            drawerToggle = new AppDrawerToggle( this,  drawerLayout,  Resource.String.openDrawer, Resource.String.closeDrawer);

            SupportActionBar.Title = "";
            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;

            drawerLayout.AddDrawerListener(drawerToggle);

            //actionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
            //actionBar.SetDisplayShowTitleEnabled(true);
            actionBar.SetHomeButtonEnabled(true);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            drawerToggle.SyncState();


        }
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.nav_menu, menu);
        //    return base.OnCreateOptionsMenu(menu);
        //}
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