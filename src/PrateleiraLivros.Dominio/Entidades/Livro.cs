using Ardalis.GuardClauses;
using PrateleiraLivros.Dominio.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrateleiraLivros.Dominio.Entidades
{
    public class Livro : Entity, IAggregateRoot
    {
        public Livro() { }

        public Livro(string titulo, string descricao, decimal valor, long iSBN_10, int edicao,
            DateTime dataPublicacao, string idioma, int numeroPaginas, Guid editoraId, Guid autorId)
        {
            Guard.Against.NullOrEmpty(titulo, nameof(Titulo));
            Guard.Against.NullOrEmpty(descricao, nameof(Descricao));
            Guard.Against.NegativeOrZero(valor, nameof(Valor));
            Guard.Against.OutOfRange(dataPublicacao, nameof(DataPublicacao), DateTime.Parse("01/01/1990 00:00:00"), DateTime.Now);
            Guard.Against.NullOrEmpty(editoraId, nameof(EditoraId));
            Guard.Against.NullOrEmpty(autorId, nameof(AutorId));

            Titulo = titulo;
            Descricao = descricao;
            Valor = valor;
            ISBN_10 = iSBN_10;
            Edicao = edicao;
            DataPublicacao = dataPublicacao;
            Idioma = idioma;
            NumeroPaginas = numeroPaginas;
            EditoraId = editoraId;
            AutorId = autorId;
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public long ISBN_10 { get; set; }
        public int Edicao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Idioma { get; set; }
        public int NumeroPaginas { get; set; }
        public Guid EditoraId { get; set; }
        public Guid AutorId { get; set; }

        [ForeignKey(nameof(EditoraId))]
        public virtual Editora Editora { get; private set; }
        
        [ForeignKey(nameof(AutorId))]
        public virtual Autor Autor { get; private set; }
    }
}
