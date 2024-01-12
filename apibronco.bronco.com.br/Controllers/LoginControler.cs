
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Services;
namespace apibronco.bronco.com.br.Controllers
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
