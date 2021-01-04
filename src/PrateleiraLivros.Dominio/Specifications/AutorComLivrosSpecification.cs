using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;
using System;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class AutorComLivrosSpecification : Specification<Autor>
    {
        public AutorComLivrosSpecification(Guid autorId)
        {
            Query.Where(autor => autor.Id == autorId)
                 .Include(autorLivros => autorLivros.Livros);
        }

        public AutorComLivrosSpecification()
        {
            Query.Include(autorLivros => autorLivros.Livros);
        }
    }
}
