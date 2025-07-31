using Api_Finish_Version.Data;
using Api_Finish_Version.IRepository.Auth;
using Api_Finish_Version.IServices.Auth;
using Api_Finish_Version.Repositorys.Auth;
using Api_Finish_Version.Services.Auth;
using Api_Finish_Version.Validation;
using API_REST_PROYECT.JWT;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Microsoft.SqlServer.Management.Sdk.Sfc.RequestObjectInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Leer la clave JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:Secret"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

//Hacemos esta modificacion del manejo de validacion en ModelState. Se explica todo en la clase ValidationFormatter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFormatter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthRepository, AuthRespository>();

builder.Services.AddScoped<IServiceAuth, ServiceAuth>();
builder.Services.AddScoped<IEmailAuthService, EmailAuthService>();


builder.Services.AddSingleton<Token>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<JwtSettingsConfirmation>(builder.Configuration.GetSection("JwtSettingsConfirmation"));
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
