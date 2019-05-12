using IntroAXamarin.classes;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using Plugin.Geolocator;
using System.Diagnostics;
//using Plugin.Geolocator;

namespace IntroAXamarin
{
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<Contact> lesContacts;
        public static int nbContacts;
        public static Plugin.Geolocator.Abstractions.IGeolocator locator;


        public MainPage()
        {
            MainPage.nbContacts = 0;
            InitializeComponent();
            MainPage.lesContacts = new ObservableCollection<Contact>();

            /*
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("IntroAXamarin.storage.save_file.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            */

            //this.loadContacts();

            getLocation();

            // DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", "Eric;0621211212");

            /*

            var position = locator.GetPositionAsync(timeoutMilliseconds: 10000);

            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);
            */
        }

        /*
         * Zone de test Locator GPS 
         * */
        public async void getLocation()
        {
            await StartListening();
        }
        private async Task RetreiveLocation()
        {
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            {

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));
                if (position == null)
                    return;

                Debug.WriteLine("Position Status: {0}", position.Timestamp);
                Debug.WriteLine("Position Latitude: {0}", position.Latitude.ToString());
                Debug.WriteLine("Position Longitude: {0}", position.Longitude.ToString());

                locator.PositionChanged += (sender, e) => {
                    position = e.Position;

                    Debug.WriteLine("Position Latitude: {0}", position.Latitude.ToString());
                    Debug.WriteLine("Position Longitude: {0}", position.Longitude.ToString());
                };

            }
            else
            {
                await DisplayAlert("Error", "Home", "OK");
            }


        }

        async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            ///This logic will run on the background automatically on iOS, however for Android and UWP you must put logic in background services. Else if your app is killed the location updates will be killed.
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });

            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var test = e.Position;
                Debug.WriteLine("Full: Lat: " + test.Latitude.ToString() + " Long: " + test.Longitude.ToString());
                Debug.WriteLine("\n" + $"Time: {test.Timestamp.ToString()})");
                Debug.WriteLine("\n" + $"Heading: {test.Heading.ToString()}");
                Debug.WriteLine("\n" + $"Speed: {test.Speed.ToString()}");
                Debug.WriteLine("\n" + $"Accuracy: {test.Accuracy.ToString()}");
                Debug.WriteLine("\n" + $"Altitude: {test.Altitude.ToString()}");
                Debug.WriteLine("\n" + $"AltitudeAccuracy: {test.AltitudeAccuracy.ToString()}");
            });
        }



        void ajoutBTClicked(object sender, EventArgs e)
        {
            string[] coords = emplacementLabel.Text.Split(';');
            double lat = Double.Parse(coords[0]);
            double lon = Double.Parse(coords[1]);
            Contact c = new Contact(nomContact.Text, prenomContact.Text, mailContact.Text, numeroContact.Text, lat, lon);
            lesContacts.Add(c);
            confirmLabel.Text = String.Format("{0} avec le numéro {1}",
                                       nomContact.Text, numeroContact.Text);
            this.saveContacts();

            /*
            string line = string.Format("{0};{1}\n", nomContact.Text, numeroContact.Text);
            var document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(document, "MyContacts.txt");
            File.AppendAllText(filename, line);
            */
        }

        public void saveContacts()
        {
            string toSaveText = "";
            foreach (Contact c in MainPage.lesContacts)
            {
                toSaveText += c.Nom + ";" + c.Prenom + ";" + c.Email + ";" + c.Numero + ";" + c.Latitude + ";" + c.Longitude + "\n";
            }
            DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", toSaveText);
        }

        public void loadContacts()
        {
            String recupere = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");

            String[] lesLignesContact = recupere.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var item in lesLignesContact)
            {
                MainPage.nbContacts++;
                if (item.Length > 0)
                {
                    string[] leContact = item.Split(';');
                    Contact c = new Contact(leContact[0], leContact[1], leContact[2], leContact[3], Double.Parse(leContact[4]), Double.Parse(leContact[5]));
                    lesContacts.Add(c);
                }
            }
        }

        void OnButtonAideClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Aide());
        }

        void OnButtonCarteClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carte());
        }

        void OnButtonChercherClicked(object sender, EventArgs e)
        {

              
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Carte.latitude != 0.0)
            {
                emplacementLabel.Text = Carte.latitude + ";" + Carte.longitdude;
                //DisplayAlert("Alerte", "Retour ici", "OK");
            }
            System.Diagnostics.Debug.WriteLine("*****Here*****");

        }

    }
}
