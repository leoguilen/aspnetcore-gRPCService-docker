using FluentAssertions;
using PrateleiraLivros.Dominio.Entidades;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.IntegrationTest.Data
{
    [Collection("Repositorio")]
    public class EfRepositoryGet : BaseEfRepoTestFixture<Editora>
    {
        [Fact]
        public async Task ListaComTodasEntidadesDoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository(true);

            var itens = await repositorio.ListAllAsync();

            itens.Should().SatisfyRespectively(
                item1 => item1.Nome.Should().Be("Escala"),
                item2 => item2.Nome.Should().Be("Alta Books"),
                item3 => item3.Nome.Should().Be("Companhia das Letras"));
        }

        [Fact]
        public async Task ObterEntidadePorIdDoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository(true);
            var id = DbContext.Editoras.First().Id;

            var item = await repositorio.GetByIdAsync(id);
            
            item.Should().NotBeNull();
            item.Id.Should().Be(id);
            item.Nome.Should().Be("Escala");
        }
    }
}
