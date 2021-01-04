using PrateleiraLivros.Dominio.Entidades;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PrateleiraLivros.Dominio.Interfaces
{
    public interface ICsvServico
    {
        Task<List<Livro>> GetCargaLivros(Stream stream);
    }
}
