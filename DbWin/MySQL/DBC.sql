-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versione server:              8.4.0 - MySQL Community Server - GPL
-- S.O. server:                  Win64
-- HeidiSQL Versione:            12.8.0.6976
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dump della struttura del database dbc00
CREATE DATABASE IF NOT EXISTS `dbc00` /*!40100 DEFAULT CHARACTER SET latin1 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dbc00`;

-- Dump della struttura di tabella dbc00.assiemi
CREATE TABLE IF NOT EXISTS `assiemi` (
  `cod` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `mod` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`,`mod`),
  CONSTRAINT `FK_assiemi_codici` FOREIGN KEY (`cod`, `mod`) REFERENCES `codici` (`cod`, `mod`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tabella dei codici degli assiemi (in comune con la tabella dei codici).';

-- Dump dei dati della tabella dbc00.assiemi: ~17 rows (circa)
REPLACE INTO `assiemi` (`cod`, `mod`) VALUES
	('100.12.001', ''),
	('100.12.002', 'd'),
	('101.10.100', ''),
	('201.10.001', ''),
	('201.11.001', ''),
	('301.10.001', ''),
	('555.55.555', 'a'),
	('789.10.001', ''),
	('789.10.001', 'a'),
	('789.10.001', 'b'),
	('987.22.123', ''),
	('987.65.432', ''),
	('998.01.001', ''),
	('998.01.001', 'a'),
	('999.80.678', 'a'),
	('999.88.776', 'a'),
	('999.99.002', '');

-- Dump della struttura di tabella dbc00.codici
CREATE TABLE IF NOT EXISTS `codici` (
  `cod` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `mod` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  `descrizione` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `id_utente` int unsigned NOT NULL,
  `creazione` datetime DEFAULT CURRENT_TIMESTAMP,
  `aggiornamento` datetime DEFAULT (now()) ON UPDATE CURRENT_TIMESTAMP,
  `id_ultimo` int unsigned DEFAULT NULL,
  PRIMARY KEY (`cod`,`mod`),
  KEY `FK_codici_dbc02.utenti` (`id_ultimo`),
  KEY `FK_codici_dbc02.utenti_2` (`id_utente`),
  CONSTRAINT `FK_codici_dbc02.utenti` FOREIGN KEY (`id_ultimo`) REFERENCES `dbc02`.`utenti` (`id`),
  CONSTRAINT `FK_codici_dbc02.utenti_2` FOREIGN KEY (`id_utente`) REFERENCES `dbc02`.`utenti` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tutti i codici con le informazioni principali.';

-- Dump dei dati della tabella dbc00.codici: ~56 rows (circa)
REPLACE INTO `codici` (`cod`, `mod`, `descrizione`, `id_utente`, `creazione`, `aggiornamento`, `id_ultimo`) VALUES
	('', '', 'Valvola Fluido sistem iso01', 2, '2023-05-12 10:52:05', '2023-05-12 11:13:04', 2),
	('100.11.123', '', 'Staffa', 5, '2019-06-22 08:33:51', '2019-06-22 08:33:51', 5),
	('100.11.123', 'a', 'STAFFA ALLUNGATA', 1, '2019-06-22 08:18:19', '2019-09-14 14:22:29', 2),
	('100.11.123', 'b', 'Staffa', 1, '2019-06-24 08:20:25', '2019-06-24 08:20:25', 1),
	('100.12.001', '', 'Assieme Antani', 6, '2024-07-01 12:01:30', '2024-07-01 12:01:33', 6),
	('100.12.002', 'd', 'Assieme Blinda', 6, '2024-07-01 12:01:43', '2024-07-01 12:01:48', 6),
	('100.12.003', 'b', 'Schema Blinda', 6, '2024-07-01 12:01:57', '2024-07-01 12:01:58', 6),
	('101.10.100', '', 'AAA', 2, '2019-11-01 10:34:21', '2019-11-01 10:34:21', 2),
	('102.11.100', '', 'STAFFA', 2, '2023-04-23 17:43:31', '2023-04-23 17:43:31', 2),
	('102.11.100', 'a', 'STAFFA', 2, '2023-04-23 17:44:58', '2023-04-23 17:45:27', 2),
	('1055.45567.122', '', 'Vite', 2, '2019-06-24 08:31:22', '2019-08-17 17:03:04', 2),
	('1055.54789.123', '', 'Guida lineare Bosch 1605.302.10.20', 2, '2019-11-02 14:09:12', '2019-11-02 14:09:12', 2),
	('1058.32765.001', '', 'Guida lineare Bosch 1605.20.345', 2, '2019-11-02 14:27:02', '2019-11-02 14:27:02', 2),
	('1058.54567.123', '', 'Guida lineare Bosch 1605.302.20.20', 2, '2023-04-23 17:47:00', '2023-04-23 17:47:00', 2),
	('1059.11111.234', '', 'Valvola Camozzi EV', 2, '2023-04-23 23:58:46', '2023-04-23 23:58:46', 2),
	('113.10.100', '', 'Supporto', 1, '2019-06-22 11:40:51', '2019-06-22 11:40:51', 1),
	('201.10.001', '', 'ASSIEME', 2, '2019-11-02 11:45:33', '2019-11-02 11:45:33', 2),
	('201.10.100', '', 'PARTICOLARE', 2, '2019-11-02 12:32:53', '2019-11-02 12:32:53', 2),
	('201.10.900', '', 'SCHEMA', 2, '2019-11-02 12:22:14', '2019-11-02 12:22:14', 2),
	('201.11.001', '', 'ASSIEME STAFFA', 2, '2019-11-09 18:16:35', '2019-11-09 18:16:35', 2),
	('201.11.100', '', 'STAFFA', 2, '2019-11-09 18:17:22', '2019-11-09 18:17:22', 2),
	('201.11.100', 'a', 'STAFFA', 2, '2023-04-26 19:15:21', '2023-04-26 19:15:21', 2),
	('20111.98765.001', '', 'Vite Umbrako M12x30', 2, '2019-11-09 18:18:07', '2019-11-09 18:18:07', 2),
	('202.10.100', '', 'Parte 3/4', 2, '2023-05-02 20:59:56', '2023-05-02 21:01:32', 2),
	('300.33.330', 'a', 'STAFFA', 2, '2019-11-25 00:51:07', '2019-11-25 00:52:35', 2),
	('301.10.001', '', 'ASSIEME', 2, '2019-11-02 14:25:32', '2019-11-02 14:25:32', 2),
	('301.10.100', '', 'STAFFA', 2, '2019-11-02 14:26:18', '2019-11-02 14:26:18', 2),
	('301.10.900', '', 'SCHEMA', 2, '2019-11-02 14:25:57', '2019-11-02 14:25:57', 2),
	('333.44.555', '', 'Valvola Festo ASD', 2, '2019-11-25 00:55:48', '2019-11-25 00:55:48', 2),
	('554.55.554', '', 'AAA', 2, '2023-04-24 07:13:26', '2023-04-24 07:13:26', 2),
	('555.55.555', 'a', 'ASSIEME PROVA', 2, '2019-09-11 21:23:16', '2019-09-11 21:23:16', 2),
	('567.89.111', '', 'LAST', 2, '2023-04-24 08:10:05', '2023-04-24 08:10:05', 2),
	('788.12.100', '', 'STAFFA', 6, '2024-07-01 06:54:06', '2024-07-01 12:02:09', 6),
	('788.12.100', 'a', 'STAFFA', 6, '2024-07-01 12:02:14', '2024-07-01 12:02:16', 6),
	('789.10.001', '', 'Assieme testa', 1, '2019-05-21 21:08:18', '2019-06-07 14:53:10', 5),
	('789.10.001', 'a', 'Assieme testa', 1, '2019-05-21 21:08:18', '2019-06-07 14:53:12', 5),
	('789.10.001', 'b', 'Assieme testa', 2, '2019-05-21 21:08:48', '2019-06-07 14:53:12', 5),
	('789.10.100', '', 'Corpo', 1, '2019-05-21 21:09:33', '2019-06-07 12:15:58', 1),
	('789.10.100', 'a', 'Corpo nuovo', 1, '2019-05-21 21:28:05', '2019-06-07 14:53:15', 5),
	('789.10.101', '', 'Supporto', 2, '2019-05-21 21:10:01', '2019-06-07 12:16:08', 1),
	('789.10.101', 'a', 'Supporto', 1, '2019-05-21 21:10:28', '2019-06-07 12:16:09', 1),
	('789.10.101', 'b', 'Supporto rettificato', 1, '2019-05-21 21:10:57', '2019-06-07 12:16:11', 1),
	('789.10.200', '', 'FUSIONE TESTA', 2, '2019-11-10 23:08:01', '2019-11-10 23:08:01', 2),
	('820.10.900', '', 'Schema lavorazione', 1, '2019-06-03 00:01:36', '2019-06-07 14:53:17', 5),
	('820.10.900', 'a', 'Schema lavorazione', 1, '2019-06-03 00:01:22', '2019-06-07 14:53:16', 5),
	('820.10.900', 'b', 'Schema lavorazione', 1, '2019-06-02 23:58:51', '2019-06-07 14:53:16', 5),
	('987.22.123', '', 'Studio montaggio', 5, '2019-06-24 07:35:59', '2019-06-24 07:35:59', 5),
	('987.43.444', 'a', 'SCHEMA AGGIORNATO', 2, '2019-11-10 19:24:27', '2019-11-10 19:24:27', 2),
	('987.65.432', '', 'ASSIEME DISCENDENTE', 5, '2023-04-25 09:42:13', '2023-04-25 09:42:13', 5),
	('998.01.001', '', 'new', 1, '2019-06-19 22:18:43', '2019-06-19 22:18:43', 1),
	('998.01.001', 'a', 'new modificati', 5, '2019-06-19 22:19:29', '2019-06-19 22:19:29', 5),
	('999.80.678', 'a', 'TEST', 2, '2019-09-11 20:55:22', '2019-09-11 20:55:22', 2),
	('999.88.776', '', 'SCHEMA', 2, '2019-11-26 21:24:38', '2019-11-26 21:24:38', 2),
	('999.88.776', 'a', 'ANTANI', 2, '2023-04-23 15:58:16', '2023-04-23 15:58:16', 2),
	('999.88.777', '', ' Festo ', 2, '2019-11-26 21:19:55', '2019-11-26 21:19:55', 2),
	('999.99.002', '', 'AAA2', 1, '2019-06-19 22:11:20', '2019-06-19 22:11:20', 1);

-- Dump della struttura di tabella dbc00.commerciali
CREATE TABLE IF NOT EXISTS `commerciali` (
  `cod` char(20) NOT NULL,
  `mod` char(1) NOT NULL DEFAULT '',
  `modello` varchar(255) NOT NULL DEFAULT '',
  `dettagli` varchar(255) NOT NULL DEFAULT '',
  `costruttore` int unsigned NOT NULL,
  `prodotto` int unsigned NOT NULL,
  PRIMARY KEY (`cod`,`mod`),
  KEY `FK_commerciali_costruttori` (`costruttore`),
  KEY `FK_commerciali_prodotti` (`prodotto`),
  CONSTRAINT `FK_commerciali_codici` FOREIGN KEY (`cod`, `mod`) REFERENCES `codici` (`cod`, `mod`),
  CONSTRAINT `FK_commerciali_costruttori` FOREIGN KEY (`costruttore`) REFERENCES `costruttori` (`id`),
  CONSTRAINT `FK_commerciali_prodotti` FOREIGN KEY (`prodotto`) REFERENCES `prodotti` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tabella dei codici commerciali (in comune con la tabella dei codici), con informazioni aggiuntive.';

-- Dump dei dati della tabella dbc00.commerciali: ~9 rows (circa)
REPLACE INTO `commerciali` (`cod`, `mod`, `modello`, `dettagli`, `costruttore`, `prodotto`) VALUES
	('1055.45567.122', '', 'M12x50', 'interamente filettata', 1, 1),
	('1055.54789.123', '', '1605.302.10.20', 'TAGLI 20, CLASSE P', 2, 4),
	('1058.32765.001', '', '1605.20.345', 'taglia 20, classe P', 2, 4),
	('1058.54567.123', '', '1605.302.20.20', 'Classe SP', 2, 4),
	('1059.11111.234', '', 'EV', '5/3 c.a.', 4, 2),
	('20111.98765.001', '', 'M12x30', '12.9', 1, 1),
	('333.44.555', '', 'ASD', 'ASDFRE', 3, 2),
	('999.88.777', '', '', '', 3, 11);

-- Dump della struttura di tabella dbc00.costruttori
CREATE TABLE IF NOT EXISTS `costruttori` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Solo maggiore di zero.',
  `costruttore` varchar(255) NOT NULL DEFAULT '-',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uk_costruttore` (`costruttore`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1 COMMENT='Tabella dei costruttori.';

-- Dump dei dati della tabella dbc00.costruttori: ~7 rows (circa)
REPLACE INTO `costruttori` (`id`, `costruttore`) VALUES
	(2, 'Bosch'),
	(4, 'Camozzi'),
	(3, 'Festo'),
	(7, 'Fluido sistem'),
	(14, 'Legris'),
	(22, 'Legris_X1'),
	(23, 'Norgren'),
	(5, 'Siemens'),
	(1, 'Umbrako');

-- Dump della struttura di tabella dbc00.legami
CREATE TABLE IF NOT EXISTS `legami` (
  `cod` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT 'codice',
  `mod` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '' COMMENT 'modifica',
  `cod_p` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT 'codice padre',
  `mod_p` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '' COMMENT 'modifica padre',
  `qta` smallint DEFAULT (1) COMMENT 'quantitò in distinta',
  `acq` tinyint DEFAULT (1) COMMENT 'approvvigionare',
  `qta_ric` smallint DEFAULT '1' COMMENT 'quantià ricambi',
  `liv_ric` tinyint DEFAULT '0' COMMENT 'livello di ricambio (0: non è un ricambio)',
  PRIMARY KEY (`cod`,`mod`,`cod_p`,`mod_p`),
  KEY `FK_padre` (`cod_p`,`mod_p`),
  CONSTRAINT `FK_figlio` FOREIGN KEY (`cod`, `mod`) REFERENCES `codici` (`cod`, `mod`),
  CONSTRAINT `FK_padre` FOREIGN KEY (`cod_p`, `mod_p`) REFERENCES `codici` (`cod`, `mod`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tabella dei legami, con informazioni aggiuntive.\r\nTutti i codici possono avere un distinta, tranne gli schemi.';

-- Dump dei dati della tabella dbc00.legami: ~25 rows (circa)
REPLACE INTO `legami` (`cod`, `mod`, `cod_p`, `mod_p`, `qta`, `acq`, `qta_ric`, `liv_ric`) VALUES
	('100.11.123', 'a', '100.12.001', '', 1, 1, 0, 0),
	('100.11.123', 'b', '100.12.002', 'd', 1, 1, 0, 0),
	('1055.45567.122', '', '100.12.001', '', 1, 1, 0, 0),
	('1055.45567.122', '', '100.12.002', 'd', 1, 1, 0, 0),
	('1055.54789.123', '', '999.99.002', '', 1, 1, 0, 0),
	('1059.11111.234', '', '998.01.001', '', 1, 1, 0, 0),
	('1059.11111.234', '', '998.01.001', 'a', 1, 1, 0, 0),
	('113.10.100', '', '201.10.001', '', 1, 1, 0, 0),
	('113.10.100', '', '201.11.001', '', 1, 1, 0, 0),
	('201.10.001', '', '998.01.001', '', 1, 1, 0, 0),
	('201.11.001', '', '998.01.001', 'a', 1, 1, 0, 0),
	('201.11.100', '', '789.10.001', '', 1, 1, 0, 0),
	('201.11.100', 'a', '789.10.001', 'b', 1, 1, 0, 0),
	('301.10.900', '', '201.10.001', '', 1, 1, 0, 0),
	('301.10.900', '', '201.11.001', '', 1, 1, 0, 0),
	('788.12.100', '', '789.10.001', '', 1, 1, 0, 0),
	('788.12.100', '', '789.10.001', 'b', 1, 1, 0, 0),
	('789.10.001', '', '100.12.001', '', 1, 1, 0, 0),
	('789.10.001', 'b', '100.12.002', 'd', 1, 1, 0, 0),
	('789.10.101', 'b', '999.99.002', '', 1, 1, 0, 0),
	('789.10.200', '', '998.01.001', '', 1, 1, 0, 0),
	('789.10.200', '', '998.01.001', 'a', 1, 1, 0, 0),
	('789.10.200', '', '999.99.002', '', 1, 1, 0, 0),
	('998.01.001', '', '100.12.001', '', 1, 1, 0, 0),
	('998.01.001', 'a', '100.12.002', 'd', 1, 1, 0, 0),
	('999.99.002', '', '201.10.001', '', 1, 1, 0, 0),
	('999.99.002', '', '789.10.001', 'b', 1, 1, 0, 0);

-- Dump della struttura di tabella dbc00.materiali
CREATE TABLE IF NOT EXISTS `materiali` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Solo maggiore di zero.',
  `materiale` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uk_materiale` (`materiale`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1 COMMENT='Tabella dei materiali.';

-- Dump dei dati della tabella dbc00.materiali: ~10 rows (circa)
REPLACE INTO `materiali` (`id`, `materiale`) VALUES
	(19, '18NiCrMo5'),
	(6, '39NiCrMo3'),
	(5, 'AlSi1MgMn'),
	(8, 'Cu Te'),
	(1, 'Fe360'),
	(10, 'Fe360B'),
	(2, 'Fe430'),
	(3, 'Fe510'),
	(4, 'GAlSi7'),
	(11, 'GAlSi9'),
	(18, 'GAlSi9_Y1');

-- Dump della struttura di tabella dbc00.particolari
CREATE TABLE IF NOT EXISTS `particolari` (
  `cod` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `mod` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  `materiale` int unsigned NOT NULL,
  PRIMARY KEY (`cod`,`mod`),
  KEY `FK_particolari_materiali` (`materiale`),
  CONSTRAINT `FK_particolari_codici` FOREIGN KEY (`cod`, `mod`) REFERENCES `codici` (`cod`, `mod`),
  CONSTRAINT `FK_particolari_materiali` FOREIGN KEY (`materiale`) REFERENCES `materiali` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tabella dei codici dei particolari (in comune con la tabella dei codici), con informazioni aggiuntive.';

-- Dump dei dati della tabella dbc00.particolari: ~18 rows (circa)
REPLACE INTO `particolari` (`cod`, `mod`, `materiale`) VALUES
	('100.11.123', 'a', 1),
	('102.11.100', 'a', 1),
	('301.10.100', '', 1),
	('789.10.100', '', 1),
	('789.10.100', 'a', 1),
	('201.10.100', '', 2),
	('789.10.101', '', 2),
	('789.10.101', 'a', 2),
	('100.11.123', 'b', 3),
	('789.10.101', 'b', 3),
	('789.10.200', '', 4),
	('100.11.123', '', 6),
	('201.11.100', '', 6),
	('202.10.100', '', 6),
	('113.10.100', '', 8),
	('102.11.100', '', 10),
	('201.11.100', 'a', 10),
	('788.12.100', '', 19),
	('788.12.100', 'a', 19);

-- Dump della struttura di tabella dbc00.prodotti
CREATE TABLE IF NOT EXISTS `prodotti` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Solo maggiore di zero.',
  `prodotto` varchar(255) NOT NULL DEFAULT '-',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uk_prodotto` (`prodotto`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=latin1 COMMENT='Tabella dei tipi di prodotto.';

-- Dump dei dati della tabella dbc00.prodotti: ~10 rows (circa)
REPLACE INTO `prodotti` (`id`, `prodotto`) VALUES
	(11, '???'),
	(12, 'Giunto'),
	(4, 'Guida lineare'),
	(5, 'Pattino a sfere'),
	(21, 'Pressostato'),
	(13, 'Profilato'),
	(20, 'Profilato_X1'),
	(6, 'Raccordo'),
	(3, 'Regolatore di pressione'),
	(22, 'Scatolato'),
	(2, 'Valvola'),
	(1, 'Vite'),
	(9, 'Vite a ricircolo di sfere');

-- Dump della struttura di tabella dbc00.schemi
CREATE TABLE IF NOT EXISTS `schemi` (
  `cod` char(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `mod` char(1) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`,`mod`),
  CONSTRAINT `FK_schemi_codici` FOREIGN KEY (`cod`, `mod`) REFERENCES `codici` (`cod`, `mod`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Tabella dei codici degli schemi (in comune con la tabella dei codici).';

-- Dump dei dati della tabella dbc00.schemi: ~10 rows (circa)
REPLACE INTO `schemi` (`cod`, `mod`) VALUES
	('100.12.003', 'b'),
	('201.10.900', ''),
	('300.33.330', 'a'),
	('301.10.900', ''),
	('554.55.554', ''),
	('567.89.111', ''),
	('820.10.900', ''),
	('820.10.900', 'a'),
	('820.10.900', 'b'),
	('987.43.444', 'a'),
	('999.88.776', '');


-- Dump della struttura del database dbc01
CREATE DATABASE IF NOT EXISTS `dbc01` /*!40100 DEFAULT CHARACTER SET latin1 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dbc01`;

-- Dump della struttura di procedura dbc01.CancCodice
DELIMITER //
CREATE PROCEDURE `CancCodice`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10)
)
    COMMENT 'CancCodice(_cod,_mod): cancella un singolo codice.'
BEGIN
DECLARE n INT DEFAULT 0;	-- Numero codici trovati: deve essere 1, solo codice univoco
DECLARE np, ns, na, nc INT DEFAULT 0;	-- Numero di particolari, schemi ecc...
DECLARE ok BOOL DEFAULT FALSE;
DECLARE done BOOL DEFAULT FALSE;
DECLARE nLeg BIGINT DEFAULT 0;
DECLARE nImp INT DEFAULT 0;
DECLARE risposta, rLeg, rImp VARCHAR(255) DEFAULT "";
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
SELECT COUNT(*) FROM dbc00.codici c WHERE c.cod = _cod AND c.`mod` = _mod INTO n;
SET nImp = _NumImpieghi(_cod, _mod);
SET nLeg = _NumLegami(_cod, _mod);
IF n = 1 THEN
	SELECT COUNT(*) FROM dbc00.particolari c WHERE c.cod = _cod AND c.`mod` = _mod INTO np;
	IF np = 0 THEN
		SELECT COUNT(*) FROM dbc00.assiemi c WHERE c.cod = _cod AND c.`mod` = _mod INTO na;
	END IF;
	IF na = 0 THEN
		SELECT COUNT(*) FROM dbc00.schemi c WHERE c.cod = _cod AND c.`mod` = _mod INTO ns;
	END IF;
	IF ns = 0 THEN
		SELECT COUNT(*) FROM dbc00.commerciali c WHERE c.cod = _cod AND c.`mod` = _mod INTO nc;
	END IF;
	IF np + ns + na + nc = 1 then
		SET ok = TRUE;
	END IF;
END IF;
IF nImp > 0 THEN
	SELECT CONCAT(" Ha ", nImp, " impieghi.") INTO rImp;
	SET ok = FALSE;
END IF;
IF nLeg > 0 THEN
	SELECT CONCAT(" Ha ", nLeg, " legami.") INTO rLeg;
	SET ok = FALSE;
END IF;
IF ok THEN
	START TRANSACTION;
	IF np = 1 THEN
		DELETE FROM dbc00.particolari c WHERE c.cod = _cod AND c.`mod` = _mod;
	ELSEIF ns = 1 THEN
		DELETE FROM dbc00.schemi c WHERE c.cod = _cod AND c.`mod` = _mod;
	ELSEIF na = 1 THEN
		DELETE FROM dbc00.assiemi c WHERE c.cod = _cod AND c.`mod` = _mod;
	ELSEIF nc = 1 THEN
		DELETE FROM dbc00.commerciali c WHERE c.cod = _cod AND c.`mod` = _mod;
	END IF;
	DELETE FROM dbc00.codici c WHERE c.cod = _cod AND c.`mod` = _mod;
	COMMIT;
	SET done = TRUE;
END IF;
IF done THEN
	SELECT CONCAT(_cod,_mod," cancellato.") AS `RISPOSTA`;
ELSE
	SELECT CONCAT(_cod,_mod," non cancellato.", rLeg, rImp) AS `RISPOSTA`;
END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.ContaCodici
DELIMITER //
CREATE PROCEDURE `ContaCodici`(
	IN `_cod` VARCHAR(255),
	IN `_mod` CHAR(1)
)
    SQL SECURITY INVOKER
    COMMENT 'ContaCodici(_cod,_mod): conta il numero di codici'
BEGIN
SELECT COUNT(*) AS `COUNT` FROM dbc00.codici WHERE (dbc00.codici.cod LIKE _cod) AND (dbc00.codici.`mod` LIKE _mod);
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.ContaRevisioni
DELIMITER //
CREATE PROCEDURE `ContaRevisioni`(
	IN `_cod` VARCHAR(50)
)
    READS SQL DATA
    SQL SECURITY INVOKER
    COMMENT 'ContaRevisioni(_cod): conta il numero di revisioni di un codice'
BEGIN
SELECT COUNT(*) AS `COUNT` FROM dbc00.codici WHERE dbc00.codici.cod LIKE _cod;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.DaAggiornare
DELIMITER //
CREATE PROCEDURE `DaAggiornare`(
	IN `_tab` VARCHAR(20)
)
BEGIN
	IF _tab = 'commerciali' THEN
		UPDATE dbc02.aggiornato SET commerciali = 1;
	ELSEIF _tab = 'costruttori' THEN
		UPDATE dbc02.aggiornato SET costruttori = 1;
	ELSEIF _tab = 'materiali' THEN
		UPDATE dbc02.aggiornato SET materiali = 1;
	ELSEIF _tab = 'prodotti' THEN
		UPDATE dbc02.aggiornato SET prodotti = 1;
	END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.Esplodi
DELIMITER //
CREATE PROCEDURE `Esplodi`(
	IN `iniCod` VARCHAR(50),
	IN `iniMod` VARCHAR(2),
	IN `maxP` INT
)
    DETERMINISTIC
    COMMENT 'Esplodi(iniCod,iniMod.maxP): esplode la lista dei codici divisi per livello'
BEGIN

WITH RECURSIVE lcte AS
	(
	SELECT
		L1.cod,
		L1.`mod`,
		L1.cod_p,
		L1.mod_p,
		L1.qta,
		1 AS livello
	FROM
		dbc00.legami L1
	WHERE
		L1.cod_p = iniCod AND L1.mod_p = iniMod AND L1.qta > 0
	UNION ALL
	SELECT
		L2.cod,
		L2.`mod`,
		L2.cod_p,
		L2.mod_p,
		L2.qta,
		lcte.livello + 1 AS livello
	FROM
		dbc00.legami L2
	INNER JOIN
		lcte ON L2.cod_p = lcte.cod AND L2.mod_p = lcte.`mod` AND lcte.qta > 0 AND lcte.livello < maxP
	)
SELECT lcte.cod, lcte.`mod`, c.descrizione, lcte.cod_p, lcte.mod_p, lcte.qta, livello
FROM lcte, dbc00.codici c
WHERE c.cod = lcte.cod AND c.`mod` = lcte.`mod` AND lcte.livello < maxP

UNION SELECT ci.cod, ci.`mod`, ci.descrizione, "-" , "-", 1, 0 AS livello
FROM dbc00.codici ci
WHERE ci.cod = iniCod AND ci.`mod` = iniMod
GROUP BY ci.cod, ci.`mod`
ORDER BY livello;
		
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.EsplodiCodici
DELIMITER //
CREATE PROCEDURE `EsplodiCodici`(
	IN `iniCod` VARCHAR(50),
	IN `iniMod` VARCHAR(2),
	IN `maxP` INT
)
    DETERMINISTIC
    COMMENT 'EsplodiCodici(iniCod,iniMod.maxP): esplode ed estrare i codici univoci'
BEGIN
SELECT DISTINCT q.cod, q.`mod` FROM	
(WITH RECURSIVE lcte AS
	(
	SELECT
		L1.cod,
		L1.`mod`,
		L1.cod_p,
		L1.mod_p,
		L1.qta,
		1 AS livello
	FROM
		dbc00.legami L1
	WHERE
		L1.cod_p = iniCod AND L1.mod_p = iniMod AND L1.qta > 0
	UNION ALL
	SELECT
		L2.cod,
		L2.`mod`,
		L2.cod_p,
		L2.mod_p,
		L2.qta,
		lcte.livello + 1 AS livello
	FROM
		dbc00.legami L2
	INNER JOIN
		lcte ON L2.cod_p = lcte.cod AND L2.mod_p = lcte.`mod` AND lcte.qta > 0 AND lcte.livello < maxP
	)
SELECT lcte.cod, lcte.`mod`, lcte.cod_p, lcte.mod_p, lcte.qta, livello
FROM lcte, dbc00.codici c
WHERE c.cod = lcte.cod AND c.`mod` = lcte.`mod` AND lcte.livello < maxP

UNION SELECT ci.cod, ci.`mod`,  "-" , "-", 1, 0 AS livello
FROM dbc00.codici ci
WHERE ci.cod = iniCod AND ci.`mod` = iniMod
GROUP BY ci.cod, ci.`mod`
ORDER BY livello) AS q
;

	
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.GetCode
DELIMITER //
CREATE PROCEDURE `GetCode`(
	IN `_cod` VARCHAR(255),
	IN `_mod` VARCHAR(1)
)
    SQL SECURITY INVOKER
    COMMENT 'GetCode(_cod,_mod): estrae tutti i dati di un codice'
BEGIN
DECLARE n_cod INT;
SELECT COUNT(*) FROM dbc00.codici WHERE (dbc00.codici.cod LIKE _cod) AND (dbc00.codici.mod LIKE _mod) INTO n_cod;
IF n_cod = 1 THEN
	SELECT c.cod AS `CODICE`,
	c.mod AS `MODIFICA`,
	c.descrizione AS `DESCRIZIONE`,
	CONCAT (IF(ISNULL(cm.cod),"","C"),IF(ISNULL(cs.cod),"","P"),IF(ISNULL(ca.cod),"","A"),IF(ISNULL(ck.cod),"","S")) AS TIPO,
	mat.materiale AS `MATERIALE`,
	cm.modello AS `MODELLO`,
	cm.dettagli AS `DETTAGLI`,
	cstr.costruttore AS `COSTRUTTORE`,
	prd.prodotto AS `PRODOTTO`,
	u.sigla AS `OPERATORE`,
	c.creazione AS `CREAZIONE`,
	ulast.sigla AS `ULTIMO`,
	c.aggiornamento AS `AGGIORNAMENTO`	
	FROM dbc00.codici c	
	LEFT JOIN dbc00.particolari cs
	ON c.cod = cs.cod AND c.`mod` = cs.`mod`
	LEFT JOIN dbc00.commerciali cm
	ON c.cod = cm.cod AND c.`mod` = cm.`mod`
	LEFT JOIN dbc00.assiemi ca
	ON c.cod = ca.cod AND c.`mod` = ca.`mod`
	LEFT JOIN dbc00.schemi ck
	ON c.cod = ck.cod AND c.`mod` = ck.`mod`
	LEFT JOIN dbc02.utenti u
	ON c.id_utente = u.id
	LEFT JOIN dbc02.utenti ulast
	ON c.id_ultimo = ulast.id
	LEFT JOIN dbc00.costruttori cstr
	ON cm.costruttore = cstr.id
	LEFT JOIN dbc00.materiali mat
	ON cs.materiale = mat.id
	LEFT JOIN dbc00.prodotti prd
	ON cm.prodotto = prd.id
	WHERE (c.cod LIKE _cod) AND (c.mod LIKE _mod);
ELSEIF n_cod = 0 THEN
	SELECT "-" AS `ERRORE`;
ELSE
	SELECT CONCAT("+",n_cod) AS `MULTIPLI`;
END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.InsAssieme
DELIMITER //
CREATE PROCEDURE `InsAssieme`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10),
	IN `_desc` VARCHAR(255)
)
    COMMENT 'InsAssieme(_cod, _mod, _desc): inserisce un nuovo assieme'
BEGIN
DECLARE n INT;
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
START TRANSACTION;
SET n = _InsCodice(_cod, _mod, _desc);
IF n = 1 THEN
	INSERT INTO dbc00.assiemi(cod, `mod`) VALUES(_cod, _mod);
	SELECT CONCAT(_cod,_mod," inserito.") AS `RISPOSTA`;
ELSEIF n = -1 THEN
	DO 0;	-- UPDATE assiemi SET cod = _cod WHERE assiemi.cod = _cod AND assiemi.`mod` = _mod;
	SELECT CONCAT(_cod,_mod," aggiornato.") AS `RISPOSTA`;
ELSE
	ROLLBACK;
	SELECT CONCAT(_cod,_mod," non inserito.") AS `RISPOSTA`;
END IF;
COMMIT;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.InsCommerciale
DELIMITER //
CREATE PROCEDURE `InsCommerciale`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10),
	IN `_cos` VARCHAR(255),
	IN `_pro` VARCHAR(255),
	IN `_model` VARCHAR(255),
	IN `_dett` VARCHAR(255)
)
    COMMENT 'InsCommerciale(_cod, _mod, _cos, _pro, _model, _dett).'
BEGIN
DECLARE n, idc, idp INT;
DECLARE _desc VARCHAR(255);
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
SET _cos = TRIM(_cos);
SET _pro = TRIM(_pro);
SET _model = TRIM(_model);
SET _dett = TRIM(_dett);
SET _desc = CONCAT(_pro, " ", UPPER(_cos), " ", _model);	-- Compone descrizione
START TRANSACTION;
SET idc = _InsCostruttore(_cos); -- id o -1 se errore
SET idp = _InsProdotto(_pro);
SET n = _InsCodice(_cod, _mod, _desc); -- 1 inserito, -1 aggiornato, 0 se errore
IF (idc != -1) AND (idp != -1) AND (n != 0) THEN
	IF n = 1 THEN
		INSERT INTO dbc00.commerciali(cod, `mod`, modello, dettagli, costruttore, prodotto)
		VALUES(_cod, _mod, _model, _dett, idc, idp);
		SELECT CONCAT(_cod,_mod," inserito.") AS `RISPOSTA`;
	ELSEIF n = -1 THEN
		UPDATE dbc00.commerciali c SET c.modello = _model, c.dettagli = _dett, c.costruttore = idc, c.prodotto = idp
		WHERE c.cod = _cod AND c.`mod` = _mod;
		SELECT CONCAT(_cod,_mod," aggiornato.") AS `RISPOSTA`;
	END IF;
ELSE
	ROLLBACK;
	SELECT CONCAT(_cod,_mod," non inserito.") AS `RISPOSTA`;	
END IF;
COMMIT;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.InsLegame
DELIMITER //
CREATE PROCEDURE `InsLegame`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10),
	IN `_cod_p` VARCHAR(50),
	IN `_mod_p` VARCHAR(10),
	IN `_qta` INT,
	IN `_acq` INT,
	IN `_qta_ric` INT,
	IN `_liv_ric` INT
)
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'InsLegame(_cod, _mod, _cod_p, _mod_p, _qta, _acq, _qta_ric, _liv_ric)'
BEGIN
DECLARE n1, n2, nc INT DEFAULT 0;

SET nc = _ChkCiclico(_cod, _mod, _cod_p, _mod_p, 100);
-- Usato maxP = 100. Si potrebbe passare come parametro e usare ISNULL(x,b_se_null), ma appesantisce la funzione
IF nc = 0 THEN
	START TRANSACTION;
		SET n1 = _InsLegame(_cod, _mod, _cod_p, _mod_p, _qta, _acq, _qta_ric, _liv_ric); -- 1 inserito, -1 aggiornato, 0 non inserito, -2 cancellato
		IF n1 = 1 OR n1 = -1 THEN
			SET n2 = _CorreggeLegame(_cod, _mod, _cod_p, _mod_p); -- 1 inserito, 0 non trovato
		END IF;
		IF (n1 = 1 OR n1 = -1) AND n2 = 1 THEN
			COMMIT;
			IF n1 = 1 THEN
				SELECT CONCAT(_cod,_mod," -> [ ", _cod_p, _mod_p, " ] inserito.") AS `RISPOSTA`;
			ELSE 
				SELECT CONCAT(_cod,_mod," -> [ ", _cod_p, _mod_p, " ] aggiornato.") AS `RISPOSTA`;
			END IF;	
		ELSE
			ROLLBACK;
			SELECT CONCAT(_cod,_mod," -> [ ", _cod_p, _mod_p, " ] non inserito.") AS `RISPOSTA`;
		END IF;
	COMMIT;
ELSE
	SELECT "Controllo ciclico non superato";
END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.InsParticolare
DELIMITER //
CREATE PROCEDURE `InsParticolare`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10),
	IN `_desc` VARCHAR(255),
	IN `_mat` VARCHAR(255)
)
    COMMENT 'InsParticolare(_cod, _mod, _desc, _mat)'
BEGIN
DECLARE n, idm INT;
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
START TRANSACTION;
SET idm = _InsMateriale(_mat); -- id o -1 se errore
SET n = _InsCodice(_cod, _mod, _desc); -- 1 inserito, -1 aggiornato, 0 se errore
IF (idm != -1) AND (n != 0) THEN
	IF n = 1 THEN
		INSERT INTO dbc00.particolari(cod, `mod`, materiale) VALUES(_cod, _mod, idm);
		SELECT CONCAT(_cod,_mod," inserito.") AS `RISPOSTA`;	
	ELSEIF n = -1 THEN
		UPDATE dbc00.particolari SET materiale = idm WHERE dbc00.particolari.cod = _cod AND dbc00.particolari.`mod` = _mod;
		SELECT CONCAT(_cod,_mod," aggiornato.") AS `RISPOSTA`;
	END IF;
ELSE
	ROLLBACK;
	SELECT CONCAT(_cod,_mod," non inserito.") AS `RISPOSTA`;
END IF;
COMMIT;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.InsSchema
DELIMITER //
CREATE PROCEDURE `InsSchema`(
	IN `_cod` VARCHAR(50),
	IN `_mod` VARCHAR(10),
	IN `_desc` VARCHAR(255)
)
    COMMENT 'InsSchema(_cod, _mod, _desc)'
BEGIN
DECLARE n INT;
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
START TRANSACTION;
SET n = _InsCodice(_cod, _mod, _desc);
IF n = 1 THEN
	INSERT INTO dbc00.schemi(cod, `mod`) VALUES(_cod, _mod);
	SELECT CONCAT(_cod,_mod," inserito.") AS `RISPOSTA`;
ELSEIF n = -1 THEN
	DO 0;	-- UPDATE schemi SET cod = _cod WHERE schemi.cod = _cod AND schemi.`mod` = _mod;
	SELECT CONCAT(_cod,_mod," aggiornato.") AS `RISPOSTA`;
ELSE
	ROLLBACK;
	SELECT CONCAT(_cod,_mod," non inserito.") AS `RISPOSTA`;
END IF;
COMMIT;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.ListaFunzioni
DELIMITER //
CREATE PROCEDURE `ListaFunzioni`()
BEGIN
SELECT ROUTINE_NAME, ROUTINE_COMMENT FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA=DATABASE() AND ROUTINE_TYPE='FUNCTION';
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.ListaProcedure
DELIMITER //
CREATE PROCEDURE `ListaProcedure`()
BEGIN
SELECT ROUTINE_NAME, ROUTINE_COMMENT FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA=DATABASE() AND ROUTINE_TYPE='PROCEDURE';
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.SetAggiorna
DELIMITER //
CREATE PROCEDURE `SetAggiorna`(
	IN `_tab` VARCHAR(20),
	IN `_usr` VARCHAR(20)
)
BEGIN
	DECLARE n INT;
	DECLARE u, nu INT;
	-- Verifica se esiste la colonna
	SET _tab = TRIM(_tab);
	SET n = -1;
	IF LENGTH(_tab) > 1 THEN
		SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = 'dbc02' AND table_name = 'aggiornato' AND column_name = _tab INTO n;
		IF n = 1 THEN
			SELECT "Existing", n;
		ELSE
			SELECT "Not existing", n;
		END IF;
	ELSE
		SELECT "No _tab specified", n;
	END IF;
	-- Verifica se esiste l'utente
	SET _usr = TRIM(_usr);
	SET u = -1;
	IF LENGTH(_usr) > 1 THEN
		SELECT COUNT(*) FROM dbc02.utenti WHERE utente = _usr INTO nu;
		IF nu = 1 THEN
			SELECT id FROM dbc02.utenti WHERE utente = _usr INTO u;
		ELSE
			SET u = 0;
		END IF;
		SELECT "id utente", u;
	ELSE
		SELECT "No _usr specified", u;
	END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.StatoUtente
DELIMITER //
CREATE PROCEDURE `StatoUtente`(
	INOUT `_uid` INT,
	INOUT `_st` TINYINT
)
    COMMENT 'Legge lo StatoUtente(INOUT _uid INT, INOUT _st TINYINT), soltanto se  _uid = -1. Altrimenti non fa né modifica nulla.'
BEGIN
DECLARE x INT;
DECLARE cusr VARCHAR(50);
IF _uid = -1 THEN
	SET _st = 0;
	SELECT CURRENT_USER() INTO cusr;
	SELECT count(*) FROM dbc02.utenti uc WHERE uc.utenteSQL = cusr INTO X;
	IF	x=1 THEN	
		SELECT uc.can_write FROM dbc02.utenti uc WHERE uc.utenteSQL = cusr INTO _st;
		SELECT uc.id FROM dbc02.utenti uc WHERE uc.utenteSQL = cusr INTO _uid;
		-- UPDATE u_connessi SET lasttime = NOW() WHERE idc = _uid;
	END IF;
END IF;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.VediCodici
DELIMITER //
CREATE PROCEDURE `VediCodici`(
	IN `_limite` INT,
	IN `_cod` VARCHAR(255),
	IN `_mod` CHAR(1)
)
    SQL SECURITY INVOKER
    COMMENT 'Elenca tutti i codici (senza filtri)'
BEGIN
SELECT c.cod AS `CODICE`, c.mod AS `MODIFICA`, c.descrizione AS `DESCRIZIONE`,
u.utente AS `OPERATORE`, c.creazione AS `CREAZIONE`,
uu.utente AS `ULTIMO`, c.aggiornamento AS `AGGIORNAMENTO`
FROM dbc00.codici c
LEFT JOIN dbc02.utenti u
ON c.id_utente = u.id
LEFT JOIN dbc02.utenti uu
ON c.id_ultimo = uu.id
WHERE (c.cod LIKE _cod) AND (c.`mod` LIKE _mod)
ORDER BY c.cod, c.`mod`
LIMIT _limite;
END//
DELIMITER ;

-- Dump della struttura di procedura dbc01.VediDescrizioni
DELIMITER //
CREATE PROCEDURE `VediDescrizioni`(
	IN `_limite` INT,
	IN `_cod` VARCHAR(255),
	IN `_mod` CHAR(1)
)
    SQL SECURITY INVOKER
    COMMENT 'Elenca codici con descrizioni complete'
BEGIN
SELECT c.cod AS CODICE,
c.mod AS MODIFICA,
CONCAT(c.descrizione, IFNULL(UPPER(CONCAT(" [",cstr.costruttore,"]")),""), IFNULL(CONCAT(" [",cm.modello,"]"),"")) AS DESCRIZIONE,
CONCAT (IF(ISNULL(cm.cod),"","C"),IF(ISNULL(cs.cod),"","P"),IF(ISNULL(ca.cod),"","A"),IF(ISNULL(ck.cod),"","S")) AS TIPO,
u.sigla AS OPERATORE,
c.creazione AS CREAZIONE
FROM dbc00.codici c
LEFT JOIN dbc00.particolari cs
ON c.cod = cs.cod AND c.mod = cs.mod
LEFT JOIN dbc00.commerciali cm
ON c.cod = cm.cod AND c.mod = cm.mod
LEFT JOIN dbc00.assiemi ca
ON c.cod = ca.cod AND c.mod = ca.mod
LEFT JOIN dbc00.schemi ck
ON c.cod = ck.cod AND c.mod = ck.mod
LEFT JOIN dbc02.utenti u
ON c.id_utente = u.id
LEFT JOIN dbc00.costruttori cstr
ON cm.costruttore = cstr.id
WHERE (c.cod LIKE _cod) AND (c.`mod` LIKE _mod)
ORDER BY CODICE
LIMIT _limite;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._ChkCiclico
DELIMITER //
CREATE FUNCTION `_ChkCiclico`(
	`iniCod` VARCHAR(50),
	`iniMod` VARCHAR(10),
	`_cod_p` VARCHAR(50),
	`_mod_p` VARCHAR(10),
	`maxP` INT
) RETURNS int
    DETERMINISTIC
    COMMENT 'Conta, tra i discendenti di iniCod,iniMod, il numero di ricorrenze di _cod_p,_mod_p'
BEGIN
DECLARE n INT DEFAULT 0;
SELECT COUNT(DISTINCT q.cod, q.`mod`) FROM	
	(WITH RECURSIVE lcte AS
		(
		SELECT
			L1.cod,
			L1.`mod`,
			L1.cod_p,
			L1.mod_p,
			L1.qta,
			1 AS livello
		FROM
			dbc00.legami L1
		WHERE
			L1.cod_p = iniCod AND L1.mod_p = iniMod AND L1.qta > 0
		UNION ALL
		SELECT
			L2.cod,
			L2.`mod`,
			L2.cod_p,
			L2.mod_p,
			L2.qta,
			lcte.livello + 1 AS livello
		FROM
			dbc00.legami L2
		INNER JOIN
			lcte ON L2.cod_p = lcte.cod AND L2.mod_p = lcte.`mod` AND lcte.qta > 0 AND lcte.livello < maxP
		)
	SELECT lcte.cod, lcte.`mod`, lcte.cod_p, lcte.mod_p, lcte.qta, livello
	FROM lcte, dbc00.codici c
	WHERE c.cod = lcte.cod AND c.`mod` = lcte.`mod` AND lcte.livello < maxP
	
	UNION SELECT ci.cod, ci.`mod`,  "-" , "-", 1, 0 AS livello
	FROM dbc00.codici ci
	WHERE ci.cod = iniCod AND ci.`mod` = iniMod
	GROUP BY ci.cod, ci.`mod`
	ORDER BY livello) AS q
	WHERE q.cod = _cod_p AND q.`mod` = _mod_p
	INTO n;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._CorreggeLegame
DELIMITER //
CREATE FUNCTION `_CorreggeLegame`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10),
	`_cod_p` VARCHAR(50),
	`_mod_p` VARCHAR(10)
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'DA CONTROLLARE !!!! Restituisce 1 se eseguito controllo, 0 se legame non trovato'
BEGIN
DECLARE n INT DEFAULT 0;	-- return value.
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato.
DECLARE _qta, _qta_ric SMALLINT DEFAULT 0; -- Temporanee per lettura record
DECLARE _acq, _liv_ric TINYINT DEFAULT 0;
DECLARE _tipoP CHAR DEFAULT '-';

CALL StatoUtente(_idx, _stx);
IF _stx = 2 THEN
	SET _cod = TRIM(_cod);
	SET _mod = TRIM(_mod);
	SET _cod_p = TRIM(_cod_p);
	SET _mod_p = TRIM(_mod_p);
	SELECT COUNT(*) FROM dbc00.legami lg WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p INTO n;
	IF n = 1 THEN
		SELECT qta, acq, qta_ric, liv_ric
			FROM dbc00.legami lg
			WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p
			INTO _qta, _acq, _qta_ric, _liv_ric;			
		SET _tipoP = _Tipo(_cod_p, _mod_p);
		
		-- Se la quantità è nulla oppure se il codice padre è uno schema -> elimina il legame (e azzera n, per dopo).
		IF _qta = 0 THEN	
			DELETE FROM dbc00.legami lg
				WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p;
			SET n = 0;
		ELSEIF _Tipo(_cod_p, _mod_p) = 'S' THEN	
			DELETE FROM dbc00.legami lg
				WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p;
			SET n = 0;
		END IF;
	END IF;
	
	IF n = 1 THEN
	
		-- Se il livello di ricambio è 0, -> azzera la quantità.
		IF _liv_ric = 0 THEN
			UPDATE dbc00.legami lg SET lg.qta_ric = 0
				WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p;
		END IF;
		
		-- Se il codice è uno schema -> non deve essere acquistato né usato come ricambio, ed è sempre in quantità unitaria.
		IF _tipoP = 'S' THEN
			UPDATE dbc00.legami lg SET lg.acq = 0, lg.qta = 1, lg.qta_ric = 0, lg.liv_ric = 0
				WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p;	
		END IF;

		-- Se il codice padre è un particolare o un commerciale -> non deve essere acquistato (ma può essere un ricambio).
		IF _tipoP = 'P' OR _tipoP = 'C' THEN
			UPDATE dbc00.legami lg SET lg.acq = 0
				WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p;
		END IF;
		
	END IF;

	

END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._InsCodice
DELIMITER //
CREATE FUNCTION `_InsCodice`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10),
	`_desc` VARCHAR(255)
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'Inserisce un codice. Controlla permessi dell''utente. Restituisce 1 se inserito, -1 se aggiornato, 0 se non inserito.'
BEGIN
DECLARE n INT;	-- return value
DECLARE x INT;	-- numero di codici trovati
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato
SET n = 0;
CALL StatoUtente(_idx, _stx);
IF _stx = 2 THEN
	SET _cod = TRIM(_cod);
	SET _mod = TRIM(_mod);
	SET _desc = TRIM(_desc);
	SELECT COUNT(*) FROM dbc00.codici c WHERE c.cod = _cod AND c.`mod` = _mod INTO x;
	IF x = 0 THEN
		INSERT INTO dbc00.codici(cod, `mod`, descrizione, id_utente, id_ultimo) VALUES(_cod, _mod, _desc, _idx, _idx);
		SET n = 1;
	ELSEIF x = 1 THEN
		UPDATE dbc00.codici SET descrizione = _desc, id_ultimo = _idx, aggiornamento = DEFAULT WHERE dbc00.codici.cod = _cod AND dbc00.codici.`mod` = _mod;
		SET n = -1;
	ELSE
		SET n = 0;
	END IF;
END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._InsCostruttore
DELIMITER //
CREATE FUNCTION `_InsCostruttore`(
	`_cos` VARCHAR(255)
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'Inserisce un record (per nome, univoco).  Controlla permessi dell''utente. Restituisce id o -1 se mancante o non unico.'
BEGIN
DECLARE n INT;	-- return value
DECLARE x INT;	-- numero di codici trovati
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato
SET n = -1;
CALL StatoUtente(_idx, _stx);
IF _stx = 2 THEN
	SET _cos = TRIM(_cos);
	SELECT COUNT(*) FROM dbc00.costruttori c WHERE c.costruttore = _cos INTO x;
	IF x = 0 THEN
		INSERT INTO dbc00.costruttori(costruttore) VALUES(_cos);
		SET n = 1;
	ELSEIF x = 1 THEN	
		SET n = 1; -- UPDATE costruttori SET ... WHERE costruttori.costruttore = _cos;
	ELSE
		SET n = -1;
	END IF;
END IF;
IF n = 1 THEN
	SELECT c.id FROM dbc00.costruttori c WHERE c.costruttore = _cos INTO n;
END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._InsLegame
DELIMITER //
CREATE FUNCTION `_InsLegame`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10),
	`_cod_p` VARCHAR(50),
	`_mod_p` VARCHAR(10),
	`_qta` INT,
	`_acq` INT,
	`_qta_ric` INT,
	`_liv_ric` INT
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'Inserisce un legame. Controlla permessi dell''utente.Se _qta<=0: cancella legame. Restituisce 1 se inserito, -1 se aggiornato, 0 se non inserito, -2 se cancellato.'
BEGIN
DECLARE n INT DEFAULT 0;	-- return value.
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato.
DECLARE nc INT;	-- numero di codici.
DECLARE ncp INT;	-- numero di codici padre.
DECLARE nl INT;	-- numero di legami.
CALL StatoUtente(_idx, _stx);
SET n = _stx;
IF _stx = 2 THEN
	SET _cod = TRIM(_cod);
	SET _mod = TRIM(_mod);
	SET _cod_p = TRIM(_cod_p);
	SET _mod_p = TRIM(_mod_p);
	SELECT COUNT(*) FROM dbc00.codici c WHERE c.cod = _cod AND c.`mod` = _mod INTO nc;
	SELECT COUNT(*) FROM dbc00.codici c WHERE c.cod = _cod_p AND c.`mod` = _mod_p INTO ncp;
	IF nc = 1 AND ncp = 1 THEN
		SELECT COUNT(*) FROM dbc00.legami lg WHERE lg.cod = _cod AND lg.`mod` = _mod AND lg.cod_p = _cod_p AND lg.mod_p = _mod_p INTO nl;
		-- Inserisce se _qta > 0
		IF nl = 0 AND _qta > 0 THEN
			INSERT INTO dbc00.legami(cod, `mod`, cod_p, mod_p, qta, acq, qta_ric, liv_ric)
			VALUES(_cod, _mod, _cod_p, _mod_p, _qta, _acq, _qta_ric, _liv_ric);
			SET n = 1;
		ELSE
			-- Aggiorna se _qta > 0
			IF _qta > 0 THEN 
				UPDATE dbc00.legami
				SET qta = _qta, acq = _acq, qta_ric = _qta_ric, liv_ric = _liv_ric
				WHERE dbc00.legami.cod = _cod AND dbc00.legami.`mod` = _mod AND dbc00.legami.cod_p = _cod_p AND dbc00.legami.mod_p = _mod_p;
				SET n = -1;
			-- Se _qta = 0 (oppure < 0): elimina il legame
			ELSE	
				DELETE FROM dbc00.legami WHERE dbc00.legami.cod = _cod AND dbc00.legami.`mod` = _mod AND dbc00.legami.cod_p = _cod_p AND dbc00.legami.mod_p = _mod_p;
				SET n = -2;
			END IF;
		END IF;
	END IF;
END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._InsMateriale
DELIMITER //
CREATE FUNCTION `_InsMateriale`(
	`_mat` VARCHAR(255)
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'Inserisce un record (per nome, univoco).  Controlla permessi dell''utente. Restituisce id o -1 se mancante o non unico.'
BEGIN
DECLARE n INT;	-- return value
DECLARE x INT;	-- numero di codici trovati
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato
SET n = -1;
CALL StatoUtente(_idx, _stx);
IF _stx = 2 THEN
	SET _mat = TRIM(_mat);
	SELECT COUNT(*) FROM dbc00.materiali m WHERE m.materiale = _mat INTO x;
	IF x = 0 THEN
		INSERT INTO dbc00.materiali(materiale) VALUES(_mat);
		SET n = 1;
	ELSEIF x = 1 THEN	
		SET n = 1; -- UPDATE materiali SET ... WHERE materiali.materiale = _mat;
	ELSE
		SET n = -1;
	END IF;
END IF;
IF n = 1 THEN
	SELECT m.id FROM dbc00.materiali m WHERE m.materiale = _mat INTO n;
END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._InsProdotto
DELIMITER //
CREATE FUNCTION `_InsProdotto`(
	`_pro` VARCHAR(255)
) RETURNS int
    MODIFIES SQL DATA
    DETERMINISTIC
    COMMENT 'Inserisce un record (per nome, univoco).  Controlla permessi dell''utente. Restituisce id o -1 se mancante o non unico.'
BEGIN
DECLARE n INT;	-- return value
DECLARE x INT;	-- numero di codici trovati
DECLARE _idx INT DEFAULT -1;	-- id del current user e...
DECLARE _stx INT DEFAULT 0; -- ...stato
SET n = -1;
CALL StatoUtente(_idx, _stx);
IF _stx = 2 THEN
	SET _pro = TRIM(_pro);
	SELECT COUNT(*) FROM dbc00.prodotti p WHERE p.prodotto = _pro INTO x;
	IF x = 0 THEN
		INSERT INTO dbc00.prodotti(prodotto) VALUES(_pro);
		SET n = 1;
	ELSEIF x = 1 THEN	
		SET n = 1; -- UPDATE prodotti SET ... WHERE prodotti.prodotto = _pro;
	ELSE
		SET n = -1;
	END IF;
END IF;
IF n = 1 THEN
	SELECT p.id FROM dbc00.prodotti p WHERE p.prodotto = _pro INTO n;
END IF;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._NumImpieghi
DELIMITER //
CREATE FUNCTION `_NumImpieghi`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10)
) RETURNS int
    READS SQL DATA
    DETERMINISTIC
    COMMENT 'Restituisce il numero di legami in cui il codice (competo di revisione) è impiegato.'
BEGIN
DECLARE n INT DEFAULT 0;
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
SELECT COUNT(*) FROM dbc00.legami lg WHERE lg.cod = _cod AND lg.`mod` = _mod INTO n;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._NumLegami
DELIMITER //
CREATE FUNCTION `_NumLegami`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10)
) RETURNS bigint
    READS SQL DATA
    DETERMINISTIC
    COMMENT 'Restituisce il numero di legami in cui il codice (completo di revisione) è padre.'
BEGIN
DECLARE n BIGINT DEFAULT 0;
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
SELECT COUNT(*) FROM dbc00.legami lg WHERE lg.cod_p = _cod AND lg.mod_p = _mod INTO n;
RETURN n;
END//
DELIMITER ;

-- Dump della struttura di funzione dbc01._Tipo
DELIMITER //
CREATE FUNCTION `_Tipo`(
	`_cod` VARCHAR(50),
	`_mod` VARCHAR(10)
) RETURNS char(1) CHARSET latin1
    READS SQL DATA
    DETERMINISTIC
    COMMENT '_Tipo(_cod, _mod). Restituisce ''A'', ''P'', ''C'', ''S'' se assieme, particolare, commerciale o schema. ''-'' se assente o se incongruenze.'
BEGIN
DECLARE n INT DEFAULT 0;
DECLARE np, nc, na, ns INT DEFAULT 0;
DECLARE tp CHAR DEFAULT '-';
SET _cod = TRIM(_cod);
SET _mod = TRIM(_mod);
SELECT COUNT(*) FROM dbc00.codici c WHERE c.cod = _cod AND c.`mod` = _mod INTO n;
SELECT COUNT(*) FROM dbc00.assiemi c WHERE c.cod = _cod AND c.`mod` = _mod INTO na;
SELECT COUNT(*) FROM dbc00.particolari c WHERE c.cod = _cod AND c.`mod` = _mod INTO np;
SELECT COUNT(*) FROM dbc00.commerciali c WHERE c.cod = _cod AND c.`mod` = _mod INTO nc;
SELECT COUNT(*) FROM dbc00.schemi c WHERE c.cod = _cod AND c.`mod` = _mod INTO ns;
IF n = 1 AND na + np + nc + ns = 1 THEN
	IF (na = 1) THEN
		SET tp = 'A';
	ELSEIF np = 1 THEN
		SET tp = 'P';
	ELSEIF nc = 1 THEN
		SET tp = 'C';
	ELSEIF ns = 1 THEN
		SET tp = 'S';
	END IF;
END IF;
RETURN tp;
END//
DELIMITER ;


-- Dump della struttura del database dbc02
CREATE DATABASE IF NOT EXISTS `dbc02` /*!40100 DEFAULT CHARACTER SET latin1 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dbc02`;

-- Dump della struttura di tabella dbc02.aggiornato
CREATE TABLE IF NOT EXISTS `aggiornato` (
  `id` int unsigned NOT NULL COMMENT 'ID dell''utente',
  `commerciali` int unsigned NOT NULL DEFAULT '0' COMMENT '0 invariato 1 modificato',
  `costruttori` int unsigned NOT NULL DEFAULT '0',
  `materiali` int unsigned NOT NULL DEFAULT '0',
  `prodotti` int unsigned NOT NULL DEFAULT '0',
  KEY `FK_aggiornato_utenti` (`id`),
  CONSTRAINT `FK_aggiornato_utenti` FOREIGN KEY (`id`) REFERENCES `utenti` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella dbc02.aggiornato: ~0 rows (circa)
REPLACE INTO `aggiornato` (`id`, `commerciali`, `costruttori`, `materiali`, `prodotti`) VALUES
	(1, 0, 1, 1, 1),
	(2, 1, 1, 1, 1),
	(3, 1, 1, 1, 1),
	(4, 1, 1, 1, 0),
	(5, 1, 1, 1, 0),
	(6, 1, 0, 0, 0);

-- Dump della struttura di tabella dbc02.utenti
CREATE TABLE IF NOT EXISTS `utenti` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Usare come INT, max_value sufficiente. id > 0;',
  `utente` varchar(20) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `utenteSQL` varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL COMMENT 'current_user()',
  `sigla` char(5) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT '',
  `password` varchar(64) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL COMMENT 'crittografata (sha256?)',
  `can_write` tinyint unsigned DEFAULT NULL COMMENT '0 none, 1 read, 2 write',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `uk_utente` (`utente`) USING BTREE,
  UNIQUE KEY `uk_sigla` (`sigla`) USING BTREE,
  UNIQUE KEY `uk_usql` (`utenteSQL`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COMMENT='utenti';

-- Dump dei dati della tabella dbc02.utenti: ~6 rows (circa)
REPLACE INTO `utenti` (`id`, `utente`, `utenteSQL`, `sigla`, `password`, `can_write`) VALUES
	(1, 'pippo', 'pippo@localhost', 'PIP', NULL, 2),
	(2, 'pluto', 'pluto@localhost', 'PLU', NULL, 2),
	(3, 'paperino', NULL, 'PAP', NULL, 1),
	(4, 'qui', NULL, 'QUI', NULL, 1),
	(5, 'topolino', NULL, 'TOP', NULL, 0),
	(6, 'root', 'root@localhost', 'ADM', NULL, 2);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
