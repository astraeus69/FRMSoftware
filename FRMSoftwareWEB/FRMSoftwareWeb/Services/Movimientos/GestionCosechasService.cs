using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionCosechasService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionCosechasService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/Cosechas";

        }

        // Método para obtener todas las producciones
        public async Task<List<ProduccionDto>> GetProduccionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProduccionDto>>(_baseUrl + "/producciones");
        }

        // Método para obtener una producción por ID
        public async Task<ProduccionDto> GetProduccionPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProduccionDto>($"{_baseUrl}/producciones/{id}");
        }

        // Método para obtener todas las cosechas
        public async Task<List<CosechasDto>> GetCosechasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CosechasDto>>(_baseUrl + "/cosechas");
        }

        // Método para obtener una cosecha por ID
        public async Task<CosechasDto> GetCosechaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CosechasDto>($"{_baseUrl}/cosechas/{id}");
        }

        // Método para obtener todas las cosechas con producción
        public async Task<List<CosechasProduccionDto>> GetCosechasProduccionAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CosechasProduccionDto>>($"{_baseUrl}/GetCosechasProduccion");
        }

        // Método para obtener una cosecha con producción por su ID
        public async Task<CosechasProduccionDto> GetCosechaProduccionPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CosechasProduccionDto>($"{_baseUrl}/GetCosechasProduccion/{id}");
        }

        // Método para crear una nueva producción
        public async Task<(bool Success, string Message)> CreateProduccionAsync(ProduccionDto produccion)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/produccion", produccion);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Producción registrada exitosamente.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar producción: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Método para crear una nueva cosecha
        public async Task<(bool Success, string Message, int IdCosecha)> CreateCosechaAsync(CosechasDto cosecha)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/cosechas", cosecha);
                if (response.IsSuccessStatusCode)
                {
                    // 🔹 Obtener la respuesta JSON y deserializar el IdCosecha
                    var createdCosecha = await response.Content.ReadFromJsonAsync<CosechasDto>();
                    return (true, "Cosecha registrada exitosamente.", createdCosecha.IdCosecha);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar cosecha: {errorMessage}", 0);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}", 0);
            }
        }

        // Método para actualizar una producción
        public async Task<bool> UpdateProduccionAsync(int id, ProduccionDto produccion)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/produccion/{id}", produccion);
            return response.IsSuccessStatusCode;
        }

        // Método para actualizar una cosecha
        public async Task<bool> UpdateCosechaAsync(int id, CosechasDto cosecha)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/cosechas/{id}", cosecha);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar todas las producciones por idCosecha
        public async Task<bool> DeleteProduccionesPorCosechaAsync(int idCosecha)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/produccion/porcosecha/{idCosecha}");
            return response.IsSuccessStatusCode;
        }


        // Método para eliminar una cosecha
        public async Task<bool> DeleteCosechaAsync(int idCosecha)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/cosechas/{idCosecha}");
            return response.IsSuccessStatusCode;
        }
    }
}
