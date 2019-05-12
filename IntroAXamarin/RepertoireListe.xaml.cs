using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;

using IntroAXamarin.classes;

namespace IntroAXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RepertoireListe : ContentPage
    {
        public static ObservableCollection<Contact> lesContacts;

        public RepertoireListe()
        {
            InitializeComponent();

            ContactView.ItemsSource = MainPage.lesContacts;

            ContactView.ItemSelected += ContactView_ItemSelected;
        }

        async private void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Contact c = (Contact)e.SelectedItem;
            bool res = await DisplayAlert("Informations du contact", "Contact : " + c.Nom + " " + c.Prenom + " " + c.Email + " " + c.Numero, "OK", "Supprimer");
            if (res == false) {
                bool resu = await DisplayAlert("Suppression du contact", "Etes vous sur ?", "OK", "Annuler");
                if (resu)
                {
                    MainPage.lesContacts.Remove(c);
                }
            }
            
        }

        void OnButtonCarteClicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new AmisOnCarte());
        }
    }
}