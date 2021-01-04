using Ardalis.GuardClauses;
using PrateleiraLivros.Dominio.Interfaces;
using System.Collections.Generic;

namespace PrateleiraLivros.Dominio.Entidades
{
    public class Autor : Entity, IAggregateRoot
    {
        public Autor() { }

        public Autor(string nome, string sobrenome, string email, string bio = "", string avatar = "")
        {
            Guard.Against.NullOrEmpty(nome, nameof(Nome));
            Guard.Against.NullOrEmpty(sobrenome, nameof(Sobrenome));
            Guard.Against.NullOrEmpty(email, nameof(Email));

            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Bio = bio;
            Avatar = avatar;
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }
}
