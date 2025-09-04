-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 18, 2025 at 05:17 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bdfrmhistorico`
--

-- --------------------------------------------------------

--
-- Table structure for table `conductores_historico`
--

CREATE TABLE `conductores_historico` (
  `IdConductor` int(11) NOT NULL,
  `IdEmpleado` int(11) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `cosechas_historico`
--

CREATE TABLE `cosechas_historico` (
  `IdCosecha` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `FechaCosecha` date NOT NULL,
  `NumSemCosecha` int(11) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `cultivos_historico`
--

CREATE TABLE `cultivos_historico` (
  `IdCultivo` int(11) NOT NULL,
  `TipoBerry` varchar(30) DEFAULT NULL,
  `Variedad` varchar(60) DEFAULT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `empleados_historico`
--

CREATE TABLE `empleados_historico` (
  `IdEmpleado` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Departamento` varchar(100) NOT NULL,
  `Puesto` varchar(100) NOT NULL,
  `Direccion` varchar(40) NOT NULL,
  `Colonia` varchar(30) NOT NULL,
  `CP` varchar(5) NOT NULL,
  `Municipio` varchar(30) NOT NULL,
  `Estado` varchar(30) NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Estatus` varchar(20) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `errorlogs_historico`
--

CREATE TABLE `errorlogs_historico` (
  `Id` int(11) NOT NULL,
  `UserName` varchar(100) DEFAULT NULL,
  `ErrorMessage` text NOT NULL,
  `ErrorProcedure` varchar(255) DEFAULT NULL,
  `ErrorLine` varchar(255) DEFAULT NULL,
  `ErrorTime` datetime DEFAULT current_timestamp(),
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `llaves_historico`
--

CREATE TABLE `llaves_historico` (
  `IdLlave` int(11) NOT NULL,
  `IdRancho` int(11) NOT NULL,
  `NombreLlave` varchar(60) NOT NULL,
  `SuperficieHA` decimal(18,2) NOT NULL,
  `SuperficieAcres` decimal(18,2) NOT NULL,
  `CantidadTuneles` int(11) NOT NULL,
  `Disponibilidad` varchar(4) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `personal_cosecha_historico`
--

CREATE TABLE `personal_cosecha_historico` (
  `IdPersonalCosecha` int(11) NOT NULL,
  `IdCosecha` int(11) NOT NULL,
  `IdEmpleado` int(11) NOT NULL,
  `Jarras` int(11) NOT NULL,
  `PrecioJarra` decimal(10,0) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `plantaciones_historico`
--

CREATE TABLE `plantaciones_historico` (
  `IdPlantacion` int(11) NOT NULL,
  `IdCultivo` int(11) NOT NULL,
  `IdVivero` int(11) NOT NULL,
  `IdLlave` int(11) NOT NULL,
  `CantidadPlantas` int(11) NOT NULL,
  `PlantasPorMetro` decimal(18,2) NOT NULL,
  `FechaPlantacion` date NOT NULL,
  `NumSemPlantacion` int(11) NOT NULL,
  `EstatusPlantacion` varchar(15) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `podas_historico`
--

CREATE TABLE `podas_historico` (
  `IdPoda` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `TipoPoda` varchar(20) NOT NULL,
  `FechaPoda` date NOT NULL,
  `NumSemPoda` int(11) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `proceso_historico`
--

CREATE TABLE `proceso_historico` (
  `IdProceso` int(11) NOT NULL,
  `IdViaje` int(11) NOT NULL,
  `ClaseAkg` decimal(10,2) NOT NULL,
  `ClaseBkg` decimal(10,2) NOT NULL,
  `ClaseCkg` decimal(10,2) NOT NULL,
  `Rechazo` decimal(10,2) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `produccion_historico`
--

CREATE TABLE `produccion_historico` (
  `IdProduccion` int(11) NOT NULL,
  `IdCosecha` int(11) NOT NULL,
  `TipoCaja` varchar(10) NOT NULL,
  `CantidadCajas` int(11) NOT NULL,
  `KilosProceso` decimal(10,0) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ranchos_historico`
--

CREATE TABLE `ranchos_historico` (
  `IdRancho` int(11) NOT NULL,
  `NombreRancho` varchar(60) NOT NULL,
  `NumeroRancho` varchar(60) NOT NULL,
  `SuperficieHA` decimal(18,2) NOT NULL,
  `SuperficieAcres` decimal(18,2) NOT NULL,
  `Direccion` varchar(100) NOT NULL,
  `CP` varchar(5) NOT NULL,
  `Municipio` varchar(30) NOT NULL,
  `Estado` varchar(30) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `replantes_historico`
--

CREATE TABLE `replantes_historico` (
  `IdReplante` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `IdCultivo` int(11) NOT NULL,
  `IdVivero` int(11) NOT NULL,
  `CantidadReplante` int(11) NOT NULL,
  `FechaReplante` date NOT NULL,
  `NumSemReplante` int(11) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tarimas_historico`
--

CREATE TABLE `tarimas_historico` (
  `IdTarima` int(11) NOT NULL,
  `IdProduccion` int(11) NOT NULL,
  `IdViaje` int(11) NOT NULL,
  `CantidadCajasViaje` int(11) NOT NULL,
  `Licencia` int(11) NOT NULL,
  `KilosProcesoViaje` decimal(10,2) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `usuarios_historico`
--

CREATE TABLE `usuarios_historico` (
  `IdUsuario` int(11) NOT NULL,
  `Usuario` varchar(20) NOT NULL,
  `Contrasena` varchar(20) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Email` varchar(40) NOT NULL,
  `Rol` varchar(15) NOT NULL,
  `Estatus` varchar(20) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `vehiculos_historico`
--

CREATE TABLE `vehiculos_historico` (
  `IdVehiculo` int(11) NOT NULL,
  `Placas` varchar(7) DEFAULT NULL,
  `Modelo` varchar(30) DEFAULT NULL,
  `Marca` varchar(30) DEFAULT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ventasdetalles_historico`
--

CREATE TABLE `ventasdetalles_historico` (
  `IdVenta` int(11) NOT NULL,
  `IdTarima` int(11) NOT NULL,
  `PrecioVentaTarima` decimal(10,0) NOT NULL,
  `FechaRecepcion` date NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ventas_historico`
--

CREATE TABLE `ventas_historico` (
  `IdVenta` int(11) NOT NULL,
  `FechaFacturacion` date NOT NULL,
  `Total` decimal(10,0) NOT NULL,
  `PrecioDolar` decimal(10,0) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `viajes_historico`
--

CREATE TABLE `viajes_historico` (
  `IdViaje` int(11) NOT NULL,
  `IdVehiculo` int(11) NOT NULL,
  `IdConductor` int(11) NOT NULL,
  `FechaSalida` date NOT NULL,
  `NumSemViaje` int(11) NOT NULL,
  `EstadoAprobacion` varchar(30) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `viveros_historico`
--

CREATE TABLE `viveros_historico` (
  `IdVivero` int(11) NOT NULL,
  `NombreVivero` varchar(60) NOT NULL,
  `CodigoVivero` varchar(30) NOT NULL,
  `FechaTraspaso` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
