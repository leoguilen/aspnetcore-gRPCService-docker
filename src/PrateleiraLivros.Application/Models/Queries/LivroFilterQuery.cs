using System;

namespace PrateleiraLivros.Application.Models.Queries
{
    public class LivroFilterQuery
    {
        public string Titulo { get; set; }
        public string Idioma { get; set; }
        public Guid? EditoraId { get; set; }
        public Guid? AutorId { get; set; }
    }
}
