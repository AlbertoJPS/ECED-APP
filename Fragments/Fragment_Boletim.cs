﻿using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECED_APP.DBConection;
using static AndroidX.AppCompat.App.AppCompatActivity;
//using Google.Cloud.Firestore;

namespace ECED_APP.Fragments
{
    [Activity(Label = "@string/app_name", Theme = "@style/EcedTheme", MainLauncher = false)]
    public class Fragment_Boletim : AndroidX.Fragment.App.Fragment
    {
        private FirebaseFirestore boletimDB;

        private ScrollView scrollViewBoletim;
        private List<string> menuListBoletim;
        private ArrayAdapter listAdapterBoletim;
        private string nomeAluno;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //boletimDB = ECED_APP.DBConection.GetDatabase();
            // MostrarDados(nomeAluno);




            //SetContentView(Resource.Layout.Fragment_Boletim);
            //scrollView = (ScrollView)FindViewById(Resource.Id.scrollViewBoletim);

            //menuList = new List<string>();
            //menuList.Add("Acessar Boletim");
            //menuList.Add("Perfil Responsável");
            //menuList.Add("Perfil Aluno");
            //menuList.Add("Meu Endereço");
            //menuList.Add("Info Escola");
            //menuList.Add("Relatar Ausência");
            //menuList.Add("Fazer Comunicado");
            //menuList.Add("Configurações");

            //ArrayAdapter<string> listAdapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleListItem1, menuList);
            //scrollView.Adapter = listAdapter;

            //materia = FindViewById<TextView>(Resource.Id.materiaBoletim);

            FetchData();
        }

        void FetchData()
        {
            boletimDB.Collection(nomeAluno).Get().AddOnSuccessListener((IOnSuccessListener)this);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Boletim, container, false);


            return view;
        }

    }
}