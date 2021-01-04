using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using PrateleiraLivros.Application.Interfaces;
using PrateleiraLivros.Application.Models;
using PrateleiraLivros.Application.Models.Queries;
using PrateleiraLivros.Application.Validators;
using PrateleiraLivros.Dominio.Entidades;
using PrateleiraLivros.Dominio.Extensions;
using PrateleiraLivros.Dominio.Interfaces;
using PrateleiraLivros.Dominio.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrateleiraLivros.Application.Servicos
{
    public class EditoraAppServico : IEditoraAppServico
    {
        private readonly IAsyncRepository<Editora> _repositorio;
        private readonly IAppLogger<EditoraAppServico> _logger;
        private readonly IMapper _mapper;

        public EditoraAppServico(IAsyncRepository<Editora> repositorio,
            IAppLogger<EditoraAppServico> logger, IMapper mapper)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<Result<EditoraModel>> AddEditoraAsync(EditoraModel editora)
        {
            var novaEditora = _mapper.Map<Editora>(editora);
            var validator = new EditoraValidator();

            var resultValidation = validator.Validate(novaEditora);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Nova editora não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<EditoraModel>.Invalid(resultValidation.AsErrors());
            }

            var editoraAdicionada = await _repositorio.AddAsync(novaEditora);
            Guard.Against.Null(editoraAdicionada, "Novo editora");

            _logger.LogInformation("Nova editora foi inserida com sucesso");
            return Result<EditoraModel>.Success(
                _mapper.Map<EditoraModel>(editoraAdicionada));
        }

        public async Task<Result<bool>> DeleteEditoraAsync(Guid editoraId)
        {
            var editora = await _repositorio.GetByIdAsync(editoraId);

            if (editora is null)
            {
                _logger.LogWarning("Não foi encontrado editora com id igual a '{0}'", editoraId);
                return Result<bool>.NotFound();
            }

            await _repositorio.DeleteAsync(editora);

            _logger.LogInformation("Editora com id '{0}' deletada com sucesso", editoraId);
            return Result<bool>.Success(true);
        }

        public async Task<Result<EditoraModel>> GetEditoraAsync(Guid editoraId)
        {
            Guard.Against.NullOrEmpty(editoraId, "EditoraId");

            var spec = new EditoraComLivrosSpecification(editoraId);
            var editora = (await _repositorio.ListAsync(spec)).FirstOrDefault();

            if (editora is null)
            {
                _logger.LogWarning("Não foi encontrado editora com id igual a '{0}'", editoraId);
                return Result<EditoraModel>.NotFound();
            }

            _logger.LogInformation("Editora com id '{0}' encontrada na base de dados", editoraId);
            return Result<EditoraModel>.Success(
                _mapper.Map<EditoraModel>(editora));
        }

        public async Task<Result<IReadOnlyList<EditoraModel>>> GetEditorasAsync(EditoraSearchQuery filter = null)
        {
            var spec = new EditoraSearchSpecification(filter.Nome);
            var editoras = (await _repositorio.ListAsync(spec)).ToList();

            return Result<IReadOnlyList<EditoraModel>>.Success(
                _mapper.Map<IReadOnlyList<EditoraModel>>(editoras));
        }

        public async Task<Result<List<LivroModel>>> GetLivrosEditoraAsync(Guid editoraId)
        {
            Guard.Against.Null(editoraId, "EditoraId");

            var spec = new EditoraComLivrosSpecification(editoraId);
            var editora = (await _repositorio.ListAsync(spec)).FirstOrDefault();

            if (editora is null)
            {
                _logger.LogWarning("Não foi encontrado editora com id igual a '{0}'", editoraId);
                return Result<List<LivroModel>>.NotFound();
            }

            _logger.LogInformation("Editora com id '{0}' encontrada, contendo {1} livros cadastrados", editoraId, editora.Livros.Count);
            return Result<List<LivroModel>>.Success(
                _mapper.Map<List<LivroModel>>(editora.Livros));
        }

        public async Task<Result<EditoraModel>> UpdateEditoraAsync(Guid editoraId, EditoraModel editoraModel)
        {
            Guard.Against.NullOrEmpty(editoraId, "EditoraId");

            var editora = await _repositorio.GetByIdAsync(editoraId);

            if (editora is null)
            {
                _logger.LogWarning("Não foi encontrado editora com id igual a '{0}'", editoraId);
                return Result<EditoraModel>.NotFound();
            }

            editora.Nome = !string.IsNullOrEmpty(editoraModel.Nome) && editora.Nome != editoraModel.Nome ? editoraModel.Nome : editora.Nome;
            editora.SiteURL = editora.SiteURL != editoraModel.SiteURL ? editoraModel.SiteURL : editora.SiteURL;
            editora.Endereco = !string.IsNullOrEmpty(editoraModel.Endereco) && editora.Endereco != editoraModel.Endereco ? editoraModel.Endereco : editora.Endereco;
            editora.DataAtualizacao = DateTime.Now;

            var validator = new EditoraValidator();
            var resultValidation = validator.Validate(editora);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Atualização da editora não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<EditoraModel>.Invalid(resultValidation.AsErrors());
            }

            await _repositorio.UpdateAsync(editora);

            _logger.LogInformation("Editora foi atualizada com sucesso", editora.ToJson());
            return Result<EditoraModel>.Success(
                _mapper.Map<EditoraModel>(editora));
        }
    }
}
