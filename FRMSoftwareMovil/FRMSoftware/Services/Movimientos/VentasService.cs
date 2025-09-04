using System.Net.Http.Json;
using FRMSoftware.Data; // Ajusta si tus DTOs están en otro namespace

namespace FRMSoftware.Services.Movimientos
{
    public class VentasService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VentasService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Ventas"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Ventas"; // Para emulador Android
            #endif
        }

        // Obtener todas las ventas
        public async Task<List<VentasDto>> GetVentasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VentasDto>>(_baseUrl);
        }

        // Obtener una venta por ID
        public async Task<VentasDto> GetVentaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VentasDto>($"{_baseUrl}/{id}");
        }

        // Obtener ID de viajes con ventas
        public async Task<List<int>> GetIdsViajesConVentasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>($"{_baseUrl}/GetIdsViajesConVentas");
        }

        // Obtener todas las ventas por viaje
        public async Task<List<VentasDetallesCompletosDto>> GetVentasDCAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VentasDetallesCompletosDto>>($"{_baseUrl}/GetVentasDC");
        }

        // Obtener ventas por ID de viaje
        public async Task<List<VentasDetallesCompletosDto>> GetVentasPorIdAsync(int idVenta)
        {
            return await _httpClient.GetFromJsonAsync<List<VentasDetallesCompletosDto>>($"{_baseUrl}/GetVentasPorId/{idVenta}");
        }


        // Crear una nueva venta
        public async Task<(bool Success, string Message, int IdVenta)> CreateVentaAsync(VentasDto venta)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, venta);
                if (response.IsSuccessStatusCode)
                {
                    var ventaCreada = await response.Content.ReadFromJsonAsync<VentasDto>();
                    return (true, "Venta registrada exitosamente.", ventaCreada?.IdVenta ?? 0);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar la venta: {errorMessage}", 0);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}", 0);
            }
        }

        // Eliminar una venta
        public async Task<bool> DeleteVentaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        // Obtener todos los detalles de venta
        public async Task<List<VentasDetallesDto>> GetVentasDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VentasDetallesDto>>($"{_baseUrl}/Detalles");
        }

        // Obtener detalle de venta por ID de venta
        public async Task<List<VentasDetallesDto>> GetVentasDetallesPorVentaIdAsync(int idVenta)
        {
            return await _httpClient.GetFromJsonAsync<List<VentasDetallesDto>>($"{_baseUrl}/Detalles/{idVenta}");
        }

        // Crear un detalle de venta
        public async Task<(bool Success, string Message)> CreateVentaDetalleAsync(List<VentasDetallesDto> detalle)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/VentasDetalles/Lote", detalle);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Detalle de venta registrado exitosamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar el detalle de venta: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Eliminar un detalle de venta
        public async Task<bool> DeleteVentaDetalleAsync(int idVenta, int idTarima)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/Detalles/{idVenta}/{idTarima}");
            return response.IsSuccessStatusCode;
        }
    }
}
