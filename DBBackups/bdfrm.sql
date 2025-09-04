-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 18, 2025 at 05:16 PM
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
(7, 5, '2025-05-09', 19),
(8, 4, '2025-05-16', 20);

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
(8, 'Arándano', 'Variedad X'),
(9, 'Fresa', 'Sweet Mary');

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
(4, 'Laura Díaz', 'Producción', 'Supervisor', 'Plaza Z 101', 'Colonia D', '45678', 'Municipio D', 'Estado D', '2233445566', 'Activo'),
(5, 'Pedro Sánchez', 'Recursos Humanos', 'Jefe', 'Calle W 102', 'Colonia E', '56789', 'Municipio E', 'Estado E', '3344556677', 'Activo'),
(6, 'Vicente Fernández', 'Campo', 'Cosechador', 'Dirección #6', 'Colonia 6', '66666', 'Municipio 6', 'Coahuila', '3411778674', 'Activo'),
(7, 'Pedro Salaz', 'Producción', 'Cosechador', 'Dir 100', 'Col 100', '11245', 'Mun 100', 'Ciudad de México', '1212232122', 'Activo'),
(8, 'Carlos González', 'Producción', 'Cosechador', 'Federico del Toro #347', 'Centro', '49000', 'Zapotlán El Grande', 'Jalisco', '3411476769', 'Activo');

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
(20, 'Usuario', 'Response status code does not indicate success: 500 (Internal Server Error).', 'ConsultarViajesDetalles', '   at System.Net.Http.HttpResponseMessage.EnsureSuccessStatusCode()\r', '2025-05-01 20:28:38'),
(21, '', 'Error de prueba generado manualmente', 'ConsultarUsuarios', '256', '2025-05-14 21:51:09'),
(22, '', 'Error de prueba generado manualmente', 'ConsultarUsuarios', '256', '2025-05-14 22:08:35'),
(23, 'joel', 'Error de prueba generado manualmente', 'ConsultarUsuarios', '256', '2025-05-14 22:10:33'),
(24, 'joel', 'Error de prueba generado manualmente', 'ConsultarUsuarios', '256', '2025-05-14 22:13:23'),
(25, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'OnInitializedAsync', '121', '2025-05-15 00:31:25'),
(26, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'OnInitializedAsync', '121', '2025-05-15 00:32:53'),
(27, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '995', '2025-05-15 01:08:23'),
(28, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '889', '2025-05-15 01:08:29'),
(29, 'pancho', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 16:36:52'),
(30, 'pancho', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 16:36:52'),
(31, 'pancho', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '900', '2025-05-15 16:37:35'),
(32, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 18:40:58'),
(33, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 18:40:59'),
(34, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 19:08:07'),
(35, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:08:07'),
(36, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '900', '2025-05-15 19:12:11'),
(37, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '807', '2025-05-15 19:12:22'),
(38, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantaciones', '1007', '2025-05-15 19:12:32'),
(39, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 19:12:36'),
(40, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:12:36'),
(41, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '169', '2025-05-15 19:12:58'),
(42, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 19:12:59'),
(43, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:12:59'),
(44, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 19:13:17'),
(45, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:13:17'),
(46, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ConsultarPlantacion', '675', '2025-05-15 19:13:44'),
(47, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:13:44'),
(48, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:27:33'),
(49, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'SeleccionarLlave', '521', '2025-05-15 19:30:16'),
(50, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'SeleccionarLlave', '521', '2025-05-15 19:30:31'),
(51, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'SeleccionarLlave', '521', '2025-05-15 19:30:33'),
(52, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'SeleccionarLlave', '521', '2025-05-15 19:30:40'),
(53, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:32:28'),
(54, 'joel', 'An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.', 'ObtenerRanchosDesdeLlaves', '1200', '2025-05-15 19:32:35'),
(55, 'joel', 'Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\nError: Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\n    at https://0.0.0.1/_framework/blazor.webview.js:1:375\n    at Array.forEach (<anonymous>)\n    at l.findFunction (https://0.0.0.1/_framework/blazor.webview.js:1:343)\n    at E (https://0.0.0.1/_framework/blazor.webview.js:1:5092)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:2885\n    at new Promise (<anonymous>)\n    at y.beginInvokeJSFromDotNet (https://0.0.0.1/_framework/blazor.webview.js:1:2848)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:47076\n    at EventTarget.<anonymous> (<anonymous>:7:62)\n    at EmbeddedBrowserWebView.<anonymous> (<anonymous>:1:29616)', 'DescargarPDF - GestionCosechas', '271', '2025-05-17 06:09:12'),
(56, 'joel', 'Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\nError: Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\n    at https://0.0.0.1/_framework/blazor.webview.js:1:375\n    at Array.forEach (<anonymous>)\n    at l.findFunction (https://0.0.0.1/_framework/blazor.webview.js:1:343)\n    at E (https://0.0.0.1/_framework/blazor.webview.js:1:5092)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:2885\n    at new Promise (<anonymous>)\n    at y.beginInvokeJSFromDotNet (https://0.0.0.1/_framework/blazor.webview.js:1:2848)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:47076\n    at EventTarget.<anonymous> (<anonymous>:7:62)\n    at EmbeddedBrowserWebView.<anonymous> (<anonymous>:1:29616)', 'DescargarPDF - GestionCosechas', '271', '2025-05-17 06:09:14'),
(57, 'joel', 'Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\nError: Could not find \'descargarPDF_GestionCosechas\' (\'descargarPDF_GestionCosechas\' was undefined).\n    at https://0.0.0.1/_framework/blazor.webview.js:1:375\n    at Array.forEach (<anonymous>)\n    at l.findFunction (https://0.0.0.1/_framework/blazor.webview.js:1:343)\n    at E (https://0.0.0.1/_framework/blazor.webview.js:1:5092)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:2885\n    at new Promise (<anonymous>)\n    at y.beginInvokeJSFromDotNet (https://0.0.0.1/_framework/blazor.webview.js:1:2848)\n    at https://0.0.0.1/_framework/blazor.webview.js:1:47076\n    at EventTarget.<anonymous> (<anonymous>:7:62)\n    at EmbeddedBrowserWebView.<anonymous> (<anonymous>:1:29616)', 'DescargarPDF - GestionCosechas', '271', '2025-05-17 06:09:20'),
(58, 'joel', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1123', '2025-05-18 04:15:55'),
(59, 'joel', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1123', '2025-05-18 04:15:58'),
(60, 'joel', 'Response status code does not indicate success: 404 (Not Found).', 'SeleccionarViaje', '1149', '2025-05-18 04:53:57');

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
(8, 6, 'A1', 0.20, 0.49, 13, 'Sí'),
(9, 9, 'Llave prueba', 0.50, 1.24, 10, 'Sí'),
(10, 1, 'Llave prueba', 1.00, 2.47, 10, 'Sí');

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
(4, 7, 1, 100, 10);

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
(2, 2, 1, 3, 12, 1.00, '0001-01-01', 0, 'Inactiva'),
(4, 4, 5, 2, 1000, 1.00, '2025-03-08', 10, 'Activa'),
(5, 8, 4, 6, 2333, 3.00, '2025-03-10', 11, 'Activa'),
(6, 2, 3, 4, 1000, 2.00, '2025-03-10', 11, 'Activa'),
(7, 4, 1, 5, 1000, 1.00, '2025-03-13', 11, 'Activa'),
(8, 3, 1, 3, 1000, 1.00, '2025-04-09', 15, 'Activa'),
(9, 1, 3, 10, 1000, 1.00, '2025-05-06', 19, 'Activa');

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
(6, 4, 'Green cut back', '2025-05-22', 21);

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
(9, 12, 1.00, 1.00, 0.00, 0.00);

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
(7, 7, '6oz', 12, 12),
(8, 8, '10oz', 12, 12);

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
(7, 'Rancho Pérez', 'P100', 1.00, 2.47, 'Direccion P', '12000', 'Allende', 'Durango'),
(8, 'Rancho González', 'G200', 1.00, 2.47, 'Direccion G', '50000', 'Guanajuato', 'Guanajuato'),
(9, 'Rancho Peña', 'P340', 1.00, 2.47, 'Peña 34', '34000', 'Acapulco', 'Guerrero'),
(10, 'Rancho H', 'H200', 1.00, 2.47, 'Direccion H', '40000', 'Zapotlán El Grande', 'Jalisco');

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
(6, 4, 4, 5, 100, '2025-05-23', 21);

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
(4, 2, 2, 140, 4, 0.00),
(5, 2, 2, 50, 5, 0.00),
(8, 2, 5, 120, 7, 0.00),
(9, 2, 5, 180, 8, 0.00),
(10, 4, 6, 90, 9, 0.00),
(19, 7, 11, 1, 1242343, 0.00),
(20, 7, 11, 2, 3453532, 0.00),
(21, 7, 12, 0, 0, 2.00),
(22, 8, 13, 2, 22300, 0.00),
(23, 8, 14, 2, 34500, 0.00),
(24, 8, 15, 3, 5006, 0.00),
(25, 8, 16, 2, 2007, 0.00),
(26, 8, 16, 1, 2009, 0.00),
(27, 7, 17, 0, 0, 2.00),
(28, 8, 18, 0, 0, 4.00),
(29, 7, 19, 0, 0, 2.00);

-- --------------------------------------------------------

--
-- Table structure for table `usuarios`
--

CREATE TABLE `usuarios` (
  `IdUsuario` int(11) NOT NULL,
  `Usuario` varchar(20) NOT NULL,
  `Contrasena` varchar(60) NOT NULL,
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
(2, 'usuario2', '$2a$11$lcQAg.WdiiD/DzQDKU3nBOGH1ToP84pYcMkzCMbXtAoOeF9ebn.wG', 'Ana Gómez', '0987654321', 'anagomez@mail.com', 'Editor', 'Activo'),
(3, 'usuario3', 'password3', 'Carlos Martínez', '1122334455', 'carlosmartinez@mail.com', 'Usuario', 'Activo'),
(5, 'usuario5', 'password5', 'Pedro Sánchez', '3344556677', 'pedrosanchez@mail.com', 'Usuario', 'Activo'),
(6, 'usuario6', '123456', 'Carlos Ramírez Sánchez', '3411478762', 'carlosr@gmail.com', 'Admin', 'Activo'),
(7, '2', '3', '3', '3121122222', '3@g.com', 'Admin', 'Activo'),
(8, 'Juan', 'ewfes', 'ewfds', '1212232122', '3@g.com', 'Admin', 'Activo'),
(9, 'juan', '$2a$11$xL/WA5wqZBmh0', 'juan', '3411478766', 'so@gmail.com', 'admin', 'Activo'),
(10, 'pedro', '$2a$11$e.jb7uHuLMd6H', 'pedro', '3411478766', 'so@gmail.com', 'admin', 'Activo'),
(11, 'pedro', '$2a$11$sFRydepRY4zYS', 'pedro', '3411478766', 'pedro@gmail.com', 'admin', 'Activo'),
(12, 'pedro', '$2a$11$hk2gCo9XyM0HOWXlvjUJeuaZWCnDfI1kJxTVZSvwaThaC0F5rCkrO', 'pedro', '3411478766', 'pedro@gmail.com', 'admin', 'Activo'),
(13, 'pedro', '$2a$11$apjzYiC7xTXh5w.BEtDSIeY69qL3JKIrSeXUK.r8HDgisFk0yyQza', 'pedro', '3411478766', 'pedro@gmail.com', 'admin', 'Activo'),
(14, 'joel', '$2a$11$3dlgYYSEXGuLzsoaegDb0uRDnrPaWKYFgwqEibyD/6adGNCe8qidu', 'Joel González', '3411476766', 'joel@gmail.com', 'Admin', 'Activo'),
(15, 'pancho', '$2a$11$nKeaLc9P08wtJDND0PBXIebwwAndSNFPZd.qgx278ei7K5WJ2yjRS', 'Francisco Pérez', '3411456787', 'pancho@gmail.com', 'Consultor', 'Activo'),
(16, 'panchoa', '$2a$11$c4FFQBnKXdqYHVzgTgFL0uAEwCLqd7wzGXUVuHN3zch7D/rEYgVfy', 'Francisco Ochoa', '3411456787', 'pancho@gmail.com', 'Editor', 'Activo'),
(17, 'jc', '$2a$11$3zyIXr3fAeO.GeTUTwaHE.QwATTfXE/hqY7hON5.eiRVPVeP6gDlK', 'Juan Mendoza', '3414565767', 'jc@gmail.com', 'Admin', 'Activo'),
(18, 'Carlos', '$2a$11$gtnuFGai19AVFZ4ubIMTLO9nMxA/TPU2TFbqyc8C90NiMx41GqEcy', 'Carlos Mendoza', '3411476769', 'carlos@gmail.com', 'Editor', 'Activo');

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
(6, 'AV456S', 'Hilux 2020', 'Toyota'),
(7, 'AVX2035', 'L200 2020', 'Mitsubishi');

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
(8, '2025-05-17', 20, 20),
(9, '2025-05-17', 10, 20),
(10, '2025-05-18', 10, 20),
(11, '2025-05-18', 20, 20),
(12, '2025-05-30', 10, 20);

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
(8, 19, 10, '2025-05-17'),
(8, 20, 10, '2025-05-17'),
(9, 21, 10, '2025-05-17'),
(10, 23, 10, '2025-05-18'),
(11, 25, 10, '2025-05-18'),
(11, 26, 10, '2025-05-18'),
(12, 22, 10, '2025-05-18');

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
(11, 3, 2, '2025-05-17', 19, 'Pendiente'),
(12, 2, 5, '2025-05-17', 19, 'Pendiente'),
(13, 4, 4, '2025-05-18', 20, 'Aceptado'),
(14, 4, 2, '2025-05-18', 20, 'Aceptado'),
(15, 6, 4, '2025-05-18', 20, 'Rechazado'),
(16, 1, 6, '2025-05-18', 20, 'Aceptado'),
(17, 4, 4, '2025-05-18', 19, 'Pendiente'),
(18, 2, 2, '2025-05-18', 20, 'Aceptado'),
(19, 4, 1, '2025-05-18', 19, 'Rechazado');

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
(6, 'Vivero 6', '666999'),
(7, 'Vivero H', 'HS1256');

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
  MODIFY `IdCosecha` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `cultivos`
--
ALTER TABLE `cultivos`
  MODIFY `IdCultivo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `empleados`
--
ALTER TABLE `empleados`
  MODIFY `IdEmpleado` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `errorlogs`
--
ALTER TABLE `errorlogs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT for table `llaves`
--
ALTER TABLE `llaves`
  MODIFY `IdLlave` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `personal_cosecha`
--
ALTER TABLE `personal_cosecha`
  MODIFY `IdPersonalCosecha` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `plantaciones`
--
ALTER TABLE `plantaciones`
  MODIFY `IdPlantacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `podas`
--
ALTER TABLE `podas`
  MODIFY `IdPoda` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `proceso`
--
ALTER TABLE `proceso`
  MODIFY `IdProceso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `produccion`
--
ALTER TABLE `produccion`
  MODIFY `IdProduccion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `ranchos`
--
ALTER TABLE `ranchos`
  MODIFY `IdRancho` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `replantes`
--
ALTER TABLE `replantes`
  MODIFY `IdReplante` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `tarimas`
--
ALTER TABLE `tarimas`
  MODIFY `IdTarima` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IdUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `vehiculos`
--
ALTER TABLE `vehiculos`
  MODIFY `IdVehiculo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `ventas`
--
ALTER TABLE `ventas`
  MODIFY `IdVenta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `viajes`
--
ALTER TABLE `viajes`
  MODIFY `IdViaje` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `viveros`
--
ALTER TABLE `viveros`
  MODIFY `IdVivero` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
