using Bogus;
using PrateleiraLivros.Dominio.Entidades;
using System;
using Xunit;

namespace PrateleiraLivros.UnitTest.Dominio.Entidades
{
    [Collection("Entidades")]
    public class AutorTeste
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Autor _autorFake;

        public AutorTeste()
        {
            _autorFake = new Autor
            {
                Nome = _faker.Person.FirstName,
                Sobrenome = _faker.Person.LastName,
                Email = _faker.Person.Email
            };
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Nome')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Nome was empty. (Parameter 'Nome')", typeof(ArgumentException))]
        public void ConstruirAutor_DisparaExceptionSeAutorTiverNomeNuloOuVazio(string nomeInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var autor = new Autor(nomeInvalido,
                    _autorFake.Sobrenome, _autorFake.Email);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Sobrenome')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Sobrenome was empty. (Parameter 'Sobrenome')", typeof(ArgumentException))]
        public void ConstruirAutor_DisparaExceptionSeAutorTiverSobrenomeNuloOuVazio(string sobrenomeInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var autor = new Autor(_autorFake.Nome,
                    sobrenomeInvalido, _autorFake.Email);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Theory]
        [InlineData(null, "Value cannot be null. (Parameter 'Email')", typeof(ArgumentNullException))]
        [InlineData("", "Required input Email was empty. (Parameter 'Email')", typeof(ArgumentException))]
        public void ConstruirAutor_DisparaExceptionSeAutorTiverEmailNuloOuVazio(string emailInvalido, string mensagemErroEsperada, Type tipoExcessaoEsperado)
        {
            var mensagemErro = Assert.Throws(tipoExcessaoEsperado, () =>
            {
                var autor = new Autor(_autorFake.Nome,
                    _autorFake.Sobrenome, emailInvalido);
            }).Message;

            Assert.Equal(mensagemErroEsperada, mensagemErro);
        }

        [Fact]
        public void ConstruirAutor_SucessoNaConstruicaoDoAutor()
        {
            var autor = new Autor(_autorFake.Nome,
                _autorFake.Sobrenome, _autorFake.Email);

            Assert.NotNull(autor);
            Assert.Equal(_autorFake.Nome, autor.Nome);
            Assert.Equal(_autorFake.Sobrenome, autor.Sobrenome);
            Assert.Equal(_autorFake.Email, autor.Email);
        }
    }
}
