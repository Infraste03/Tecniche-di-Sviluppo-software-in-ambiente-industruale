-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Giu 16, 2022 alle 17:34
-- Versione del server: 10.4.21-MariaDB
-- Versione PHP: 7.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbcruise`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `crociere`
--

CREATE TABLE `crociere` (
  `id_crociera` int(255) NOT NULL,
  `nome_nave` varchar(255) NOT NULL,
  `localita` varchar(255) NOT NULL,
  `data` date NOT NULL,
  `numero_persone` int(255) NOT NULL,
  `prezzo` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `crociere`
--

INSERT INTO `crociere` (`id_crociera`, `nome_nave`, `localita`, `data`, `numero_persone`, `prezzo`) VALUES
(1, 'Checca', 'Mediterraneo', '2022-09-03', 2000, 650),
(2, 'Rubby', 'Caraibi', '2022-10-07', 1800, 750),
(3, 'Combare', 'Asiatica', '2023-04-10', 1600, 800);

-- --------------------------------------------------------

--
-- Struttura della tabella `partecipanti`
--

CREATE TABLE `partecipanti` (
  `fk_crociera` int(255) NOT NULL,
  `fk_utente` int(255) NOT NULL,
  `Numero_Biglietti` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `partecipanti`
--

INSERT INTO `partecipanti` (`fk_crociera`, `fk_utente`, `Numero_Biglietti`) VALUES
(2, 12, 0),
(2, 15, 4),
(2, 16, 4),
(1, 16, 4),
(1, 16, 2),
(1, 19, 5),
(1, 20, 2),
(1, 16, 5),
(1, 16, 53),
(2, 16, 7),
(1, 16, 3),
(1, 16, 0),
(2, 16, -1),
(1, 16, 0),
(1, 16, 51),
(3, 16, 2),
(1, 16, 0),
(3, 16, 0),
(2, 16, 50),
(3, 23, 300),
(2, 23, 2);

-- --------------------------------------------------------

--
-- Struttura della tabella `transazioni`
--

CREATE TABLE `transazioni` (
  `id_transazione` int(255) NOT NULL,
  `importo` varchar(255) NOT NULL,
  `fk_utente` int(255) NOT NULL,
  `fk_barca` int(255) NOT NULL,
  `data` date NOT NULL DEFAULT current_timestamp(),
  `Numero_Tickets` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `transazioni`
--

INSERT INTO `transazioni` (`id_transazione`, `importo`, `fk_utente`, `fk_barca`, `data`, `Numero_Tickets`) VALUES
(10, '3000', 15, 2, '2022-04-23', 4),
(11, '3000', 16, 2, '2022-05-13', 4),
(16, '2600', 16, 1, '2022-05-16', 4),
(17, '1300', 16, 1, '2022-06-14', 2),
(18, '3250', 19, 1, '2022-06-15', 5),
(19, '1300', 20, 1, '2022-06-15', 2),
(20, '3250', 16, 1, '2022-06-15', 5),
(21, '34450', 16, 1, '2022-06-15', 53),
(22, '5250', 16, 2, '2022-06-15', 7),
(23, '1950', 16, 1, '2022-06-16', 3),
(24, '0', 16, 1, '2022-06-16', 0),
(25, '-750', 16, 2, '2022-06-16', -1),
(26, '0', 16, 1, '2022-06-16', 0),
(27, '33150', 16, 1, '2022-06-16', 51),
(28, '1600', 16, 3, '2022-06-16', 2),
(29, '0', 16, 1, '2022-06-16', 0),
(30, '0', 16, 3, '2022-06-16', 0),
(31, '37500', 16, 2, '2022-06-16', 50),
(32, '240000', 23, 3, '2022-06-16', 300),
(33, '1500', 23, 2, '2022-06-16', 2);

-- --------------------------------------------------------

--
-- Struttura della tabella `utente`
--

CREATE TABLE `utente` (
  `id_utente` int(255) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `cognome` varchar(255) NOT NULL,
  `is_admin` tinyint(1) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `carta_credito` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `utente`
--

INSERT INTO `utente` (`id_utente`, `nome`, `cognome`, `is_admin`, `email`, `password`, `carta_credito`) VALUES
(1, 'Antonio', 'Signorelli', 1, 'w', 'w', 'numero carta'),
(6, 'Checca', 'Pietà', 0, 'sfsf@sfsf.uk', 'ciao', '4rgvrfrgftrf'),
(12, 'salsa', 'ferdy', 0, 'salsa@ferdy.it', 'ciaociso', '7485289629'),
(14, 'asdf', 'sxdcfg', 0, 'zxcv', 'zxcvb', 'cvbnm,'),
(15, 'Mario', 'Rossi', 0, 'mariorossi@example.com', 'mario', '4830 6373 6631 9363'),
(16, 'g', 'g', 0, 'g', 'g', 'g'),
(17, '', 'rossi', 0, 'dario', 'dario', '2893'),
(18, 'q', 'q', 0, 'qy', '', 'q'),
(19, 't', 't', 0, 't', 't', 't'),
(20, 'hg', 'jh', 0, 'gf', 'gf', 'fds'),
(21, 't', 'teeeeeeeeeee', 0, 'trdes', '5', 't'),
(22, 'Gio', 'Ski', 0, 'gio.ski@iy.it', 'sksk', '6959123579'),
(23, 'gio', 'schio', 0, 'schianchi10@gmail.com', 'ciao', '11111111111111');

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `crociere`
--
ALTER TABLE `crociere`
  ADD PRIMARY KEY (`id_crociera`);

--
-- Indici per le tabelle `partecipanti`
--
ALTER TABLE `partecipanti`
  ADD KEY `fk_crociera` (`fk_crociera`),
  ADD KEY `fk_utente` (`fk_utente`);

--
-- Indici per le tabelle `transazioni`
--
ALTER TABLE `transazioni`
  ADD PRIMARY KEY (`id_transazione`),
  ADD KEY `fk_utente2` (`fk_utente`),
  ADD KEY `fk_barca2` (`fk_barca`);

--
-- Indici per le tabelle `utente`
--
ALTER TABLE `utente`
  ADD PRIMARY KEY (`id_utente`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `crociere`
--
ALTER TABLE `crociere`
  MODIFY `id_crociera` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT per la tabella `transazioni`
--
ALTER TABLE `transazioni`
  MODIFY `id_transazione` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT per la tabella `utente`
--
ALTER TABLE `utente`
  MODIFY `id_utente` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `partecipanti`
--
ALTER TABLE `partecipanti`
  ADD CONSTRAINT `fk_crociera` FOREIGN KEY (`fk_crociera`) REFERENCES `crociere` (`id_crociera`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_utente` FOREIGN KEY (`fk_utente`) REFERENCES `utente` (`id_utente`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `transazioni`
--
ALTER TABLE `transazioni`
  ADD CONSTRAINT `fk_barca2` FOREIGN KEY (`fk_barca`) REFERENCES `crociere` (`id_crociera`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_utente2` FOREIGN KEY (`fk_utente`) REFERENCES `utente` (`id_utente`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
