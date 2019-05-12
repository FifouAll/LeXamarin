using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace IntroAXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carte : ContentPage
    {
        public static double latitude = 0.0;
        public static double longitdude = 0.0;

        public Carte()
        {
            
            /*
            var map = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
            */

            InitializeComponent();
            /*
            var position = new Position(37, -122); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "custom pin",
                Address = "custom detail info"
            };
            MyMap.Pins.Add(pin);
            */
        }

        void OnButtonValiderClicked(object sender, EventArgs e)
        {
            MyMap.Pins.Clear();

            Carte.latitude = MyMap.VisibleRegion.Center.Latitude;
            Carte.longitdude = MyMap.VisibleRegion.Center.Longitude;

            
            var position = new Position(Carte.latitude, Carte.longitdude); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Ami",
                Address = "Habite ici !"
            };
            MyMap.Pins.Add(pin);
            

            //Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}