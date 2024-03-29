using UserAPI.Models;
using Microsoft.EntityFrameworkCore;
using UserAPI.Entities;
using UserAPI.Repositories;
using UserAPI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();


//Read connection string from appsettings.json
builder.Services.AddDbContext<User_DBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));

var AllowSpecificOrigin = "_allowSpecificOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigin,
        policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
