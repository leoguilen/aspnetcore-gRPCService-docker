using EPPlus.Core.Extensions.Attributes;
using System;

namespace PrateleiraLivros.Infra.Dto
{
    public class LivroDto
    {
        [ExcelTableColumn]
        public string Titulo { get; set; }

        [ExcelTableColumn]
        public string Descricao { get; set; }

        [ExcelTableColumn]
        public decimal Valor { get; set; }

        [ExcelTableColumn("ISBN10")]
        public long ISBN_10 { get; set; }

        [ExcelTableColumn]
        public int Edicao { get; set; }

        [ExcelTableColumn]
        public DateTime DataPublicacao { get; set; }

        [ExcelTableColumn]
        public string Idioma { get; set; }

        [ExcelTableColumn]
        public int NumeroPaginas { get; set; }

        [ExcelTableColumn]
        public string EditoraId { get; set; }

        [ExcelTableColumn]
        public string AutorId { get; set; }
    }
}
