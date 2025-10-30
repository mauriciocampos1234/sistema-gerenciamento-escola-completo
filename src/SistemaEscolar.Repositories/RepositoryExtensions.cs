using MySql.Data.MySqlClient;

namespace SistemaEscolar.Repositories
{
    public static class RepositoryExtensions
    {
        public static decimal? GetDecimalOrNull(this MySqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (decimal)reader[name];
        }

        public static int? GetInt32OrNull(this MySqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (int)reader[name];
        }
    }
}
