using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;
using System;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class LivroComEditoraEAutorSpecification : Specification<Livro>
    {
        public LivroComEditoraEAutorSpecification(Guid livroId)
        {
            Query.Where(livro => livro.Id == livroId)
                 .Include(livroEditora => livroEditora.Editora);
            Query.Where(livro => livro.Id == livroId)
                 .Include(livroAutor => livroAutor.Autor);
        }

        public LivroComEditoraEAutorSpecification()
        {
            Query.Include(livroEditora => livroEditora.Editora);
            Query.Include(livroAutor => livroAutor.Autor);
        }
    }
}
