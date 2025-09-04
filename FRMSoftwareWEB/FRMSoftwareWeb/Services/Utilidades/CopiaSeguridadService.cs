using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Diagnostics; 

namespace FRMSoftware.Services.Utilidades
{
    public class CopiaSeguridadService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private string ultimaRutaDescarga = string.Empty; // Nueva variable para almacenar la ruta

        public CopiaSeguridadService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/CopiaSeguridad";

        }

        public async Task<(bool Success, string Message)> DescargarCopiaSeguridadAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/descargar");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, $"Error en la descarga: {errorMessage}");
                }

                var nombreArchivo = $"copia_seguridad_{DateTime.Now:yyyyMMddHHmmss}.sql";
                var rutaDescarga = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), nombreArchivo);

                await using var stream = await response.Content.ReadAsStreamAsync();
                await using var fileStream = new FileStream(rutaDescarga, FileMode.Create, FileAccess.Write, FileShare.None);
                await stream.CopyToAsync(fileStream);

                ultimaRutaDescarga = rutaDescarga; // Guardar la ruta de descarga

                return (true, $"Copia de seguridad guardada en: {rutaDescarga}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public void AbrirCarpetaDescarga()
        {
            if (!string.IsNullOrEmpty(ultimaRutaDescarga))
            {
                string carpeta = Path.GetDirectoryName(ultimaRutaDescarga);
                if (!string.IsNullOrEmpty(carpeta) && Directory.Exists(carpeta))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = carpeta,
                        UseShellExecute = true
                    });
                }
            }
        }

        // Para obtener la última ruta de descarga
        public string ObtenerUltimaRutaDescarga()
        {
            return ultimaRutaDescarga;
        }
    }
}
