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
    public class EditoraComLivrosSpecificationTeste
    {
        private readonly Guid _editoraId = Guid.Parse("8386ED5D-9467-42BB-AD87-A1E6D31CA7FD");
        private readonly SpecificationEvaluator<Editora> _evaluator = new SpecificationEvaluator<Editora>();

        // Editoras mocadas para teste
        private readonly IQueryable<Editora> _autorMock = DadosTesteMock.GetColecaoEditorasTeste().AsQueryable();

        [Fact]
        public void BuscarEditora_NuloSeNaoExistirEditoraComIdEspecificado()
        {
            var idInvalido = Guid.NewGuid();

            var spec = new EditoraComLivrosSpecification(idInvalido);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void BuscarEditora_RetornaEditoraComIdEspecificado()
        {
            var spec = new EditoraComLivrosSpecification(_editoraId);
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_editoraId, result.Id);
        }

        [Fact]
        public void BuscarEditora_RetornaListaComEditorasSeIdNaoForEspecificado()
        {
            var spec = new EditoraComLivrosSpecification();
            var result = _evaluator
                .GetQuery(_autorMock, spec)
                .ToList();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
    }
}
