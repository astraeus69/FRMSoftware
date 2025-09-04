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
    public class ViverosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ViverosService(HttpClient httpClient)
        {
            _httpClient = httpClient;


            _baseUrl = "http://localhost:5159/api/Viveros";

        }

        public async Task<List<ViverosDto>> GetViverosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ViverosDto>>(_baseUrl);
        }

        // Método para obtener un vivero por su ID
        public async Task<ViverosDto> GetVivero(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ViverosDto>($"{_baseUrl}/{id}");
                return response; // Retorna el vivero con el id solicitado
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el vivero: {ex.Message}");
                return null; // En caso de error, se retorna null
            }
        }

        public async Task<bool> ExisteViveroAsync(string codigoVivero)
        {
            try
            {
                var codigoViveroEscaped = Uri.EscapeDataString(codigoVivero ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteVivero?codigoVivero={codigoViveroEscaped}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteViveroAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> CreateViveroAsync(ViverosDto vivero)
        {
            try
            {
                // Verificar si ya existe un registro duplicado
                var existe = await ExisteViveroAsync(vivero.CodigoVivero);

                if (existe)
                {
                    return (false, "Ya existe un registro con el mismo código de vivero.");
                }

                // Si no existe, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, vivero);

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

        public async Task<bool> UpdateViveroAsync(int id, ViverosDto vivero)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", vivero);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteViveroAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
