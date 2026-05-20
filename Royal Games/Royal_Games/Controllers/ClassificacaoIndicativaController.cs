using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.Domains;
using Royal_Games.DTOs.ClassificacaoDto;
using Royal_Games.Exceptions;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.GeneroDto;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
            private readonly ClassificacaoIndicativaService _service;

            public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
            {
                _service = service;
            }

            [HttpGet]
            public ActionResult<List<LerClassificacaoDto>> Listar()
            {
                List<LerClassificacaoDto> classificacoes = _service.Listar();
                return Ok(classificacoes);
            }

            [HttpGet("{id}")]
            public ActionResult<LerClassificacaoDto> ObterPorId(int id)
            {
                try
                {
                LerClassificacaoDto Classificacao = _service.ObterPorId(id);
                    return Ok(Classificacao);
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpPost]
            //[Authorize]
            public ActionResult Adicionar(CriarClassificacaoDto criarDto)
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
            public ActionResult Atualizar(int id, CriarClassificacaoDto criarDto)
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
