using Grpc.Core;
using PrateleiraLivros.Application.Interfaces;
using PrateleiraLivros.Dominio.Interfaces;
using PrateleiraLivros.gRPCService.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PrateleiraLivros.gRPCService.Services
{
    public class CargaLivroGrpcService : CargaLivro.CargaLivroBase
    {
        private readonly ICsvServico _csvServico;
        private readonly ILivroAppServico _appServico;

        public CargaLivroGrpcService(ICsvServico csvServico, ILivroAppServico appServico)
        {
            _csvServico = csvServico ?? throw new ArgumentNullException(nameof(csvServico));
            _appServico = appServico ?? throw new ArgumentNullException(nameof(appServico));
        }

        public override async Task<FileUploadResponse> UploadFile(IAsyncStreamReader<FileUploadRequest> requestStream, ServerCallContext context)
        {
            try
            {
                List<Dominio.Entidades.Livro> livrosCarga = null;

                while (await requestStream.MoveNext())
                {
                    using var ms = new MemoryStream(requestStream.Current.Content.ToByteArray());
                    livrosCarga = await _csvServico.GetCargaLivros(ms);
                }

                return new FileUploadResponse
                {
                    Message = "Carga dos livros realizada com sucesso",
                    Status = "SUCCESS",
                    CountUploaded = livrosCarga.Count
                };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse
                {
                    Message = ex.Message,
                    Status = "ERROR",
                    CountUploaded = 0
                };
            }
        }
    }
}
