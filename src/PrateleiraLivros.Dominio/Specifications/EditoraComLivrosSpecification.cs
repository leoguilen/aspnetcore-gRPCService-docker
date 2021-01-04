using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;
using System;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class EditoraComLivrosSpecification : Specification<Editora>
    {
        public EditoraComLivrosSpecification(Guid editoraId)
        {
            Query.Where(editora => editora.Id == editoraId)
                 .Include(editoraLivros => editoraLivros.Livros)
                    .ThenInclude(livro => livro.Autor);
        }

        public EditoraComLivrosSpecification()
        {
            Query.Include(editoraLivros => editoraLivros.Livros);
        }
    }
}
