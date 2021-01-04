using Ardalis.Specification;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Dominio.Specifications
{
    public class EditoraSearchSpecification : Specification<Editora>
    {
        public EditoraSearchSpecification(string nome = null)
        {
            Query.Where(autor => (string.IsNullOrEmpty(nome) || autor.Nome.Contains(nome)))
                .Include(autor => autor.Livros);
        }
    }
}
