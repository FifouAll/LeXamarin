using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAXamarin.classes
{
    public class Contact
    {
        private int id;
        private string nom;
        private string prenom;
        private string email;
        private string numero;
        private double latitude;
        private double longitude;
        
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public Contact() { }

        public Contact(int id, string nom, string num)
        {
            this.Id = id;
            this.Nom = nom;
            this.Numero = num;
        }

        public Contact(string nom, string num)
        {
            this.Nom = nom;
            this.Numero = num;
        }

        public Contact(int id, string nom, string prenom, string email, string numero, double latitude, double longitude)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Numero = numero;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Contact(string nom, string prenom, string email, string numero, double latitude, double longitude)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Numero = numero;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
