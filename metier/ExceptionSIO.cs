using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class ExceptionSIO : Exception
    {
        private int niveauExc;
        private string libelleExc;

        public ExceptionSIO(int pNiveau, string pLibelle)
        {
            niveauExc = pNiveau;
            libelleExc = pLibelle;
        }

        public int NiveauExc { get => niveauExc; set => niveauExc = value; }
        public string LibelleExc { get => libelleExc; set => libelleExc = value; }
    }
}
