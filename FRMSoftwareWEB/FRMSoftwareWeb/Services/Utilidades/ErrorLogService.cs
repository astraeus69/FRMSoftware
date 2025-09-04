using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FRMSoftware.Data;
using System.Net.Http.Json;
using System;

namespace FRMSoftware.Services.Utilidades
{
    public class ErrorLogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ErrorLogService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/ErrorLogs";

        }

        // Método para obtener todos los registros de error
        public async Task<List<ErrorLogDto>> GetErrorLogsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ErrorLogDto>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros de errores: {ex.Message}");
                return new List<ErrorLogDto>(); // Retorna una lista vacía en caso de error
            }
        }


        // Método para registrar un nuevo error
        public async Task<(bool Success, string Message)> CreateErrorLogAsync(ErrorLogDto errorLog)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, errorLog);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Error registrado correctamente.");
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
    }
}
