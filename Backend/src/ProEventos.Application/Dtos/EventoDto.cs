using ProEventos.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório."),
         //MinLength(3, ErrorMessage = "{0} deve ter no mínimo 4 caracteres."),
         //MaxLength(50, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")
         StringLength(50, MinimumLength = 3,
                          ErrorMessage = "O campo {0} tem Intervalo de 3 a 50 caracteres.")]

        [Display(Name = "Qtd Pessoas")]
        [Range(1, 120000, ErrorMessage = "{0} não pode ser menor que 1 e maior que 120.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                           ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "e-mail")] //ordem não intefere server para o decorator acima mensagem com e-mail também
        [EmailAddress(ErrorMessage = "É necessário ser um {0} válido")]
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; } = new List<LoteDto>();

        public IEnumerable<RedeSocialDto> RedesSociais { get; set; } = new List<RedeSocialDto>();

        public IEnumerable<PalestranteDto> Palestrantes { get; set; } = new List<PalestranteDto>();
    }
}
