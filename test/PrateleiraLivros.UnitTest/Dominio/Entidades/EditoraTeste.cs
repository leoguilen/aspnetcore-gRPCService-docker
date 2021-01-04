using Bogus;
using PrateleiraLivros.Dominio.Entidades;
using System;
using Xunit;

namespace PrateleiraLivros.UnitTest.Dominio.Entidades
{
    [Collection("Entidades")]
    public class EditoraTeste
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Editora _editoraFake;

        public EditoraTeste()
        {
            _editoraFake = new Editora
            {
                Nome = _faker.Company.CompanyName(),
                SiteURL = _faker.Internet.Url(),
                Endereco = _faker.Address.FullAddress(),
                Livros = null
            };
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Nome')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Nome was empty. (Parameter 'Nome')", typeof(ArgumentException))]
        public void ConstruirEditora_DisparaExceptionSeEditoraTiverNomeNuloOuVazio(string nomeInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var editora = new Editora(nomeInvalido,
                    _editoraFake.SiteURL, _editoraFake.Endereco);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Endereco')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Endereco was empty. (Parameter 'Endereco')", typeof(ArgumentException))]
        public void ConstruirEditora_DisparaExceptionSeEditoraTiverEnderecoNuloOuVazio(string enderecoInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var editora = new Editora(_editoraFake.Nome,
                    _editoraFake.SiteURL, enderecoInvalido);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Fact]
        public void ConstruirEditora_SucessoNaConstruicaoDaEditora()
        {
            var editora = new Editora(_editoraFake.Nome,
                _editoraFake.SiteURL, _editoraFake.Endereco);

            Assert.NotNull(editora);
            Assert.Equal(_editoraFake.Nome, editora.Nome);
            Assert.Equal(_editoraFake.SiteURL, editora.SiteURL);
            Assert.Equal(_editoraFake.Endereco, editora.Endereco);
        }
    }
}
