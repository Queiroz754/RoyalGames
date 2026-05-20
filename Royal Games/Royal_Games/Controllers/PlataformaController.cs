using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.Domains;
using Royal_Games.DTOs.PlataformaDto;
using Royal_Games.Exceptions;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.GeneroDto;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly PlataformaService _service;

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<lerPlataformaDto>> Listar()
        {
            List<lerPlataformaDto> Plataformas = _service.Listar();
            return Ok(Plataformas);
        }

        [HttpGet("{id}")]
        public ActionResult<lerPlataformaDto> ObterPorId(int id)
        {
            try
            {
                lerPlataformaDto plataforma = _service.ObterPorId(id);
                return Ok(plataforma);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult Adicionar(CriarPlataformaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return Ok(criarDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[Authorize]
        public ActionResult Atualizar(int id, CriarPlataformaDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

