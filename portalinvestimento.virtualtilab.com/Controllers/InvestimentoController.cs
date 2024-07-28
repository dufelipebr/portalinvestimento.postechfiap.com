using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;
//using portalinvestimento.virtualtilab.com.Interfaces;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using portalinvestimento.virtualtilab.com.Interfaces.Service;
using portalinvestimento.virtualtilab.com.Services;

//using portalinvestimento.virtualtilab.com.Services;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace portalativo.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("Investimento")]
    public class InvestimentoController : ControllerBase
    {
        private IAtivoRepository _ativoRepository;
        private IAplicacaoRepository _aplicacaoRepository;
        private IUsuarioRepository _usuarioRepository;
        private IRentabilidadeRepository _rentabilidadeRepository;
        private IPortfolioRepository _portfolioRepository;
        //private ILoggerFactory _loggerFactory;
        //private ILoggerProvider _loggerProvider;
        private readonly ILogger<InvestimentoController> _logger;
        private readonly IAplicacaoService _ativoService;


        public InvestimentoController(
                IAtivoRepository repository, 
                IAplicacaoRepository aplicacaoRepository, 
                IUsuarioRepository usuarioRepository, 
                IRentabilidadeRepository rentabilidadeRepository,
                IPortfolioRepository portfolioRepository,
                ILogger<InvestimentoController> logger, 
                IAplicacaoService ativoService)
        {
            
            _aplicacaoRepository = aplicacaoRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _ativoService = ativoService;
            _rentabilidadeRepository = rentabilidadeRepository;
            _portfolioRepository = portfolioRepository;
        }

      


        /// <summary>
        /// obtem lista de aplicações realizadas pelo usuario. acesso somente autorizado via token.
        /// </summary>
        /// <returns>lista de objetos do tipo Aplicacao</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("listar_aplicacoes_por_usuario/{usuario_id:int}")]
        public IActionResult ListaAplicacoesUsuario(int Usuario_id)
        {
            _logger.Log(LogLevel.Information, "Iniciando ListaAplicacoesUsuario...");
            try
            {
                var user = _usuarioRepository.ObterPorId(Usuario_id);
                if (user == null)
                    return BadRequest("usuario invalido!");

                var aplicacoes = _aplicacaoRepository.ObterAplicacaoPorUserId(Usuario_id);
                return Ok(aplicacoes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail creating ListaAplicacoesUsuario ...{ex.Message}");
                return BadRequest("falhar ao ler as aplicações");
            }
        }

        /// <summary>
        /// realiza um aporte do ativo para o usuario mediante a saldo em carteira. 
        /// caso não tenha saldo na carteira não irá permitir a inclusão do aporte. 
        /// caso seja necessário aportar saldo na carteira utilizar as funções de modificar_usuario.
        /// acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - aplicacao realizada com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("aportar_investimento")]
        public IActionResult AportarInvestimento(CadastrarAplicacaoDTO aplicacao)
        {
            _logger.Log(LogLevel.Information, "Iniciando AportarInvestimento...");
            try
            {
                InvestimentoService ativoService = new InvestimentoService();

                Portfolio p = _portfolioRepository.ObterPorId(aplicacao.Id_Portfolio);
                Ativo i = _ativoRepository.ObterPorId(aplicacao.Id_Investimento);
                Transacao t = new Transacao(aplicacao);
                //Transacao ap = _aplicacaoRepository.ObterAplicacao(aplicacao.Id_Usuario, aplicacao.Id_Investimento);

                //if (ap == null) // AP não existe precisa ser criado
                //{
                    //ap = ativoService.CriarAplicacao(u, i, aplicacao);
                    _aplicacaoRepository.Cadastrar(t);
                //}
                //else // precisa aumentar o valor de aplicacão
                //{
                //    //if (aplicacao.Valor_Aplicacao > u.Saldo_Carteira)
                //    //    return BadRequest("valor da aplicação maior que disponivel para saldo");

                //    ap.Preco += aplicacao.Valor_Aplicacao;
                //    _aplicacaoRepository.Alterar(ap);
                    
                //}

                //u.Saldo_Carteira -= aplicacao.Valor_Aplicacao;
                //_usuarioRepository.Alterar(u);
                return Ok("aplicacao realizada com sucesso");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Falha na Aporte da aplicação: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail creating AportarInvestimento ...{ex.Message}");
                return BadRequest("Falha na Aporte da aplicação.");
            }
        }

        /// <summary>
        /// realiza um resgate do ativo e coloca na carteira do usuario. 
        /// nesse caso o valor de rentabilidade será perdido e somente calculado após proximo calculo de rentabilidade.
        /// acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - resgate realizado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("resgatar_investimento")]
        public IActionResult ResgatarInvestimento(ResgatarAplicacaoDTO cad)
        {
            _logger.Log(LogLevel.Information, "Iniciando ResgatarInvestimento...");
            try
            {
                InvestimentoService ativoService = new InvestimentoService();

                Portfolio u = _portfolioRepository.ObterPorId(cad.Id_Portfolio);
                if (u == null)
                    throw new Exception("Porfolio não encontrado");
                
                Ativo i = _ativoRepository.ObterPorId(cad.Id_Investimento);
                if (i == null)
                    throw new Exception("Ativo não encontrado");
                Transacao p = new Transacao(cad);

                _aplicacaoRepository.Cadastrar(p);

                //if (ap == null) // AP não existe precisa ser criado
                //{
                //    return BadRequest("Não existe aplicação para o usuario e ativo informados.");
                //}
                //else // precisa aumentar o valor de aplicacão
                //{
                //    if (cad.Valor_Resgate > ap.Valor_Atualizado)
                //        return BadRequest($"valor do resgate maior que valor disponivel para resgate.:{ap.Valor_Atualizado}");

                //    ap.Preco = ap.Valor_Atualizado; // Zerar a rentabilidade do cara e jogar tudo na aplicação para poder resgatar.
                //    ap.Rentabilidade = 0;
                //    ap.Preco -= cad.Valor_Resgate;
                //    //if (ap.Valor_Aplicacao < 0) {  // se ficou negativo a aplicação tem que pegar do valor aplicado.
                //    //    var rent = ap.Rentabilidade;
                //    //    ap.Valor_Aplicacao = rent- ap.Rentabilidade;

                //    //}
                //    _aplicacaoRepository.Alterar(ap);
                    
                //}

                //u.Saldo_Carteira += cad.Valor_Resgate;
                //_usuarioRepository.Alterar(u);
                return Ok("resgate realizado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail creating ResgatarInvestimento ...{ex.Message}");
                return BadRequest("Falha no resgate do ativo.");
            }
        }

        /// <summary>
        /// cadastrar rentabilidade diaria\mensal do ativo e aplica nos ativos dos usuarios. 
        /// acesso somente autorizado via token.
        /// </summary>
        /// <returns>string - rentabilidade realizado com sucesso</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("cadastrar_rentabilidade")]
        public IActionResult CadastrarRentabilidade(CadastrarRentabilidadeDTO cad)
        {
            _logger.Log(LogLevel.Information, "Iniciando CadastrarRentabilidade...");
            try
            {
                //Services.InvestimentoService ativoService = new Services.InvestimentoService();

                //Usuario u = _usuarioRepository.ObterPorId(cad.Id_Usuario);
                // checar ativo existente.
                Ativo i = _ativoRepository.ObterPorId(cad.Id_Investimento);
                if (i == null)
                    return BadRequest("ativo não encontrado!");

                // checar se já foi cadastrado para data e ativo
                var listRentabilidade = _rentabilidadeRepository.ObterTodos();
                var result = listRentabilidade.Where(x => x.Data_Operacao == cad.Data_Operacao &&
                    x.Investimento.Id == cad.Id_Investimento).FirstOrDefault();

                RentabilidadeInvestimento ri;
                if (result == null)
                { 
                    ri = new RentabilidadeInvestimento(cad);
                    _rentabilidadeRepository.Cadastrar(ri);
                }
                else
                {
                    ri = result;
                    _rentabilidadeRepository.Alterar(ri);
                }

                List<Transacao> list = (List<Transacao>)_aplicacaoRepository.ObterTodos();
                foreach(var item in list.Where(x => x.Investimento.Id == cad.Id_Investimento))
                {
                    var rentabilidade_atual = item.Valor_Atualizado * cad.Valor_Rentabilidade;
                    item.Rentabilidade += rentabilidade_atual;
                    item.Ultima_Rentabilidade_Calculada = DateTime.Now;
                    _aplicacaoRepository.Alterar(item);
                }

                return Ok("rentabilidade realizado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail creating CadastrarRentabilidade ...{ex.Message}");
                return BadRequest("Falha no calculo de rentabilidade.");
            }
        }

        /// <summary>
        /// obter todas as rentabilidade de ativo cadastradas. 
        /// acesso somente autorizado via token.
        /// </summary>
        /// <returns>List de objeto Rentabilidade</returns>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        [Authorize (Roles = "Admin")]
        [HttpGet("obter_rentabilidades")]
        public IActionResult GetRentabilidadeAll()
        {
            _logger.Log(LogLevel.Information, "Iniciando GetRentabilidadeAll...");
            IEnumerable<RentabilidadeInvestimento> list = new List<RentabilidadeInvestimento>();
            try
            {
                list = _rentabilidadeRepository.ObterTodos();
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar GetRentabilidadeAll.ObterTodos() : {ex.Message}");
                return BadRequest();
            }
            return Ok(list);
        }

        ///// <summary>
        ///// Permite alterar o saldo da Carteira do usuario
        ///// </summary>
        ///// <param name="dto">informações do saldo e usuario id</param>
        ///// <returns>string - saldo alterado com sucesso</returns>
        //[HttpPut("modifica_saldo_carteira")]
        //public IActionResult ChangeUser(AlterarUsuarioDTO dto)
        //{
        //    _logger.Log(LogLevel.Information, "Iniciando ModifyUser...");

        //    try
        //    {
        //        //return _usuarioRepository.ObterTodos();
        //        Usuario u = new Usuario(dto);
        //        _usuarioRepository.Alterar(u);
        //        return Ok("saldo alterado com sucesso");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"erro na ModifyUser() ex: {ex.Message}");
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
