using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MISA.Core.Exceptions;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Core.Interface.Repositories.Base;
using MISA_Core.Interface.Services;
using MISA_Core.Interface.Services.Base;
using MISA_Core.Services;
using MISA_Core.Validator;
using MISA_Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


//Dependency Injection
builder.Services.AddScoped<IBaseRepository<Organization>, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationRepo, OrganizationRepository>();
//builder.Services.AddScoped<IBaseService<Organization>, OrganizationService>();
//builder.Services.AddScoped<IOrganizationService, OrganizationService>();

builder.Services.AddScoped<IBaseRepository<SalariesComposition>, SalariesCompositionRepository>();
builder.Services.AddScoped<ISalariesCompositionRepo, SalariesCompositionRepository>();
builder.Services.AddScoped<IBaseService<SalariesComposition>, SalariesCompositionService>();
builder.Services.AddScoped<ISalariesCompositionService, SalariesCompositionService>();


builder.Services.AddScoped<IBaseRepository<SalariesCompositionSystem>, SalariesCompositionSystemRepository>();
builder.Services.AddScoped<ISalariesCompositionSystemRepo, SalariesCompositionSystemRepository>();
builder.Services.AddScoped<IBaseService<SalariesCompositionSystem>, SalariesCompositionSystemService>();
builder.Services.AddScoped<ISalariesCompositionSystemService, SalariesCompositionSystemService>();

builder.Services.AddScoped<IValidator<SalariesComposition>, SalariesCompositionValidator>();


var app = builder.Build();
app.UseCors("AllowOrigins");




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
