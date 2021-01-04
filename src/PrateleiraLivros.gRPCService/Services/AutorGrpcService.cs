using Ardalis.Result;
using Grpc.Core;
using PrateleiraLivros.Application.Interfaces;
using PrateleiraLivros.Application.Models.Queries;
using PrateleiraLivros.gRPCService.Protos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrateleiraLivros.gRPCService.Services
{
    public class AutorGrpcService : Autor.AutorBase
    {
        private readonly IAutorAppServico _servico;

        public AutorGrpcService(IAutorAppServico servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        public override async Task ListAll(AutorSearch request, IServerStreamWriter<AutoresResponse> responseStream, ServerCallContext context)
        {
            var autoresResponse = new AutoresResponse();
            var searchQuery = new AutorSearchQuery
            {
                Nome = request.Nome,
                Email = request.Email
            };

            var result = await _servico.GetAutoresAsync(filter: searchQuery);

            autoresResponse.Code = (int)context.Status.StatusCode;
            autoresResponse.Errors.AddRange(result.Errors);
            autoresResponse.Message = result.Errors.Any() ?
                "Erro ao retornar lista de autores" :
                "Autores retornados com sucesso";
            autoresResponse.Data.AddRange(
                result.Value.Select(x => new AutorModel
                {
                    Id = x.Id.ToString(),
                    Nome = x.Nome,
                    Sobrenome = x.Sobrenome,
                    Email = x.Email,
                    Bio = x.Bio,
                    Avatar = x.Avatar
                }));

            await responseStream.WriteAsync(autoresResponse);
        }

        public override async Task<AutorResponse> GetById(AutorFilter request, ServerCallContext context)
        {
            var autorResponse = new AutorResponse();

            var result = await _servico.GetAutorAsync(Guid.Parse(request.AutorId));

            if (result.Status is ResultStatus.NotFound)
            {
                autorResponse.Code = (int)context.Status.StatusCode;
                autorResponse.Errors.AddRange(result.Errors);
                autorResponse.Message = $"Nenhum autor com o id '{request.AutorId}' foi encontrado";

                return autorResponse;
            }

            autorResponse.Code = (int)result.Status;
            autorResponse.Errors.AddRange(result.Errors);
            autorResponse.Message = "Busca realizada com sucesso";
            autorResponse.Data = new AutorModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                Sobrenome = result.Value.Sobrenome,
                Email = result.Value.Email,
                Bio = result.Value.Bio,
                Avatar = result.Value.Avatar
            };

            return autorResponse;
        }

        public override async Task<AutorResponse> Create(AutorCreateRequest request, ServerCallContext context)
        {
            var autorResponse = new AutorResponse();
            var autorModel = new Application.Models.AutorModel
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Bio = request.Bio,
                Avatar = request.Avatar
            };

            var result = await _servico.AddAutorAsync(autorModel);

            if (result.Status is ResultStatus.Invalid)
            {
                autorResponse.Code = (int)context.Status.StatusCode;
                autorResponse.Errors.AddRange(result.Errors);
                autorResponse.Message = $"Autor possui erros de validação";
                autorResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new AutorValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return autorResponse;
            }

            autorResponse.Code = (int)result.Status;
            autorResponse.Errors.AddRange(result.Errors);
            autorResponse.Message = "Autor criado com sucesso";
            autorResponse.Data = new AutorModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                Sobrenome = result.Value.Sobrenome,
                Email = result.Value.Email,
                Bio = result.Value.Bio,
                Avatar = result.Value.Avatar
            };

            return autorResponse;
        }

        public override async Task<AutorResponse> Update(AutorUpdateRequest request, ServerCallContext context)
        {
            var autorResponse = new AutorResponse();
            var autorModel = new Application.Models.AutorModel
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Bio = request.Bio,
                Avatar = request.Avatar
            };

            var result = await _servico.UpdateAutorAsync(Guid.Parse(request.AutorId), autorModel);

            if (result.Status is ResultStatus.NotFound)
            {
                autorResponse.Code = (int)context.Status.StatusCode;
                autorResponse.Errors.AddRange(result.Errors);
                autorResponse.Message = $"Nenhum autor com o id '{request.AutorId}' foi encontrado";

                return autorResponse;
            }

            if (result.Status is ResultStatus.Invalid)
            {
                autorResponse.Code = (int)context.Status.StatusCode;
                autorResponse.Errors.AddRange(result.Errors);
                autorResponse.Message = $"Autor possui erros de validação";
                autorResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new AutorValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return autorResponse;
            }

            autorResponse.Code = (int)result.Status;
            autorResponse.Errors.AddRange(result.Errors);
            autorResponse.Message = "Autor atualizado com sucesso";
            autorResponse.Data = new AutorModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                Sobrenome = result.Value.Sobrenome,
                Email = result.Value.Email,
                Bio = result.Value.Bio,
                Avatar = result.Value.Avatar
            };

            return autorResponse;
        }

        public override async Task<AutorResponse> Delete(AutorFilter request, ServerCallContext context)
        {
            var autorResponse = new AutorResponse();

            var result = await _servico.DeleteAutorAsync(Guid.Parse(request.AutorId));

            if (result.Status is ResultStatus.NotFound)
            {
                autorResponse.Code = (int)context.Status.StatusCode;
                autorResponse.Errors.AddRange(result.Errors);
                autorResponse.Message = $"Nenhum autor com o id '{request.AutorId}' foi encontrado";

                return autorResponse;
            }

            autorResponse.Code = (int)result.Status;
            autorResponse.Errors.AddRange(result.Errors);
            autorResponse.Message = "Autor deletado com sucesso";

            return autorResponse;
        }

        public override async Task GetLivros(AutorFilter request, IServerStreamWriter<AutorLivrosResponse> responseStream, ServerCallContext context)
        {
            var livrosResponse = new AutorLivrosResponse();

            var result = await _servico.GetLivrosAutorAsync(Guid.Parse(request.AutorId));

            if (result.Status is ResultStatus.NotFound)
            {
                livrosResponse.Code = (int)context.Status.StatusCode;
                livrosResponse.Errors.AddRange(result.Errors);
                livrosResponse.Message = $"Nenhum autor com o id '{request.AutorId}' foi encontrado";

                await responseStream.WriteAsync(livrosResponse);
            }

            livrosResponse.Code = (int)result.Status;
            livrosResponse.Errors.AddRange(result.Errors);
            livrosResponse.Message = "Busca realizada com sucesso";
            livrosResponse.Data.AddRange(
                result.Value.Select(x => new AutorLivroModel
                {
                    Id = x.Id.ToString(),
                    Titulo = x.Titulo,
                    Descricao = x.Descricao,
                    Valor = Convert.ToDouble(x.Valor),
                    Isbn10 = x.ISBN_10,
                    Edicao = x.Edicao,
                    Idioma = x.Idioma,
                    DataPublicacao = x.DataPublicacao.ToShortDateString(),
                    NumeroPaginas = x.NumeroPaginas,
                    AutorId = x.AutorId.ToString(),
                    EditoraId = x.EditoraId.ToString()
                }));

            await responseStream.WriteAsync(livrosResponse);
        }
    }
}
