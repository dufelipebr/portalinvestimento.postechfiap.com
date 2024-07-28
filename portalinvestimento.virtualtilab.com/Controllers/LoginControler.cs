
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using portalinvestimento.virtualtilab.com.Interfaces.Service;
using portalinvestimento.virtualtilab.com.Services;
namespace portalinvestimento.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginControler : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginControler> _logger;


        public LoginControler(IUsuarioRepository usuarioRepository, ITokenService tokenService, ILogger<LoginControler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// Usado para gerar o  token de acesso a API necessario e-mail e senha cadastrados. acesso publico.
        /// </summary>
        /// <param name="LoginDTO">login e senha do usuario</param>
        /// <returns>Usuario e Token</returns>
        /// <remarks> Exemplo: Autenticar(dto)</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">Não autenticado</response>
        /// <response code="403">Não autorizado</response>
        /// <response code="501">Erro</response>
        [HttpPost]
        public IActionResult Autenticar([FromBody] LoginDTO login)
        {
            var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(login.Email, login.Senha);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuario e ou Senha invalidos" });
            
            var token = _tokenService.GerarToken(usuario);

            usuario.Senha = null;

            return Ok(new
            {
                Usuario = usuario, 
                Token = token
            });
        }

        /// <summary>
        /// lista todos os usuarios do sistema. É aberto ao publico
        /// </summary>
        /// <returns>Lista de objeto Usuario</returns>
        [HttpGet("GetAllUsers")]
        public IActionResult obter_usuarios()
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
        /// Validação - Campo Senha - Minimo 8 caracteres e maximo 30, One upper case, Atleast one lower case, No white space, Check for one special character.
        /// Validação - Codigo_Conta e Digito_Conta - Codigo de conta já existente. Informar outra conta! - Não pode cadastrar caso já exista um usuario com a conta.
        /// </summary>
        /// <param name="dto">informações do Usuario</param>
        /// <returns>string - user cadastrado com sucesso</returns>
        [HttpPost("adicionar_usuario")]
        public IActionResult AddUser(CadastrarUsuarioDTO dto)
        {
            _logger.Log(LogLevel.Information, "Iniciando AddUser...");

            try
            {
                var resultado = _usuarioRepository.ObterTodos().
                    Where(x => x.Email == dto.Email).FirstOrDefault();

                if (resultado != null)
                    return BadRequest("Codigo de conta já existente. Informar outra conta!");

                UsuarioService d = new UsuarioService();
                bool checkPassword = d.CheckPassword(dto.Senha);
                if (checkPassword)
                {
                    //return _usuarioRepository.ObterTodos();
                    Usuario u = new Usuario(dto);
                    

                    _usuarioRepository.Cadastrar(u);
                    return Ok("user cadastrado com sucesso");
                }
                else
                {
                    return BadRequest("Senha muito fraca!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro na AddUser() ex: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// excluir usuário na lista de usuarios do sistema. É aberto ao publico
        /// </summary>
        /// <param name="id">usuario id</param>
        /// <returns>string - user excluido com sucesso</returns>
        [HttpDelete("excluir_usuario")]
        public IActionResult DeleteUser(AlterarUsuarioDTO user)
        {
            _logger.Log(LogLevel.Information, "Iniciando DeleteUser...");

            try
            {
                //return _usuarioRepository.ObterTodos();
                Usuario u = new Usuario(user);
                //u.Id = id;
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
