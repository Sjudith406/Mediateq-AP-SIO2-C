using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class Abonnee
    {
        private string loginA;
        private string nomA;
        private string prenomA;
        private string adresseMail;
        private string mdpA;
        private string adresseA;
        private string numTel;
        private DateTime dateNaissance;
        private DateTime datePremierAb;
        private DateTime dateFinAb;
        private string typeA;
        private string numA;

        public Abonnee(string unLoginA, string unNomA, string unPrenomA, string unAdresseMail, string unMdpA, string unAdresseA, string unNumtel, DateTime uneDateNaissance, DateTime uneDatePremierAb, DateTime uneDateFinAb, string unTypeA, string unNumA)
        {
            loginA = unLoginA;
            nomA = unNomA;
            prenomA = unPrenomA;
            adresseMail = unAdresseMail;
            mdpA = unMdpA;
            adresseA = unAdresseA;
            numTel = unNumtel;
            dateNaissance = uneDateNaissance;
            datePremierAb = uneDatePremierAb;
            dateFinAb = uneDateFinAb;
            typeA = unTypeA;
            numA = unNumA;
        }


        public string LoginA { get => loginA; set => loginA = value; }
        public string NomA { get => nomA; set => nomA = value; }
        public string PrenomA { get => prenomA; set => prenomA = value; }
        public string AdresseMail { get => adresseMail; set => adresseMail = value; }
        public string MdpA { get => mdpA; set => mdpA = value; }
        public string AdresseA { get => adresseA; set => adresseA = value; }
        public string NumTel { get => numTel; set => numTel = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public DateTime DatePremierAb { get => datePremierAb; set => datePremierAb = value; }
        public DateTime DateFinAb { get => dateFinAb; set => dateFinAb = value; }
        public string TypeA { get => typeA; set => typeA = value; }
        public string NumA { get => numA; set => numA = value; }
    }
}

