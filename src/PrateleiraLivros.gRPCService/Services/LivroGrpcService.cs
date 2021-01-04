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
    public class LivroGrpcService : Livro.LivroBase
    {
        private readonly ILivroAppServico _servico;

        public LivroGrpcService(ILivroAppServico servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        public override async Task ListAll(LivroSearch request, IServerStreamWriter<LivrosResponse> responseStream, ServerCallContext context)
        {
            var livrosResponse = new LivrosResponse();
            Guid? editoraId, autorId;

            if (string.IsNullOrEmpty(request.EditoraId))
                editoraId = null;
            else
                editoraId = Guid.Parse(request.EditoraId);

            if (string.IsNullOrEmpty(request.AutorId))
                autorId = null;
            else
                autorId = Guid.Parse(request.AutorId);

            var searchQuery = new LivroFilterQuery
            {
                Titulo = request.Titulo,
                Idioma = request.Idioma,
                EditoraId = editoraId,
                AutorId = autorId
            };

            var result = await _servico.GetLivrosAsync(filter: searchQuery);

            livrosResponse.Code = (int)context.Status.StatusCode;
            livrosResponse.Errors.AddRange(result.Errors);
            livrosResponse.Message = result.Errors.Any() ?
                "Erro ao retornar lista de livros" :
                "Livros retornados com sucesso";
            livrosResponse.Data.AddRange(
                result.Value.Select(x => new LivroModel
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
                    EditoraId = x.EditoraId.ToString(),
                    AutorId = x.AutorId.ToString()
                }));

            await responseStream.WriteAsync(livrosResponse);
        }

        public override async Task<LivroResponse> GetById(LivroFilter request, ServerCallContext context)
        {
            var livroResponse = new LivroResponse();

            var result = await _servico.GetLivroAsync(Guid.Parse(request.LivroId));

            if (result.Status is ResultStatus.NotFound)
            {
                livroResponse.Code = (int)context.Status.StatusCode;
                livroResponse.Errors.AddRange(result.Errors);
                livroResponse.Message = $"Nenhum livro com o id '{request.LivroId}' foi encontrado";

                return livroResponse;
            }

            livroResponse.Code = (int)result.Status;
            livroResponse.Errors.AddRange(result.Errors);
            livroResponse.Message = "Busca realizada com sucesso";
            livroResponse.Data = new LivroModel
            {
                Id = result.Value.Id.ToString(),
                Titulo = result.Value.Titulo,
                Descricao = result.Value.Descricao,
                Valor = Convert.ToDouble(result.Value.Valor),
                Isbn10 = result.Value.ISBN_10,
                Edicao = result.Value.Edicao,
                Idioma = result.Value.Idioma,
                DataPublicacao = result.Value.DataPublicacao.ToShortDateString(),
                NumeroPaginas = result.Value.NumeroPaginas,
                EditoraId = result.Value.EditoraId.ToString(),
                AutorId = result.Value.AutorId.ToString()
            };

            return livroResponse;
        }

        public override async Task<LivroResponse> Create(LivroCreateRequest request, ServerCallContext context)
        {
            var livroResponse = new LivroResponse();
            var livroModel = new Application.Models.LivroModel
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Valor = Convert.ToDecimal(request.Valor),
                ISBN_10 = request.Isbn10,
                Edicao = request.Edicao,
                Idioma = request.Idioma,
                DataPublicacao = DateTime.Parse(request.DataPublicacao),
                NumeroPaginas = request.NumeroPaginas,
                EditoraId = Guid.Parse(request.EditoraId),
                AutorId = Guid.Parse(request.AutorId)
            };

            var result = await _servico.AddLivroAsync(livroModel);

            if (result.Status is ResultStatus.Invalid)
            {
                livroResponse.Code = (int)context.Status.StatusCode;
                livroResponse.Errors.AddRange(result.Errors);
                livroResponse.Message = $"Livro possui erros de validação";
                livroResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new LivroValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return livroResponse;
            }

            livroResponse.Code = (int)result.Status;
            livroResponse.Errors.AddRange(result.Errors);
            livroResponse.Message = "Livro criado com sucesso";
            livroResponse.Data = new LivroModel
            {
                Id = result.Value.Id.ToString(),
                Titulo = result.Value.Titulo,
                Descricao = result.Value.Descricao,
                Valor = Convert.ToDouble(result.Value.Valor),
                Isbn10 = result.Value.ISBN_10,
                Edicao = result.Value.Edicao,
                Idioma = result.Value.Idioma,
                DataPublicacao = result.Value.DataPublicacao.ToShortDateString(),
                NumeroPaginas = result.Value.NumeroPaginas,
                EditoraId = result.Value.EditoraId.ToString(),
                AutorId = result.Value.AutorId.ToString()
            };

            return livroResponse;
        }

        public override async Task<LivroResponse> Update(LivroUpdateRequest request, ServerCallContext context)
        {
            var livroResponse = new LivroResponse();
            var livroModel = new Application.Models.LivroModel
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Valor = Convert.ToDecimal(request.Valor),
                ISBN_10 = request.Isbn10,
                Edicao = request.Edicao,
                Idioma = request.Idioma,
                DataPublicacao = DateTime.Parse(request.DataPublicacao),
                NumeroPaginas = request.NumeroPaginas
            };

            var result = await _servico.UpdateLivroAsync(Guid.Parse(request.LivroId), livroModel);

            if (result.Status is ResultStatus.NotFound)
            {
                livroResponse.Code = (int)context.Status.StatusCode;
                livroResponse.Errors.AddRange(result.Errors);
                livroResponse.Message = $"Nenhum livro com o id '{request.LivroId}' foi encontrado";

                return livroResponse;
            }

            if (result.Status is ResultStatus.Invalid)
            {
                livroResponse.Code = (int)context.Status.StatusCode;
                livroResponse.Errors.AddRange(result.Errors);
                livroResponse.Message = $"Livro possui erros de validação";
                livroResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new LivroValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return livroResponse;
            }

            livroResponse.Code = (int)result.Status;
            livroResponse.Errors.AddRange(result.Errors);
            livroResponse.Message = "Livro atualizado com sucesso";
            livroResponse.Data = new LivroModel
            {
                Id = result.Value.Id.ToString(),
                Titulo = result.Value.Titulo,
                Descricao = result.Value.Descricao,
                Valor = Convert.ToDouble(result.Value.Valor),
                Isbn10 = result.Value.ISBN_10,
                Edicao = result.Value.Edicao,
                Idioma = result.Value.Idioma,
                DataPublicacao = result.Value.DataPublicacao.ToShortDateString(),
                NumeroPaginas = result.Value.NumeroPaginas,
                EditoraId = result.Value.EditoraId.ToString(),
                AutorId = result.Value.AutorId.ToString()
            };

            return livroResponse;
        }

        public override async Task<LivroResponse> Delete(LivroFilter request, ServerCallContext context)
        {
            var livroResponse = new LivroResponse();

            var result = await _servico.DeleteLivroAsync(Guid.Parse(request.LivroId));

            if (result.Status is ResultStatus.NotFound)
            {
                livroResponse.Code = (int)context.Status.StatusCode;
                livroResponse.Errors.AddRange(result.Errors);
                livroResponse.Message = $"Nenhum livro com o id '{request.LivroId}' foi encontrado";

                return livroResponse;
            }

            livroResponse.Code = (int)result.Status;
            livroResponse.Errors.AddRange(result.Errors);
            livroResponse.Message = "Livro deletado com sucesso";

            return livroResponse;
        }
    }
}
