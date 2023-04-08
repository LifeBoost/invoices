using Application.Companies.CreateCompany;
using Domain.Companies;
using Domain.Users;
using Infrastructure.Database;
using Infrastructure.Domain.Addresses;
using Infrastructure.Domain.Companies;
using Infrastructure.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(CreateCompanyCommand).Assembly);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<IUserContext, AuthenticationServiceUserContext>(_ => new AuthenticationServiceUserContext(builder.Configuration["AuthorizationService:BaseUrl"]));
builder.Services.AddSingleton<DapperContext>(_ => new DapperContext(builder.Configuration.GetConnectionString("MainDatabase")));

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