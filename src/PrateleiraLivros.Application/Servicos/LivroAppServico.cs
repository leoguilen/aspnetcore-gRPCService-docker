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
    public class LivroAppServico : ILivroAppServico
    {
        private readonly IAsyncRepository<Livro> _repositorio;
        private readonly IAppLogger<LivroAppServico> _logger;
        private readonly IMapper _mapper;

        public LivroAppServico(IAsyncRepository<Livro> repositorio,
            IAppLogger<LivroAppServico> logger, IMapper mapper)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<Result<LivroModel>> AddLivroAsync(LivroModel livro)
        {
            var novoLivro = _mapper.Map<Livro>(livro);
            var validator = new LivroValidator();

            var resultValidation = validator.Validate(novoLivro);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Novo livro não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<LivroModel>.Invalid(resultValidation.AsErrors());
            }

            var livroAdicionado = await _repositorio.AddAsync(novoLivro);
            Guard.Against.Null(livroAdicionado, "Novo livro");

            _logger.LogInformation("Novo livro foi inserido com sucesso");
            return Result<LivroModel>.Success(
                _mapper.Map<LivroModel>(livroAdicionado));
        }

        public async Task<Result<bool>> DeleteLivroAsync(Guid livroId)
        {
            var livro = await _repositorio.GetByIdAsync(livroId);

            if (livro is null)
            {
                _logger.LogWarning("Não foi encontrado livro com id igual a '{0}'", livroId);
                return Result<bool>.NotFound();
            }

            await _repositorio.DeleteAsync(livro);

            _logger.LogInformation("Livro com id '{0}' deletado com sucesso", livroId);
            return Result<bool>.Success(true);
        }

        public async Task<Result<LivroModel>> GetLivroAsync(Guid livroId)
        {
            Guard.Against.NullOrEmpty(livroId, "LivroId");

            var spec = new LivroComEditoraEAutorSpecification(livroId);
            var livro = (await _repositorio.ListAsync(spec)).FirstOrDefault();

            if (livro is null)
            {
                _logger.LogWarning("Não foi encontrado livro com id igual a '{0}'", livroId);
                return Result<LivroModel>.NotFound();
            }

            _logger.LogInformation("Livro com id '{0}' encontrada na base de dados", livroId);
            return Result<LivroModel>.Success(
                _mapper.Map<LivroModel>(livro));
        }

        public async Task<Result<IReadOnlyList<LivroModel>>> GetLivrosAsync(LivroFilterQuery filter = null)
        {
            var spec = new LivroFilterSpecification(filter.EditoraId,
                filter.AutorId, filter.Titulo, filter.Idioma);
            var livros = (await _repositorio.ListAsync(spec)).ToList();

            return Result<IReadOnlyList<LivroModel>>.Success(
                _mapper.Map<IReadOnlyList<LivroModel>>(livros));
        }

        public async Task<Result<LivroModel>> UpdateLivroAsync(Guid livroId, LivroModel livroModel)
        {
            Guard.Against.NullOrEmpty(livroId, "LivroId");

            var livro = await _repositorio.GetByIdAsync(livroId);

            if (livro is null)
            {
                _logger.LogWarning("Não foi encontrado livro com id igual a '{0}'", livroId);
                return Result<LivroModel>.NotFound();
            }

            livro.Titulo = !string.IsNullOrEmpty(livroModel.Titulo) && livro.Titulo != livroModel.Titulo ? livroModel.Titulo : livro.Titulo;
            livro.Descricao = !string.IsNullOrEmpty(livroModel.Descricao) && livro.Descricao != livroModel.Descricao ? livroModel.Descricao : livro.Descricao;
            livro.Valor = livroModel.Valor != decimal.Zero && livro.Valor != livroModel.Valor ? livroModel.Valor : livro.Valor;
            livro.ISBN_10 = livroModel.Valor != 0 && livro.ISBN_10 != livroModel.ISBN_10 ? livroModel.ISBN_10 : livro.ISBN_10;
            livro.Edicao = livro.Edicao != livroModel.Edicao ? livroModel.Edicao : livro.Edicao;
            livro.DataPublicacao = livro.DataPublicacao != livroModel.DataPublicacao ? livroModel.DataPublicacao : livro.DataPublicacao;
            livro.Idioma = livro.Idioma != livroModel.Idioma ? livroModel.Idioma : livro.Idioma;
            livro.NumeroPaginas = livro.NumeroPaginas != livroModel.NumeroPaginas ? livroModel.NumeroPaginas : livro.NumeroPaginas;
            livro.DataAtualizacao = DateTime.Now;

            var validator = new LivroValidator();
            var resultValidation = validator.Validate(livro);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Atualização do livro não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<LivroModel>.Invalid(resultValidation.AsErrors());
            }

            await _repositorio.UpdateAsync(livro);

            _logger.LogInformation("Livro foi atualizado com sucesso", livro.ToJson());
            return Result<LivroModel>.Success(
                _mapper.Map<LivroModel>(livro));
        }
    }
}
