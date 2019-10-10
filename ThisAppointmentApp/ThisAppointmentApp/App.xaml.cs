﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPageViewModel mainpageModel = new MainPageViewModel();
            MainPage = new ThisAppointmentTab();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
