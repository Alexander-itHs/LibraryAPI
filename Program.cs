using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(opt =>
	opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<LibraryContext>(opt =>
{
	//var DbCon = builder.Configuration.GetConnectionString("DbCon");
	var DbCon = builder.Configuration.GetConnectionString("TestDbCon");
	opt.UseSqlServer(DbCon);
	opt.LogTo(l => Debug.WriteLine(l)).EnableSensitiveDataLogging(true);
});
//


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
