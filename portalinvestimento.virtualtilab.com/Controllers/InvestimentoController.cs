using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace portalinvestimento.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("Investimento")]
    public class InvestimentoController : ControllerBase
    {
        private IInvestimentoRepository _investimentoRepository;
        private ICarteiraRepository _carteiraRepository;
        private IUsuarioRepository _usuarioRepository;
        //private ILoggerFactory _loggerFactory;
        //private ILoggerProvider _loggerProvider;
        private readonly ILogger<InvestimentoController> _logger;


        public InvestimentoController(IInvestimentoRepository repository, ICarteiraRepository carteiraRepository, IUsuarioRepository usuarioRepository, ILogger<InvestimentoController> logger)
        {
            _investimentoRepository = repository;
            _carteiraRepository = carteiraRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpGet("GetInvestimentoAll")]
        //public IEnumerable<Investimento> GetInvestimentoList()
        public IActionResult GetInvestimentoList()
        {
            _logger.Log(LogLevel.Information, "Iniciando getInvestimentoList...");
            IEnumerable<Investimento> list = new List<Investimento>();
            try
            {
                list = _investimentoRepository.ObterTodos();
            }
            catch (Exception ex) {
                _logger.LogError($"falha ao executar _investimentoRepository.ObterTodos() : {ex.Message}");
                return BadRequest();
            }
            return Ok(_investimentoRepository.ObterTodos());
        }

        [HttpGet("GetInvestimentoByID/{id:int}")]
        public Investimento GetInvestimentoByID(int id)
        {
            return (Investimento)_investimentoRepository.ObterPorId(id);

        }

        [HttpGet("GetCarteira/{id}")]
        public Carteira GetCarteira(int ID)
        {
            return (Carteira)_carteiraRepository.ObterPorId(ID);
        }

        [HttpGet("Authenticate/{user}/{pwd}")]
        public string Authenticate(string user, string pwd)
        {
            //if (user == "system" && pwd == "123")
            //{

            //}
            //else
            //{
            //    throw new Exception("auth error not valid user.");
            //}
            return "ok";

        }

        [HttpPost("CriarCarteira")]
        public void CriarCarteira(Carteira newCarteira)
        {
            Regex cpfRx = new Regex(@"^(((\d{3}).(\d{3}).(\d{3})-(\d{2}))?((\d{2}).(\d{3}).(\d{3})/(\d{4})-(\d{2}))?)*$", RegexOptions.None);
            Regex emailRx = new Regex(@"^([-a-zA-Z0-9_-]*@(gmail|yahoo|ymail|rocketmail|bol|hotmail|live|msn|ig|globomail|oi|pop|inteligweb|r7|folha|zipmail).(com|info|gov|net|org|tv)(.[-a-z]{2})?)*$");

            //Carteira ct = (Carteira)_carteiraRepository.GetCarteiraByID(newCarteira.CodigoConta, newCarteira.DigitoConta);
            //if (ct != null) throw new Exception("erro02: carteira já existente");

            if (newCarteira.DigitoConta > 0 &&
                newCarteira.CodigoConta > 0 &&
                newCarteira.NomeBeneficiario != null &&
                newCarteira.CPF != null && cpfRx.Match(newCarteira.CPF) != null &&
                newCarteira.Email != null && emailRx.Match(newCarteira.Email) != null &&
                newCarteira.Endereco != null
            )
                _carteiraRepository.Cadastrar(newCarteira);
            else
                throw new Exception("erro3: dados invalidos para carteira, verifique informações.");
        }


        [HttpGet("GetAllUsers")]
        public IEnumerable<Usuario> GetAllUsers()
        {
            return _usuarioRepository.ObterTodos();
        }
    }
}
