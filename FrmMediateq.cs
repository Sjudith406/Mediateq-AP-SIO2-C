using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Mediateq_AP_SIO2.metier;


namespace Mediateq_AP_SIO2
{
    public partial class FrmMediateq : Form
    {
        #region Variables globales

        static List<Categorie> lesCategories;
        static List<Descripteur> lesDescripteurs;
        static List<Revue> lesRevues;
        static List<Livre> lesLivres;
        static List<DVD> lesDVDs;
        static List<Utilisateurs> lesUtilisateurs;
        static List<Abonnee> lesAbonnees;


        #endregion


        #region Procédures évènementielles

        public FrmMediateq()
        {
            InitializeComponent();
        }

        private void FrmMediateq_Load(object sender, EventArgs e)
        {
            try
            {
                // Création de la connexion avec la base de données
                DAOFactory.creerConnection();

                // Chargement des objets en mémoire
                lesDescripteurs = DAODocuments.getAllDescripteurs();
                lesRevues = DAOPresse.getAllRevues();

                // Masquer toutes les pages sauf la page de connexion
                foreach (TabPage tabPage in tabOngletsApplication.TabPages)
                {
                    if (tabPage != tabConnexion)
                    {
                        tabOngletsApplication.TabPages.Remove(tabPage);
                    }
                }
            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show(exc.NiveauExc + "-" + exc.LibelleExc + "-" + exc.Message);
            }
        }

        #endregion


        #region Parutions
        //-----------------------------------------------------------
        // ONGLET "PARUTIONS"
        //------------------------------------------------------------
        private void tabParutions_Enter(object sender, EventArgs e)
        {
            cbxTitres.DataSource = lesRevues;
            cbxTitres.DisplayMember = "titre";
        }

        private void cbxTitres_SelectedIndexChanged(object sender, EventArgs e)
        {
                List<Parution> lesParutions;

                Revue titreSelectionne = (Revue)cbxTitres.SelectedItem;
                lesParutions = DAOPresse.getParutionByTitre(titreSelectionne);

                // ré-initialisation du dataGridView
                dgvParutions.Rows.Clear();

                // Parcours de la collection des titres et alimentation du datagridview
                foreach (Parution parution in lesParutions)
                {
                    dgvParutions.Rows.Add(parution.Numero, parution.DateParution, parution.Photo);
                }
            
        }
        #endregion


        #region Revues
        //-----------------------------------------------------------
        // ONGLET "TITRES"
        //------------------------------------------------------------
        private void tabTitres_Enter(object sender, EventArgs e)
        {
            cbxDomaines.DataSource = lesDescripteurs;
            cbxDomaines.DisplayMember = "libelle";
        }

        private void cbxDomaines_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Objet Domaine sélectionné dans la comboBox
            Descripteur domaineSelectionne = (Descripteur)cbxDomaines.SelectedItem;

            // ré-initialisation du dataGridView
            dgvTitres.Rows.Clear();

            // Parcours de la collection des titres et alimentation du datagridview
            foreach (Revue revue in lesRevues)
            {
                if (revue.IdDescripteur==domaineSelectionne.Id)
                {
                    dgvTitres.Rows.Add(revue.Id, revue.Titre, revue.Empruntable, revue.DateFinAbonnement, revue.DelaiMiseADispo);
                }
            }
        }
        #endregion


        #region Livres
        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        private void tabLivres_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();

            foreach (Livre liv in lesLivres)
            {
                dgvLivres.Rows.Add(liv.IdDoc, liv.Titre, liv.Auteur, liv.ISBN1, liv.LaCollection);
            }
        }
   
        private void btnRechercher_Click(object sender, EventArgs e)
        {
            // On réinitialise les labels
            lblNumero.Text = "";
            lblTitre.Text = "";
            lblAuteur.Text = "";
            lblCollection.Text = "";
            lblISBN.Text = "";
            lblImage.Text = "";

            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool trouve = false;
            foreach (Livre livre in lesLivres)
            {
                if (livre.IdDoc==txbNumDoc.Text)
                {
                    lblNumero.Text = livre.IdDoc;
                    lblTitre.Text = livre.Titre;
                    lblAuteur.Text = livre.Auteur;
                    lblCollection.Text = livre.LaCollection;
                    lblISBN.Text = livre.ISBN1;
                    lblImage.Text = livre.Image;
                    trouve = true;
                }
            }
            if (!trouve)
                MessageBox.Show("Document non trouvé dans les livres");
        }

        private void txbTitre_TextChanged(object sender, EventArgs e)
        {
            dgvLivres.Rows.Clear();

            // On parcourt tous les livres. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (Livre l1 in lesLivres)
            {

                // on passe le champ de saisie et le nom en minuscules car la méthode Contains .ToString("dd/MM/yyyy")
                // tient compte de la casse.
                string saisieMinuscules;
                saisieMinuscules = txbTitre.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = l1.Titre.ToLower();

                //on teste si le nom de l'abonnee contient ce qui a été saisi
                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvLivres.Rows.Add(l1.IdDoc, l1.Titre, l1.Auteur, l1.ISBN1, l1.LaCollection);

                }
            }
        }
        #endregion


        #region DVD
        //-----------------------------------------------------------
        // ONGLET "DVDs"
        //-----------------------------------------------------------

        private void tabDVD_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesDVDs = DAODocuments.getAllDvds();

            foreach(DVD dvd1 in lesDVDs)
            {
                dgvDVDs.Rows.Add(dvd1.IdDoc, dvd1.Titre, dvd1.Réalisateur, dvd1.Duree);
            }
        }
    
        private void btnRechercheDVD_Click(object sender, EventArgs e)
        {
            // On réinitialise les labels
            lblAfficheNumdv.Text = "";
            lblAffTdvd.Text = "";
            lblAffSdvd.Text = "";
            lblAffRdvd.Text = "";
            lblAffDdvd.Text = "";
            lblAffEdvd.Text = "";

            // On recherche le dvd correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool trouve = false;
            foreach (DVD dvd in lesDVDs)
            {
                if (dvd.IdDoc == txbNumdvd.Text)
                {
                    lblAfficheNumdv.Text = dvd.IdDoc;
                    lblAffTdvd.Text = dvd.Titre;
                    lblAffEdvd.Text = dvd.Image;
                    lblAffSdvd.Text = dvd.Synopsis;
                    lblAffRdvd.Text = dvd.Réalisateur;
                    lblAffDdvd.Text = dvd.Duree;
                    trouve = true;
                }
            }
            if (!trouve)
                MessageBox.Show("Document non trouvé dans les Dvd");
        }
        private void txbTitredvd_TextChanged(object sender, EventArgs e)
        {
            dgvDVDs.Rows.Clear();

            // On parcourt tous les dvds. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (DVD dvd in lesDVDs)
            {
                // on passe le champ de saisie et le titre en minuscules car la méthode Contains
                // tient compte de la casse.
                string saisieMinuscules;
                saisieMinuscules = txbTitredvd.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = dvd.Titre.ToLower();

                //on teste si le titre du dvd contient ce qui a été saisi
                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvDVDs.Rows.Add(dvd.IdDoc, dvd.Titre, dvd.Réalisateur, dvd.Duree);
                }
            }
        }
        #endregion


        #region Utilisateurs
        //-----------------------------------------------------------
        // ONGLET "Utilisateurs"
        //-----------------------------------------------------------
        private void tabConnexion_Enter(object sender, EventArgs e)
        {
            lesUtilisateurs = DAODocuments.getAllUtilisateurs();
        }

        private void btnConncter_Click(object sender, EventArgs e)
        {

            bool trouve = false;

            string mail = txbUtils.Text;
            string Mdpasse = txbMdp.Text;

            // Vérifier que le mot de passe n'est pas null ou vide
            if (string.IsNullOrEmpty(Mdpasse))
            {
                MessageBox.Show("Veuillez saisir un mot de passe.");
                return;
            }


            foreach (Utilisateurs u in lesUtilisateurs)

            {

                if (u.MailU == txbUtils.Text)

                {

                    string mdp = txbMdp.Text;

                    string mdpHache = CalculerHashSHA256(mdp);

                    if (u.MdpU == mdpHache)

                    {

                        trouve = true;

                        tabOngletsApplication.TabPages.Remove(tabConnexion); // masque l'onglet Connexion

                        if (u.ServiceU == "Administrateur")

                        {

                            tabOngletsApplication.TabPages.Add(tabParutions);
                            tabOngletsApplication.TabPages.Add(tabTitres);
                            tabOngletsApplication.TabPages.Add(tabDVD);
                            tabOngletsApplication.TabPages.Add(tabLivres);
                            tabOngletsApplication.TabPages.Add(tabGestionAb);
                            tabOngletsApplication.TabPages.Add(tabCrudDvd);
                            tabOngletsApplication.TabPages.Add(tabCrudLivres);

                        }

                        else

                        if (u.ServiceU == "Prêt")

                        {
                            tabOngletsApplication.TabPages.Add(tabParutions);
                            tabOngletsApplication.TabPages.Add(tabLivres);
                            tabOngletsApplication.TabPages.Add(tabDVD);
                        }

                        else

                        if (u.ServiceU == "Culture")

                        {

                            tabOngletsApplication.TabPages.Add(tabParutions);

                        }

                        else if (u.ServiceU == "Administratif")

                        {

                            tabOngletsApplication.TabPages.Add(tabTitres);
                            tabOngletsApplication.TabPages.Add(tabLivres);
                            tabOngletsApplication.TabPages.Add(tabDVD);
                            tabOngletsApplication.TabPages.Add(tabGestionAb);
                            tabOngletsApplication.TabPages.Add(tabCrudDvd);
                            tabOngletsApplication.TabPages.Add(tabCrudLivres);

                        }

                    }

                }

            }

            if (!trouve)

                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect");
        }
        private string CalculerHashSHA256(string input) { 

            

                using (SHA256 sha256 = SHA256.Create())

                {

                    // Convertir la chaîne en tableau de bytes

                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                    // Calculer le hachage

                    byte[] hashBytes = sha256.ComputeHash(inputBytes);

                    // Convertir le hachage en une chaîne hexadécimale

                    string hashString = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                hashString = hashString.ToLower();
                    return hashString;

                }

        }

        

        #endregion


        #region Abonnees
        //-----------------------------------------------------------
        // ONGLET "Abonnees"
        //-----------------------------------------------------------
        private void tabGestionAb_Enter(object sender, EventArgs e)
        {
            lesAbonnees = DAODocuments.getAllAbonnees();
            foreach (Abonnee Ab in lesAbonnees)
            {
                dgvConsult.Rows.Add(Ab.LoginA, Ab.NomA, Ab.PrenomA, Ab.AdresseMail, Ab.MdpA, Ab.AdresseA, Ab.NumTel, Ab.DateNaissance.ToString("dd/MM/yyyy"), Ab.DatePremierAb.ToString("dd/MM/yyyy"), Ab.DateFinAb.ToString("dd/MM/yyyy"), Ab.TypeA);
            }
        }    

        private void txbConsutationAb_TextChanged(object sender, EventArgs e)
        {
            // Efface toutes les lignes du DataGridView
            dgvConsult.Rows.Clear();
           
            
                foreach (Abonnee Abo in lesAbonnees)
                {

                    // on passe le champ de saisie et le nom en minuscules car la méthode Contains .ToString("dd/MM/yyyy")
                    // tient compte de la casse.
                    string saisieMinuscules;
                    saisieMinuscules = txbConsutationAb.Text.ToLower();
                    string titreMinuscules;
                    titreMinuscules = Abo.NomA.ToLower();

                    //on teste si le nom de l'abonnee contient ce qui a été saisi
                    if (titreMinuscules.Contains(saisieMinuscules))
                    {
                        dgvConsult.Rows.Add(Abo.LoginA, Abo.NomA, Abo.PrenomA, Abo.AdresseMail, Abo.MdpA, Abo.AdresseA, Abo.NumTel, Abo.DateNaissance.ToString("dd/MM/yyyy"), Abo.DatePremierAb.ToString("dd/MM/yyyy"), Abo.DateFinAb.ToString("dd/MM/yyyy"), Abo.TypeA);
                    }
                }

        }

        private void btnCreationAb_Click(object sender, EventArgs e)
        {
            string login = txbCrLoginAb.Text;
            string nomAb = txbCrNomAb.Text;
            string prenomAb = txbCrPrenomAb.Text;
            string mail = txbCrMailAb.Text;
            string mdp = txbCrMdpAb.Text;
            string adresse = txbCrAdresseAb.Text;
            string numtel = txbCrNumAb.Text;
            string dateNaissance = dtpCrdateNAb.Text;
            string datePremier = dtpCrdatePAb.Text;
            string dateFinAb = dtpCrdateFAb.Text;
            string typeAbo = txbCrTypeAb.Text;

            // Vérification si l'abonné existe déjà
           /* foreach (Abonnee Ab in lesAbonnees)
            {
                if(login == Ab.LoginA)
                {
                    MessageBox.Show("L'abonnée existe déja !");// Sortir de la méthode si l'abonné existe déjà
                }
            }*/

            // Vérification si l'abonné existe déjà
            if (lesAbonnees.Any(ab => ab.LoginA == login))
            {
                MessageBox.Show("L'abonné existe déjà !");
                
                // Effacement des valeurs des contrôles de l'interface utilisateur
                txbCrLoginAb.Text = "";
                txbCrNomAb.Text = "";
                txbCrPrenomAb.Text = "";
                txbCrMailAb.Text = "";
                txbCrMdpAb.Text = "";
                txbCrAdresseAb.Text = "";
                txbCrNumAb.Text = "";
                dtpCrdateNAb.Value = DateTime.Now;
                dtpCrdatePAb.Value = DateTime.Now;
                dtpCrdateFAb.Value = DateTime.Now;
                txbCrTypeAb.Text = "";

                return; // Sortir de la méthode si l'abonné existe déjà
            }

            // Effacement des valeurs des contrôles de l'interface utilisateur
            txbCrLoginAb.Text = "";
            txbCrNomAb.Text = "";
            txbCrPrenomAb.Text = "";
            txbCrMailAb.Text = "";
            txbCrMdpAb.Text = "";
            txbCrAdresseAb.Text = "";
            txbCrNumAb.Text = "";
            txbCrTypeAb.Text = "";
            
            Text = "";
            try { 


            // Ajout de l'abonné dans la base de données et la liste des abonnés
            DAODocuments.AjouterAbonnee(login, nomAb, prenomAb, mail, mdp, adresse, numtel, DateTime.Parse(dateNaissance), DateTime.Parse(datePremier), DateTime.Parse(dateFinAb), typeAbo);
            
            Abonnee Ab1 = new Abonnee(login, nomAb, prenomAb, mail, mdp, adresse, numtel, DateTime.Parse(dateNaissance), DateTime.Parse(datePremier), DateTime.Parse(dateFinAb), typeAbo, "");
            lesAbonnees.Add(Ab1);

            // Affichage d'un message de confirmation
            MessageBox.Show("Abonnée créer !");
            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show("Erreur dans la création de l'abonnée : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }

        }

        private void btnMafficheAb_Click(object sender, EventArgs e)
        {
            // Récupération du login de l'abonné à afficher
            String logA = txbMAlogAb.Text;

            for (int i = 0; i < lesAbonnees.Count; i++)
            {
                // Récupération de l'abonné courant dans la boucle
                Abonnee ab = lesAbonnees[i];
                string login = ab.LoginA;
                string adresse = ab.AdresseA;
                string mail = ab.AdresseMail;
                string num = ab.NumTel;
                DateTime dateP = ab.DatePremierAb;
                DateTime dateF = ab.DateFinAb;
                string motdp = ab.MdpA;


                // Calcul de la différence entre la date de fin d'abonnement et la date actuelle
                //DateTime date = ab.DateFinAb;
                TimeSpan difference = dateF - DateTime.Now;

                // Vérification si le login correspond à celui fourni dans l'interface utilisateur
                if (logA == login)
                {
                    // Affichage des informations de l'abonnée dans des contrôles d'interface utilisateur
                    lblMaffichadresseAb.Text = adresse;
                    lblMaffichMailAb.Text = mail;
                    lblMaffichNumAb.Text = num;
                    lblMaffichdatePab.Text = dateP.ToString("dd/MM/yyyy");
                    lblMaffichDateFAb.Text = dateF.ToString("dd/MM/yyyy");
                    lblMaffichMdpAb.Text = motdp;

                    // Vérification de la proximité de la date d'expiration de l'abonnement
                    if (difference.Days <= 30 && difference.Days >= 0)
                    {
                        
                        MessageBox.Show("L'abonnement expire dans les 30 jours.");
                    }
                    else if (dateF < DateTime.Now)
                    {
                        MessageBox.Show("\nAlerte: L'abonnement a déjà expiré.");
                    }
                }
            }

        }

        private void btnModifierAb_Click(object sender, EventArgs e)
        {

            // Récupération des valeurs depuis les contrôles de l'interface utilisateur
            string log = txbMdlogAm.Text;
            string adres = txbMdadressAm.Text;
            string mail = txbMdmailAm.Text;
            string mdp = txbMmdpAm.Text;
            string numtel = txbMdnumAm.Text;
            DateTime datP = dtpMdatePab.Value;
            DateTime datF = dtpMdateFinAb.Value;
            string typAb = txbMtypeAm.Text;

            // Parcourir la liste des abonnées pour trouver celui correspondant au login fourni
            for (int i = 0; i < lesAbonnees.Count; i++)
            {
                if (log == lesAbonnees[i].LoginA.ToString())
                {
                    // Récupération de l'abonné correspondant
                    Abonnee ab = lesAbonnees[i];

                    // Mise à jour des propriétés de l'abonnée avec les nouvelles valeurs
                    ab.AdresseA = adres;
                    ab.AdresseMail = mail;
                    ab.MdpA = mdp;
                    ab.NumTel = numtel;
                    ab.DatePremierAb = datP;
                    ab.DateFinAb = datF;
                    ab.TypeA = typAb;
                   
                    
                    // Appel de la méthode pour modifier l'abonnée dans la base de données
                    DAODocuments.ModifierAbonnee(log, mdp, adres, mail, numtel, datP, datF, typAb);

                    // Affichage d'un message de confirmation
                    MessageBox.Show("Vous avez modifier les informations de l'abonnée " + log +" !");
                    
                    // Effacement des valeurs des contrôles de l'interface utilisateur
                    txbMdlogAm.Text = "";
                    txbMdadressAm.Text = "";
                    txbMdmailAm.Text = "";
                    txbMmdpAm.Text = "";
                    txbMdnumAm.Text = "";
                    dtpMdatePab.Value = DateTime.Now;
                    dtpMdateFinAb.Value = DateTime.Now;
                    txbMtypeAm.Text = "";

                }

            }
        }

        private void btnMdAffichAbo_Click(object sender, EventArgs e)
        {
            string loginAbo = txbMdlogAm.Text;

            for (int i = 0; i < lesAbonnees.Count; i++)
            {
                // Récupération de l'abonnée courant dans la boucle
                Abonnee ab = lesAbonnees[i];
                string logAb = ab.LoginA;
                string adresseAb = ab.AdresseA;
                string mailAb = ab.AdresseMail;
                string numAb = ab.NumTel;
                DateTime datePab = ab.DatePremierAb;
                DateTime dateFab = ab.DateFinAb;
                string motdpAb = ab.MdpA;
                string typeAb = ab.TypeA;


                // Vérification si le login correspond à celui fourni dans l'interface utilisateur
                if (loginAbo == logAb)
                {
                    // Affichage des informations de l'abonnée dans des contrôles d'interface utilisateur
                    txbMdadressAm.Text = adresseAb;
                    txbMdmailAm.Text = mailAb;
                    txbMdnumAm.Text = numAb;
                    txbMmdpAm.Text = motdpAb;
                    dtpMdatePab.Text = datePab.ToString("dd/MM/yyyy");
                    dtpMdateFinAb.Text = dateFab.ToString("dd/MM/yyyy");
                    txbMtypeAm.Text = typeAb;
                }
            }
        }


        #endregion


        #region Crud Livres
        //-----------------------------------------------------------
        // ONGLET "Crud Livres"
        //-----------------------------------------------------------
        private void tabCrudLivres_Enter(object sender, EventArgs e)
        {
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();
            lesCategories = DAODocuments.getAllCategories();

        }

        private void btnCreationLiv_Click(object sender, EventArgs e)
        {
            int numeroDoc = int.Parse(txbNumCrLiv.Text);
            string titreDoc = txbTitreCrLiv.Text;
            int nbCommande = int.Parse(txbCmdCrLiv.Text);
            string imageLiv = txbImgCrLiv.Text;
            int publicDoc = cbxPublicCrLiv.SelectedIndex;
            string codeLiv = txbCodeCrLiv.Text;
            string auteurLiv = txbAuteurCrLiv.Text;
            string collectionLiv = txbCollctCrLiv.Text;
            string descriptionLiv = txbDescCrLiv.Text;
            int nbpPagesLiv = int.Parse(txbNbPageCrLiv.Text);
            string dateSortie = dtpDateStCrLiv.Text;

            // Vérification si le Document existe déjà
            if (lesLivres.Any(liv => liv.IdDoc == numeroDoc.ToString()))
            {
                MessageBox.Show("Le Livre existe déjà !");

                // Effacement des valeurs des contrôles de l'interface utilisateur
                txbNumCrLiv.Text = "";
                txbTitreCrLiv.Text = "";
                txbCmdCrLiv.Text = "";
                txbImgCrLiv.Text = "";
                cbxPublicCrLiv.Text = "";
                txbCodeCrLiv.Text = "";
                txbAuteurCrLiv.Text = "";
                txbCollctCrLiv.Text = "";
                txbDescCrLiv.Text = "";
                txbNbPageCrLiv.Text = "";
                dtpDateStCrLiv.Value = DateTime.Now;

                return; // Sortir de la méthode si le livre existe déjà
            }

            // Effacement des valeurs des contrôles de l'interface utilisateur
            txbNumCrLiv.Text = "";
            txbTitreCrLiv.Text = "";
            txbCmdCrLiv.Text = "";
            txbImgCrLiv.Text = "";
            cbxPublicCrLiv.Text = "";
            txbCodeCrLiv.Text = "";
            txbAuteurCrLiv.Text = "";
            txbCollctCrLiv.Text = "";
            txbDescCrLiv.Text = "";
            txbNbPageCrLiv.Text = "";
            dtpDateStCrLiv.Value = DateTime.Now;

            try
            {
                //Créer le document
                DAODocuments.CreerDocument(numeroDoc, titreDoc, imageLiv, nbCommande, publicDoc);
                //Ensuite créer le livre
                DAODocuments.CreerLivre(numeroDoc, codeLiv, auteurLiv, collectionLiv, descriptionLiv, nbpPagesLiv, DateTime.Parse(dateSortie));
                Livre lv = new Livre(numeroDoc.ToString(), titreDoc, codeLiv, auteurLiv, collectionLiv, imageLiv);
                lesLivres.Add(lv);

                //Afficher un merssage de confirmation
                MessageBox.Show("Livre créer !");
            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show("Erreur dans la création du livre : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }

        }

        private void btnAffichLiv_Click(object sender, EventArgs e)
        {
            string numLiv = txbNumModifLiv.Text;
            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool resultat = false;
            foreach(Livre liv in lesLivres)
            {
                if(liv.IdDoc == numLiv)
                {
                    txbTitreModifLiv.Text = liv.Titre;
                    txbImgModifLiv.Text = liv.Image;
                    txbCodeModifLiv.Text = liv.ISBN1;
                    txbAuteurModifLiv.Text = liv.Auteur;
                    txbCollctModifLiv.Text = liv.LaCollection;
                    resultat = true;
                }
            }
            if (!resultat)
                MessageBox.Show("Document non trouvé dans les livres");

        }

        private void txbSearchLiv_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSupLiv_Click(object sender, EventArgs e)
        {
            int idLivre = int.Parse(txbNumModifLiv.Text);

            try
            {
                DAODocuments.SuprimerLivre(idLivre);
                DAODocuments.SuprimerDocument(idLivre);
                MessageBox.Show("Livre supprimer !!");

                // Supprimer le livre de la collection
                /*foreach (Livre liv1 in lesLivres)
                {
                    if (liv1.IdDoc == idLivre.ToString())
                    {
                        lesLivres.Remove(liv1);
                    }
                }*/

                //vider les text box
                txbNumModifLiv.Text = "";
                txbTitreModifLiv.Text = "";
                txbCodeModifLiv.Text = "";
                txbCollctModifLiv.Text = "";
                txbImgModifLiv.Text = "";
                txbAuteurModifLiv.Text = "";

            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show("Erreur dans la suppression du livre : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }
        }

        private void btnModifLiv_Click(object sender, EventArgs e)
        {
            //recupérer les données
            string idLivre = txbNumModifLiv.Text;
            string auteurLivre = txbAuteurModifLiv.Text;
            string collectionLivre = txbCollctModifLiv.Text;

            for (int i=0; i < lesLivres.Count; i++)
            {
                if(idLivre == lesLivres[i].IdDoc.ToString())
                {
                    Livre lv2 = lesLivres[i];

                    lv2.Auteur = auteurLivre;
                    lv2.LaCollection = collectionLivre;

                    //
                    DAODocuments.ModifierLivre(int.Parse(idLivre), auteurLivre, collectionLivre);
                    MessageBox.Show("Les informations du livre ont été modifier ! ");

                    //vider les text box
                    txbNumModifLiv.Text = "";
                    txbTitreModifLiv.Text = "";
                    txbCodeModifLiv.Text = "";
                    txbCollctModifLiv.Text = "";
                    txbImgModifLiv.Text = "";
                    txbAuteurModifLiv.Text = "";

                }
            }
        }
        #endregion


        #region Crud DVD
        //-----------------------------------------------------------
        // ONGLET "Crud DVDs"
        //-----------------------------------------------------------
        private void tabCrudDvd_Enter(object sender, EventArgs e)
        {
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesDVDs = DAODocuments.getAllDvds();
            lesCategories = DAODocuments.getAllCategories();
        }
        private void btnCreationDvd_Click(object sender, EventArgs e)
        {

            int numeroDvd = int.Parse(txbNumCrDvd.Text);
            string titreDvd = txbTitreCrDvd.Text;
            string imageDvd = txbImgCrDvd.Text;
            int nbCmd = int.Parse(txbCmdCrDvd.Text);
            int typeDvd = cbxCrPublicDvd.SelectedIndex;
            string sypsDvd = txbSypsCrDvd.Text;
            string realisatDvd = txbRealisateurCrDvd.Text;
            int dureDvd = int.Parse(txbDureeCrDvd.Text);
            string studioDvd = txbStudioCrDvd.Text;
            string genreDvd = txbGenreCrDvd.Text;
            DateTime dateSorti = DateTime.Parse(dtpAnneeSortieCrDvd.Text);

            //
            if(lesDVDs.Any(dvd3 => dvd3.IdDoc == numeroDvd.ToString()))
            {
                MessageBox.Show("Le DVD existe déjà ! ");

                //
                txbNumCrDvd.Text = "";
                txbTitreCrDvd.Text = "";
                txbImgCrDvd.Text = "";
                txbCmdCrDvd.Text = "";
                cbxCrPublicDvd.Text = "";
                txbSypsCrDvd.Text = "";
                txbRealisateurCrDvd.Text = "";
                txbDureeCrDvd.Text = "";
                txbStudioCrDvd.Text = "";
                txbGenreCrDvd.Text = "";
                dtpAnneeSortieCrDvd.Value = DateTime.Now;

                return;
            }

            //
            txbNumCrDvd.Text = "";
            txbTitreCrDvd.Text = "";
            txbImgCrDvd.Text = "";
            txbCmdCrDvd.Text = "";
            cbxCrPublicDvd.Text = "";
            txbSypsCrDvd.Text = "";
            txbRealisateurCrDvd.Text = "";
            txbDureeCrDvd.Text = "";
            txbStudioCrDvd.Text = "";
            txbGenreCrDvd.Text = "";
            dtpAnneeSortieCrDvd.Value = DateTime.Now;

            try
            {
                DAODocuments.CreerDocument(numeroDvd, titreDvd, imageDvd, nbCmd, typeDvd);
                DAODocuments.CreerDvd(numeroDvd, sypsDvd, realisatDvd, dureDvd, studioDvd, genreDvd, dateSorti);
                DVD dvd4 = new DVD(numeroDvd.ToString(), titreDvd, sypsDvd, realisatDvd, dureDvd.ToString(), imageDvd);
                lesDVDs.Add(dvd4);

                MessageBox.Show("Le document a été créé !");
            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show("Erreur dans la création du livre : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }

        }

        private void btnAffichDvd_Click(object sender, EventArgs e)
        {
            string numDvd = txbNumModifDvd.Text;
            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool resultat = false;
            foreach (DVD dvd2 in lesDVDs)
            {
                if (dvd2.IdDoc == numDvd)
                {
                    txbTitreModifDvd.Text = dvd2.Titre;
                    txbImgModifDvd.Text = dvd2.Image;
                    txbDureeModifDvd.Text = dvd2.Duree;
                    txbSypsModifDvd.Text = dvd2.Synopsis;
                    txbRealisateurModifDvd.Text = dvd2.Réalisateur;
                    resultat = true;
                }
            }
            if (!resultat)
                MessageBox.Show("Document non trouvé dans les DVD");
        }

        private void btnSuppDvd_Click(object sender, EventArgs e)
        {
            string idDocDvd = txbNumModifDvd.Text;

            try
            {
                //
                DAODocuments.SuprimerDvd(int.Parse(idDocDvd));
                //
                DAODocuments.SuprimerDocument(int.Parse(idDocDvd));

                MessageBox.Show("le DVD a éré suprimé !");

                //effacer les saisi
                txbImgModifDvd.Text = "";
                txbDureeModifDvd.Text = "";
                txbNumModifDvd.Text = "";
                txbSypsModifDvd.Text = "";
                txbTitreModifDvd.Text = "";
                txbRealisateurModifDvd.Text = "";
            }
            catch (ExceptionSIO exc)
            {
                MessageBox.Show("Erreur dans la suppression du DVD : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }

        }

        private void btnModifDvd_Click(object sender, EventArgs e)
        {
            string idDVD = txbNumModifDvd.Text;
            string realise = txbRealisateurModifDvd.Text;
            string dureeDV = txbDureeModifDvd.Text;

            for(int i = 0; i < lesDVDs.Count; i++)
            {
                DVD dvd1 = lesDVDs[i];
                if(idDVD == dvd1.IdDoc.ToString())
                {
                    dvd1.Réalisateur = realise;
                    dvd1.Duree = dureeDV;
                    try
                    {
                        //Appeler la méthode pour modifier les information du DVD
                        DAODocuments.ModifierDvd(int.Parse(idDVD), realise, int.Parse(dureeDV));
                        MessageBox.Show("Les informations du DVD ont été modifier !");

                        //effacer les saisi
                        txbImgModifDvd.Text = "";
                        txbDureeModifDvd.Text = "";
                        txbNumModifDvd.Text = "";
                        txbSypsModifDvd.Text = "";
                        txbTitreModifDvd.Text = "";
                        txbRealisateurModifDvd.Text = "";
                    }
                    catch (ExceptionSIO exc)
                    {
                        MessageBox.Show("Erreur dans la modification du DVD : " + exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
                    }


                }
            }
        }
        #endregion



        private void tabDVDs_Selected(object sender, TabControlEventArgs e)
        {
        }
        private void tabLivres_Click(object sender, EventArgs e)
        {
        }
        private void label18_Click(object sender, EventArgs e)
        {

        }
        private void tabParutions_Click(object sender, EventArgs e)
        {

        }
        private void tabDVDs_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesDVDs = DAODocuments.getAllDvds();
        }
        private void label21_Click(object sender, EventArgs e)
        {

        }
        private void tabGestionAb_Click(object sender, EventArgs e)
        {

        }
        private void lblCnumeroAb_Click(object sender, EventArgs e)
        {

        }


        /*il n'y a que deux document qui ont de descripteur  
        * SELECT l.idDocument, l.ISBN, l.auteur, l.collection, l.description, d.libelle AS descripteur
        FROM livre l
        INNER JOIN est_decrit_par_2 es ON l.idDocument = es.idDocument
        INNER JOIN descripteur d ON es.idDescripteur = d.id;
        SELECT d.id, d.titre, ds.libelle AS "descripteur" 
        FROM est_decrit_par_2 es 
        JOIN document d ON d.id = es.idDocument 
        JOIN descripteur ds ON ds.id = es.idDescripteur;*/
    }
}
