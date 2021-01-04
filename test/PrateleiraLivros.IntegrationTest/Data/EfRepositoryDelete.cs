using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PrateleiraLivros.Dominio.Entidades;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.IntegrationTest.Data
{
    [Collection("Repositorio")]
    public class EfRepositoryDelete : BaseEfRepoTestFixture<Editora>
    {
        [Fact]
        public async Task DeletarEntidadeDoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository();
            var item = new Editora("Editora teste", "www.editorateste.com", "Av. Teste 123, Bairro Teste");

            await repositorio.AddAsync(item);
            var itemNovo = await repositorio.GetByIdAsync(item.Id);

            itemNovo.Should().NotBeNull();

            DbContext.Entry(item).State = EntityState.Detached;

            await repositorio.DeleteAsync(item);
            var itemDeletado = await repositorio.GetByIdAsync(item.Id);

            itemDeletado.Should().BeNull();
        }
    }
}
