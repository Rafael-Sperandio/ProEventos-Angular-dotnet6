using ProEventos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(int userId,string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(int userId,bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int userId,int eventoId, bool includePalestrantes = false);
    }
}
