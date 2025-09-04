using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionPodasService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionPodasService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Podas"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Podas"; // Para el emulador de Android
            #endif
        }

        // Método para obtener todas las podas
        public async Task<List<PodasDto>> GetPodasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PodasDto>>(_baseUrl);
        }

        // Método para obtener una poda por su ID
        public async Task<PodasDto> GetPodaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PodasDto>($"{_baseUrl}/{id}");
        }

        // Método para obtener todos los detalles de las podas
        public async Task<List<PodasDetallesDto>> GetPodasDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PodasDetallesDto>>($"{_baseUrl}/GetPodasDetalles");
        }

        // Método para obtener los detalles de una poda por su ID
        public async Task<PodasDetallesDto> GetPodaDetallesPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PodasDetallesDto>($"{_baseUrl}/GetPodasDetalles/{id}");
        }

        // Método para crear una nueva poda
        public async Task<(bool Success, string Message)> CreatePodaAsync(PodasDto poda)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, poda);

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

        // Método para actualizar una poda
        public async Task<bool> UpdatePodaAsync(int id, PodasDto poda)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", poda);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar una poda
        public async Task<bool> DeletePodaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
