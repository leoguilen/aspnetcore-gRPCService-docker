using System;

namespace PrateleiraLivros.Application.Models
{
    public class LivroModel : BaseModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public long ISBN_10 { get; set; }
        public int Edicao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Idioma { get; set; }
        public int NumeroPaginas { get; set; }
        public Guid EditoraId { get; set; }
        public Guid AutorId { get; set; }
    }
}
