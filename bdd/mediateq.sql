-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost
-- Généré le : jeu. 07 déc. 2023 à 04:52
-- Version du serveur : 5.7.11
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `mediateq_judith`
--
DROP DATABASE IF EXISTS mediateq_judith;
CREATE DATABASE IF NOT EXISTS mediateq_judith DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
DROP USER IF EXISTS "mediateq-web"@"localhost";
CREATE USER "mediateq-web"@"localhost" IDENTIFIED BY "medi@teq-w3b";
GRANT SELECT ON mediateq.* TO "mediateq-web"@"localhost";
GRANT INSERT ON mediateq.* TO "mediateq-web"@"localhost";
GRANT UPDATE ON mediateq.* TO "mediateq-web"@"localhost";
GRANT DELETE ON mediateq.* TO "mediateq-web"@"localhost";

USE mediateq_judith;
-- --------------------------------------------------------

--
-- Structure de la table `abonnee`
--

CREATE TABLE `abonnee` (
  `loginA` varchar(10) NOT NULL,
  `nomA` varchar(10) NOT NULL,
  `prenomA` varchar(10) NOT NULL,
  `adresseMail` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
  `mdpA` varchar(30) NOT NULL,
  `adresseA` varchar(50) NOT NULL,
  `numTel` varchar(15) NOT NULL,
  `dateNaissance` date DEFAULT NULL,
  `datePremierAb` date DEFAULT NULL,
  `dateFinAb` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `abonnee`
--

INSERT INTO `abonnee` (`loginA`, `nomA`, `prenomA`, `adresseMail`, `mdpA`, `adresseA`, `numTel`, `dateNaissance`, `datePremierAb`, `dateFinAb`) VALUES
('0001', 'Ortega', 'Jenna', 'jenna.ortega@yahoo.fr', '100', '46 rue de Montagny', '06.00.03.08.53', '2000-06-07', '2022-11-18', '2023-11-08');

-- --------------------------------------------------------

--
-- Structure de la table `abonnement`
--

CREATE TABLE `abonnement` (
  `idAbonnement` varchar(10) NOT NULL,
  `dateDebutA` varchar(15) NOT NULL,
  `dateFinA` varchar(15) NOT NULL,
  `loginAbonnée` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE `commande` (
  `id` int(11) NOT NULL,
  `nbExemplaire` int(11) DEFAULT NULL,
  `dateCommande` date DEFAULT NULL,
  `montant` double DEFAULT NULL,
  `idDocument` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `descripteur`
--

CREATE TABLE `descripteur` (
  `id` int(11) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `descripteur`
--

INSERT INTO `descripteur` (`id`, `libelle`) VALUES
(1, 'Presse économique'),
(2, 'Presse culturelle'),
(3, 'Presse sportive'),
(4, 'Presse loisir'),
(5, 'Presse Actualités'),
(10000, 'Humour'),
(10001, 'Bande dessinée'),
(10002, 'Science Fiction'),
(10003, 'Biographie'),
(10004, 'Historique'),
(10006, 'Roman'),
(10007, 'Aventures'),
(10008, 'Essai'),
(10009, 'Documentaire'),
(10010, 'Technique'),
(10011, 'Voyages'),
(10012, 'Drame'),
(10013, 'Comédie'),
(10014, 'Policier');

-- --------------------------------------------------------

--
-- Structure de la table `document`
--

CREATE TABLE `document` (
  `id` int(11) NOT NULL,
  `titre` varchar(50) DEFAULT NULL,
  `image` text,
  `commandeEnCours` int(11) DEFAULT NULL,
  `idPublic` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `document`
--

INSERT INTO `document` (`id`, `titre`, `image`, `commandeEnCours`, `idPublic`) VALUES
(1, 'Quand sort la recluse', 'https://static.fnac-static.com/multimedia/Images/FR/NR/31/b0/84/8695857/1507-1/tsp20230105062957/Quand-sort-la-recluse.jpg', NULL, 2),
(2, 'Un pays à l\'aube', 'https://focus.telerama.fr/274x369/100/2021/08/19/-multimedia-images_produits-ZoomPE-7-6-3-9782743619367-tsp20130903072409-Un-pays-a-l-aube.jpg', NULL, 2),
(3, 'Et je danse aussi', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/61WW6LGkopL.jpg', NULL, 3),
(4, 'L\'armée furieuse', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/414cv83qQUL._SX307_BO1,204,203,200_.jpg', NULL, 2),
(5, 'Les anonymes', 'https://www.livredepoche.com/sites/default/files/styles/manual_crop_269_435/public/images/livres/couv/9782253157113-001-T.jpeg?itok=YjJE2a5e', NULL, 2),
(6, 'La marque jaune', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/91xG9uGkBBL.jpg', NULL, 3),
(7, 'Dans les coulisses du musée', 'https://www.livredepoche.com/sites/default/files/styles/manual_crop_269_435/public/images/livres/couv/9782253144908-001-T.jpeg?itok=mVyZ5mV6', NULL, 3),
(8, 'Histoire du juif errant', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/91mNV9BiLrL.jpg', NULL, 2),
(9, 'Pars vite et reviens tard', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/71FfPsoF65L.jpg', 0, 2),
(10, 'Le vestibule des causes perdues', 'https://www.babelio.com/couv/CVT_Le-vestibule-des-causes-perdues_4311.jpeg', 0, 2),
(11, 'L\'ile des oublies', 'https://www.livredepoche.com/sites/default/files/images/livres/couv/9782253161677-001-T.jpeg', 0, 2),
(12, 'La souris bleue', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/41QCZD0X+HS._SX307_BO1,204,203,200_.jpg', 0, 2),
(13, 'Sacré Pêre Noël', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/817UlBu9XML.jpg', 0, 2),
(14, 'Mauvaise étoile', 'https://www.livredepoche.com/sites/default/files/styles/manual_crop_269_435/public/images/livres/couv/9782253176077-001-T.jpeg?itok=qP8a2k-X', 0, 2),
(15, 'La confrérie des téméraires', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/715vQyw5GFL.jpg', 0, 2),
(16, 'Le butin du requin', 'https://www.actes-sud.fr/sites/default/files/couvertures/9782330121877.jpg', 0, 2),
(17, 'Catastrophes au Brésil', 'https://static.fnac-static.com/multimedia/Images/FR/NR/dd/ea/6d/7203549/1507-1/tsp20230221075906/Les-39-cles-Cahill-contre-Vesper.jpg', 0, 2),
(18, 'Le Routard - Maroc', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/91j1+5tonlL.jpg', 0, 2),
(19, 'Guide Vert - Iles Canaries', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/81vH6kVUOML.jpg', 0, 2),
(20, 'Guide Vert - Irlande', 'https://static.fnac-static.com/multimedia/Images/FR/NR/c2/f7/ad/11401154/1507-1/tsp20220418082938/Guide-Vert-Irlande.jpg', 0, 2),
(21, 'Les déferlantes', 'https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/41XbNQNHHEL._SX307_BO1,204,203,200_.jpg', 0, 2),
(22, 'Une part de Ciel', 'https://static.fnac-static.com/multimedia/Images/FR/NR/b8/0e/7c/8130232/1507-1/tsp20210114102306/Une-part-de-ciel.jpg', 0, 2),
(23, 'Le secret du janissaire', 'https://www.bedetheque.com/media/Couvertures/Couv_270396.jpg', 0, 2),
(24, 'Pavillon noir', 'https://images.epagine.fr/430/9782840551430_1_75.jpg', 0, 2),
(25, 'L\'archipel du danger', 'https://cdn.cultura.com/cdn-cgi/image/width=768/media/pim/TITELIVE/35_9782840552369_1_75.jpg', 0, 2),
(26, 'Agence tout risques', 'https://fr.web.img5.acsta.net/medias/nmedia/18/73/05/71/19447495.jpg', NULL, 3);

-- --------------------------------------------------------

--
-- Structure de la table `dvd`
--

CREATE TABLE `dvd` (
  `idDocument` int(11) NOT NULL,
  `synopsis` varchar(100) DEFAULT NULL,
  `réalisateur` varchar(50) DEFAULT NULL,
  `duree` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `emprunt`
--

CREATE TABLE `emprunt` (
  `idAbonnee` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `idDoc` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `dateDebut` date DEFAULT NULL,
  `dateFin` date DEFAULT NULL,
  `prolongeable` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `emprunt`
--

INSERT INTO `emprunt` (`idAbonnee`, `idDoc`, `dateDebut`, `dateFin`, `prolongeable`) VALUES
('1000', '13', '2023-11-26', '2023-11-10', 'Oui');

-- --------------------------------------------------------

--
-- Structure de la table `est_decrit_par_2`
--

CREATE TABLE `est_decrit_par_2` (
  `idDocument` int(11) NOT NULL,
  `idDescripteur` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `est_decrit_par_2`
--

INSERT INTO `est_decrit_par_2` (`idDocument`, `idDescripteur`) VALUES
(3, 10006),
(1, 10008),
(3, 10011),
(1, 10014);

-- --------------------------------------------------------

--
-- Structure de la table `etat`
--

CREATE TABLE `etat` (
  `id` int(11) NOT NULL,
  `libelle` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `etat`
--

INSERT INTO `etat` (`id`, `libelle`) VALUES
(1, 'neuf'),
(2, 'usagé'),
(3, 'détérioré'),
(4, 'inutilisable');

-- --------------------------------------------------------

--
-- Structure de la table `exemplaire`
--

CREATE TABLE `exemplaire` (
  `idDocument` int(11) NOT NULL,
  `numero` int(11) NOT NULL,
  `dateAchat` date DEFAULT NULL,
  `idRayon` int(11) NOT NULL,
  `idEtat` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `exemplaire`
--

INSERT INTO `exemplaire` (`idDocument`, `numero`, `dateAchat`, `idRayon`, `idEtat`) VALUES
(1, 1, '2023-01-15', 1, 1),
(1, 2, '2023-02-20', 2, 2),
(1, 3, '2023-03-10', 3, 3),
(2, 1, '2023-07-02', 1, 1),
(3, 1, '2023-04-05', 2, 1),
(3, 2, '2023-05-12', 3, 2),
(3, 3, '2023-06-08', 1, 3),
(3, 4, '2023-11-10', 2, 2),
(4, 1, '2023-07-15', 2, 2),
(6, 1, '2023-07-20', 3, 3),
(8, 1, '2023-08-05', 1, 1),
(10, 1, '2023-08-10', 2, 2),
(12, 1, '2023-08-15', 3, 3),
(14, 1, '2023-09-02', 1, 1),
(16, 1, '2023-09-10', 2, 2),
(18, 1, '2023-09-15', 3, 3),
(20, 1, '2023-10-05', 1, 1),
(22, 1, '2023-10-10', 2, 2),
(24, 1, '2023-10-15', 3, 3),
(26, 1, '2023-11-02', 1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `livre`
--

CREATE TABLE `livre` (
  `idDocument` int(11) NOT NULL,
  `ISBN` char(13) DEFAULT NULL,
  `auteur` varchar(50) DEFAULT NULL,
  `collection` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `livre`
--

INSERT INTO `livre` (`idDocument`, `ISBN`, `auteur`, `collection`) VALUES
(1, '1234569877896', 'Fred Vargas', 'Commissaire Adamsberg'),
(2, '1236547896541', 'Dennis Lehanne', ''),
(3, '6541236987410', 'Anne-Laure Bondoux', ''),
(4, '3214569874123', 'Fred Vargas', 'Commissaire Adamsberg'),
(5, '3214563214563', 'RJ Ellory', ''),
(6, '3213213211232', 'Edgar P. Jacobs', 'Blake et Mortimer'),
(7, '6541236987541', 'Kate Atkinson', ''),
(8, '1236987456321', 'Jean d\'Ormesson', ''),
(9, '3,21E+12', 'Fred Vargas', 'Commissaire Adamsberg'),
(10, '3,21E+12', 'Manon Moreau', ''),
(11, '3,21E+12', 'Victoria Hislop', ''),
(12, '3,21E+12', 'Kate Atkinson', ''),
(13, '3,21E+12', 'Raymond Briggs', ''),
(14, '3,21E+12', 'RJ Ellory', ''),
(15, '3,21E+12', 'Floriane Turmeau', ''),
(16, '3,21E+12', 'Julian Press', ''),
(17, '3,21E+12', 'Philippe Masson', ''),
(18, '3,21E+12', '', 'Guide du Routard'),
(19, '3,21E+12', '', 'Guide Vert'),
(20, '3,21E+12', '', 'Guide Vert'),
(21, '3,21E+12', 'Claudie Gallay', ''),
(22, '3,21E+12', 'Claudie Gallay', ''),
(23, '3,21E+12', 'Ayrolles - Masbou', 'De cape et de crocs'),
(24, '3,21E+12', 'Ayrolles - Masbou', 'De cape et de crocs'),
(25, '3,21E+12', 'Ayrolles - Masbou', 'De cape et de crocs');

-- --------------------------------------------------------

--
-- Structure de la table `parution`
--

CREATE TABLE `parution` (
  `idRevue` int(11) NOT NULL,
  `numero` int(11) NOT NULL,
  `dateParution` date DEFAULT NULL,
  `photo` varchar(500) DEFAULT NULL,
  `idEtat` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `parution`
--

INSERT INTO `parution` (`idRevue`, `numero`, `dateParution`, `photo`, `idEtat`) VALUES
(10001, 154, '2021-07-01', NULL, 1),
(10001, 155, '2021-08-01', NULL, 1),
(10001, 156, '2021-09-01', NULL, 1),
(10001, 157, '2021-10-01', NULL, 1),
(10001, 158, '2021-11-01', NULL, 1),
(10001, 159, '2021-12-01', NULL, 1),
(10001, 160, '2023-01-01', 'lien vers la photo de janvier 2023', 1),
(10001, 161, '2023-02-01', 'lien vers la photo de février 2023', 1),
(10002, 2154, '2021-07-01', 'lien vers la photo de juillet 2021', 1),
(10002, 2155, '2021-08-01', NULL, 1),
(10002, 2156, '2021-09-01', NULL, 1),
(10002, 2157, '2021-10-01', NULL, 1),
(10002, 2158, '2021-11-01', NULL, 1),
(10002, 2159, '2021-12-01', NULL, 1),
(10002, 2160, '2023-01-01', 'lien vers la photo de janvier 2023', 1),
(10002, 2161, '2023-02-01', 'lien vers la photo de février 2023', 1),
(10003, 215, '2021-02-01', NULL, 1),
(10003, 216, '2021-03-01', NULL, 1),
(10003, 217, '2021-04-01', NULL, 1),
(10003, 218, '2021-05-01', NULL, 1),
(10003, 219, '2021-06-01', NULL, 1),
(10003, 220, '2021-07-01', NULL, 1),
(10003, 221, '2023-01-15', 'lien vers la photo de janvier 2023', 1),
(10003, 222, '2023-02-15', 'lien vers la photo de février 2023', 1),
(10004, 163, '2023-01-10', 'lien vers la photo de janvier 2023', 1),
(10004, 164, '2023-02-10', 'lien vers la photo de février 2023', 1),
(10005, 323, '2023-01-20', 'lien vers la photo de janvier 2023', 1),
(10005, 324, '2023-02-20', 'lien vers la photo de février 2023', 1),
(10005, 3215, '2021-11-20', NULL, 1),
(10005, 3216, '2021-11-21', NULL, 1),
(10005, 3217, '2021-11-22', NULL, 1),
(10005, 3218, '2021-11-23', NULL, 1),
(10005, 3219, '2021-11-24', NULL, 1),
(10005, 3220, '2021-11-25', NULL, 1),
(10006, 223, '2023-01-05', 'lien vers la photo de janvier 2023', 1),
(10006, 224, '2023-02-05', 'lien vers la photo de février 2023', 1),
(10007, 163, '2023-01-08', 'lien vers la photo de janvier 2023', 1),
(10007, 164, '2023-02-08', 'lien vers la photo de février 2023', 1),
(10008, 223, '2023-01-02', 'lien vers la photo de janvier 2023', 1),
(10008, 224, '2023-02-02', 'lien vers la photo de février 2023', 1),
(10009, 163, '2023-01-02', 'lien vers la photo de janvier 2023', 1),
(10009, 164, '2023-02-02', 'lien vers la photo de février 2023', 1),
(10010, 323, '2023-01-03', 'lien vers la photo de janvier 2023', 1),
(10010, 324, '2023-02-03', 'lien vers la photo de février 2023', 1),
(10011, 223, '2023-01-01', 'lien vers la photo de janvier 2023', 1),
(10011, 224, '2023-02-01', 'lien vers la photo de février 2023', 1);

-- --------------------------------------------------------

--
-- Structure de la table `public`
--

CREATE TABLE `public` (
  `id` int(11) NOT NULL,
  `libelle` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `public`
--

INSERT INTO `public` (`id`, `libelle`) VALUES
(1, 'Jeunesse'),
(2, 'Adultes'),
(3, 'Tous publics'),
(4, 'Ados');

-- --------------------------------------------------------

--
-- Structure de la table `rayon`
--

CREATE TABLE `rayon` (
  `id` int(11) NOT NULL,
  `libelle` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `rayon`
--

INSERT INTO `rayon` (`id`, `libelle`) VALUES
(1, 'Romans'),
(2, 'Science-fiction'),
(3, 'Policier'),
(4, 'Jeunesse'),
(5, 'Biographie');

-- --------------------------------------------------------

--
-- Structure de la table `revue`
--

CREATE TABLE `revue` (
  `id` int(11) NOT NULL,
  `titre` varchar(50) NOT NULL,
  `empruntable` char(1) DEFAULT NULL,
  `periodicite` varchar(2) DEFAULT NULL,
  `delai_miseadispo` int(11) DEFAULT NULL,
  `dateFinAbonnement` date DEFAULT NULL,
  `idDescripteur` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `revue`
--

INSERT INTO `revue` (`id`, `titre`, `empruntable`, `periodicite`, `delai_miseadispo`, `dateFinAbonnement`, `idDescripteur`) VALUES
(10001, 'Arts Magazine', 'O', 'MS', 52, '2022-03-31', 2),
(10002, 'Alternatives Economiques', 'O', 'HB', 52, '2022-04-30', 1),
(10003, 'Challenges', 'O', 'HB', 26, '2022-02-28', 1),
(10004, 'Rock and Folk', 'O', 'MS', 52, '2022-10-31', 2),
(10005, 'Les Echos', 'N', 'QT', 5, '2022-12-31', 1),
(10006, 'L\'Equipe', 'N', 'QT', 5, '2022-01-31', 3),
(10007, 'Telerama', 'O', 'HB', 26, '2022-01-31', 2),
(10008, 'L\'Obs', 'O', 'HB', 26, '2022-01-31', 5),
(10009, 'Le Monde', 'N', 'QT', 5, '2022-01-31', 5),
(10010, 'L\'Equipe Magazine', 'O', 'HB', 12, '2022-01-31', 3),
(10011, 'Geo', 'O', 'MS', 52, '2022-01-31', 2);

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

CREATE TABLE `utilisateur` (
  `nomU` varchar(50) NOT NULL,
  `prenomU` varchar(50) NOT NULL,
  `mailU` varchar(100) NOT NULL,
  `mdpU` varchar(100) NOT NULL,
  `serviceU` varchar(50) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`nomU`, `prenomU`, `mailU`, `mdpU`, `serviceU`) VALUES
('TANDONNET', 'Jojo', 'user', '$2y$12$yZvR1TKigahmWLGQn9PrperUbgNDbgF063WZXaeneKaQ.PZNQ1DlK', 'Administrateur'),
('MBAPPE', 'Kylian', 'k.mbappe', '007', 'Prêt'),
('DESCHAMPS', 'Didier', 'd.deschamps', '008', 'Administratif'),
('FARKAS', 'Dorian', 'd.farkas', '009', 'Culture');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `abonnee`
--
ALTER TABLE `abonnee`
  ADD PRIMARY KEY (`loginA`);

--
-- Index pour la table `abonnement`
--
ALTER TABLE `abonnement`
  ADD PRIMARY KEY (`idAbonnement`),
  ADD KEY `Fk_abo_A` (`loginAbonnée`);

--
-- Index pour la table `commande`
--
ALTER TABLE `commande`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_1` (`idDocument`);

--
-- Index pour la table `descripteur`
--
ALTER TABLE `descripteur`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `document`
--
ALTER TABLE `document`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_1` (`idPublic`);

--
-- Index pour la table `dvd`
--
ALTER TABLE `dvd`
  ADD PRIMARY KEY (`idDocument`);

--
-- Index pour la table `est_decrit_par_2`
--
ALTER TABLE `est_decrit_par_2`
  ADD PRIMARY KEY (`idDocument`,`idDescripteur`),
  ADD KEY `id_1` (`idDescripteur`);

--
-- Index pour la table `etat`
--
ALTER TABLE `etat`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `exemplaire`
--
ALTER TABLE `exemplaire`
  ADD PRIMARY KEY (`idDocument`,`numero`),
  ADD KEY `id` (`idRayon`),
  ADD KEY `id_1` (`idEtat`);

--
-- Index pour la table `livre`
--
ALTER TABLE `livre`
  ADD PRIMARY KEY (`idDocument`);

--
-- Index pour la table `parution`
--
ALTER TABLE `parution`
  ADD PRIMARY KEY (`idRevue`,`numero`),
  ADD KEY `id` (`idEtat`);

--
-- Index pour la table `public`
--
ALTER TABLE `public`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `rayon`
--
ALTER TABLE `rayon`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `revue`
--
ALTER TABLE `revue`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_1` (`idDescripteur`);

--
-- Index pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD PRIMARY KEY (`mailU`);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`idDocument`) REFERENCES `document` (`id`);

--
-- Contraintes pour la table `document`
--
ALTER TABLE `document`
  ADD CONSTRAINT `document_ibfk_1` FOREIGN KEY (`idPublic`) REFERENCES `public` (`id`);

--
-- Contraintes pour la table `dvd`
--
ALTER TABLE `dvd`
  ADD CONSTRAINT `dvd_ibfk_1` FOREIGN KEY (`idDocument`) REFERENCES `document` (`id`);

--
-- Contraintes pour la table `est_decrit_par_2`
--
ALTER TABLE `est_decrit_par_2`
  ADD CONSTRAINT `est_decrit_par_2_ibfk_1` FOREIGN KEY (`idDocument`) REFERENCES `document` (`id`),
  ADD CONSTRAINT `est_decrit_par_2_ibfk_2` FOREIGN KEY (`idDescripteur`) REFERENCES `descripteur` (`id`);

--
-- Contraintes pour la table `exemplaire`
--
ALTER TABLE `exemplaire`
  ADD CONSTRAINT `exemplaire_ibfk_1` FOREIGN KEY (`idDocument`) REFERENCES `document` (`id`),
  ADD CONSTRAINT `exemplaire_ibfk_2` FOREIGN KEY (`idRayon`) REFERENCES `rayon` (`id`),
  ADD CONSTRAINT `exemplaire_ibfk_3` FOREIGN KEY (`idEtat`) REFERENCES `etat` (`id`);

--
-- Contraintes pour la table `livre`
--
ALTER TABLE `livre`
  ADD CONSTRAINT `livre_ibfk_1` FOREIGN KEY (`idDocument`) REFERENCES `document` (`id`);

--
-- Contraintes pour la table `parution`
--
ALTER TABLE `parution`
  ADD CONSTRAINT `parution_ibfk_1` FOREIGN KEY (`idRevue`) REFERENCES `revue` (`id`),
  ADD CONSTRAINT `parution_ibfk_2` FOREIGN KEY (`idEtat`) REFERENCES `etat` (`id`);

--
-- Contraintes pour la table `revue`
--
ALTER TABLE `revue`
  ADD CONSTRAINT `revue_ibfk_2` FOREIGN KEY (`idDescripteur`) REFERENCES `descripteur` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
