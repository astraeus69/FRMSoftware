using System.Net.Http.Json;
using FRMSoftware.Data;

namespace FRMSoftware.Services.Movimientos
{
    public class ProcesosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProcesosService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Proceso"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Proceso"; // Para el emulador de Android
            #endif
        }

        // Método para obtener todos los procesos
        public async Task<List<ProcesoDto>> GetProcesosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProcesoDto>>(_baseUrl);
        }

        // Método para obtener un proceso por su ID
        public async Task<ProcesoDto> GetProcesoPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProcesoDto>($"{_baseUrl}/{id}");
        }

        // Método para obtener todos los detalles de los procesos
        public async Task<List<ProcesoDetallesDto>> GetProcesosDetallesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProcesoDetallesDto>>($"{_baseUrl}/GetProcesosDetalles");
        }

        // Método para obtener los detalles de un proceso por su ID
        public async Task<ProcesoDetallesDto> GetProcesoDetallesPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProcesoDetallesDto>($"{_baseUrl}/GetProcesosDetalles/{id}");
        }

        // Método para crear un nuevo proceso
        public async Task<(bool Success, string Message)> CreateProcesoAsync(ProcesoDto proceso)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, proceso);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registro exitoso.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, errorMessage);
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

        // Método para actualizar un proceso
        public async Task<bool> UpdateProcesoAsync(int id, ProcesoDto proceso)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", proceso);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar un proceso
        public async Task<bool> DeleteProcesoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
