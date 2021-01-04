using Ardalis.Result;
using PrateleiraLivros.Application.Models;
using PrateleiraLivros.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrateleiraLivros.Application.Interfaces
{
    public interface IAutorAppServico
    {
        Task<Result<IReadOnlyList<AutorModel>>> GetAutoresAsync(AutorSearchQuery filter = null);
        Task<Result<AutorModel>> GetAutorAsync(Guid autorId);
        Task<Result<AutorModel>> AddAutorAsync(AutorModel autor);
        Task<Result<AutorModel>> UpdateAutorAsync(Guid autorId, AutorModel autor);
        Task<Result<bool>> DeleteAutorAsync(Guid autorId);
        Task<Result<List<LivroModel>>> GetLivrosAutorAsync(Guid autorId);
    }
}
