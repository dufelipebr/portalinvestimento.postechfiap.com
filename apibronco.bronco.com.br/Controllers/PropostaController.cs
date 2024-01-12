using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace apibronco.bronco.com.br.Controllers
{
    [ApiController]
    [Route("Proposta")]
    public class PropostaController : ControllerBase
    {
        private IPropostaRepository _propostaRepository;
        //private ICarteiraRepository _carteiraRepository;
        private IUsuarioRepository _usuarioRepository;
        //private ILoggerFactory _loggerFactory;
        //private ILoggerProvider _loggerProvider;
        private readonly ILogger<PropostaController> _logger;


        public PropostaController(IPropostaRepository repository, IUsuarioRepository usuarioRepository, ILogger<PropostaController> logger)
        {
            _propostaRepository = repository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetPropostaAll")]
        //public IEnumerable<Proposta> GetPropostaList()
        public IActionResult GetPropostaList()
        {
            _logger.Log(LogLevel.Information, "Iniciando GetPropostaList...");
            IEnumerable<Proposta> list;
            try
            {
                list = _propostaRepository.ObterTodos();
            }
            catch (Exception ex) {
                _logger.LogError($"falha ao executar _propostaRepository.GetPropostaList() : {ex.Message}");
                return BadRequest();
            }
            return Ok(_propostaRepository.ObterTodos());
        }

        [Authorize]
        [HttpPost("CriarProposta")]
        public IActionResult CriarProposta(Proposta newProposta)
        {
            _logger.Log(LogLevel.Information, "Iniciando CriarProposta...");

            //Regex cpfRx = new Regex(@"^(((\d{3}).(\d{3}).(\d{3})-(\d{2}))?((\d{2}).(\d{3}).(\d{3})/(\d{4})-(\d{2}))?)*$", RegexOptions.None);
            //Regex emailRx = new Regex(@"^([-a-zA-Z0-9_-]*@(gmail|yahoo|ymail|rocketmail|bol|hotmail|live|msn|ig|globomail|oi|pop|inteligweb|r7|folha|zipmail).(com|info|gov|net|org|tv)(.[-a-z]{2})?)*$");

            //Carteira ct = (Carteira)_carteiraRepository.GetCarteiraByID(newCarteira.CodigoConta, newCarteira.DigitoConta);
            //if (ct != null) throw new Exception("erro02: carteira já existente");

            if (newProposta.Ramo == null)
                throw new Exception("CriarProposta.erro1: Ramo não informado");

            if (newProposta.Moeda == "BRL")
                throw new Exception("CriarProposta.erro2: BRL deve ser informado para moeda");

            if (newProposta.Coberturas == null)
                throw new Exception("CriarProposta.erro3: Coberturas devem ser informadas");

            if (newProposta.Codigo_Empresa == null)
                throw new Exception("CriarProposta.erro4: Codigo da Empresa deve ser informado");

            if (newProposta.Forma_Pagamento == null)
                throw new Exception("CriarProposta.erro5: Forma de pagamento deve ser informado");

            if (newProposta.Codigo_Produto == null)
                throw new Exception("CriarProposta.erro6: Codigo do Produto");

            try
            {

                _propostaRepository.Cadastrar(newProposta);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar _propostaRepository.GetPropostaList() : {ex.Message}");
                return BadRequest();
            }
            return Ok("proposta cadastrada com sucesso");
        }
    }
}

