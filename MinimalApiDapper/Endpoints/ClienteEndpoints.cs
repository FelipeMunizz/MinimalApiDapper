using Dapper;
using Dapper.Contrib.Extensions;
using MinimalApiDapper.Data;
using System.Formats.Tar;
using static MinimalApiDapper.Data.ClienteContext;

namespace MinimalApiDapper.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Bem-vindo a API Clientes - {DateTime.Now}");

        app.MapGet("/clientes", async (GetConnection connectionGetter) =>
        {
            using var con = await connectionGetter();

            List<Clientes> clientes = new List<Clientes>();
            try
            {
                string query = "select * from Cliente";
                clientes = con.Query<Clientes>(query).ToList();

                if (clientes is null)
                    return Results.NotFound("Nenhum Cliente Cadastrado");
            }
            catch (Exception e)
            {

                throw e;
            }

            return Results.Ok(clientes);
        });

        app.MapGet("/clientes/{id}", async (GetConnection connectionGetter, int id) =>
        {
            using var con = await connectionGetter();
            Clientes cliente = new Clientes(null, null, null, null);

            try
            {
                var query = "select * from Cliente where Id =" + id;
                cliente = con.Query<Clientes>(query).FirstOrDefault();
                if (cliente is null)
                    return Results.NotFound("Id não encontrado");
            }
            catch (Exception e)
            {

                throw e;
            }

            return Results.Ok(cliente);
        });

        app.MapPost("/clientes", async (GetConnection connectionGetter, Clientes cliente) =>
        {
            using var con = await connectionGetter();
            var id = con.Insert(cliente);
            return Results.Created($"/clientes/{id}", cliente);
        });

        app.MapPut("/clientes", async (GetConnection connectionGetter, Clientes cliente) =>
        {
            using var con = await connectionGetter();
            var id = con.Update(cliente);
            return Results.Ok();
        });

        app.MapDelete("/clientes/{id}", async (GetConnection connectionGetter, int id) =>
        {
            using var con = await connectionGetter();
            var deleted = con.Get<Clientes>(id);
            if (deleted is null)
                return Results.NotFound("Id não encontrado");
            return Results.Ok(deleted);
        });
    }
}