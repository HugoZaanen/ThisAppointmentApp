﻿using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThisAppointmentApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekOverzicht : ContentPage
    {
        public WeekOverzicht()
        {
            InitializeComponent();
            //MainPageViewModel model = new MainPageViewModel();
            //ThisAppointmentApp.Models.Calendar calendar = new ThisAppointmentApp.Models.Calendar();
            //ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();
            //List<ScheduleAppointment> appointments = model.ScheduleAppointments;
            
            //schedule.DataSource = appointments;            
        }
    }
}