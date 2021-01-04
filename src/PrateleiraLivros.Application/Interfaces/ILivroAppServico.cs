using Ardalis.Result;
using PrateleiraLivros.Application.Models;
using PrateleiraLivros.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrateleiraLivros.Application.Interfaces
{
    public interface ILivroAppServico
    {
        Task<Result<IReadOnlyList<LivroModel>>> GetLivrosAsync(LivroFilterQuery filter = null);
        Task<Result<LivroModel>> GetLivroAsync(Guid livroId);
        Task<Result<LivroModel>> AddLivroAsync(LivroModel livro);
        Task<Result<LivroModel>> UpdateLivroAsync(Guid livroId, LivroModel livro);
        Task<Result<bool>> DeleteLivroAsync(Guid livroId);
    }
}
