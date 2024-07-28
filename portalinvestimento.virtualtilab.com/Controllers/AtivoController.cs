using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using portalinvestimento.virtualtilab.com.Interfaces.Service;
using portalinvestimento.virtualtilab.com.Services;

namespace portalativo.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("Ativo")]
    public class AtivoController : ControllerBase
    {
        private IAtivoRepository _ativoRepository;

        private readonly ILogger<AtivoController> _logger;
        private readonly IAtivoService _ativoService;


        public AtivoController(
                IAtivoRepository repository, 
                ILogger<AtivoController> logger, 
                IAtivoService ativoService)
        {
            _ativoRepository = repository;
            _logger = logger;
            _ativoService = ativoService;
        }

        /// <summary>
        /// listar ativos cadastrados no sistema. acesso somente autorizado via token.
        /// </summary>
        /// <returns>IActionResult com array de ativos IList/<Ativo/></returns>
        /// <remarks> Exemplo: GetAtivoList()</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("listar_ativos")]   
        public IActionResult GetAtivoList()
        {
            _logger.Log(LogLevel.Information, "Iniciando getAtivoList...");
            IEnumerable<Ativo> list = new List<Ativo>();
            try
            {
                list = _ativoRepository.ObterTodos();
            }
            catch (Exception ex) {
                    _logger.LogError($"falha ao executar _ativoRepository.ObterTodos() : {ex.Message}");
                return BadRequest();
            }
            return Ok(_ativoRepository.ObterTodos());
        }

        /// <summary>
        /// obter informações do ativo. acesso somente autorizado via token.
        /// </summary>
        /// <returns>objeto ativo preenchido</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("obter_info_ativo/{id:int}")]
        public Ativo GetAtivoByID(int id)
        {
            return (Ativo)_ativoRepository.ObterPorId(id);

        }

        /// <summary>
        /// adicionar novo ativo. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - ativo cadastrado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("adicionar_ativo")]
        public IActionResult AddAtivo(CadastrarAtivoDTO ativoDTO)
        {
            _logger.Log(LogLevel.Information, "Iniciando adicionar_ativo...");

            try
            {
                Ativo ativo = new Ativo(ativoDTO);
                AtivoService ativoService = new AtivoService();
                string resultado = ativoService.Create(ativo);
                if (resultado == "Ok")
                {
                    _ativoRepository.Cadastrar(ativo);
                    return Ok("ativo cadastrado com sucesso");
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na adicionar_ativo() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// modifica um ativo criado. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - ativo modificado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPut("modificar_ativo")]
        public IActionResult ChangeAtivo(ModificarAtivoDTO dto)
        {
            _logger.Log(LogLevel.Information, "Iniciando ChangeAtivo...");

            try
            {
                Ativo ativo = new Ativo(dto);
                AtivoService ativoService = new AtivoService();
                string resultado = ativoService.Modify(ativo);
                if (resultado == "Ok")
                {
                    _ativoRepository.Alterar(ativo);
                    return Ok("ativo modificado com sucesso");
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na ChangeAtivo() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// exclui um ativo. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - ativo modificado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpDelete("excluir_ativo")]
        public IActionResult DeleteAtivo(int id)
        {
            _logger.Log(LogLevel.Information, "Iniciando DeleteAtivo...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                var ativo = _ativoRepository.ObterPorId(id);
                if (ativo != null)
                {
                    _ativoRepository.Deletar(ativo);
                    return Ok("user excluido com sucesso");
                }
                else
                {
                    return BadRequest("usuario inexistente");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na DeleteAtivo() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
