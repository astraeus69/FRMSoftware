-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 05, 2025 at 08:33 PM
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
-- Database: `bdfrm`
--

-- --------------------------------------------------------

--
-- Table structure for table `calificacion_tarima`
--

CREATE TABLE `calificacion_tarima` (
  `IdCalificacion` int(11) NOT NULL,
  `IdTarima` int(11) NOT NULL,
  `EstadoAprobacion` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `conductores`
--

CREATE TABLE `conductores` (
  `IdConductor` int(11) NOT NULL,
  `IdEmpleado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `cosechas`
--

CREATE TABLE `cosechas` (
  `IdCosecha` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `FechaCosecha` date NOT NULL,
  `NumSemCosecha` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cosechas`
--

INSERT INTO `cosechas` (`IdCosecha`, `IdPlantacion`, `FechaCosecha`, `NumSemCosecha`) VALUES
(1, 5, '2025-03-22', 12),
(2, 4, '2025-03-29', 13),
(4, 8, '2025-04-30', 18),
(5, 6, '2025-04-26', 17);

-- --------------------------------------------------------

--
-- Table structure for table `cultivos`
--

CREATE TABLE `cultivos` (
  `IdCultivo` int(11) NOT NULL,
  `TipoBerry` varchar(30) DEFAULT NULL,
  `Variedad` varchar(60) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cultivos`
--

INSERT INTO `cultivos` (`IdCultivo`, `TipoBerry`, `Variedad`) VALUES
(1, 'Fresa', 'Variedad A'),
(2, 'Arándano', 'Variedad B'),
(3, 'Frambuesa', 'Variedad C'),
(4, 'Fresa Orgánica', 'Variedad D'),
(5, 'Zarzamora', 'Variedad E'),
(7, 'Fresa', 'Variedad F'),
(8, 'Arándano', 'Variedad X');

-- --------------------------------------------------------

--
-- Table structure for table `empleados`
--

CREATE TABLE `empleados` (
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
  `Estatus` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `empleados`
--

INSERT INTO `empleados` (`IdEmpleado`, `Nombre`, `Departamento`, `Puesto`, `Direccion`, `Colonia`, `CP`, `Municipio`, `Estado`, `Telefono`, `Estatus`) VALUES
(1, 'Juan Pérez', 'Ventas', 'Vendedor', 'Calle Ficticia 123', 'Colonia A', '12345', 'Municipio A', 'Estado A', '1234567890', 'Activo'),
(2, 'Ana Gómez', 'Logística', 'Coordinadora', 'Avenida X 456', 'Colonia B', '23456', 'Municipio B', 'Estado B', '0987654321', 'Activo'),
(3, 'Carlos Martínez', 'Administración', 'Contador', 'Calle Y 789', 'Colonia C', '34567', 'Municipio C', 'Estado C', '1122334455', 'Inactivo'),
(4, 'Laura Díaz', 'Producción', 'Supervisor', 'Plaza Z 101', 'Colonia D', '45678', 'Municipio D', 'Estado D', '2233445566', 'Activo'),
(5, 'Pedro Sánchez', 'Recursos Humanos', 'Jefe', 'Calle W 102', 'Colonia E', '56789', 'Municipio E', 'Estado E', '3344556677', 'Activo'),
(6, 'Vicente Fernández', 'Campo', 'Cosechador', 'Dirección #6', 'Colonia 6', '66666', 'Municipio 6', 'Coahuila', '3411778674', 'Activo'),
(7, 'Pedro Salaz', 'Producción', 'Cosechador', 'Dir 100', 'Col 100', '11245', 'Mun 100', 'Ciudad de México', '1212232122', 'Activo');

-- --------------------------------------------------------

--
-- Table structure for table `errorlogs`
--

CREATE TABLE `errorlogs` (
  `Id` int(11) NOT NULL,
  `UserName` varchar(100) DEFAULT NULL,
  `ErrorMessage` text NOT NULL,
  `ErrorProcedure` varchar(255) DEFAULT NULL,
  `ErrorLine` varchar(255) DEFAULT NULL,
  `ErrorTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `errorlogs`
--

INSERT INTO `errorlogs` (`Id`, `UserName`, `ErrorMessage`, `ErrorProcedure`, `ErrorLine`, `ErrorTime`) VALUES
(1, 'Usuario', 'x', 'ConsultarEmpleados', 'x', '2025-03-20 20:33:57'),
(2, 'Usuario', 'Error forzado para pruebas.', 'ConsultarPersonalCosechas', '   at FRMSoftware.Components.Pages.Movimientos.PersonalCosecha.PersonalCosecha.ConsultarPersonalCosechas() in C:\\Users\\soyob\\OneDrive - Instituto Tecnológico de Ciudad Guzmán\\FRMSoftware\\FRMSoftware\\Components\\Pages\\Movimientos\\PersonalCosecha\\PersonalCos', '2025-03-21 02:37:21'),
(3, 'Usuario', 'Value cannot be null. (Parameter \'source\')', 'ConsultarPersonalCosechas', '   at System.Linq.ThrowHelper.ThrowArgumentNullException(ExceptionArgument argument)\r', '2025-03-21 02:38:47'),
(4, 'Usuario', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1121', '2025-04-02 14:44:22'),
(5, 'Usuario', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1123', '2025-04-02 14:49:01'),
(6, 'Usuario', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1123', '2025-04-02 14:50:57'),
(7, 'Usuario', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1121', '2025-04-02 16:38:32'),
(8, 'Usuario', 'Error al actualizar la tarima: {\"type\":\"https://tools.ietf.org/html/rfc9110#section-15.5.1\",\"title\":\"One or more validation errors occurred.\",\"status\":400,\"errors\":{\"tarimaDto\":[\"The tarimaDto field is required.\"],\"$.cantidadCajasViaje\":[\"The JSON value could not be converted to System.Int32. Path: $.cantidadCajasViaje | LineNumber: 0 | BytePositionInLine: 68.\"]},\"traceId\":\"00-647bfef82c48fe7b1e691ce7c6553ad5-1b598c88568e221f-00\"}', 'GrabarViajeConTarimas - UpdateTarima', '1351', '2025-04-03 03:57:57'),
(9, 'Usuario', 'Error al actualizar la tarima: {\"type\":\"https://tools.ietf.org/html/rfc9110#section-15.5.1\",\"title\":\"One or more validation errors occurred.\",\"status\":400,\"errors\":{\"tarimaDto\":[\"The tarimaDto field is required.\"],\"$.cantidadCajasViaje\":[\"The JSON value could not be converted to System.Int32. Path: $.cantidadCajasViaje | LineNumber: 0 | BytePositionInLine: 68.\"]},\"traceId\":\"00-202df1127a4521e085cdc4a1ee699b5a-6229df1e6f83a8c2-00\"}', 'GrabarViajeConTarimas - UpdateTarima', '1351', '2025-04-03 03:59:24'),
(10, 'Usuario', 'No connection could be made because the target machine actively refused it. (localhost:5159)', 'ConsultarEmpleados', '   at System.Net.Http.HttpConnectionPool.ConnectToTcpHostAsync(String host, Int32 port, HttpRequestMessage initialRequest, Boolean async, CancellationToken cancellationToken)\r', '2025-04-05 02:20:53'),
(11, 'Usuario', 'Error al obtener los datos del rancho', 'SeleccionarViaje', '1116', '2025-04-06 01:07:21'),
(12, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-28 18:47:43'),
(13, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-28 18:50:03'),
(14, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-28 18:51:13'),
(15, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-29 01:48:43'),
(16, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-29 01:52:47'),
(17, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-29 02:01:10'),
(18, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-29 02:02:10'),
(19, 'Usuario', 'Error al registrar el detalle de venta: ', 'GrabarVenta - CreateVentasDetalles', '1282', '2025-04-29 02:06:35'),
(20, 'Usuario', 'Response status code does not indicate success: 500 (Internal Server Error).', 'ConsultarViajesDetalles', '   at System.Net.Http.HttpResponseMessage.EnsureSuccessStatusCode()\r', '2025-05-01 20:28:38');

-- --------------------------------------------------------

--
-- Table structure for table `llaves`
--

CREATE TABLE `llaves` (
  `IdLlave` int(11) NOT NULL,
  `IdRancho` int(11) NOT NULL,
  `NombreLlave` varchar(60) NOT NULL,
  `SuperficieHA` decimal(18,2) NOT NULL,
  `SuperficieAcres` decimal(18,2) NOT NULL,
  `CantidadTuneles` int(11) NOT NULL,
  `Disponibilidad` varchar(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `llaves`
--

INSERT INTO `llaves` (`IdLlave`, `IdRancho`, `NombreLlave`, `SuperficieHA`, `SuperficieAcres`, `CantidadTuneles`, `Disponibilidad`) VALUES
(1, 4, '1', 2.00, 4.94, 3, 'No'),
(2, 4, '23', 32.00, 79.07, 3, 'Sí'),
(3, 1, '200', 22.00, 54.36, 22, 'Sí'),
(4, 2, '21', 8.50, 21.00, 12, 'Sí'),
(5, 4, '32', 2.00, 4.94, 234, 'Sí'),
(6, 1, 'Llave 4', 12.00, 29.65, 24, 'Sí'),
(7, 4, 'Llave 7', 12.00, 29.65, 23, 'Sí'),
(8, 6, 'A1', 0.20, 0.49, 13, 'Sí');

-- --------------------------------------------------------

--
-- Table structure for table `personal_cosecha`
--

CREATE TABLE `personal_cosecha` (
  `IdPersonalCosecha` int(11) NOT NULL,
  `IdCosecha` int(11) NOT NULL,
  `IdEmpleado` int(11) NOT NULL,
  `Jarras` int(11) NOT NULL,
  `PrecioJarra` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `personal_cosecha`
--

INSERT INTO `personal_cosecha` (`IdPersonalCosecha`, `IdCosecha`, `IdEmpleado`, `Jarras`, `PrecioJarra`) VALUES
(1, 1, 2, 1202, 9),
(2, 2, 5, 350, 8),
(3, 2, 5, 124, 8);

-- --------------------------------------------------------

--
-- Table structure for table `plantaciones`
--

CREATE TABLE `plantaciones` (
  `IdPlantacion` int(11) NOT NULL,
  `IdCultivo` int(11) NOT NULL,
  `IdVivero` int(11) NOT NULL,
  `IdLlave` int(11) NOT NULL,
  `CantidadPlantas` int(11) NOT NULL,
  `PlantasPorMetro` decimal(18,2) NOT NULL,
  `FechaPlantacion` date NOT NULL,
  `NumSemPlantacion` int(11) NOT NULL,
  `EstatusPlantacion` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `plantaciones`
--

INSERT INTO `plantaciones` (`IdPlantacion`, `IdCultivo`, `IdVivero`, `IdLlave`, `CantidadPlantas`, `PlantasPorMetro`, `FechaPlantacion`, `NumSemPlantacion`, `EstatusPlantacion`) VALUES
(1, 0, 0, 0, 0, 0.00, '2025-03-08', 0, 'string'),
(2, 2, 1, 3, 12, 1.00, '0001-01-01', 0, 'Inactiva'),
(3, 0, 0, 0, 0, 0.00, '2025-03-09', 0, 'string'),
(4, 4, 5, 2, 1000, 1.00, '2025-03-08', 10, 'Activa'),
(5, 8, 4, 6, 2333, 3.00, '2025-03-10', 11, 'Activa'),
(6, 2, 3, 4, 1000, 2.00, '2025-03-10', 11, 'Activa'),
(7, 4, 1, 5, 1000, 1.00, '2025-03-13', 11, 'Activa'),
(8, 3, 1, 3, 1000, 1.00, '2025-04-09', 15, 'Activa');

-- --------------------------------------------------------

--
-- Table structure for table `podas`
--

CREATE TABLE `podas` (
  `IdPoda` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `TipoPoda` varchar(20) NOT NULL,
  `FechaPoda` date NOT NULL,
  `NumSemPoda` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `podas`
--

INSERT INTO `podas` (`IdPoda`, `IdPlantacion`, `TipoPoda`, `FechaPoda`, `NumSemPoda`) VALUES
(2, 6, '12', '2025-03-11', 11),
(3, 4, '3', '2025-03-12', 11),
(4, 5, 'Podita', '2025-03-13', 11),
(5, 8, 'Poda completa', '2025-04-16', 16);

-- --------------------------------------------------------

--
-- Table structure for table `proceso`
--

CREATE TABLE `proceso` (
  `IdProceso` int(11) NOT NULL,
  `IdViaje` int(11) NOT NULL,
  `ClaseAkg` decimal(10,2) NOT NULL,
  `ClaseBkg` decimal(10,2) NOT NULL,
  `ClaseCkg` decimal(10,2) NOT NULL,
  `Rechazo` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `proceso`
--

INSERT INTO `proceso` (`IdProceso`, `IdViaje`, `ClaseAkg`, `ClaseBkg`, `ClaseCkg`, `Rechazo`) VALUES
(2, 3, 5.00, 2.00, 1.00, 2.00),
(8, 4, 0.10, 0.00, 0.00, 0.20);

-- --------------------------------------------------------

--
-- Table structure for table `produccion`
--

CREATE TABLE `produccion` (
  `IdProduccion` int(11) NOT NULL,
  `IdCosecha` int(11) NOT NULL,
  `TipoCaja` varchar(10) NOT NULL,
  `CantidadCajas` int(11) NOT NULL,
  `KilosProceso` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `produccion`
--

INSERT INTO `produccion` (`IdProduccion`, `IdCosecha`, `TipoCaja`, `CantidadCajas`, `KilosProceso`) VALUES
(1, 1, '6oz', 120, 10),
(2, 2, '10oz', 1000, 20),
(3, 3, '10oz', 222222, 12),
(4, 4, '12oz', 1000, 100),
(5, 5, '12oz', 100, 10);

-- --------------------------------------------------------

--
-- Table structure for table `ranchos`
--

CREATE TABLE `ranchos` (
  `IdRancho` int(11) NOT NULL,
  `NombreRancho` varchar(60) NOT NULL,
  `NumeroRancho` varchar(60) NOT NULL,
  `SuperficieHA` decimal(18,2) NOT NULL,
  `SuperficieAcres` decimal(18,2) NOT NULL,
  `Direccion` varchar(100) NOT NULL,
  `CP` varchar(5) NOT NULL,
  `Municipio` varchar(30) NOT NULL,
  `Estado` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ranchos`
--

INSERT INTO `ranchos` (`IdRancho`, `NombreRancho`, `NumeroRancho`, `SuperficieHA`, `SuperficieAcres`, `Direccion`, `CP`, `Municipio`, `Estado`) VALUES
(1, 'Rancho A', '001', 150.00, 370.66, 'Calle Real 123', '12345', 'Municipio A', 'Estado A'),
(2, 'Rancho B', '002', 200.00, 494.20, 'Avenida Central 456', '23456', 'Municipio B', 'Estado B'),
(3, 'Rancho C', '003', 300.00, 742.30, 'Calle 789', '34567', 'Municipio C', 'Estado C'),
(4, 'Rancho D', '004', 250.00, 617.30, 'Plaza Mayor 101', '45678', 'Municipio D', 'Estado D'),
(5, 'Rancho E', '005', 100.00, 247.10, 'Calle Larga 102', '56789', 'Municipio E', 'Estado E'),
(6, 'Rancho F', '006', 4.00, 9.88, 'Dirección #6', '60606', 'Municipio F', 'Durango'),
(7, '1', '1', 1.00, 2.47, '1', '12121', '1', 'Durango'),
(8, '12', '12', 1.00, 2.47, '1', '11111', '111', 'Guanajuato'),
(9, '56', '234a', 1.00, 2.47, '23', '23423', '23', 'Guerrero');

-- --------------------------------------------------------

--
-- Table structure for table `recepcion_viaje`
--

CREATE TABLE `recepcion_viaje` (
  `IdRecepcion` int(11) NOT NULL,
  `IdViaje` int(11) NOT NULL,
  `FechaRecepcion` date NOT NULL,
  `NumSemRecepcion` int(11) NOT NULL,
  `HoraRecepcion` time NOT NULL,
  `HoraInspeccion` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `replantes`
--

CREATE TABLE `replantes` (
  `IdReplante` int(11) NOT NULL,
  `IdPlantacion` int(11) NOT NULL,
  `IdCultivo` int(11) NOT NULL,
  `IdVivero` int(11) NOT NULL,
  `CantidadReplante` int(11) NOT NULL,
  `FechaReplante` date NOT NULL,
  `NumSemReplante` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `replantes`
--

INSERT INTO `replantes` (`IdReplante`, `IdPlantacion`, `IdCultivo`, `IdVivero`, `CantidadReplante`, `FechaReplante`, `NumSemReplante`) VALUES
(1, 2, 2, 1, 1000, '2025-03-12', 0),
(2, 5, 8, 4, 12, '2025-03-15', 11),
(3, 4, 4, 5, 32, '2025-03-12', 11),
(4, 4, 4, 5, 12132, '2025-03-19', 12),
(5, 6, 2, 3, 200, '2025-04-19', 16);

-- --------------------------------------------------------

--
-- Table structure for table `tarimas`
--

CREATE TABLE `tarimas` (
  `IdTarima` int(11) NOT NULL,
  `IdProduccion` int(11) NOT NULL,
  `IdViaje` int(11) NOT NULL,
  `CantidadCajasViaje` int(11) NOT NULL,
  `Licencia` int(11) NOT NULL,
  `KilosProcesoViaje` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tarimas`
--

INSERT INTO `tarimas` (`IdTarima`, `IdProduccion`, `IdViaje`, `CantidadCajasViaje`, `Licencia`, `KilosProcesoViaje`) VALUES
(1, 1, 1, 1, 1, 0.00),
(2, 1, 1, 2, 2, 0.00),
(3, 1, 1, 3, 3, 0.00),
(4, 2, 2, 140, 4, 0.00),
(5, 2, 2, 50, 5, 0.00),
(6, 4, 3, 0, 0, 10.00),
(7, 2, 4, 0, 0, 0.30),
(8, 2, 5, 120, 7, 0.00),
(9, 2, 5, 180, 8, 0.00),
(10, 4, 6, 90, 9, 0.00),
(11, 2, 7, 60, 10, 0.00),
(12, 2, 7, 40, 11, 0.00),
(13, 4, 8, 0, 0, 20.00),
(14, 5, 9, 0, 0, 10.00),
(15, 4, 10, 10, 10, 0.00),
(16, 4, 10, 11, 11, 0.00),
(17, 4, 10, 12, 12, 0.00),
(18, 4, 10, 13, 13, 0.00);

-- --------------------------------------------------------

--
-- Table structure for table `usuarios`
--

CREATE TABLE `usuarios` (
  `IdUsuario` int(11) NOT NULL,
  `Usuario` varchar(20) NOT NULL,
  `Contrasena` varchar(20) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Email` varchar(40) NOT NULL,
  `Rol` varchar(15) NOT NULL,
  `Estatus` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usuarios`
--

INSERT INTO `usuarios` (`IdUsuario`, `Usuario`, `Contrasena`, `Nombre`, `Telefono`, `Email`, `Rol`, `Estatus`) VALUES
(1, 'usuario1', 'password1', 'Juan Pérez', '1234567890', 'juanperez@mail.com', 'Admin', 'Inactivo'),
(2, 'usuario2', 'password2', 'Ana Gómez', '0987654321', 'anagomez@mail.com', 'Usuario', 'Activo'),
(3, 'usuario3', 'password3', 'Carlos Martínez', '1122334455', 'carlosmartinez@mail.com', 'Usuario', 'Activo'),
(4, 'usuario4', 'password4', 'Laura Díaz', '2233445566', 'lauradiaz@mail.com', 'Admin', 'Inactivo'),
(5, 'usuario5', 'password5', 'Pedro Sánchez', '3344556677', 'pedrosanchez@mail.com', 'Usuario', 'Activo'),
(6, 'usuario6', '123456', 'Carlos Ramírez Sánchez', '3411478762', 'carlosr@gmail.com', 'Admin', 'Activo'),
(7, '2', '3', '3', '3121122222', '3@g.com', 'Admin', 'Activo'),
(8, 'Juan', 'ewfes', 'ewfds', '1212232122', '3@g.com', 'Admin', 'Activo');

-- --------------------------------------------------------

--
-- Table structure for table `vehiculos`
--

CREATE TABLE `vehiculos` (
  `IdVehiculo` int(11) NOT NULL,
  `Placas` varchar(7) DEFAULT NULL,
  `Modelo` varchar(30) DEFAULT NULL,
  `Marca` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `vehiculos`
--

INSERT INTO `vehiculos` (`IdVehiculo`, `Placas`, `Modelo`, `Marca`) VALUES
(1, 'ABC123', 'Modelo A', 'Marca A'),
(2, 'DEF456', 'Modelo B', 'Marca B'),
(3, 'GHI789', 'Modelo C', 'Marca C'),
(4, 'JKL012', 'Modelo D', 'Marca D'),
(5, 'MNO345', 'Modelo E', 'Marca E'),
(6, 'AV456S', 'Hilux 2020', 'Toyota');

-- --------------------------------------------------------

--
-- Table structure for table `ventas`
--

CREATE TABLE `ventas` (
  `IdVenta` int(11) NOT NULL,
  `FechaFacturacion` date NOT NULL,
  `Total` decimal(10,0) NOT NULL,
  `PrecioDolar` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ventas`
--

INSERT INTO `ventas` (`IdVenta`, `FechaFacturacion`, `Total`, `PrecioDolar`) VALUES
(1, '0000-00-00', 6, 20),
(2, '2025-04-30', 6, 1),
(4, '2025-05-01', 31, 20),
(5, '2025-05-01', 127, 20),
(6, '2025-05-01', 40, 20);

-- --------------------------------------------------------

--
-- Table structure for table `ventasdetalles`
--

CREATE TABLE `ventasdetalles` (
  `IdVenta` int(11) NOT NULL,
  `IdTarima` int(11) NOT NULL,
  `PrecioVentaTarima` decimal(10,0) NOT NULL,
  `FechaRecepcion` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ventasdetalles`
--

INSERT INTO `ventasdetalles` (`IdVenta`, `IdTarima`, `PrecioVentaTarima`, `FechaRecepcion`) VALUES
(1, 1, 1, '2025-04-28'),
(1, 2, 2, '2025-04-28'),
(1, 3, 3, '2025-04-28'),
(2, 1, 1, '2025-04-30'),
(2, 2, 2, '2025-04-30'),
(2, 3, 3, '2025-04-30'),
(4, 6, 10, '2025-05-01'),
(4, 7, 1, '2025-05-01'),
(4, 13, 20, '2025-05-01'),
(5, 4, 12, '2025-05-01'),
(5, 5, 25, '2025-05-01'),
(5, 10, 90, '2025-05-01'),
(6, 8, 20, '2025-05-01'),
(6, 9, 20, '2025-05-01');

-- --------------------------------------------------------

--
-- Table structure for table `viajes`
--

CREATE TABLE `viajes` (
  `IdViaje` int(11) NOT NULL,
  `IdVehiculo` int(11) NOT NULL,
  `IdConductor` int(11) NOT NULL,
  `FechaSalida` date NOT NULL,
  `NumSemViaje` int(11) NOT NULL,
  `EstadoAprobacion` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `viajes`
--

INSERT INTO `viajes` (`IdViaje`, `IdVehiculo`, `IdConductor`, `FechaSalida`, `NumSemViaje`, `EstadoAprobacion`) VALUES
(1, 3, 4, '2025-03-22', 12, 'Aceptado'),
(2, 3, 1, '2025-03-29', 13, 'Pendiente'),
(3, 5, 4, '2025-04-30', 18, 'Pendiente'),
(4, 3, 2, '2025-03-29', 13, 'Aceptado'),
(5, 5, 2, '2025-04-24', 13, 'Pendiente'),
(6, 5, 4, '2025-04-30', 18, 'Pendiente'),
(7, 3, 4, '2025-04-24', 13, 'Rechazado'),
(8, 5, 4, '2025-04-25', 18, 'Aceptado'),
(9, 6, 3, '2025-04-26', 17, 'Rechazado'),
(10, 3, 2, '2025-04-30', 18, 'Aceptado');

-- --------------------------------------------------------

--
-- Table structure for table `viveros`
--

CREATE TABLE `viveros` (
  `IdVivero` int(11) NOT NULL,
  `NombreVivero` varchar(60) NOT NULL,
  `CodigoVivero` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `viveros`
--

INSERT INTO `viveros` (`IdVivero`, `NombreVivero`, `CodigoVivero`) VALUES
(1, 'Vivero A', 'VIV001'),
(2, 'Vivero B', 'VIV002'),
(3, 'Vivero C', 'VIV003'),
(4, 'Vivero D', 'VIV004'),
(5, 'Vivero E', 'VIV005'),
(6, 'Vivero 6', '666999');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `conductores`
--
ALTER TABLE `conductores`
  ADD PRIMARY KEY (`IdConductor`);

--
-- Indexes for table `cosechas`
--
ALTER TABLE `cosechas`
  ADD PRIMARY KEY (`IdCosecha`);

--
-- Indexes for table `cultivos`
--
ALTER TABLE `cultivos`
  ADD PRIMARY KEY (`IdCultivo`);

--
-- Indexes for table `empleados`
--
ALTER TABLE `empleados`
  ADD PRIMARY KEY (`IdEmpleado`);

--
-- Indexes for table `errorlogs`
--
ALTER TABLE `errorlogs`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `llaves`
--
ALTER TABLE `llaves`
  ADD PRIMARY KEY (`IdLlave`);

--
-- Indexes for table `personal_cosecha`
--
ALTER TABLE `personal_cosecha`
  ADD PRIMARY KEY (`IdPersonalCosecha`);

--
-- Indexes for table `plantaciones`
--
ALTER TABLE `plantaciones`
  ADD PRIMARY KEY (`IdPlantacion`);

--
-- Indexes for table `podas`
--
ALTER TABLE `podas`
  ADD PRIMARY KEY (`IdPoda`);

--
-- Indexes for table `proceso`
--
ALTER TABLE `proceso`
  ADD PRIMARY KEY (`IdProceso`);

--
-- Indexes for table `produccion`
--
ALTER TABLE `produccion`
  ADD PRIMARY KEY (`IdProduccion`);

--
-- Indexes for table `ranchos`
--
ALTER TABLE `ranchos`
  ADD PRIMARY KEY (`IdRancho`);

--
-- Indexes for table `recepcion_viaje`
--
ALTER TABLE `recepcion_viaje`
  ADD PRIMARY KEY (`IdRecepcion`);

--
-- Indexes for table `replantes`
--
ALTER TABLE `replantes`
  ADD PRIMARY KEY (`IdReplante`);

--
-- Indexes for table `tarimas`
--
ALTER TABLE `tarimas`
  ADD PRIMARY KEY (`IdTarima`);

--
-- Indexes for table `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`IdUsuario`);

--
-- Indexes for table `vehiculos`
--
ALTER TABLE `vehiculos`
  ADD PRIMARY KEY (`IdVehiculo`);

--
-- Indexes for table `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`IdVenta`);

--
-- Indexes for table `viajes`
--
ALTER TABLE `viajes`
  ADD PRIMARY KEY (`IdViaje`);

--
-- Indexes for table `viveros`
--
ALTER TABLE `viveros`
  ADD PRIMARY KEY (`IdVivero`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `conductores`
--
ALTER TABLE `conductores`
  MODIFY `IdConductor` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cosechas`
--
ALTER TABLE `cosechas`
  MODIFY `IdCosecha` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `cultivos`
--
ALTER TABLE `cultivos`
  MODIFY `IdCultivo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `empleados`
--
ALTER TABLE `empleados`
  MODIFY `IdEmpleado` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `errorlogs`
--
ALTER TABLE `errorlogs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `llaves`
--
ALTER TABLE `llaves`
  MODIFY `IdLlave` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `personal_cosecha`
--
ALTER TABLE `personal_cosecha`
  MODIFY `IdPersonalCosecha` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `plantaciones`
--
ALTER TABLE `plantaciones`
  MODIFY `IdPlantacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `podas`
--
ALTER TABLE `podas`
  MODIFY `IdPoda` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `proceso`
--
ALTER TABLE `proceso`
  MODIFY `IdProceso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `produccion`
--
ALTER TABLE `produccion`
  MODIFY `IdProduccion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `ranchos`
--
ALTER TABLE `ranchos`
  MODIFY `IdRancho` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `recepcion_viaje`
--
ALTER TABLE `recepcion_viaje`
  MODIFY `IdRecepcion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `replantes`
--
ALTER TABLE `replantes`
  MODIFY `IdReplante` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `tarimas`
--
ALTER TABLE `tarimas`
  MODIFY `IdTarima` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IdUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `vehiculos`
--
ALTER TABLE `vehiculos`
  MODIFY `IdVehiculo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `ventas`
--
ALTER TABLE `ventas`
  MODIFY `IdVenta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `viajes`
--
ALTER TABLE `viajes`
  MODIFY `IdViaje` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `viveros`
--
ALTER TABLE `viveros`
  MODIFY `IdVivero` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
