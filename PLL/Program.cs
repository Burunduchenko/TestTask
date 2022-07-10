using AutoMapper;
using BLL;
using BLL.AddModels;
using BLL.Astractions;
using BLL.Services;
using BLL.ViewModels;
using DAL;
using DAL.Abstractions;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<IncidentDbContext>(options => options
    .UseSqlServer(connectionString, b => b.MigrationsAssembly("DAL")));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<Contact>, ContactRepository>();
builder.Services.AddScoped<IRepository<Incedent>, IncedentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IService<IncedentViewModel, IncedentAddModel>, IncedentService>();
builder.Services.AddScoped<IService<AccountViewModel, AccountAddModel>, AccountService>();
builder.Services.AddScoped<IService<ContactViewModel, ContactAddModel>, ContactService>();

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
