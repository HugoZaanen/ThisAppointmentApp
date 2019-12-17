using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThisAppointmentApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThisAppointmentApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThisAppointmentTab : TabbedPage
    {   
        public ThisAppointmentTab()
        {
            InitializeComponent();
            
            var DayOverView = new NavigationPage(new DayOverview());
            var CurrentOverView = new NavigationPage(new AppointmentView());
            var WeekOverview = new NavigationPage(new WeekOverzicht());

            DayOverView.Title = "Day overview";
            CurrentOverView.Title = "Next appointment";
            WeekOverview.Title = "Week overview";

            Children.Add(CurrentOverView);
            Children.Add(DayOverView);
            Children.Add(WeekOverview);
        }
    }
}