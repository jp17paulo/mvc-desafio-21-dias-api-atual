using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
  public partial class Aluno
    {
        #region "Propriedades"
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }

        //Criação de atributo para resolver o problema da edição, onde não estava salvando as notas após alteração
        public string notasEditar { get; set; }
        private List<string> notas;     
        public List<string> Notas
        {
            get
            {
                if (this.notas == null) this.notas = new List<string>();
                return this.notas;
            }
            set
            {
                this.notas = value;
            }
        }

        #endregion

        #region Metodos de instancia
        public string StrNotas()
        {
            return string.Join(", ", this.Notas.ToArray());
        }

        public void AtualizaNotas()
        {
            foreach (var nota in notasEditar.Split(','))
            {
                Notas.Add(nota);
            }
        }
        
        public double CalcularMedia()
        {
            var somaNotas = 0.0;
            foreach (var nota in this.Notas)
            {
                somaNotas += Convert.ToDouble(nota);
            }
            return somaNotas / this.Notas.Count;
        }

        public string Situacao()
        {
            return this.CalcularMedia() >= 7 ? "Aprovado" : "Reprovado";
        }

        public void Apagar()
        {
            Aluno.ApagarPorId(this.Id);
        }

        public void Salvar()
        {
            if (this.Id > 0)
            {
                Aluno.Atualizar(this);
            }
            else
            {
                Aluno.Incluir(this);
            }
        }

        #endregion
    }
}