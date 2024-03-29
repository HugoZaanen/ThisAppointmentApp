﻿using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ThisAppointmentApp.Models;

namespace ThisAppointmentApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayOverview : ContentPage
    {
        public DayOverview()
        {
            InitializeComponent();
            //MainPageViewModel model = new MainPageViewModel();
            //list.ItemsSource = model.GetAppointments();
            ThisAppointmentApp.Models.Calendar calendar = new ThisAppointmentApp.Models.Calendar();
            list.ItemsSource = calendar.GetTodayAppointments();
            
        }
    }
}