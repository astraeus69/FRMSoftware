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
    public class EmpleadosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EmpleadosService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/Empleados";


        }

        public async Task<List<EmpleadosDto>> GetEmpleadosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EmpleadosDto>>(_baseUrl);
        }

        // Método para obtener un empleado por su ID
        public async Task<EmpleadosDto> GetEmpleadoPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmpleadosDto>($"{_baseUrl}/{id}");
        }


        public async Task<bool> ExisteEmpleadoAsync(string nombre, string telefono)
        {
            try
            {
                var nombreEscaped = Uri.EscapeDataString(nombre ?? string.Empty);
                var telefonoEscaped = Uri.EscapeDataString(telefono ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteEmpleado?nombre={nombreEscaped}&telefono={telefonoEscaped}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteEmpleadoAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> CreateEmpleadoAsync(EmpleadosDto empleado)
        {
            try
            {
                // Verificar si ya existe un registro duplicado
                var existe = await ExisteEmpleadoAsync(empleado.Nombre, empleado.Telefono);

                if (existe)
                {
                    return (false, "Ya existe un registro con el mismo nombre y teléfono.");
                }

                // Si no existe, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, empleado);

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

        public async Task<bool> UpdateEmpleadoAsync(int id, EmpleadosDto empleado)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", empleado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
