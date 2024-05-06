using System;
using Mediateq_AP_SIO2.metier;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace Mediateq_AP_SIO2
{
    class DAODocuments
    {

        public static List<Categorie> getAllCategories()
        {
            List<Categorie> lesCategories = new List<Categorie>();
            try
            {
                string req = "Select * from public";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Categorie categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
                    lesCategories.Add(categorie);
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lesCategories;
        }

        public static List<Descripteur> getAllDescripteurs()
        {
            List<Descripteur> lesGenres = new List<Descripteur>();
            try
            {
                string req = "Select * from descripteur";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Descripteur genre = new Descripteur(reader[0].ToString(), reader[1].ToString());
                    lesGenres.Add(genre);
                }
                DAOFactory.deconnecter();
            }
            catch(Exception exc)
            {
                throw exc;
            }
            return lesGenres;
        }
        
        public static List<Livre> getAllLivres()
        {
            List<Livre> lesLivres = new List<Livre>();
            try
            {
                string req = "Select l.idDocument, l.ISBN, l.auteur, d.titre, d.image, l.collection from livre l join document d on l.idDocument=d.id";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    Livre livre = new Livre(reader[0].ToString(), reader[3].ToString(), reader[1].ToString(),
                        reader[2].ToString(), reader[5].ToString(), reader[4].ToString());
                    lesLivres.Add(livre);
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lesLivres;
        }

        public static Categorie getCategorieByLivre(Livre pLivre)
        {
            Categorie categorie;
            try
            {
                string req = "Select p.id,p.libelle from public p,document d where p.id = d.idPublic and d.id='";
                req += pLivre.IdDoc + "'";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                if (reader.Read())
                {
                    categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
                }
                else
                {
                    categorie = null;
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return categorie;
        }

        public static List<DVD> getAllDvds()
        {
            List<DVD> lesDVDs = new List<DVD>();
            try
            {
                string req = "SELECT d.idDocument, dc.titre, dc.image, d.synopsis, d.réalisateur, d.duree  FROM dvd d JOIN document dc ON d.idDocument = dc.id";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    DVD dvd = new DVD(reader[0].ToString(), reader[1].ToString(), reader[3].ToString(),
                        reader[4].ToString(), reader[5].ToString(), reader[2].ToString());
                    lesDVDs.Add(dvd);
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lesDVDs;
        }


        public static List<Utilisateurs> getAllUtilisateurs()
        {
            List<Utilisateurs> lesUtilisateurs = new List<Utilisateurs>();
            try
            {
                string req = "SELECT * FROM utilisateur ";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    Utilisateurs utilisateur = new Utilisateurs(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString());
                    lesUtilisateurs.Add(utilisateur);
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lesUtilisateurs;
        }

        public static List<Abonnee> getAllAbonnees()
        {
            List<Abonnee> lesAbonnees = new List<Abonnee>();
            try
            {
                string req = "SELECT * FROM abonnee ";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    Abonnee abonnee = new Abonnee(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), DateTime.Parse(reader[7].ToString()), DateTime.Parse(reader[8].ToString()), DateTime.Parse(reader[9].ToString()), reader[10].ToString(), reader[11].ToString());
                    lesAbonnees.Add(abonnee);
                }
                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return lesAbonnees;
        }

        public static void AjouterAbonnee(string logA, string nomA, string premA, string adressMail, string mdpA, string adressA, string numtel, DateTime dateN, DateTime datePAb, DateTime dateFAb, string TypeAb)
        {
            
            try
            {
                string req = "INSERT INTO `abonnee`(`loginA`, `nomA`, `prenomA`, `mailA`, `mdpA`, `adresseA`, `numTel`, `dateNaissance`, `datePremierAb`, `dateFinAb`, `typeA`) VALUES ('" + logA + "', '" + nomA + "','" + premA + "', '" + adressMail + "', '" + mdpA + "', '" + adressA + "', '" + numtel + "', '" + dateN.ToString("yyyy-MM-dd") + "', '" + datePAb.ToString("yyyy-MM-dd") + "', '" + dateFAb.ToString("yyyy-MM-dd") + "', '" + TypeAb + "')";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
        }

        public static void ModifierAbonnee(string logA, string newMdpA, string newAdressMailA, string newAdressA, string newNumA, DateTime newDatePAb, DateTime newDateFAb, string newTypeA)
        {
            try
            {
                // Utilisez une requête UPDATE pour modifier les champs spécifiés
                string req = " UPDATE `abonnee` SET `mailA`='" + newAdressMailA + "',`mdpA`='" + newMdpA + "',`adresseA`='" + newAdressA + "',`numTel`='" + newNumA + "',`datePremierAb`='" + newDatePAb.ToString("yyyy-MM-dd") + "',`dateFinAb`='" + newDateFAb.ToString("yyyy-MM-dd") + "',`typeA`='" + newTypeA + "' WHERE `loginA`='" + logA + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void CreerDocument(int numeroD, string titreD, string imageL, int nbCommandeD, int publicD)
        {
            try
            {
                //
                string req = "INSERT INTO `document`(`id`, `titre`, `image`, `commandeEnCours`, `idPublic`) VALUES('" + numeroD + "', '" + titreD + "', '" + imageL + "', '" + nbCommandeD + "', '" + publicD + "')";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void CreerLivre(int numeroD, string codeL, string auteurL, string collectionL, string descriptionL, int nbPageL, DateTime dateSL)
        {
            try
            {
                //
                string req = "INSERT INTO `livre`(`idDocument`, `ISBN`, `auteur`, `collection`, `description`, `nbPage`, `Date`) VALUES ('" + numeroD + "','" + codeL + "','" + auteurL + "','" + collectionL + "','" + descriptionL + "','" + nbPageL + "','" + dateSL.ToString("yyyy-MM-dd") + "')";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void CreerDvd(int numeroD, string synopsisDvd, string realisateurDvd, int dureeDvd, string studioDvd, string genreDvd, DateTime anneeSortieDvd)
        {
            try
            {
                //
                string req = "INSERT INTO `dvd`(`idDocument`, `synopsis`, `réalisateur`, `duree`, `studio`, `genre`, `date`) VALUES ('" + numeroD + "','" + synopsisDvd + "','" + realisateurDvd + "','" + dureeDvd + "','" + studioDvd + "','" + genreDvd + "','" + anneeSortieDvd.ToString("yyyy") + "')";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void SuprimerDocument(int numeroD)
        {
            try
            {
                //
                string req = "DELETE FROM `document` WHERE document.id = '" + numeroD + "'";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public static void SuprimerLivre(int numeroD)
        {
            try
            {
                //
                string req = "DELETE FROM `livre` WHERE livre.idDocument = '" + numeroD + "'";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void SuprimerDvd(int numeroD)
        {
            try
            {
                //
                string req = "DELETE FROM `dvd` WHERE dvd.idDocument = '" + numeroD +"'";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void ModifierLivre(int idD, string auteurLv, string collectionLv)
        {
            try
            {
                //
                string req = "UPDATE `livre` SET `auteur`='" + auteurLv + "',`collection`='" + collectionLv +"' WHERE idDocument = '" + idD + "'";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void ModifierDvd(int identifiantD, string realisateurDVD, int dureeDVD)
        {
            try
            {
                //
                string req = "UPDATE `dvd` SET `réalisateur`='" + realisateurDVD + "',`duree`='" + dureeDVD + "' WHERE idDocument = '" + identifiantD + "'";
                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);

                DAOFactory.deconnecter();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
    
}

