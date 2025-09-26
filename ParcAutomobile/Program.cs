using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using SharedLibrary.Entities;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

//starting

builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Sorry,your connection is not found"));
    });


builder.Services.AddAuthentication(options =>

{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}

).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Utilisateur>, UtilisateurRepository>();
//builder.Services.AddScoped<IGenericRepositoryInterface<Voiture>, VoitureRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Entretien>, EntretienRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Fournisseur>, FournisseurRepository>();
//builder.Services.AddScoped<IGenericRepositoryInterface<Trajet>, TrajetRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Affectation>, AffectationRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<DocumentAdministratif>, DocumentAdministratifRepository>();


builder.Services.AddScoped<ITrajetRepository, TrajetRepository>();
builder.Services.AddScoped<IAffectationRepository, AffectationRepository>();

builder.Services.AddScoped<IVoitureRepository, VoitureRepository>();













builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("http://localhost:5022", "https://localhost:7017")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
