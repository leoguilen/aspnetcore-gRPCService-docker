using FluentAssertions;
using Microsoft.Extensions.Logging;
using PrateleiraLivros.Dominio.Interfaces;
using PrateleiraLivros.Infra.Logging;
using PrateleiraLivros.Infra.Servicos;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PrateleiraLivros.UnitTest.Infraestrutura.Servicos
{
    public class CsvServicoTeste
    {
        const string FILE = @"../../../__mock__/CARGA_LIVRO_MOCK.xlsx";
        private readonly ICsvServico _csvServico;

        public CsvServicoTeste()
        {
            var logger = new LoggerAdapter<CsvServico>(new LoggerFactory());
            _csvServico = new CsvServico(logger);
        }

        [Fact]
        public async Task CargaLivros_TransformeCSVParaListaDeLivros()
        {
            using var stream = new MemoryStream(File.ReadAllBytes(FILE));

            var livros = await _csvServico.GetCargaLivros(stream);

            livros.Should().HaveCount(5);
            livros.Should().SatisfyRespectively(
                l1 => l1.Titulo.Should().Be("Trivium"),
                l2 => l2.Titulo.Should().Be("O panóptico"),
                l3 => l3.Titulo.Should().Be("Kafka: Por uma literatura menor"),
                l4 => l4.Titulo.Should().Be("A divina comédia: 344"),
                l5 => l5.Titulo.Should().Be("Pensar e ser em geografia"));
        }
    }
}
