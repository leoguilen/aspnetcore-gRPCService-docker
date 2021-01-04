using Ardalis.Specification.EntityFrameworkCore;
using PrateleiraLivros.Dominio.Entidades;
using PrateleiraLivros.Dominio.Specifications;
using PrateleiraLivros.UnitTest.__mock__;
using System;
using System.Linq;
using Xunit;

namespace PrateleiraLivros.UnitTest.Dominio.Specifications
{
    [Collection("Specifications")]
    public class LivroFilterSpecificationTeste
    {
        private readonly Guid _editoraId = Guid.Parse("F9A17D52-E0E1-40C3-8204-C95A0265D18C");
        private readonly Guid _autorId = Guid.Parse("2828097C-CA3B-498E-9E54-21BB073757F0");
        private readonly string _titulo = "hábito";
        private readonly string _idioma = "Português";
        private readonly SpecificationEvaluator<Livro> _evaluator = new SpecificationEvaluator<Livro>();

        // Autores mocados para teste
        private readonly IQueryable<Livro> _autorMock = DadosTesteMock.GetColecaoLivrosTeste().AsQueryable();

        [Fact]
        public void BuscarLivro_RetornaLivroComEditoraIdEspecificado()
        {
            var spec = new LivroFilterSpecification(_editoraId, null);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_editoraId, result.EditoraId);
        }

        [Fact]
        public void BuscarLivro_RetornaLivroComAutorIdEspecificado()
        {
            var spec = new LivroFilterSpecification(null, _autorId);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_autorId, result.AutorId);
        }

        [Fact]
        public void BuscarLivro_RetornaLivroPelaPesquisaPorTituloEspecificado()
        {
            var spec = new LivroFilterSpecification(null, null, titulo: _titulo);
            var result = _evaluator
                .GetQuery(_autorMock, spec);

            Assert.NotNull(result);
            Assert.All(result, r => r.Titulo.Contains(_titulo));
        }

        [Fact]
        public void BuscarLivro_RetornaLivroPelaPesquisaPorIdiomaEspecificado()
        {
            var spec = new LivroFilterSpecification(null, null, idioma: _idioma);
            var result = _evaluator
                .GetQuery(_autorMock, spec);

            Assert.NotNull(result);
            Assert.All(result, r => r.Idioma.Contains(_idioma));
        }
    }
}
