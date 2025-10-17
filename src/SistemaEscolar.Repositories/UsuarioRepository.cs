using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    //Definição da interface IUsuarioRepository
    public interface IUsuarioRepository
    {
        Usuario? ObterPorLogin(string login); //Método para obter um usuário pelo login
        int? Inserir(Usuario usuario); //Método para inserir um novo usuário

        int? Atualizar(Usuario usuario); //Método para atualizar um usuário existente

        int? Apagar(int id); //Método para apagar um usuário existente


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

        public int? Inserir(Usuario usuario)
        {
            int? usuarioId = null;

            // Usando a conexão com o banco de dados MySQL
            using (var conn = new MySqlConnection(ConnectionString))
            {
                // Consulta SQL para inserir um novo professor e obter o ID gerado
                string query = @"INSERT INTO usuario (login, senha, funcao_id) VALUES (@login, @senha, @funcao_id); 
                                 SELECT LAST_INSERT_ID()";

                // Criando o comando SQL
                var cmd = new MySqlCommand(query, conn);

                // Adicionando os parâmetros para evitar SQL Injection
                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@funcao_id", usuario.FuncaolId);

                // Abrindo a conexão com o banco de dados
                conn.Open();

                // Executando o comando e obtendo o ID do professor inserido
                usuarioId = Convert.ToInt32(cmd.ExecuteScalar());


            }

            // Retornando o ID do professor inserido
            return usuarioId;
        }

        public int? Atualizar(Usuario usuario)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = @"UPDATE usuario SET login = @login, senha = @senha WHERE usuario_id = @usuario_id";
                
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
                var query = @"DELETE FROM usuario WHERE usuario_id = @usuario_id";
                
                var cmd = new MySqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("@usuario_id", id);
                
                conn.Open();
                
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
