using Dapper.Contrib.Extensions;

namespace MinimalApiDapper.Data;

[Table("Cliente")]
public record Clientes(int? id, string? Nome, string? Email, string? Documento);