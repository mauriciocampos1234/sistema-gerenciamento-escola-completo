using Microsoft.Data.SqlClient;


namespace SistemaEscolar.Repositories
{
    public static class SqlServerRepositoryExtensions
    {
        public static decimal? GetDecimalOrNull(this SqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (decimal)reader[name];
        }

        public static int? GetInt32OrNull(this SqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (int)reader[name];
        }
    }
}