using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using portalinvestimento.virtualtilab.com.Interfaces.Service;
using portalinvestimento.virtualtilab.com.Services;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace portalativo.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("Portfolio")]
    public class PortfolioController : ControllerBase
    {
        private IPortfolioRepository _portfolioRepository;
        //private ILoggerFactory _loggerFactory;
        //private ILoggerProvider _loggerProvider;
        private readonly ILogger<PortfolioController> _logger;
        private IUsuarioRepository _usuarioRepository;


        public PortfolioController(
                IPortfolioRepository portfolioRepository,
                ILogger<PortfolioController> logger, 
                IUsuarioRepository usuarioRepository
        ){
            _logger = logger;
            _portfolioRepository = portfolioRepository;
            _usuarioRepository = usuarioRepository;
        }



        #region portfolio

        /// <summary>
        /// listar portfolios cadastrados no sistema. acesso somente autorizado via token.
        /// </summary>
        /// <returns>IActionResult com array de portfolios IList/<Investimento/></returns>
        /// <remarks> Exemplo: GetInvestimentoList()</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("listar_portfolios")]
        public IActionResult GetPortfolioList()
        {
            _logger.Log(LogLevel.Information, "Iniciando getInvestimentoList...");
            IEnumerable<Portfolio> list = new List<Portfolio>();
            try
            {
                list = _portfolioRepository.ObterTodos();
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar _portfolioRepository.ObterTodos() : {ex.Message}");
                return BadRequest();
            }
            return Ok(_portfolioRepository.ObterTodos());
        }

        /// <summary>
        /// adicionar novo portfolio. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - portfolio cadastrado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("adicionar_portfolio")]
        public IActionResult AddPortfolio(CadastrarPortfolioDTO dto)
        {
            _logger.Log(LogLevel.Information, "Iniciando adicionar_portfolio...");

            try
            {
                Portfolio portfolio = new Portfolio(dto);
                PortfolioService portfolioService = new PortfolioService();

                Usuario usr = _usuarioRepository.ObterPorId(dto.Id_Usuario);
                if (usr == null)
                    throw new ArgumentException("Id_Usuario não é valido");

                string resultado = portfolioService.Create(portfolio);
                if (resultado == "Ok")
                {
                    _portfolioRepository.Cadastrar(portfolio);
                    return Ok("portfolio cadastrado com sucesso");
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na adicionar_portfolio() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// modifica um portfolio criado. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - portfolio modificado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPut("modificar_portfolio")]
        public IActionResult ChangePortfolio(ModificarPortfolioDTO dto)
        {
            _logger.Log(LogLevel.Information, "Iniciando ChangeInvestimento...");

            try
            {
                Portfolio changed = new Portfolio(dto);
                Portfolio original = _portfolioRepository.ObterPorId(dto.Id);

                if (original == null)
                    throw new ArgumentException("Portfolio não encontrado");

                Usuario usr = _usuarioRepository.ObterPorId(dto.Id_Usuario);
                if (usr == null)
                    throw new ArgumentException("Id_Usuario não é valido");


                original.Codigo = dto.Codigo;
                original.Descricao = dto.Descricao;
                original.Nome = dto.Nome;
                original.Id_Usuario = dto.Id_Usuario;
                original.LastChanged = DateTime.Now;
                original.Slug = "modificado";

                original.ValidateEntity();
                _portfolioRepository.Alterar(original);
                return Ok("portfolio modificado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na ChangeInvestimento() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// exclui um portfolio. acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - portfolio modificado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpDelete("excluir_portfolio")]
        public IActionResult DeletePortfolio(int id)
        {
            _logger.Log(LogLevel.Information, "Iniciando DeleteInvestimento...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                var portfolio = _portfolioRepository.ObterPorId(id);
                if (portfolio != null)
                {
                    _portfolioRepository.Deletar(portfolio);
                    return Ok("user excluido com sucesso");
                }
                else
                {
                    return BadRequest("usuario inexistente");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na DeleteInvestimento() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}
