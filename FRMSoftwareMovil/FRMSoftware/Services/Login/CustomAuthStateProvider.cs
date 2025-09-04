using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.Storage;
using System.Text.Json;
using System;
using System.Threading;
using Microsoft.AspNetCore.Components;
using FRMSoftware.Data;


namespace FRMSoftware
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private System.Threading.Timer? _expirationTimer;

        private readonly NavigationManager _navigation;
        private readonly UserSession _userSession;

        public CustomAuthStateProvider(NavigationManager navigation, UserSession userSession)
        {
            _navigation = navigation;
            _userSession = userSession;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var claims = ParseClaimsFromJwt(token);
                identity = new ClaimsIdentity(claims, "jwt");

                // Restaurar datos del usuario al iniciar la app
                _userSession.IdUsuario = int.TryParse(claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var id) ? id : 0;
                _userSession.Usuario = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ?? "";
                _userSession.Nombre = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
                _userSession.Rol = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";

                // Opcional: volver a iniciar el monitor de expiración
                StartTokenExpirationMonitor(token);
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }


        public async Task MarkUserAsAuthenticated(string token)
        {
            await SecureStorage.SetAsync("jwt_token", token);

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            _userSession.IdUsuario = int.TryParse(claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var id) ? id : 0;
            _userSession.Usuario = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ?? "";
            _userSession.Nombre = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
            _userSession.Rol = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            StartTokenExpirationMonitor(token);
        }

        public async Task MarkUserAsLoggedOut()
        {
            SecureStorage.Remove("jwt_token");

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));

            // Limpiar los datos del usuario
            _userSession.IdUsuario = 0;
            _userSession.Usuario = string.Empty;
            _userSession.Nombre = string.Empty;
            _userSession.Rol = string.Empty;

            _expirationTimer?.Dispose();
        }

        private void StartTokenExpirationMonitor(string token)
        {
            _expirationTimer?.Dispose();

            var jwt = new JwtSecurityToken(token);
            var expiration = jwt.ValidTo;
            var tiempoRestante = expiration - DateTime.UtcNow;

            if (tiempoRestante <= TimeSpan.Zero)
            {
                ExpirarSesion();
                return;
            }

            _expirationTimer = new Timer(_ =>
            {
                ExpirarSesion();
            }, null, tiempoRestante, Timeout.InfiniteTimeSpan);
        }

        private void ExpirarSesion()
        {
            SecureStorage.Remove("jwt_token");

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));

            // Redirigir al login o al Home
            _navigation.NavigateTo("/", true);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            using var document = JsonDocument.Parse(jsonBytes);
            var claims = new List<Claim>();

            var root = document.RootElement;

            if (root.TryGetProperty("exp", out var expElement) && expElement.ValueKind == JsonValueKind.Number)
            {
                if (expElement.TryGetInt64(out var expUnix))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

                    if (expDate < DateTime.UtcNow)
                    {
                        ExpirarSesion();
                        return Enumerable.Empty<Claim>();
                    }
                }
            }

            foreach (var element in root.EnumerateObject())
            {
                if (element.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var arrayItem in element.Value.EnumerateArray())
                    {
                        claims.Add(new Claim(element.Name, arrayItem.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(element.Name, element.Value.ToString()));
                }
            }

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
                case 1: base64 += "==="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
