using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;
using ProEventos.Domain.Models;


namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }

        public Titulo Titulo { get; set; } = Titulo.NaoInformado;
        public string Descricao { get; set; } = string.Empty;
        public Funcao Funcao { get; set; } = Funcao.NaoInformado;
        public string ImagemURL { get; set; } = string.Empty;
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
