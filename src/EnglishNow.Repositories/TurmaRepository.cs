using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNow.Repositories
{
    public interface ITurmaRepository
    {
        int? Inserir(Turma turma);

        int? Atualizar(Turma turma);

        int? Apagar(int id);

        IList<Turma> Listar();

        IList<Turma> ListarPorProfessor(int professorId);

        Turma? ObterPorId(int id);

        int ContarPorProfessor(int professorId);
    }

    public class TurmaRepository : BaseRepository, ITurmaRepository
    {
        private const string SelectBase = @"SELECT 
                t.turma_id, 
                t.semestre, 
                t.ano, 
                t.periodo, 
                t.nivel, 
                t.professor_id, 
                p.nome AS professor_nome 
            FROM turma t 
            INNER JOIN professor p 
            ON t.professor_id = p.professor_id";

        private const string Ordenacao = " ORDER BY t.ano DESC, t.semestre DESC, t.nivel, t.periodo";

        public TurmaRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Turma turma)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO turma (semestre, ano, periodo, nivel, professor_id) 
                                VALUES (@semestre, @ano, @periodo, @nivel, @professor_id); 
                                SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@semestre", turma.Semestre);
                cmd.Parameters.AddWithValue("@ano", turma.Ano);
                cmd.Parameters.AddWithValue("@periodo", turma.Periodo);
                cmd.Parameters.AddWithValue("@nivel", turma.Nivel);
                cmd.Parameters.AddWithValue("@professor_id", turma.ProfessorId);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int? Atualizar(Turma turma)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"UPDATE turma SET 
                                    semestre = @semestre, 
                                    ano = @ano, 
                                    periodo = @periodo, 
                                    nivel = @nivel, 
                                    professor_id = @professor_id 
                                WHERE turma_id = @turma_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@semestre", turma.Semestre);
                cmd.Parameters.AddWithValue("@ano", turma.Ano);
                cmd.Parameters.AddWithValue("@periodo", turma.Periodo);
                cmd.Parameters.AddWithValue("@nivel", turma.Nivel);
                cmd.Parameters.AddWithValue("@professor_id", turma.ProfessorId);
                cmd.Parameters.AddWithValue("@turma_id", turma.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand("DELETE FROM turma WHERE turma_id = @turma_id", conn);

                cmd.Parameters.AddWithValue("@turma_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public IList<Turma> Listar()
        {
            return ListarComFiltro(string.Empty, null);
        }

        public IList<Turma> ListarPorProfessor(int professorId)
        {
            return ListarComFiltro("WHERE t.professor_id = @valor", professorId);
        }

        public Turma? ObterPorId(int id)
        {
            return ListarComFiltro("WHERE t.turma_id = @valor", id).FirstOrDefault();
        }

        public int ContarPorProfessor(int professorId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM turma WHERE professor_id = @professor_id", conn);

                cmd.Parameters.AddWithValue("@professor_id", professorId);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private IList<Turma> ListarComFiltro(string where, int? valor)
        {
            var result = new List<Turma>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand(SelectBase + " " + where + Ordenacao, conn);

                if (valor.HasValue)
                {
                    cmd.Parameters.AddWithValue("@valor", valor.Value);
                }

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Turma
                        {
                            Id = reader.GetInt32("turma_id"),
                            Semestre = reader.GetInt32("semestre"),
                            Ano = reader.GetInt32("ano"),
                            Periodo = reader.GetString("periodo"),
                            Nivel = reader.GetString("nivel"),
                            ProfessorId = reader.GetInt32("professor_id"),
                            ProfessorNome = reader.GetString("professor_nome")
                        });
                    }
                }
            }

            return result;
        }
    }
}
