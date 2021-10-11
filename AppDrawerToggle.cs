using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportDrawerToggle = AndroidX.AppCompat.App.ActionBarDrawerToggle;
using ActionBarActivity = AndroidX.AppCompat.App.AppCompatActivity;
using AndroidX.DrawerLayout.Widget;

namespace ECED_APP
{
    public class AppDrawerToggle : SupportDrawerToggle
    {

        private ActionBarActivity hostActivity;
        private int openedDrawer1;
        private int closedDrawer1;

        public AppDrawerToggle (ActionBarActivity host, DrawerLayout drawerLayout, int openedDrawer2, int closedDrawer2)
            : base(host, drawerLayout, openedDrawer2, closedDrawer2)
        {
            hostActivity = host;
            openedDrawer1 = openedDrawer2;
            closedDrawer1 = closedDrawer2;
        }
        public override void OnDrawerOpened (Android.Views.View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            hostActivity.SupportActionBar.SetTitle(openedDrawer1);
        }
        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            hostActivity.SupportActionBar.SetTitle(closedDrawer1);
        }
        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffSet)
        {
            base.OnDrawerSlide(drawerView, slideOffSet);
        }


    }
}