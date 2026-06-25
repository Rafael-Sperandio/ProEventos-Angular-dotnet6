using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface IPalestrantePersist : IGeralPersist
    {

        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos= false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos);
    }
}
