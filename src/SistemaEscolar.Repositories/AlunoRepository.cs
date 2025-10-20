using SistemaEscolar.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SistemaEscolar.Repositories
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Aluno aluno)
        {
            int? alunoId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO aluno (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id);
                                 SELECT LAST_INSERT_ID();";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_id", aluno.UsuarioId);

                conn.Open();

                alunoId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return alunoId;
        }

        public int? Atualizar(Aluno aluno)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"UPDATE aluno SET nome = @nome, email = @email, usuario_id = @usuario_id WHERE aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_id", aluno.UsuarioId);
                cmd.Parameters.AddWithValue("@aluno_id", aluno.Id);

                conn.Open();

                var rows = cmd.ExecuteNonQuery();
                if (rows > 0) return aluno.Id;
                return null;
            }
        }

        public IList<Aluno> Listar()
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, a.usuario_id, u.login, u.senha
                                 FROM aluno a
                                 LEFT JOIN usuario u ON a.usuario_id = u.usuario_id
                                 ORDER BY a.aluno_id";

                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                            UsuarioId = reader.IsDBNull(reader.GetOrdinal("usuario_id")) ? 0 : reader.GetInt32("usuario_id"),
                            Usuario = reader.IsDBNull(reader.GetOrdinal("usuario_id")) ? null : new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Login = reader.IsDBNull(reader.GetOrdinal("login")) ? null! : reader.GetString("login"),
                                Senha = reader.IsDBNull(reader.GetOrdinal("senha")) ? null! : reader.GetString("senha")
                            }
                        };

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public Aluno? ObterPorId(int id)
        {
            Aluno? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, a.usuario_id, u.login, u.senha
                                 FROM aluno a
                                 LEFT JOIN usuario u ON a.usuario_id = u.usuario_id
                                 WHERE a.aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aluno_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                            UsuarioId = reader.IsDBNull(reader.GetOrdinal("usuario_id")) ? 0 : reader.GetInt32("usuario_id"),
                            Usuario = reader.IsDBNull(reader.GetOrdinal("usuario_id")) ? null : new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Login = reader.IsDBNull(reader.GetOrdinal("login")) ? null! : reader.GetString("login"),
                                Senha = reader.IsDBNull(reader.GetOrdinal("senha")) ? null! : reader.GetString("senha")
                            }
                        };
                    }
                }
            }

            return result;
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "DELETE FROM aluno WHERE aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aluno_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        // Ajuste a query abaixo caso sua tabela de vínculo aluno-turma tenha nome diferente (ex: aluno_turma_boletim)
        public IList<Aluno> ListarPorTurma(int turmaId)
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, a.usuario_id
                                 FROM aluno a
                                 INNER JOIN aluno_turma at ON a.aluno_id = at.aluno_id
                                 WHERE at.turma_id = @turma_id
                                 ORDER BY a.aluno_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@turma_id", turmaId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                            UsuarioId = reader.IsDBNull(reader.GetOrdinal("usuario_id")) ? 0 : reader.GetInt32("usuario_id")
                        };
                        result.Add(aluno);
                    }
                }
            }

            return result;
        }
    }
}