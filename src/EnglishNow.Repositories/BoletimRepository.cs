using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;

namespace EnglishNow.Repositories
{
    public interface IBoletimRepository
    {
        int? Inserir(Boletim boletim);

        int? Atualizar(Boletim boletim);

        int? Apagar(int id);

        Boletim? ObterPorId(int id);

        bool Existe(int alunoId, int turmaId);

        IList<Boletim> ListarPorTurma(int turmaId);

        IList<Boletim> ListarPorAluno(int alunoId);

        int ContarPorTurma(int turmaId);

        int ContarPorAluno(int alunoId);
    }

    public class BoletimRepository : BaseRepository, IBoletimRepository
    {
        private const string SelectBase = @"SELECT 
                b.aluno_turma_boletim_id, 
                b.aluno_id, 
                b.turma_id, 
                b.nota_bim1_escrita, 
                b.nota_bim1_leitura, 
                b.nota_bim1_conversacao, 
                b.nota_bim1_final, 
                b.nota_bim2_escrita, 
                b.nota_bim2_leitura, 
                b.nota_bim2_conversacao, 
                b.nota_bim2_final, 
                b.nota_final_semestre, 
                b.faltas_semestre, 
                a.nome AS aluno_nome, 
                t.semestre, 
                t.ano, 
                t.periodo, 
                t.nivel, 
                t.professor_id, 
                p.nome AS professor_nome 
            FROM aluno_turma_boletim b 
            INNER JOIN aluno a ON b.aluno_id = a.aluno_id 
            INNER JOIN turma t ON b.turma_id = t.turma_id 
            INNER JOIN professor p ON t.professor_id = p.professor_id";

        public BoletimRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Boletim boletim)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO aluno_turma_boletim (aluno_id, turma_id) VALUES (@aluno_id, @turma_id); 
                                SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@aluno_id", boletim.AlunoId);
                cmd.Parameters.AddWithValue("@turma_id", boletim.TurmaId);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int? Atualizar(Boletim boletim)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"UPDATE aluno_turma_boletim SET 
                                    nota_bim1_escrita = @nota_bim1_escrita, 
                                    nota_bim1_leitura = @nota_bim1_leitura, 
                                    nota_bim1_conversacao = @nota_bim1_conversacao, 
                                    nota_bim1_final = @nota_bim1_final, 
                                    nota_bim2_escrita = @nota_bim2_escrita, 
                                    nota_bim2_leitura = @nota_bim2_leitura, 
                                    nota_bim2_conversacao = @nota_bim2_conversacao, 
                                    nota_bim2_final = @nota_bim2_final, 
                                    nota_final_semestre = @nota_final_semestre, 
                                    faltas_semestre = @faltas_semestre 
                                WHERE aluno_turma_boletim_id = @id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nota_bim1_escrita", Valor(boletim.NotaBim1Escrita));
                cmd.Parameters.AddWithValue("@nota_bim1_leitura", Valor(boletim.NotaBim1Leitura));
                cmd.Parameters.AddWithValue("@nota_bim1_conversacao", Valor(boletim.NotaBim1Conversacao));
                cmd.Parameters.AddWithValue("@nota_bim1_final", Valor(boletim.NotaBim1Final));
                cmd.Parameters.AddWithValue("@nota_bim2_escrita", Valor(boletim.NotaBim2Escrita));
                cmd.Parameters.AddWithValue("@nota_bim2_leitura", Valor(boletim.NotaBim2Leitura));
                cmd.Parameters.AddWithValue("@nota_bim2_conversacao", Valor(boletim.NotaBim2Conversacao));
                cmd.Parameters.AddWithValue("@nota_bim2_final", Valor(boletim.NotaBim2Final));
                cmd.Parameters.AddWithValue("@nota_final_semestre", Valor(boletim.NotaFinalSemestre));
                cmd.Parameters.AddWithValue("@faltas_semestre", Valor(boletim.FaltasSemestre));
                cmd.Parameters.AddWithValue("@id", boletim.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand("DELETE FROM aluno_turma_boletim WHERE aluno_turma_boletim_id = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public Boletim? ObterPorId(int id)
        {
            return Listar("WHERE b.aluno_turma_boletim_id = @valor", id).FirstOrDefault();
        }

        public bool Existe(int alunoId, int turmaId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM aluno_turma_boletim WHERE aluno_id = @aluno_id AND turma_id = @turma_id", conn);

                cmd.Parameters.AddWithValue("@aluno_id", alunoId);
                cmd.Parameters.AddWithValue("@turma_id", turmaId);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public IList<Boletim> ListarPorTurma(int turmaId)
        {
            return Listar("WHERE b.turma_id = @valor ORDER BY a.nome", turmaId);
        }

        public IList<Boletim> ListarPorAluno(int alunoId)
        {
            return Listar("WHERE b.aluno_id = @valor ORDER BY t.ano DESC, t.semestre DESC, t.nivel", alunoId);
        }

        public int ContarPorTurma(int turmaId)
        {
            return Contar("turma_id", turmaId);
        }

        public int ContarPorAluno(int alunoId)
        {
            return Contar("aluno_id", alunoId);
        }

        private int Contar(string coluna, int valor)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                // "coluna" vem sempre de uma constante deste arquivo, nunca do usuário.
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM aluno_turma_boletim WHERE " + coluna + " = @valor", conn);

                cmd.Parameters.AddWithValue("@valor", valor);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private IList<Boletim> Listar(string where, int valor)
        {
            var result = new List<Boletim>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var cmd = new MySqlCommand(SelectBase + " " + where, conn);

                cmd.Parameters.AddWithValue("@valor", valor);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Boletim
                        {
                            Id = reader.GetInt32("aluno_turma_boletim_id"),
                            AlunoId = reader.GetInt32("aluno_id"),
                            TurmaId = reader.GetInt32("turma_id"),
                            AlunoNome = reader.GetString("aluno_nome"),
                            NotaBim1Escrita = LerDecimal(reader, "nota_bim1_escrita"),
                            NotaBim1Leitura = LerDecimal(reader, "nota_bim1_leitura"),
                            NotaBim1Conversacao = LerDecimal(reader, "nota_bim1_conversacao"),
                            NotaBim1Final = LerDecimal(reader, "nota_bim1_final"),
                            NotaBim2Escrita = LerDecimal(reader, "nota_bim2_escrita"),
                            NotaBim2Leitura = LerDecimal(reader, "nota_bim2_leitura"),
                            NotaBim2Conversacao = LerDecimal(reader, "nota_bim2_conversacao"),
                            NotaBim2Final = LerDecimal(reader, "nota_bim2_final"),
                            NotaFinalSemestre = LerDecimal(reader, "nota_final_semestre"),
                            FaltasSemestre = LerInt(reader, "faltas_semestre"),
                            Turma = new Turma
                            {
                                Id = reader.GetInt32("turma_id"),
                                Semestre = reader.GetInt32("semestre"),
                                Ano = reader.GetInt32("ano"),
                                Periodo = reader.GetString("periodo"),
                                Nivel = reader.GetString("nivel"),
                                ProfessorId = reader.GetInt32("professor_id"),
                                ProfessorNome = reader.GetString("professor_nome")
                            }
                        });
                    }
                }
            }

            return result;
        }

        // Colunas de nota/faltas aceitam NULL (ainda não lançado).
        private static decimal? LerDecimal(MySqlDataReader reader, string coluna)
        {
            var indice = reader.GetOrdinal(coluna);

            return reader.IsDBNull(indice) ? null : reader.GetDecimal(indice);
        }

        private static int? LerInt(MySqlDataReader reader, string coluna)
        {
            var indice = reader.GetOrdinal(coluna);

            return reader.IsDBNull(indice) ? null : reader.GetInt32(indice);
        }

        private static object Valor(object? valor)
        {
            return valor ?? DBNull.Value;
        }
    }
}
