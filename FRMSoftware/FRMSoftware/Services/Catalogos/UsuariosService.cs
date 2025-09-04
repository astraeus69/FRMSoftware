using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System;
using System.Text;
using System.Net.Http.Headers;
using FRMSoftware.Data;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Authorization;
using FRMSoftware;

namespace FRMSoftware.Services.Catalogos
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authProvider;

        public UsuariosService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authProvider = authProvider;

                #if WINDOWS || IOS || MAC
                            _baseUrl = "http://localhost:5159/api/Usuarios";
                #elif ANDROID
                            _baseUrl = "http://10.0.2.2:5159/api/Usuarios";
                #else
                            throw new InvalidOperationException("Unsupported platform");
                #endif
        }

        public async Task<List<UsuariosDto>> GetUsuariosAsync()
        {
            var usuarios = await _httpClient.GetFromJsonAsync<List<UsuariosDto>>(_baseUrl);
            return usuarios ?? new List<UsuariosDto>();
        }

        // Método para obtener un empleado por su ID
        public async Task<UsuariosDto> GetUsuarioPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UsuariosDto>($"{_baseUrl}/{id}");
        }

        public async Task<bool> ExisteUsuarioAsync(string usuario)
        {
            try
            {
                var usuarioEscaped = Uri.EscapeDataString(usuario ?? string.Empty);
                var response = await _httpClient.GetAsync($"{_baseUrl}/ExisteUsuario?usuario={usuarioEscaped}");

                return response.IsSuccessStatusCode &&
                       await response.Content.ReadFromJsonAsync<bool>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ExisteUsuarioAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> CreateUsuarioAsync(UsuariosDto usuario)
        {
            try
            {
                if (await ExisteUsuarioAsync(usuario.Usuario))
                    return (false, "Ya existe un usuario con ese nombre.");

                var response = await _httpClient.PostAsJsonAsync(_baseUrl, usuario);
                if (response.IsSuccessStatusCode)
                    return (true, "Registro exitoso.");

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Error al registrar: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public async Task<bool> UpdateUsuarioAsync(int id, UsuariosDto usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool Exito, string Token, string Mensaje)> LoginAsync(LoginDto login)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Login", login);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (result?.Token != null)
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorageFunctions.saveToken", result.Token);

                        if (_authProvider is CustomAuthStateProvider authStateProvider)
                        {
                            await authStateProvider.MarkUserAsAuthenticated(result.Token);
                        }

                        return (true, result.Token, "Inicio de sesión exitoso.");
                    }

                    return (false, null!, "No se recibió token del servidor.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, null!, errorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null!, $"Error inesperado: {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            // Eliminar token de localStorage
            await _jsRuntime.InvokeVoidAsync("localStorageFunctions.removeToken");

            if (_authProvider is CustomAuthStateProvider authStateProvider)
            {
                // Notificar a Blazor que el usuario ha cerrado sesión
                await authStateProvider.MarkUserAsLoggedOut();
            }
        }

        public async Task<bool> ValidarContrasenaActualAsync(int idUsuario, string contrasenaActual)
        {
            var request = new ValidarContrasenaRequest
            {
                IdUsuario = idUsuario,
                ContrasenaActual = contrasenaActual
            };

            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/ValidarContrasenaActual", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }


    }
}
