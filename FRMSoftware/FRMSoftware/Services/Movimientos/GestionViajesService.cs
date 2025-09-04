using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionViajesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionViajesService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Viajes"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Viajes"; // Para el emulador de Android
            #endif
        }

        // Obtener todos los viajes
        public async Task<List<ViajesDto>> GetViajesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ViajesDto>>(_baseUrl);
        }

        // Obtener un viaje por ID
        public async Task<ViajesDto> GetViajePorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ViajesDto>($"{_baseUrl}/{id}");
        }

        // Obtener todas las tarimas
        public async Task<List<TarimasDto>> GetTarimasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TarimasDto>>($"{_baseUrl}/Tarimas");
        }

        public async Task<TarimasDto> GetTarimasPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TarimasDto>($"{_baseUrl}/GetTarimasPorId/{id}");
        }

        public async Task<TarimasDto> GetTarimaPorIdViajeAsync(int idViaje)
        {
            return await _httpClient.GetFromJsonAsync<TarimasDto>($"{_baseUrl}/GetTarimaPorIdViaje/{idViaje}");
        }

        // Crear un nuevo viaje
        public async Task<(bool Success, string Message, int IdViaje)> CreateViajeAsync(ViajesDto viaje)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, viaje);

                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta y extraer el ID generado
                    var viajeCreado = await response.Content.ReadFromJsonAsync<ViajesDto>();

                    return (true, "Viaje registrado exitosamente.", viajeCreado?.IdViaje ?? 0);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar viaje: {errorMessage}", 0);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}", 0);
            }
        }

        // Crear una nueva tarima
        public async Task<(bool Success, string Message)> CreateTarimaAsync(TarimasDto tarima)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Tarimas", tarima);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Tarima registrada exitosamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar tarima: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Crear múltiples tarimas (Lote)
        public async Task<(bool Success, string Message)> CreateTarimasLoteAsync(List<TarimasDto> tarimas)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Tarimas/Lote", tarimas);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Tarimas registradas exitosamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar tarimas: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }


        // Obtener todos los viajes
        public async Task<List<ViajesDetallesDto>> GetViajesDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ViajesDetallesDto>>($"{_baseUrl}/GetViajesDetalles");
        }

        // Obtener un viaje por ID
        public async Task<ViajesDetallesDto> GetViajeDetallesPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ViajesDetallesDto>($"{_baseUrl}/GetViajeDetalles/{id}");
        }


        // Actualizar un viaje existente
        public async Task<(bool Success, string Message)> UpdateViajeAsync(ViajesDto viaje)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/ActualizarViajes/{viaje.IdViaje}", viaje);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Viaje actualizado correctamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al actualizar el viaje: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Actualizar una tarima existente
        public async Task<(bool Success, string Message)> UpdateTarimaAsync(TarimasDto tarima)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/ActualizarTarima/{tarima.IdTarima}", tarima);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Tarima actualizada correctamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al actualizar la tarima: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Actualizar múltiples tarimas (Lote)
        public async Task<(bool Success, string Message)> UpdateTarimasLoteAsync(List<TarimasDto> tarimas)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/ActualizarTarimas/Lote", tarimas);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Tarimas actualizadas correctamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al actualizar las tarimas: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }


        // Eliminar un viaje
        public async Task<bool> DeleteViajeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
