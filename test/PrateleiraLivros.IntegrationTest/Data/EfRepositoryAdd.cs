using FluentAssertions;
using PrateleiraLivros.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.IntegrationTest.Data
{
    [Collection("Repositorio")]
    public class EfRepositoryAdd : BaseEfRepoTestFixture<Editora>
    {
        [Fact]
        public async Task AdicionarEntidadeNoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository();
            var item = new Editora("Editora teste", "www.editorateste.com", "Av. Teste 123, Bairro Teste");

            await repositorio.AddAsync(item);

            var novoItem = (await repositorio.ListAllAsync())
                .FirstOrDefault();

            item.Should().Equals(novoItem);
            novoItem.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task AdicionarListaDeEntidadesNoBancoDeDadosEmMemoria()
        {
            var repositorio = await GetRepository();
            var itens = new List<Editora> 
            {
                new Editora("Editora teste 1", "www.editorateste1.com", "Av. Teste 1, Bairro Teste"),
                new Editora("Editora teste 2", "www.editorateste2.com", "Av. Teste 2, Bairro Teste"),
                new Editora("Editora teste 3", "www.editorateste3.com", "Av. Teste 3, Bairro Teste")
            };

            await repositorio.AddRangeAsync(itens);

            var novosItens = (await repositorio.ListAllAsync()).ToList();

            itens.Should().HaveCount(3);
            itens.Should().BeEquivalentTo(novosItens, assert => assert
                .Excluding(prop => prop.DataCadastro)
                .Excluding(prop => prop.DataAtualizacao));
        }
    }
}
