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
    public class EditoraGrpcService : Editora.EditoraBase
    {
        private readonly IEditoraAppServico _servico;

        public EditoraGrpcService(IEditoraAppServico servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        public override async Task ListAll(EditoraSearch request, IServerStreamWriter<EditorasResponse> responseStream, ServerCallContext context)
        {
            var editorasResponse = new EditorasResponse();
            var searchQuery = new EditoraSearchQuery
            {
                Nome = request.Nome
            };

            var result = await _servico.GetEditorasAsync(filter: searchQuery);

            editorasResponse.Code = (int)context.Status.StatusCode;
            editorasResponse.Errors.AddRange(result.Errors);
            editorasResponse.Message = result.Errors.Any() ?
                "Erro ao retornar lista de editoras" :
                "Editoras retornadas com sucesso";
            editorasResponse.Data.AddRange(
                result.Value.Select(x => new EditoraModel
                {
                    Id = x.Id.ToString(),
                    Nome = x.Nome,
                    SiteURL = x.SiteURL,
                    Endereco = x.Endereco
                }));

            await responseStream.WriteAsync(editorasResponse);
        }

        public override async Task<EditoraResponse> GetById(EditoraFilter request, ServerCallContext context)
        {
            var editoraResponse = new EditoraResponse();

            var result = await _servico.GetEditoraAsync(Guid.Parse(request.EditoraId));

            if (result.Status is ResultStatus.NotFound)
            {
                editoraResponse.Code = (int)context.Status.StatusCode;
                editoraResponse.Errors.AddRange(result.Errors);
                editoraResponse.Message = $"Nenhum editora com o id '{request.EditoraId}' foi encontrada";

                return editoraResponse;
            }

            editoraResponse.Code = (int)result.Status;
            editoraResponse.Errors.AddRange(result.Errors);
            editoraResponse.Message = "Busca realizada com sucesso";
            editoraResponse.Data = new EditoraModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                SiteURL = result.Value.SiteURL,
                Endereco = result.Value.Endereco
            };

            return editoraResponse;
        }

        public override async Task<EditoraResponse> Create(EditoraCreateRequest request, ServerCallContext context)
        {
            var editoraResponse = new EditoraResponse();
            var editoraModel = new Application.Models.EditoraModel
            {
                Nome = request.Nome,
                SiteURL = request.SiteURL,
                Endereco = request.Endereco
            };

            var result = await _servico.AddEditoraAsync(editoraModel);

            if (result.Status is ResultStatus.Invalid)
            {
                editoraResponse.Code = (int)context.Status.StatusCode;
                editoraResponse.Errors.AddRange(result.Errors);
                editoraResponse.Message = $"Editora possui erros de validação";
                editoraResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new EditoraValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return editoraResponse;
            }

            editoraResponse.Code = (int)result.Status;
            editoraResponse.Errors.AddRange(result.Errors);
            editoraResponse.Message = "Editora criada com sucesso";
            editoraResponse.Data = new EditoraModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                SiteURL = result.Value.SiteURL,
                Endereco = result.Value.Endereco
            };

            return editoraResponse;
        }

        public override async Task<EditoraResponse> Update(EditoraUpdateRequest request, ServerCallContext context)
        {
            var editoraResponse = new EditoraResponse();
            var editoraModel = new Application.Models.EditoraModel
            {
                Nome = request.Nome,
                SiteURL = request.SiteURL,
                Endereco = request.Endereco
            };

            var result = await _servico.UpdateEditoraAsync(Guid.Parse(request.EditoraId), editoraModel);

            if (result.Status is ResultStatus.NotFound)
            {
                editoraResponse.Code = (int)context.Status.StatusCode;
                editoraResponse.Errors.AddRange(result.Errors);
                editoraResponse.Message = $"Nenhum editora com o id '{request.EditoraId}' foi encontrada";

                return editoraResponse;
            }

            if (result.Status is ResultStatus.Invalid)
            {
                editoraResponse.Code = (int)context.Status.StatusCode;
                editoraResponse.Errors.AddRange(result.Errors);
                editoraResponse.Message = $"Editora possui erros de validação";
                editoraResponse.Validations.AddRange(result.ValidationErrors
                    .Select(x => new EditoraValidationErrorsModel
                    {
                        Error = x.ErrorMessage,
                        Property = x.Identifier,
                        Nivel = x.Severity.ToString()
                    }));

                return editoraResponse;
            }

            editoraResponse.Code = (int)result.Status;
            editoraResponse.Errors.AddRange(result.Errors);
            editoraResponse.Message = "Editora atualizado com sucesso";
            editoraResponse.Data = new EditoraModel
            {
                Id = result.Value.Id.ToString(),
                Nome = result.Value.Nome,
                SiteURL = result.Value.SiteURL,
                Endereco = result.Value.Endereco
            };

            return editoraResponse;
        }

        public override async Task<EditoraResponse> Delete(EditoraFilter request, ServerCallContext context)
        {
            var editoraResponse = new EditoraResponse();

            var result = await _servico.DeleteEditoraAsync(Guid.Parse(request.EditoraId));

            if (result.Status is ResultStatus.NotFound)
            {
                editoraResponse.Code = (int)context.Status.StatusCode;
                editoraResponse.Errors.AddRange(result.Errors);
                editoraResponse.Message = $"Nenhum editora com o id '{request.EditoraId}' foi encontrada";

                return editoraResponse;
            }

            editoraResponse.Code = (int)result.Status;
            editoraResponse.Errors.AddRange(result.Errors);
            editoraResponse.Message = "Editora deletada com sucesso";

            return editoraResponse;
        }

        public override async Task GetLivros(EditoraFilter request, IServerStreamWriter<EditoraLivrosResponse> responseStream, ServerCallContext context)
        {
            var livrosResponse = new EditoraLivrosResponse();

            var result = await _servico.GetLivrosEditoraAsync(Guid.Parse(request.EditoraId));

            if (result.Status is ResultStatus.NotFound)
            {
                livrosResponse.Code = (int)context.Status.StatusCode;
                livrosResponse.Errors.AddRange(result.Errors);
                livrosResponse.Message = $"Nenhum editora com o id '{request.EditoraId}' foi encontrada";

                await responseStream.WriteAsync(livrosResponse);
            }

            livrosResponse.Code = (int)result.Status;
            livrosResponse.Errors.AddRange(result.Errors);
            livrosResponse.Message = "Busca realizada com sucesso";
            livrosResponse.Data.AddRange(
                result.Value.Select(x => new EditoraLivroModel
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
    }
}
