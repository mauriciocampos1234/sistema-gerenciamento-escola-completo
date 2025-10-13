using MySql.Data.MySqlClient;
using SistemaEscolar.Repositories.Entities;

namespace SistemaEscolar.Repositories
{
    // Interface para o repositório de professor (Injeção de dependência)
    public interface IProfessorRepository
    {
        // Método para inserir um novo professor no banco de dados (Assinatura)
        public int? Inserir(Professor professor);
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
    }
}
