using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MISA.Infrastructure.Database;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories.Base;
using MISA_Core.Interface.Services;
using MISA_Core.Interface.Services.Base;
using MISA_Core.Validator;
using MISA_Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DBConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))) // MySQL 8
);

//Dependency Injection
builder.Services.AddScoped<IBaseRepository<Organization>, OrganizationRepository>();
builder.Services.AddScoped<IBaseService<Organization>, IOrganizationService>();

builder.Services.AddScoped<IBaseRepository<SalariesComposition>, SalariesCompositionRepository>();
builder.Services.AddScoped<IBaseService<SalariesCompositionSystem>, ISalariesCompositionSystemService>();

builder.Services.AddScoped<IBaseRepository<SalariesCompositionSystem>, SalariesCompositionSystemRepository>();
builder.Services.AddScoped<IBaseService<SalariesComposition>, ISalariesCompositionService>();

builder.Services.AddScoped<IValidator<SalariesComposition>, SalariesCompositionValidator>();


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
