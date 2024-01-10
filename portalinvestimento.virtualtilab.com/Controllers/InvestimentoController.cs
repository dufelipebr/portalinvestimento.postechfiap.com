using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet("GetInvestimentoByID/{id:int}")]
        public Investimento GetInvestimentoByID(int id)
        {
            return (Investimento)_investimentoRepository.ObterPorId(id);

        }

        [Authorize]
        [HttpGet("GetCarteira/{id}")]
        public Carteira GetCarteira(int ID)
        {
            return (Carteira)_carteiraRepository.ObterPorId(ID);
        }

        [Authorize]
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

        //[Authorize(Roles = Permissoes.Administrador)]
        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            _logger.Log(LogLevel.Information, "Iniciando GetAllUsers...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                return Ok(_usuarioRepository.ObterTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na _usuarioRepository.ObterTodos() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona usuário na lista de usuarios do sistema. É aberto ao publico
        /// </summary>
        /// <param name="nome">Nome do usuario</param>
        /// <param name="pass">Senha do usuario</param>
        /// <param name="email">Email do usuario</param>
        /// <param name="tipoPermissao">1 - Administrador e 2 - Funcionario</param>
        /// <returns>IActionResult com resultado da operação</returns>
        /// <remarks> Exemplo: AddUser("carlos", "12345", "carlosedu@itnext.com.br", 1)</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        [HttpPost("AddUser")]
        public IActionResult AddUser(string nome, string pass, string email, int tipoPermissao)
        {
            _logger.Log(LogLevel.Information, "Iniciando AddUser...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                Usuario u = new Usuario();
                u.Nome = nome;
                u.Senha = pass;
                u.Email = email;
                u.TipoPermissao = (EnTipoAcesso) tipoPermissao;
                _usuarioRepository.Cadastrar(u);
                return Ok("user cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na AddUser() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Permissoes.Administrador)]
        [HttpPut("ModifyUser")]
        public IActionResult ChangeUser(int id, string nome, string pass, string email, int tipoPermissao)
        {
            _logger.Log(LogLevel.Information, "Iniciando ModifyUser...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                Usuario u = new Usuario();
                u.Id = id;
                u.Nome = nome;
                u.Senha = pass;
                u.Email = email;
                u.TipoPermissao = (EnTipoAcesso)tipoPermissao;
                _usuarioRepository.Alterar(u);
                return Ok("user alterado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na ModifyUser() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Permissoes.Administrador)]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            _logger.Log(LogLevel.Information, "Iniciando DeleteUser...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                Usuario u = new Usuario();
                u.Id = id;
                _usuarioRepository.Deletar(u);
                return Ok("user excluido com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na DeleteUser() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
