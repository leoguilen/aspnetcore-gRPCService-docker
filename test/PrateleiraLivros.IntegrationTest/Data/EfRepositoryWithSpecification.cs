using FluentAssertions;
using PrateleiraLivros.Dominio.Entidades;
using PrateleiraLivros.Dominio.Specifications;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.IntegrationTest.Data
{
    [Collection("Repositorio")]
    public class EfRepositoryWithSpecification : BaseEfRepoTestFixture<Editora>
    {
        [Fact]
        public async Task QuantidadeDeEntidadesComCondicaoDaEspecificacaoNoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository(true);
            var id = DbContext.Editoras.First().Id;

            var spec = new EditoraComLivrosSpecification(id);
            var result = await repositorio.CountAsync(spec);

            result.Should().Be(1);
        }

        [Fact]
        public async Task ListarTodasEntidadesComEspecificacaoNoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository(true);

            var spec = new EditoraComLivrosSpecification();
            var itens = await repositorio.ListAsync(spec);

            itens.Should().SatisfyRespectively(
                item1 => 
                {
                    item1.Nome.Should().Be("Escala");
                    item1.Livros.Should().NotBeNull();
                },
                item2 => 
                {
                    item2.Nome.Should().Be("Alta Books");
                    item2.Livros.Should().NotBeNull();
                },
                item3 => 
                { 
                    item3.Nome.Should().Be("Companhia das Letras");
                    item3.Livros.Should().NotBeNull();
                });
        }
    }
}
