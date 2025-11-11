using SistemaEscolar.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace SistemaEscolar.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? ObterPorLogin(string login);

        int? Inserir(Usuario usuario);

        int? Atualizar(Usuario usuario);

        int? Apagar(int id);
    }

    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(string connectionString) : base(connectionString)
        {
        }

        public Usuario? ObterPorLogin(string login)
        {
            Usuario? usuario = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "SELECT usuario_id, login, senha, funcao_id FROM usuario WHERE login = @login";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("login", login);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32("usuario_id"),
                            Login = reader.GetString("login"),
                            Senha = reader.GetString("senha"),
                            FuncaoId = reader.GetInt32("funcao_id")
                        };
                    }
                }
            }

            return usuario;
        }

        public int? Inserir(Usuario usuario)
        {
            int? usuarioId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO usuario (login, senha, funcao_id) VALUES (@login, @senha, @funcao_id); 
                                 SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@funcao_id", usuario.FuncaoId);

                conn.Open();

                usuarioId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return usuarioId;
        }

        public int? Atualizar(Usuario usuario)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "UPDATE usuario SET login = @login, senha = @senha WHERE usuario_id = @usuario_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@usuario_id", usuario.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "DELETE FROM usuario WHERE usuario_id = @usuario_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("usuario_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}