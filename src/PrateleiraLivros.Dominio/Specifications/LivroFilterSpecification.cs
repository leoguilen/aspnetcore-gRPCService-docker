using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;
using System;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class LivroFilterSpecification : Specification<Livro>
    {
        public LivroFilterSpecification(Guid? editoraId, Guid? autorId, string titulo = null, string idioma = null)
        {
            Query.Where(livro => (!editoraId.HasValue || livro.EditoraId == editoraId)
                              && (!autorId.HasValue || livro.AutorId == autorId)
                              && (string.IsNullOrEmpty(titulo) || livro.Titulo.Contains(titulo))
                              && (string.IsNullOrEmpty(idioma) || livro.Idioma.Contains(idioma)));
            Query.Include(livro => livro.Editora);
            Query.Include(livro => livro.Autor);
        }
    }
}
