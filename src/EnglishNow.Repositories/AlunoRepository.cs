using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNow.Repositories
{
    public interface IAlunoRepository
    {
        int? Inserir(Aluno aluno);

        int? Atualizar(Aluno aluno);

        int? Apagar(int id);

        IList<Aluno> Listar();

        Aluno? ObterPorId(int id);

        Aluno? ObterPorUsuarioId(int usuarioId);
    }

    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        private const string SelectBase = @"SELECT 
                p.aluno_id, 
                p.nome, 
                p.email, 
                u.usuario_id, 
                u.login, 
                u.senha 
            FROM aluno p 
            INNER JOIN usuario u 
            ON p.usuario_id = u.usuario_id";

        public AlunoRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Aluno aluno)
        {
            int? alunoId = null;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO aluno (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id); 
                                SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email);
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
                var query = "UPDATE aluno SET nome = @nome, email = @email WHERE aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email);
                cmd.Parameters.AddWithValue("@aluno_id", aluno.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public IList<Aluno> Listar()
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand(SelectBase + " ORDER BY p.aluno_id", conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Mapear(reader));
                    }
                }
            }

            return result;
        }

        public Aluno? ObterPorId(int id)
        {
            return ObterUm("WHERE p.aluno_id = @valor", id);
        }

        public Aluno? ObterPorUsuarioId(int usuarioId)
        {
            return ObterUm("WHERE p.usuario_id = @valor", usuarioId);
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

        private Aluno? ObterUm(string where, int valor)
        {
            Aluno? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand(SelectBase + " " + where, conn);

                cmd.Parameters.AddWithValue("@valor", valor);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = Mapear(reader);
                    }
                }
            }

            return result;
        }

        private static Aluno Mapear(MySqlDataReader reader)
        {
            return new Aluno
            {
                Id = reader.GetInt32("aluno_id"),
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
