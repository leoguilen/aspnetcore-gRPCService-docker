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
    public class AutorAppServico : IAutorAppServico
    {
        private readonly IAsyncRepository<Autor> _repositorio;
        private readonly IAppLogger<AutorAppServico> _logger;
        private readonly IMapper _mapper;

        public AutorAppServico(IAsyncRepository<Autor> repositorio,
            IAppLogger<AutorAppServico> logger, IMapper mapper)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<Result<AutorModel>> AddAutorAsync(AutorModel autor)
        {
            var novoAutor = _mapper.Map<Autor>(autor);
            var validator = new AutorValidator();

            var resultValidation = validator.Validate(novoAutor);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Novo autor não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<AutorModel>.Invalid(resultValidation.AsErrors());
            }

            var autorAdicionado = await _repositorio.AddAsync(novoAutor);
            Guard.Against.Null(autorAdicionado, "Novo Autor");

            _logger.LogInformation("Novo autor foi inserido com sucesso");
            return Result<AutorModel>.Success(
                _mapper.Map<AutorModel>(autorAdicionado));
        }

        public async Task<Result<bool>> DeleteAutorAsync(Guid autorId)
        {
            var autor = await _repositorio.GetByIdAsync(autorId);

            if (autor is null)
            {
                _logger.LogWarning("Não foi encontrado autor com id igual a '{0}'", autorId);
                return Result<bool>.NotFound();
            }

            await _repositorio.DeleteAsync(autor);

            _logger.LogInformation("Autor com id '{0}' deletado com sucesso", autorId);
            return Result<bool>.Success(true);
        }

        public async Task<Result<AutorModel>> GetAutorAsync(Guid autorId)
        {
            Guard.Against.NullOrEmpty(autorId, "AutorId");

            var spec = new AutorComLivrosSpecification(autorId);
            var autor = (await _repositorio.ListAsync(spec)).FirstOrDefault();

            if (autor is null)
            {
                _logger.LogWarning("Não foi encontrado autor com id igual a '{0}'", autorId);
                return Result<AutorModel>.NotFound();
            }

            _logger.LogInformation("Autor com id '{0}' encontrado na base de dados", autorId);
            return Result<AutorModel>.Success(
                _mapper.Map<AutorModel>(autor));
        }

        public async Task<Result<IReadOnlyList<AutorModel>>> GetAutoresAsync(AutorSearchQuery filter = null)
        {
            var spec = new AutorSearchSpecification(filter.Nome, filter.Email);
            var autores = (await _repositorio.ListAsync(spec)).ToList();

            return Result<IReadOnlyList<AutorModel>>.Success(
                _mapper.Map<IReadOnlyList<AutorModel>>(autores));
        }

        public async Task<Result<List<LivroModel>>> GetLivrosAutorAsync(Guid autorId)
        {
            Guard.Against.Null(autorId, "AutorId");

            var spec = new AutorComLivrosSpecification(autorId);
            var autor = (await _repositorio.ListAsync(spec)).FirstOrDefault();

            if (autor is null)
            {
                _logger.LogWarning("Não foi encontrado autor com id igual a '{0}'", autorId);
                return Result<List<LivroModel>>.NotFound();
            }

            _logger.LogInformation("Autor com id '{0}' encontrado, contendo {1} livros cadastrados", autorId, autor.Livros.Count);
            return Result<List<LivroModel>>.Success(
                _mapper.Map<List<LivroModel>>(autor.Livros));
        }

        public async Task<Result<AutorModel>> UpdateAutorAsync(Guid autorId, AutorModel autorModel)
        {
            Guard.Against.NullOrEmpty(autorId, "AutorId");

            var autor = await _repositorio.GetByIdAsync(autorId);

            if (autor is null)
            {
                _logger.LogWarning("Não foi encontrado autor com id igual a '{0}'", autorId);
                return Result<AutorModel>.NotFound();
            }

            autor.Nome = !string.IsNullOrEmpty(autorModel.Nome) && autor.Nome != autorModel.Nome ? autorModel.Nome : autor.Nome;
            autor.Sobrenome = !string.IsNullOrEmpty(autorModel.Sobrenome) && autor.Sobrenome != autorModel.Sobrenome ? autorModel.Sobrenome : autor.Sobrenome;
            autor.Email = !string.IsNullOrEmpty(autorModel.Nome) && autor.Email != autorModel.Email ? autorModel.Email : autor.Email;
            autor.Bio = autor.Bio != autorModel.Bio ? autorModel.Bio : autor.Bio;
            autor.Avatar = autor.Avatar != autorModel.Avatar ? autorModel.Avatar : autor.Avatar;
            autor.DataAtualizacao = DateTime.Now;

            var validator = new AutorValidator();
            var resultValidation = validator.Validate(autor);
            if (!resultValidation.IsValid)
            {
                _logger.LogWarning("Atualização do autor não passou na validação. Erros: {0}", resultValidation.Errors.ToJson());
                return Result<AutorModel>.Invalid(resultValidation.AsErrors());
            }

            await _repositorio.UpdateAsync(autor);

            _logger.LogInformation("Autor foi atualizado com sucesso");
            return Result<AutorModel>.Success(
                _mapper.Map<AutorModel>(autor));
        }
    }
}
