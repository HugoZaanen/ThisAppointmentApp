﻿using System.ComponentModel;
using Xamarin.Forms;

namespace ThisAppointmentApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
            var viewModel = this.BindingContext as MainPageViewModel;
            list.ItemsSource = viewModel.Meetings;
        }
    }
}
