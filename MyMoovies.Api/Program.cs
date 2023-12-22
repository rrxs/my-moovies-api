using MyMooviesApi.HttpClients;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Authentication;
using System.IdentityModel.Tokens.Jwt;
using MyMooviesApi.Services;

var originsConfigName = "originsConfig";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongo()
    .AddMongoRepository<User>("users")
    .AddMongoRepository<UserMovie>("usermovies");

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// HttpClients Configuration
builder.Services.AddHttpClient<ITMDBClient, TMDBClient>();

builder.Services.AddAuthenticationJwt();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

app.UseCors(originsConfigName);
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
