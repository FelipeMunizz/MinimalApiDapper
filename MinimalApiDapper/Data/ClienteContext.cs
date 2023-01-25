using System.Data;

namespace MinimalApiDapper.Data;

public class ClienteContext
{
    public delegate Task<IDbConnection> GetConnection();
}
