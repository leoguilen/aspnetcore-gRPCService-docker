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
    public class LivroComEditoraEAutorSpecificationTeste
    {
        private readonly Guid _livroId = Guid.Parse("4BCC69BF-1F85-48CA-9F4E-FFDB4588DAE4");
        private readonly SpecificationEvaluator<Livro> _evaluator = new SpecificationEvaluator<Livro>();

        // Autores mocados para teste
        private readonly IQueryable<Livro> _autorMock = DadosTesteMock.GetColecaoLivrosTeste().AsQueryable();

        [Fact]
        public void BuscarLivro_NuloSeNaoExistirLivroComIdEspecificado()
        {
            var idInvalido = Guid.NewGuid();

            var spec = new LivroComEditoraEAutorSpecification(idInvalido);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void BuscarLivro_RetornaLivroComIdEspecificado()
        {
            var spec = new LivroComEditoraEAutorSpecification(_livroId);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_livroId, result.Id);
        }

        [Fact]
        public void BuscarLivro_RetornaListaComLivrosSeIdNaoForEspecificado()
        {
            var spec = new LivroComEditoraEAutorSpecification();
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .ToList();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
    }
}
