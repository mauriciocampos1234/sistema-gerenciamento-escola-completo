using MySql.Data.MySqlClient;
using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    // Interface para o repositório de professor (Injeção de dependência)
    public interface IProfessorRepository
    {
        // Método para inserir um novo professor no banco de dados (Assinatura)
        public int? Inserir(Professor professor);

        //método para atualizar um professor no banco de dados (Assinatura)
        int? Atualizar(Professor professor);

        //Método para apagar(Excluir)um professor no banco de dados (Assinatura)
        int? Apagar(int id);

        // Metodo para listar todos os professores (Assinatura)
        IList<Professor> Listar();

        // Metodo para obter um professor pelo ID (Assinatura)
        Professor? ObterPorId(int id);
    }

    // Implementação do repositório de professor
    public class ProfessorRepository: BaseRepository, IProfessorRepository
    {
        // Construtor que recebe a string de conexão e a passa para a classe base
        public ProfessorRepository(string connectionString) : base(connectionString)
        {
        }

        

        // Método para inserir um novo professor no banco de dados
        public int? Inserir(Professor professor)
        {
            int? professorId = null;

            // Usando a conexão com o banco de dados MySQL
            using (var conn = new MySqlConnection(ConnectionString))
            {
                // Consulta SQL para inserir um novo professor e obter o ID gerado
                string query = @"INSERT INTO professor (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id); 
                                 SELECT LAST_INSERT_ID()";

                // Criando o comando SQL
                var cmd = new MySqlCommand(query, conn);

                // Adicionando os parâmetros para evitar SQL Injection
                cmd.Parameters.AddWithValue("@nome", professor.Nome);
                cmd.Parameters.AddWithValue("@email", professor.Email);
                cmd.Parameters.AddWithValue("@usuario_id", professor.UsuarioId);

                // Abrindo a conexão com o banco de dados
                conn.Open();

                // Executando o comando e obtendo o ID do professor inserido
                professorId = Convert.ToInt32(cmd.ExecuteScalar());


            }

            // Retornando o ID do professor inserido
            return professorId;
        }

        public int? Atualizar(Professor professor)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"UPDATE professor SET nome = @nome, email = @email WHERE professor_id = @professor_id";
                
                var cmd = new MySqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("@nome", professor.Nome);
                cmd.Parameters.AddWithValue("@email", professor.Email);
                cmd.Parameters.AddWithValue("@professor_id", professor.Id);
                
                conn.Open();
                
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return professor.Id;
                }
                else
                {
                    return null; // Retorna null se nenhum registro foi atualizado
                }
            }
        }

        // Método para listar todos os professores do banco de dados
        public IList<Professor> Listar()
        {
            var result = new List<Professor>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                // Dica: Quando colamos o script do Workbench, ele traz \r\n (quebra de linha),
                // então basta dar um CTRL+Z, que ele remove e Colocar o @ depois do = para manter a formatação
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                professor p INNER JOIN 
                                usuario u ON p.usuario_id = u.usuario_id
                                ORDER BY
                                p.professor_id";
                
                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) //Enquanto conseguir ler (houver registros(Linhas))
                    {
                        var professor = new Professor //Criando um objeto professor para cada registro(Linha) retornado
                        {
                            Id = reader.GetInt32("professor_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            UsuarioId = reader.GetInt32("usuario_id"),

                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Login = reader.GetString("login"),
                                Senha = reader.GetString("senha")
                            }
                        };
                        result.Add(professor);
                    }
                }
            }
            return result;
        }

        public Professor ObterPorId(int id)
        {
            Professor? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                professor p INNER JOIN 
                                usuario u ON p.usuario_id = u.usuario_id
                                WHERE p.professor_id = @professor_id";
                
                var cmd = new MySqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("professor_id", id);
                
                conn.Open();
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) //Se conseguir ler (houver registro(Linha))
                    {
                        result = new Professor //Criando um objeto professor para o registro(Linha) retornado
                        {
                            Id = reader.GetInt32("professor_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            UsuarioId = reader.GetInt32("usuario_id"),
                            
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Login = reader.GetString("login"),
                                Senha = reader.GetString("senha")
                            }
                        };
                    }
                }
            }
            return result!;
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "DELETE FROM professor WHERE professor_id = @professor_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@professor_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
