using System.Data.SqlClient;
using static MinimalApiDapper.Data.ClienteContext;

namespace MinimalApiDapper.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddScoped<GetConnection>(sp => async () =>
        {
            var connection = new SqlConnection(connectionstring);
            await connection.OpenAsync();
            return connection;
        });

        return builder;
    }
}