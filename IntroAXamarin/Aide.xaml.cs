using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntroAXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aide : ContentPage
    {
        public Aide()
        {
            InitializeComponent();

            labelText.Text += MainPage.nbContacts + " contacts enregistrés.";
        }
    }
}