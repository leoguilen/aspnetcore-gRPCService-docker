using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class AutorSearchSpecification : Specification<Autor>
    {
        public AutorSearchSpecification(string nome = null, string email = null)
        {
            Query.Where(autor => (string.IsNullOrEmpty(nome) || autor.Nome.Contains(nome))
                              && (string.IsNullOrEmpty(email) || autor.Email.Contains(email)))
                .Include(autor => autor.Livros);
        }
    }
}
