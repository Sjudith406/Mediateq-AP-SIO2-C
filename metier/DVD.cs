using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class DVD : Document
    {
        private string synopsis;
        private string réalisateur;
        private string duree;


        public DVD(string unId, string unTitre, string unSynopsis, string unRealisateur, string uneDuree, string uneImage) : base(unId, unTitre, uneImage)
        {
            synopsis = unSynopsis;
            réalisateur = unRealisateur;
            duree = uneDuree;
        }

        public string Synopsis { get => synopsis; set => synopsis = value; }
        public string Réalisateur { get => réalisateur; set => réalisateur = value; }
        public string Duree { get => duree; set => duree = value; }
    }
}
