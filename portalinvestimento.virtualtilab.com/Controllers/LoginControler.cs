
using Microsoft.AspNetCore.Mvc;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
namespace portalinvestimento.virtualtilab.com.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginControler : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginControler(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

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
    }
}
