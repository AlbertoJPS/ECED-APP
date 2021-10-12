using Android.App;
using Android.Content;
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

namespace ECED_APP.Fragments
{
    public class Fragment_Boletim : AndroidX.Fragment.App.Fragment
    {
        FirebaseFirestore boletimDB;
        EditText Aluno;
        string nomeAluno;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            boletimDB = DBConection.GetDatabase();
            nomeAluno = Aluno.Text.ToUpper();
            DocumentReference reference = boletimDB.Collection(nomeAluno).Document("Boletim");


            //Log.Info(tag, "Is persistence enabled: " + boletimDB.getFirestoreSettings().isPersistenceEnabled());
            //reference.Get().AddOnCompleteListener(new OnCompleteListener<DocumentSnapshot>())
            //{
            //}

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