using dotnet_todo_list_app.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/** MS-SQL
 * docker run --rm --name mssql-server -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyP@ssw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
 */
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(connectionString));
/** Migration
 * dotnet tool install --global dotnet-ef
 * # change directory to the project directory
 * dotnet restore
 * dotnet ef migrations add initial
 * dotnet ef database update
 */
/** Port overwrite
 * # set env
 * ASPNETCORE_URLS=http://*:8080
 */
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

