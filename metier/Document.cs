

namespace Mediateq_AP_SIO2.metier
{
    class Document
    {
        private string idDoc;
        private string titre;
        private string image;
        private string laCategorie;

        public Document(string unId, string unTitre, string uneImage)
        {
            idDoc = unId;
            titre = unTitre;
            image = uneImage;
        }


        public string IdDoc { get => idDoc; set => idDoc = value; }
        public string Titre { get => titre; set => titre = value; }
        public string Image { get => image; set => image = value; }
        public string LaCategorie { get => laCategorie; set => laCategorie = value; }
    }


}
