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
    public class AutorComLivrosSpecificationTeste
    {
        private readonly Guid _autorId = Guid.Parse("E334A11F-FFA5-441C-A737-8A6CF54034C0");
        private readonly SpecificationEvaluator<Autor> _evaluator = new SpecificationEvaluator<Autor>();

        // Autores mocados para teste
        private readonly IQueryable<Autor> _autorMock = DadosTesteMock.GetColecaoAutoresTeste().AsQueryable();

        [Fact]
        public void BuscarAutor_NuloSeNaoExistirAutorComIdEspecificado()
        {
            var idInvalido = Guid.NewGuid();

            var spec = new AutorComLivrosSpecification(idInvalido);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void BuscarAutor_RetornaAutorComIdEspecificado()
        {
            var spec = new AutorComLivrosSpecification(_autorId);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_autorId, result.Id);
        }

        [Fact]
        public void BuscarAutor_RetornaListaComAutoresSeIdNaoForEspecificado()
        {
            var spec = new AutorComLivrosSpecification();
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .ToList();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
    }
}
