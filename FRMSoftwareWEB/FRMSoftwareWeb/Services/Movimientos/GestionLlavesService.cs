using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using FRMSoftware.Data;
using System.Net.Http.Json;
using FRMSoftware.Services.Catalogos;

namespace FRMSoftware.Services.Movimientos
{
    public class GestionLlavesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GestionLlavesService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/Llaves";

        }

        // Método para consultar todas las llaves
        public async Task<List<LlavesDto>> GetLlavesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LlavesDto>>(_baseUrl);
        }

        // Método para consultar todas las llaves con información de ranchos
        public async Task<List<LlaveConRanchoDto>> GetLlavesConRancho()
        {
            var response = await _httpClient.GetFromJsonAsync<List<LlaveConRanchoDto>>($"{_baseUrl}/GetLlavesConRancho");
            return response ?? new List<LlaveConRanchoDto>(); // Retorna una lista vacía si no hay datos
        }

        // Método para calcular el total de superficie acumulada por llaves relacionadas a un rancho
        public async Task<decimal?> GetSuperficieTotalLlaves(int idRancho)
        {
            var llavesConRancho = await _httpClient.GetFromJsonAsync<List<LlaveConRanchoDto>>($"{_baseUrl}/GetLlavesConRancho");

            // Filtrar las llaves asociadas al rancho
            var llavesDelRancho = llavesConRancho.Where(l => l.IdRancho == idRancho).ToList();

            // Sumar las superficies (en Ha)
            decimal? superficieTotal = llavesDelRancho.Sum(l => l.SuperficieHa); // o SuperficieAcres, según corresponda

            return superficieTotal;
        }

        // Método para obtener una llave por su ID
        public async Task<LlavesDto> GetLlave(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<LlavesDto>($"{_baseUrl}/{id}");
                return response; // Retorna la llave con el id solicitado
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la llave: {ex.Message}");
                return null; // En caso de error, se retorna null
            }
        }

        // Verifica existencia de llave por nombre e IdRancho
        public async Task<bool> ExisteLlaveAsync(string nombreLlave, int idRancho)
        {
            try
            {
                var nombreLlaveEscaped = Uri.EscapeDataString(nombreLlave ?? string.Empty);

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteLlave?nombreLlave={nombreLlaveEscaped}&idRancho={idRancho}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                return false; // En caso de error, asumimos que no existe
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteLlaveAsync: {ex.Message}");
                return false;
            }
        }

        // Crea una nueva llave
        public async Task<(bool Success, string Message)> CreateLlaveAsync(LlavesDto llave, decimal? superficieRancho)
        {
            try
            {
                // Verificar si ya existe una llave duplicada en el rancho 
                var existe = await ExisteLlaveAsync(llave.NombreLlave, llave.IdRancho);

                if (existe)
                {
                    return (false, "Ya existe una llave con el mismo nombre en este rancho.");
                }

                // Obtener la superficie total de las llaves actuales en el rancho
                decimal? superficieTotalLlaves = await GetSuperficieTotalLlaves(llave.IdRancho);

                // Verificar si la suma de las superficies supera la del rancho
                if (superficieTotalLlaves + llave.SuperficieHa > superficieRancho)
                {
                    return (false, "La superficie total de las llaves supera la superficie del rancho por " + ((superficieTotalLlaves + llave.SuperficieHa) - superficieRancho) + " Ha");
                }

                // Si no existe y no supera la superficie, realizar el registro
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, llave);

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


        public async Task<bool> UpdateLlaveAsync(int id, LlavesDto llave)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", llave);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLlaveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
