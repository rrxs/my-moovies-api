using MyMooviesApi.HttpClients;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Authentication;
using System.IdentityModel.Tokens.Jwt;

var originsConfigName = "originsConfig";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: originsConfigName,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

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

// HttpClients Configuration
builder.Services.AddHttpClient<ITMDBClient, TMDBClient>();

builder.Services.AddAuthenticationJwt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

app.UseCors(originsConfigName);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
