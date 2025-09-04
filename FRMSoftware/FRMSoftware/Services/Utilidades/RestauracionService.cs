using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace FRMSoftware.Services.Utilidades
{
    public class RestauracionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RestauracionService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            #if WINDOWS || IOS || MAC
                _baseUrl = "http://localhost:5159/api/Restauracion"; // Para Windows
            #elif ANDROID
                _baseUrl = "http://10.0.2.2:5159/api/Restauracion"; // Para el emulador de Android
            #endif
        }

        public async Task<(bool Success, string Message)> RestaurarBaseDeDatosAsync(IBrowserFile archivo)
        {
            try
            {
                var content = new MultipartFormDataContent();
                var fileStream = archivo.OpenReadStream(maxAllowedSize: 50_000_000); // Máximo 50MB
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/sql");

                content.Add(fileContent, "archivoSql", archivo.Name);

                var response = await _httpClient.PostAsync($"{_baseUrl}/restaurar", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Restauración completada con éxito.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error en la restauración: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }
    }
}
