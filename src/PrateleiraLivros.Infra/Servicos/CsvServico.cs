using EPPlus.Core.Extensions;
using OfficeOpenXml;
using PrateleiraLivros.Dominio.Entidades;
using PrateleiraLivros.Dominio.Interfaces;
using PrateleiraLivros.Infra.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrateleiraLivros.Infra.Servicos
{
    public class CsvServico : ICsvServico
    {
        private readonly IAppLogger<CsvServico> _logger;

        public CsvServico(IAppLogger<CsvServico> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<List<Livro>> GetCargaLivros(Stream stream)
        {
            List<Livro> livros = new List<Livro>();

            try
            {
                using var excelPackage = new ExcelPackage(stream);
                using var excelWorksheet = excelPackage.Workbook.Worksheets.First();

                var livrosDto = excelWorksheet.ToList<LivroDto>();

                foreach (var livroDto in livrosDto)
                {
                    var livro = new Livro(livroDto.Titulo, livroDto.Descricao,
                        livroDto.Valor, livroDto.ISBN_10, livroDto.Edicao,
                        livroDto.DataPublicacao, livroDto.Idioma,
                        livroDto.NumeroPaginas, Guid.Parse(livroDto.EditoraId),
                        Guid.Parse(livroDto.AutorId));

                    livros.Add(livro);
                }

                _logger.LogInformation("CSV de carga dos livros convertido com sucesso. Total de livros carregados: {0}", livros.Count);
                return Task.FromResult(livros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na conversão do CSV com carga dos livros para o sistema");
                throw;
            }
        }
    }
}
