using ProEventos.Domain.Identity;

namespace ProEventos.Domain.Models
{
    public class Palestrante
    {
        public int Id { get; set; }
        //public string Nome { get; set; } //remover
        public string MiniCurriculo { get; set; }
/*
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
*/
        public int UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<RedeSocial> RedesSociais { get; set; } = new List<RedeSocial>();

        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }  = new List<PalestranteEvento>();


    }
}