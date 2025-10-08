using MySql.Data.MySqlClient;
using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    //Definição da interface IUsuarioRepository
    public interface IUsuarioRepository
    {
        Usuario? ObterPorLogin(string login);
    }
    public class UsuarioRepository : BaseRepository, IUsuarioRepository //Implementação da interface IUsuarioRepository e BaseRepository
    {
        public UsuarioRepository(string connectionString) : base(connectionString)
        {
        }

        //Chamando o metodo ValidarLogin do UsuarioService.cs
        public Usuario? ObterPorLogin(string login)
        {
            //Temporario só para testes
            Usuario? usuario = null;

            // Usamos using para garantir que a conexão será fechada corretamente
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = "SELECT usuario_id, login, senha, funcao_id FROM Usuario WHERE Login = @Login";
                var cmd = new MySqlCommand(sql, conn); //Não estamos usando o Entiteframework para ter total autonomia do banco de dados(Queryes)
                //Entiteframework é uma ferramenta de persistência de dados(ORM) que facilita o mapeamento entre objetos do C# e tabelas do banco de dados

                cmd.Parameters.AddWithValue("@Login", login); //Prevenção contra SQL Injection

                //Abrindo a conexão
                conn.Open();

                //Executando o comando e obtendo o leitor de dados
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) //Se encontrou um registro(Linha)
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32("usuario_id"),
                            Login = reader.GetString("login"),
                            Senha = reader.GetString("senha"),
                            FuncaolId = reader.GetInt32("funcao_id")
                        };
                    }
                }
            }

            return usuario;
        }

    }
}
