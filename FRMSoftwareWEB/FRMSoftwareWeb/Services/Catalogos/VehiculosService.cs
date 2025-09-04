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
    public class VehiculosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VehiculosService(HttpClient httpClient)
        {
            _httpClient = httpClient;


            _baseUrl = "http://localhost:5159/api/Vehiculos";

        }

        public async Task<List<VehiculosDto>> GetVehiculosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VehiculosDto>>(_baseUrl);
        }

        public async Task<VehiculosDto> GetVehiculosPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VehiculosDto>($"{_baseUrl}/{id}");
        }



        public async Task<bool> ExisteVehiculoAsync(string placas, string modelo)
        {
            try
            {
                var placasEscaped = Uri.EscapeDataString(placas ?? string.Empty);
                var modeloEscaped = Uri.EscapeDataString(modelo ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteVehiculo?placas={placasEscaped}&modelo={modeloEscaped}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteVehiculoAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> CreateVehiculoAsync(VehiculosDto vehiculo)
        {
            try
            {
                // Verificar si ya existe un registro duplicado
                var existe = await ExisteVehiculoAsync(vehiculo.Placas, vehiculo.Modelo);

                if (existe)
                {
                    return (false, "Ya existe un registro con las mismas placas y modelo.");
                }

                // Si no existe, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, vehiculo);

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

        public async Task<bool> UpdateVehiculoAsync(int id, VehiculosDto vehiculo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", vehiculo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
