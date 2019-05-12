using IntroAXamarin.classes;
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
	public partial class AmisOnCarte : ContentPage
	{
		public AmisOnCarte ()
		{
			InitializeComponent ();

            foreach(Contact c in MainPage.lesContacts)
            {
                var position = new Position(c.Latitude, c.Longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = c.Nom + " " +c.Prenom,
                    Address = c.Email + " " + c.Numero
                };
                AmisMap.Pins.Add(pin);
            }
            
        }
	}
}