using Ardalis.Result;
using PrateleiraLivros.Application.Models;
using PrateleiraLivros.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrateleiraLivros.Application.Interfaces
{
    public interface IEditoraAppServico
    {
        Task<Result<IReadOnlyList<EditoraModel>>> GetEditorasAsync(EditoraSearchQuery filter = null);
        Task<Result<EditoraModel>> GetEditoraAsync(Guid editoraId);
        Task<Result<EditoraModel>> AddEditoraAsync(EditoraModel editora);
        Task<Result<EditoraModel>> UpdateEditoraAsync(Guid editoraId, EditoraModel editora);
        Task<Result<bool>> DeleteEditoraAsync(Guid editoraId);
        Task<Result<List<LivroModel>>> GetLivrosEditoraAsync(Guid editoraId);
    }
}
