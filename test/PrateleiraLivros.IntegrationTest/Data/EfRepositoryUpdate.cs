using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PrateleiraLivros.Dominio.Entidades;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.IntegrationTest.Data
{
    [Collection("Repositorio")]
    public class EfRepositoryUpdate : BaseEfRepoTestFixture<Editora>
    {
        [Fact]
        public async Task AtualizarEntidadeNoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository();
            var item = new Editora("Editora teste", "www.editorateste.com", "Av. Teste 123, Bairro Teste");

            await repositorio.AddAsync(item);

            DbContext.Entry(item).State = EntityState.Detached;

            var nomeNovo = "Editora Nova";
            item.Nome = nomeNovo;

            await repositorio.UpdateAsync(item);

            item.Nome.Should().Be(nomeNovo);
        }
    }
}
