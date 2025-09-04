using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionReplantesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionReplantesService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Replantes"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Replantes"; // Para el emulador de Android
            #endif
        }

        // Método para obtener todos los replantes
        public async Task<List<ReplantesDto>> GetReplantesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReplantesDto>>(_baseUrl);
        }

        // Método para obtener un replante por su ID
        public async Task<ReplantesDto> GetReplantePorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ReplantesDto>($"{_baseUrl}/{id}");
        }

        // Método para obtener todos los detalles de los replantes
        public async Task<List<ReplantesDetallesDto>> GetReplantesDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReplantesDetallesDto>>($"{_baseUrl}/GetReplantesDetalles");
        }

        // Método para obtener los detalles de un replante por su ID
        public async Task<ReplantesDetallesDto> GetReplanteDetallesPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ReplantesDetallesDto>($"{_baseUrl}/GetReplantesDetalles/{id}");
        }



        // Método para crear un nuevo replante
        public async Task<(bool Success, string Message)> CreateReplanteAsync(ReplantesDto replante)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, replante);

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

        // Método para actualizar un replante
        public async Task<bool> UpdateReplanteAsync(int id, ReplantesDto replante)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", replante);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar un replante
        public async Task<bool> DeleteReplanteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
