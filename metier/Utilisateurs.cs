using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class Utilisateurs
    {
        private string nomU;
        private string prenomU;
        private string mailU;
        private string mdpU;
        private string serviceU;

        public Utilisateurs(string unNomU, string unPrenom, string unMailU, string unMdpU, string unServiceU)
        {
            nomU = unNomU;
            prenomU = unPrenom;
            mailU = unMailU;
            mdpU = unMdpU;
            serviceU = unServiceU;
        }


        public string NomU { get => nomU; set => nomU = value; }
        public string PrenomU { get => prenomU; set => prenomU = value; }
        public string MailU { get => mailU; set => mailU = value; }
        public string MdpU { get => mdpU; set => mdpU = value; }
        public string ServiceU { get => serviceU; set => serviceU = value; }
    }
}

