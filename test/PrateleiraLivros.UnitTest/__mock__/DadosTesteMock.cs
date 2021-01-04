using Bogus;
using PrateleiraLivros.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace PrateleiraLivros.UnitTest.__mock__
{
    public static class DadosTesteMock
    {
        private const string _defaultLocale = "pt_BR";
        private const int _countToGenerate = 3;

        public static List<Autor> GetColecaoAutoresTeste()
        {
            var ids = new List<Guid> { Guid.Parse("E334A11F-FFA5-441C-A737-8A6CF54034C0"), Guid.NewGuid() };
            var faker = new Faker<Autor>(_defaultLocale)
                .StrictMode(true)
                .Ignore(autor => autor.Livros)
                .Ignore(autor => autor.DataAtualizacao)
                .Ignore(autor => autor.DataCadastro)
                .RuleFor(autor => autor.Id, faker => faker.PickRandom(ids))
                .RuleFor(autor => autor.Nome, faker => faker.Person.FirstName)
                .RuleFor(autor => autor.Sobrenome, faker => faker.Person.LastName)
                .RuleFor(autor => autor.Email, faker => faker.Person.Email)
                .RuleFor(autor => autor.Bio, faker => faker.Lorem.Text())
                .RuleFor(autor => autor.Avatar, faker => faker.Person.Avatar);

            return faker.Generate(_countToGenerate);
        }

        public static List<Editora> GetColecaoEditorasTeste()
        {
            var ids = new List<Guid> { Guid.Parse("8386ED5D-9467-42BB-AD87-A1E6D31CA7FD"), Guid.NewGuid() };
            var faker = new Faker<Editora>(_defaultLocale)
                .StrictMode(true)
                .Ignore(editora => editora.Livros)
                .Ignore(editora => editora.DataAtualizacao)
                .Ignore(editora => editora.DataCadastro)
                .RuleFor(editora => editora.Id, faker => faker.PickRandom(ids))
                .RuleFor(editora => editora.Nome, faker => faker.Company.CompanyName())
                .RuleFor(editora => editora.SiteURL, faker => faker.Internet.Url())
                .RuleFor(editora => editora.Endereco, faker => faker.Address.FullAddress());

            return faker.Generate(_countToGenerate);
        }

        public static List<Livro> GetColecaoLivrosTeste()
        {
            var ids = new List<Guid> { Guid.Parse("4BCC69BF-1F85-48CA-9F4E-FFDB4588DAE4"), Guid.NewGuid() };
            var editoraIds = new List<Guid> { Guid.Parse("F9A17D52-E0E1-40C3-8204-C95A0265D18C"), Guid.NewGuid() };
            var autorIds = new List<Guid> { Guid.Parse("2828097C-CA3B-498E-9E54-21BB073757F0"), Guid.NewGuid() };
            var faker = new Faker<Livro>(_defaultLocale)
                .StrictMode(true)
                .Ignore(livro => livro.Editora)
                .Ignore(livro => livro.Autor)
                .Ignore(livro => livro.DataCadastro)
                .Ignore(livro => livro.DataAtualizacao)
                .RuleFor(livro => livro.Id, faker => faker.PickRandom(ids))
                .RuleFor(livro => livro.Titulo, faker => faker.PickRandom(new[] { "O poder do hábito", "Drácula" }))
                .RuleFor(livro => livro.Idioma, faker => faker.PickRandom(new[] { "Português", "Inglês" }))
                .RuleFor(livro => livro.Descricao, faker => faker.Lorem.Text())
                .RuleFor(livro => livro.Valor, faker => faker.Random.Decimal(15, 30))
                .RuleFor(livro => livro.ISBN_10, faker => faker.Random.Long(99999))
                .RuleFor(livro => livro.Edicao, faker => faker.Random.Int(1, 5))
                .RuleFor(livro => livro.DataPublicacao, faker => faker.Date.Recent())
                .RuleFor(livro => livro.NumeroPaginas, faker => faker.Random.Int(150, 350))
                .RuleFor(livro => livro.EditoraId, faker => faker.PickRandom(editoraIds))
                .RuleFor(livro => livro.AutorId, faker => faker.PickRandom(autorIds));

            return faker.Generate(_countToGenerate);
        }
    }
}
