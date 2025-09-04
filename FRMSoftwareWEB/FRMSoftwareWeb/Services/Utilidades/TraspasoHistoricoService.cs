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

namespace FRMSoftware.Services.Utilidades
{
    public class TraspasoHistoricoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string ultimaRutaDescarga = string.Empty; // Nueva variable para almacenar la ruta


        public TraspasoHistoricoService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseUrl = "http://localhost:5159/api/Historicos";

        }

        public async Task<List<string>> TraspasarTodoAsync(int anio, string usuario)
        {
            var resultados = new List<string>();

            async Task LlamarPost(string endpoint)
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}?usuario={usuario}", null);
                var contenido = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    resultados.Add($"{endpoint}: ERROR - {contenido}");
                }
                else
                {
                    resultados.Add($"{endpoint}: OK");
                }
            }

            async Task LlamarPostConAnio(string endpoint)
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}?anio={anio}&usuario={usuario}", null);
                var contenido = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    resultados.Add($"{endpoint}: ERROR - {contenido}");
                }
                else
                {
                    resultados.Add($"{endpoint}: OK");
                }
            }

            // Catálogos
            await LlamarPost("TraspasarUsuarios");
            await LlamarPost("TraspasarEmpleados");
            await LlamarPost("TraspasarCultivos");
            await LlamarPost("TraspasarViveros");
            await LlamarPost("TraspasarRanchos");
            await LlamarPost("TraspasarVehiculos");

            // Datos por año
            await LlamarPost("TraspasarLlaves");
            await LlamarPostConAnio("TraspasarPlantaciones");
            await LlamarPostConAnio("TraspasarReplantes");
            await LlamarPostConAnio("TraspasarPodas");
            await LlamarPostConAnio("TraspasarProceso");
            await LlamarPostConAnio("TraspasarPersonalCosecha");
            await LlamarPostConAnio("TraspasarCosechasProduccion");
            await LlamarPostConAnio("TraspasarTarimas");
            await LlamarPostConAnio("TraspasarViajes");
            await LlamarPostConAnio("TraspasarVentasDetalles");
            await LlamarPostConAnio("TraspasarVentas");

            return resultados;
        }


        public async Task<List<int>> GetAniosPlantacionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>($"{_baseUrl}/anios-disponibles");
        }

    }
}
