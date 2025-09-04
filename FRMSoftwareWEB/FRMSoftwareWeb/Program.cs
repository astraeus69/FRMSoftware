using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FRMSoftware.Data;
using FRMSoftware.Services.Catalogos;
using FRMSoftware.Services.Movimientos;
using FRMSoftware.Services.Utilidades;
using Microsoft.AspNetCore.Components.Authorization;
using FRMSoftwareWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for API
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:5159/") });

// Registro de servicios de catálogos
builder.Services.AddScoped<CultivoService>();
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<EmpleadosService>();
builder.Services.AddScoped<ViverosService>();
builder.Services.AddScoped<RanchosService>();
builder.Services.AddScoped<VehiculosService>();

// Registro de servicios de movimientos
builder.Services.AddScoped<GestionLlavesService>();
builder.Services.AddScoped<GestionPlantacionesService>();
builder.Services.AddScoped<GestionReplantesService>();
builder.Services.AddScoped<GestionPodasService>();
builder.Services.AddScoped<GestionCosechasService>();
builder.Services.AddScoped<GestionPersonalCosechaService>();
builder.Services.AddScoped<GestionViajesService>();
builder.Services.AddScoped<ProcesosService>();
builder.Services.AddScoped<VentasService>();

// Registro de servicios de utilidades
builder.Services.AddScoped<ErrorLogService>();
builder.Services.AddScoped<RestauracionService>();
builder.Services.AddScoped<CopiaSeguridadService>();
builder.Services.AddScoped<TraspasoHistoricoService>();

// User session singleton
builder.Services.AddSingleton<UserSession>();

// Auth
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
