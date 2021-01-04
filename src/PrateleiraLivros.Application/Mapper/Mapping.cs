using AutoMapper;
using PrateleiraLivros.Application.Models;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Application.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Autor, AutorModel>().ReverseMap();
            CreateMap<Editora, EditoraModel>().ReverseMap();
            CreateMap<Livro, LivroModel>().ReverseMap();
        }
    }
}
