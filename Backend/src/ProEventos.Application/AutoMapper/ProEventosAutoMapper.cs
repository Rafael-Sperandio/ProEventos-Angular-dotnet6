
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Models;

namespace ProEventos.Application.AutoMapper
{
    public class ProEventosAutoMapper : Profile
    {
        public ProEventosAutoMapper()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
        }
    }
}
