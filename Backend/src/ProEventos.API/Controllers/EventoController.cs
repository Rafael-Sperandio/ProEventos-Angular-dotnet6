using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {

        private readonly ILogger<EventoController> _logger;

        public EventoController(ILogger<EventoController> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Evento> _evento = new Evento[] {
            new Evento() {
                EventoId = 1,
                Tema = "Angular 11 e .NET 5",
                Local = "Belo Horizonte",
                Lote = "1º Lote",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemURL = "foto.png"
            },
            new Evento() {
                EventoId = 2,
                Tema = "Angular e Suas Novidades",
                Local = "São Paulo",
                Lote = "2º Lote",
                QtdPessoas = 350,
                DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
                ImagemURL = "foto1.png"
            }
        };

        [HttpGet]
        public IEnumerable<Evento> Get()
        {

            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }
    

    }
}
