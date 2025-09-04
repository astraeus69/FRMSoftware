using Microsoft.Extensions.Logging;
using Microsoft.Maui.Devices;
using System.Globalization;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.LifecycleEvents;
using FRMSoftware.Data;

namespace FRMSoftware
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");


            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   })
                   .ConfigureLifecycleEvents(events =>
                   {
                    #if WINDOWS
                               events.AddWindows(windows =>
                                   windows.OnClosed((window, args) =>
                                   {
                                       SecureStorage.Remove("jwt_token");
                                   }));
                    #endif
                   });

            builder.Services.AddMauiBlazorWebView();

            // 🔹 Detectar plataforma y configurar la URL base
            string baseUrl;
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                baseUrl = "http://10.0.2.2:5159/"; // Para Android (Emulador)
            }
            else
            {
                baseUrl = "http://localhost:5159/"; // Para Windows y iOS (Simulador)
            }

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            builder.Services.AddHttpClient("FRMSoftware", c =>
            {
                c.BaseAddress = new Uri(baseUrl);
            });


            // Registro de servicios de catálogos
            builder.Services.AddScoped<Services.Catalogos.CultivoService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddSingleton<UserSession>();

            builder.Services.AddScoped<Services.Catalogos.UsuariosService>();

            builder.Services.AddScoped<Services.Catalogos.EmpleadosService>();
            builder.Services.AddScoped<Services.Catalogos.ViverosService>();
            builder.Services.AddScoped<Services.Catalogos.RanchosService>();
            builder.Services.AddScoped<Services.Catalogos.VehiculosService>();

            // Registro de servicios de movimientos
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionLlavesService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionPlantacionesService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionReplantesService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionPodasService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionCosechasService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionPersonalCosechaService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.GestionViajesService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.ProcesosService>();
            builder.Services.AddScoped<FRMSoftware.Services.Movimientos.VentasService>();

            // Registro de servicios de utilidades
            builder.Services.AddScoped<FRMSoftware.Services.Utilidades.ErrorLogService>();
            builder.Services.AddScoped<FRMSoftware.Services.Utilidades.RestauracionService>();
            builder.Services.AddScoped<FRMSoftware.Services.Utilidades.CopiaSeguridadService>();
            builder.Services.AddScoped<FRMSoftware.Services.Utilidades.TraspasoHistoricoService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
