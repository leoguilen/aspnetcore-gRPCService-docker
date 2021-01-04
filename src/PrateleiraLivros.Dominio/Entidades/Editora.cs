using Ardalis.GuardClauses;
using PrateleiraLivros.Dominio.Interfaces;
using System.Collections.Generic;

namespace PrateleiraLivros.Dominio.Entidades
{
    public class Editora : Entity, IAggregateRoot
    {
        public Editora() { }

        public Editora(string nome, string siteURL, string endereco)
        {
            Guard.Against.NullOrEmpty(nome, nameof(Nome));
            Guard.Against.NullOrEmpty(endereco, nameof(Endereco));

            Nome = nome;
            SiteURL = siteURL;
            Endereco = endereco;
        }

        public string Nome { get; set; }
        public string SiteURL { get; set; }
        public string Endereco { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }
}
