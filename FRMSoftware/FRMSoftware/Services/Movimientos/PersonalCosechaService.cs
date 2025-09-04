using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionPersonalCosechaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionPersonalCosechaService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/PersonalCosecha"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/PersonalCosecha"; // Para el emulador de Android
            #endif
        }

        // Método para obtener todos los registros de personal de cosecha
        public async Task<List<PersonalCosechaDto>> GetPersonalCosechaAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonalCosechaDto>>(_baseUrl);
        }

        // Método para obtener un personal de cosecha por su ID
        public async Task<PersonalCosechaDto> GetPersonalCosechaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonalCosechaDto>($"{_baseUrl}/{id}");
        }

        // Método para obtener todos los detalles del personal de cosecha
        public async Task<List<PersonalCosechaDetallesDto>> GetPersonalCosechaDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonalCosechaDetallesDto>>($"{_baseUrl}/GetPersonalCosechaDetalles");
        }

        // Método para obtener los detalles de un personal de cosecha por su ID
        public async Task<PersonalCosechaDetallesDto> GetPersonalCosechaDetallesPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonalCosechaDetallesDto>($"{_baseUrl}/GetPersonalCosechaDetalles/{id}");
        }

        // Método para crear un nuevo registro de personal de cosecha
        public async Task<(bool Success, string Message)> CreatePersonalCosechaAsync(PersonalCosechaDto personalCosecha)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, personalCosecha);

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

        // Método para actualizar un registro de personal de cosecha
        public async Task<bool> UpdatePersonalCosechaAsync(int id, PersonalCosechaDto personalCosecha)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", personalCosecha);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar un registro de personal de cosecha
        public async Task<bool> DeletePersonalCosechaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
