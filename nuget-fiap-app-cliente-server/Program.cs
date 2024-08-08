using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using nuget_fiap_app_cliente.Services;
using nuget_fiap_app_cliente_common.Interfaces.Repository;
using nuget_fiap_app_cliente_common.Interfaces.Services;
using nuget_fiap_app_cliente_repository;
using nuget_fiap_app_cliente_repository.DB;
using nuget_fiap_app_cliente_repository.Interface;

public partial class Program
{
    public static void Main(string[] args)
    { 
        var builder = WebApplication.CreateBuilder(args);

        // Configura��es dos servi�os e reposit�rios
        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<RepositoryDB>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        // Configura��es do HealthCheck
        builder.Services.AddHealthChecks();

        // Adiciona e configura os controllers
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NuGET Burger",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Miro",
                    Url = new Uri("https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=124875092732")
                }
            });
        });

        var app = builder.Build();

        // Configura o pipeline de requisi��es HTTP
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(c =>
        {
            c.DocumentTitle = "REDOC API Documentation";
            c.SpecUrl = "/swagger/v1/swagger.json";
        });

        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
        });

        app.Run();
    }
}