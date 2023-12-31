using FluentMigrator.Runner;
using Matchermania.Api.Middlewares;
using td.Application;
using td.Persistence;
using td.Persistence.Migrations.Tables;
using td.Persistence.Services;
using td.Shared;
using td.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceRegistration();
builder.Services.AddApplicationRegistration();
builder.Services.AddSharedRegistration(builder.Configuration);

builder.Services.AddFluentMigratorCore()
                    .ConfigureRunner(
                    migrationBuilder => migrationBuilder
                    .AddSqlServer()
                    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SqlConnection"))
                    .WithMigrationsIn(typeof(ProductTableMigration).Assembly));

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.MigrateDatabase(builder.Configuration);
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseMiddleware<RequestIdentifyMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
