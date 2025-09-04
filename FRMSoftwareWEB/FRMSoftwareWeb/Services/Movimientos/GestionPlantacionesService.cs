using System.Net.Http.Json;
using FRMSoftware.Data;
using FRMSoftware.Services.Catalogos;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionPlantacionesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionPlantacionesService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/Plantaciones";

        }


        // Método para consultar todas las plantaciones
        public async Task<List<PlantacionesDto>> GetPlantacionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PlantacionesDto>>(_baseUrl);
        }

        // Método para obtener todas las plantaciones con los datos completos
        public async Task<List<PlantacionesDetallesDto>> GetPlantacionesDetalles()
        {
            // Primero, se obtienen las plantaciones
            return await _httpClient.GetFromJsonAsync<List<PlantacionesDetallesDto>>($"{_baseUrl}/GetPlantacionesDetalles");

        }

        // Método para obtener una plantación por su ID y completar los datos relacionados
        public async Task<PlantacionesDetallesDto> GetPlantacionPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PlantacionesDetallesDto>($"{_baseUrl}/{id}");

        }

        // Método para crear una nueva plantación
        public async Task<(bool Success, string Message)> CreatePlantacionAsync(PlantacionesDto plantacion)
        {
            try
            {
                // Verificar si ya existe una plantación con la misma llave y estatus
                var plantacionesExistentes = await GetPlantacionesAsync();
                if (plantacionesExistentes.Any(p => p.IdLlave == plantacion.IdLlave && p.EstatusPlantacion == "Activa"))
                {
                    return (false, "Ya existe una plantación activa con la misma llave.");
                }


                // Si no existe, realizamos el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, plantacion);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registro exitoso.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        // Método para actualizar una plantación
        public async Task<bool> UpdatePlantacionAsync(int id, PlantacionesDto plantacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", plantacion);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar una plantación
        public async Task<bool> DeletePlantacionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
