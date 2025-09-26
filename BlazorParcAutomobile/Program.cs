using Blazored.LocalStorage;
using BlazorParcAutomobile;
using BlazorParcAutomobile.ApplicationStates;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedLibrary.Entities;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF1cWWhAYVB2WmFZfVtgdVRMYVxbRX9PMyBoS35Rc0VrWn5fcXZXRWZYV0NyVEFd");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();

builder.Services.AddHttpClient("SystemApiClient", client =>
{

    client.BaseAddress = new Uri("https://localhost:7066/");
}).AddHttpMessageHandler<CustomHttpHandler>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/") });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();


builder.Services.AddScoped<IGenericServiceInterface<Utilisateur>, GenericServiceImplementation<Utilisateur>>();
//builder.Services.AddScoped<IGenericServiceInterface<Voiture>, GenericServiceImplementation<Voiture>>();
builder.Services.AddScoped<IGenericServiceInterface<Entretien>, GenericServiceImplementation<Entretien>>();
builder.Services.AddScoped<IGenericServiceInterface<Fournisseur>, GenericServiceImplementation<Fournisseur>>();
builder.Services.AddScoped<IGenericServiceInterface<Trajet>, GenericServiceImplementation<Trajet>>();
builder.Services.AddScoped<IGenericServiceInterface<Affectation>, GenericServiceImplementation<Affectation>>();
builder.Services.AddScoped<IGenericServiceInterface<DocumentAdministratif>, GenericServiceImplementation<DocumentAdministratif>>();

builder.Services.AddScoped<ITrajetService, TrajetService>();
builder.Services.AddScoped<IVoitureService, VoitureService>();





builder.Services.AddScoped<AllState>();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();




await builder.Build().RunAsync();
