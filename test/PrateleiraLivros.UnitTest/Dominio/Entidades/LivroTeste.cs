using Bogus;
using PrateleiraLivros.Dominio.Entidades;
using System;
using Xunit;

namespace PrateleiraLivros.UnitTest.Dominio.Entidades
{
    [Collection("Entidades")]
    public class LivroTeste
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Livro _livroFake;

        public LivroTeste()
        {
            _livroFake = new Livro
            {
                Titulo = _faker.Lorem.Lines(1),
                Descricao = _faker.Lorem.Text(),
                Valor = _faker.Random.Decimal(10, 30),
                ISBN_10 = _faker.Random.Long(99999),
                Edicao = _faker.Random.Int(1,5),
                DataPublicacao = _faker.Date.Recent(),
                Idioma = _faker.Random.String2(12),
                NumeroPaginas = _faker.Random.Int(120, 350),
                EditoraId = _faker.Random.Guid(),
                AutorId = _faker.Random.Guid()
            };
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Titulo')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Titulo was empty. (Parameter 'Titulo')", typeof(ArgumentException))]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverTituloNuloOuVazio(string tituloInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var livro = new Livro(tituloInvalido, _livroFake.Descricao,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataAtualizacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, _livroFake.AutorId);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Descricao')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Descricao was empty. (Parameter 'Descricao')", typeof(ArgumentException))]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverDescricaoNuloOuVazio(string descricaoInvalida, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var livro = new Livro(_livroFake.Titulo, descricaoInvalida,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataAtualizacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, _livroFake.AutorId);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Fact]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverValorNegativoOuZerado()
        {
            var mensagemErro = Assert.Throws<ArgumentException>("Valor", () =>
            {
                var livro = new Livro(_livroFake.Titulo, _livroFake.Descricao,
                    _faker.Random.Decimal(-30,0), _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataAtualizacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, _livroFake.AutorId);
            }).Message;

            Assert.Equal("Required input Valor cannot be zero or negative. (Parameter 'Valor')", mensagemErro);
        }

        [Fact]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverDataDePublicacaoMuitoAntiga()
        {
            var dataPublicacaoInvalida = _faker.Date.Between(
                DateTime.Parse("01/01/1900 00:00:00"), 
                DateTime.Parse("01/01/1990 00:00:00"));

            var mensagemErro = Assert.Throws<ArgumentOutOfRangeException>("DataPublicacao", () =>
            {
                var livro = new Livro(_livroFake.Titulo, _livroFake.Descricao,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    dataPublicacaoInvalida, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, _livroFake.AutorId);
            }).Message;

            Assert.Equal("Input DataPublicacao was out of range (Parameter 'DataPublicacao')", mensagemErro);
        }

        [Fact]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverEditoraIdNuloOuVazio()
        {
            var mensagemErro = Assert.Throws<ArgumentException>("EditoraId", () =>
            {
                var livro = new Livro(_livroFake.Titulo, _livroFake.Descricao,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataPublicacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, Guid.Empty, _livroFake.AutorId);
            }).Message;

            Assert.Equal("Required input EditoraId was empty. (Parameter 'EditoraId')", mensagemErro);
        }

        [Fact]
        public void ConstruirLivro_DisparaExceptionSeLivroTiverAutorIdNuloOuVazio()
        {
            var mensagemErro = Assert.Throws<ArgumentException>("AutorId", () =>
            {
                var livro = new Livro(_livroFake.Titulo, _livroFake.Descricao,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataPublicacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, Guid.Empty);
            }).Message;

            Assert.Equal("Required input AutorId was empty. (Parameter 'AutorId')", mensagemErro);
        }

        [Fact]
        public void ConstruirLivro_SucessoNaConstruicaoDoLivro()
        {
            var livro = new Livro(_livroFake.Titulo, _livroFake.Descricao,
                    _livroFake.Valor, _livroFake.ISBN_10, _livroFake.Edicao,
                    _livroFake.DataPublicacao, _livroFake.Idioma,
                    _livroFake.NumeroPaginas, _livroFake.EditoraId, _livroFake.AutorId);

            Assert.NotNull(livro);
            Assert.Equal(_livroFake.Titulo, livro.Titulo);
            Assert.Equal(_livroFake.Descricao, livro.Descricao);
            Assert.Equal(_livroFake.DataPublicacao, livro.DataPublicacao);
        }

    }
}
