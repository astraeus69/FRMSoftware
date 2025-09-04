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
    public class RanchosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RanchosService(HttpClient httpClient)
        {
            _httpClient = httpClient;


            #if WINDOWS || IOS || MAC
                        _baseUrl = "http://localhost:5159/api/Ranchos"; // Para Windows

            #elif ANDROID
                        _baseUrl = "http://10.0.2.2:5159/api/Ranchos"; // Para el emulador de Android
            #endif
        }

        public async Task<List<RanchosDto>> GetRanchosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RanchosDto>>(_baseUrl);
        }


        // Método para obtener un rancho por su Id
        public async Task<RanchosDto> GetRancho(int idRancho)
        {
            try
            {
                // Realizar una solicitud HTTP GET para obtener el rancho por su ID
                var response = await _httpClient.GetAsync($"{_baseUrl}/{idRancho}");
 
                // Verificar si la respuesta fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Deserializar la respuesta JSON en un objeto RanchoDto
                    var rancho = await response.Content.ReadFromJsonAsync<RanchosDto>();
                    return rancho;
                }
                else
                {
                    throw new Exception("Error al obtener los datos del rancho.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, por ejemplo, en caso de que haya un problema con la solicitud HTTP
                Console.WriteLine($"Error al obtener el rancho: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> ExisteRanchoAsync(string numeroRancho)
        {
            try
            {
                var numeroRanchoEscaped = Uri.EscapeDataString(numeroRancho ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteRancho?numeroRancho={numeroRanchoEscaped}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteRanchoAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> CreateRanchoAsync(RanchosDto rancho)
        {
            try
            {
                // Verificar si ya existe un registro duplicado
                var existe = await ExisteRanchoAsync(rancho.NumeroRancho);

                if (existe)
                {
                    return (false, "Ya existe un registro con el mismo número de rancho.");
                }

                // Si no existe, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, rancho);

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

        public async Task<bool> UpdateRanchoAsync(int id, RanchosDto rancho)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", rancho);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRanchoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
