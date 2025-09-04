using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using FRMSoftware.Data;
using System.Net.Http.Json;

namespace FRMSoftware.Services.Catalogos
{
    public class CultivoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CultivoService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Cultivos"; // Para Windows
            #elif ANDROID
                        _baseUrl = "http://10.0.2.2:5159/api/Cultivos"; // Para el emulador de Android
            #endif
        }

        public async Task<List<CultivosDto>> GetCultivosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CultivosDto>>(_baseUrl);
        }
        
        // Método para obtener un cultivo por su ID
        public async Task<CultivosDto> GetCultivos(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CultivosDto>($"{_baseUrl}/{id}");
                return response; // Retorna el cultivo con el id solicitado
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el cultivo: {ex.Message}");
                return null; // En caso de error, se retorna null
            }
        }


        public async Task<bool> ExisteCultivoAsync(string tipoBerry, string variedad)
        {
            try
            {
                var tipoBerryEscaped = Uri.EscapeDataString(tipoBerry ?? string.Empty);
                var variedadEscaped = Uri.EscapeDataString(variedad ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteCultivo?tipoBerry={tipoBerryEscaped}&variedad={variedadEscaped}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteCultivoAsync: {ex.Message}");
                return false;
            }
        }


        public async Task<(bool Success, string Message)> CreateCultivoAsync(CultivosDto cultivo)
        {
            try
            {
                // Verificar si ya existe un registro duplicado
                var existe = await ExisteCultivoAsync(cultivo.TipoBerry, cultivo.Variedad);

                if (existe)
                {
                    return (false, "Ya existe un registro con el mismo tipo de berry y variedad.");
                }

                // Si no existe, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, cultivo);

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

        public async Task<bool> UpdateCultivoAsync(int id, CultivosDto cultivo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", cultivo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCultivoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
