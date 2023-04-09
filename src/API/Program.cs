using API.Configuration;
using Application.Companies.CreateCompany;
using Application.Companies.DomainServices;
using Application.Configuration.Validation;
using Domain.Companies;
using Domain.Users;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Domain.Addresses;
using Infrastructure.Domain.Companies;
using Infrastructure.Domain.Users;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(CreateCompanyCommand).Assembly);
});

builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddScoped<ICompanyUniquenessChecker, CompanyUniquenessChecker>();

// Repositories
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<AddressRepository>();

// Database
builder.Services.AddScoped<IUserContext, AuthenticationServiceUserContext>(_ => new AuthenticationServiceUserContext(builder.Configuration["AuthorizationService:BaseUrl"]));
builder.Services.AddSingleton<DapperContext>(_ => new DapperContext(builder.Configuration.GetConnectionString("MainDatabase")));

// Validation
builder.Services.AddScoped<IValidator<CreateCompanyCommand>, CreateCompanyCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();

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

app.UseMiddleware<ApplicationExceptionsMiddleware>();
app.UseMiddleware<UserAuthenticationMiddleware>();

app.Run();