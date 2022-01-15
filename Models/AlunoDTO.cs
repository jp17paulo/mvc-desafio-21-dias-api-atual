using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
    public partial class Aluno
    {
        #region Metodos de classe ou staticos
        private static string connectionString()
        {
            // create database desafio21diasapi
            /*
              CREATE TABLE Alunos (
                  id int IDENTITY(1,1) PRIMARY KEY,
                  nome varchar(150) NOT NULL,
                  matricula varchar(15) NOT NULL,
                  notas varchar(255)
              );
            */
            return "Server=DESKTOP-U62OFFR;database=desafio21diasapi;user=sa;password=jp17paulo";
        }

        public static void Incluir(Aluno aluno)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString()))
            {
                sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand($"insert into alunos(nome, matricula, notas)values(@nome, @matricula, @notas)", sqlConn);
                sqlCommand.Parameters.Add("@nome", SqlDbType.VarChar);
                sqlCommand.Parameters["@nome"].Value = aluno.Nome;

                sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
                sqlCommand.Parameters.AddWithValue("@notas", string.Join(",", aluno.Notas.ToArray()));
                sqlCommand.ExecuteNonQuery();

                sqlConn.Close();
            }
        }
        public static void Atualizar(Aluno aluno)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand =
                  new SqlCommand(
                    $"update alunos set nome=@nome, matricula=@matricula, notas=@notas where id=@id",
                         sqlConn);

            //AddWithValue não precisa colocar o tipo do parametro
            sqlCommand.Parameters.AddWithValue("@id", aluno.Id);

            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);

            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);

            sqlCommand.Parameters.AddWithValue("@notas", aluno.notasEditar);

            //Não espera retorno
            sqlCommand.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public static void ApagarPorId(int id)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"delete from alunos where id={id}", sqlConn);
            sqlCommand.ExecuteNonQuery();

            sqlConn.Close();
            sqlConn.Dispose();
        }

        public static List<Aluno> Todos()
        {
            var alunos = new List<Aluno>();

            //Cria a conexão
            SqlConnection sqlConn = new SqlConnection(connectionString());

            //Abri a conexão
            sqlConn.Open();

            //Executa um comando sql
            SqlCommand sqlCommand = new SqlCommand("select * from alunos", sqlConn);

            //Recebe as linhas referentes ao comando
            var reader = sqlCommand.ExecuteReader();

            //Enquanto houver linha
            while (reader.Read())
            {
                var notas = new List<string>();
                string strNotas = reader["notas"].ToString();

                //Cria um array de notas, já que no banco estão em apenas um campo
                foreach (var nota in strNotas.Split(','))
                {
                    //Adiciona as notas retornadas na consulta
                    notas.Add(nota);
                }

                var aluno = new Aluno()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Notas = notas,
                };

                alunos.Add(aluno);
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return alunos;
        }
        public static Aluno BuscarPorId(int id)
        {
            var aluno = new Aluno();

            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"select * from alunos where id={id}", sqlConn);
            var reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                var notasEditar = new List<string>();
                string strNotas = reader["notas"].ToString();
                foreach (var nota in strNotas.Split(','))
                {
                    notasEditar.Add(nota);
                }

                aluno = new Aluno()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    notasEditar = reader["notas"].ToString(),
                };

            }

            sqlConn.Close();
            sqlConn.Dispose();
            return aluno;
        }
        #endregion

    }
}