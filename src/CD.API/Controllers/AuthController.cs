using CD.API.Extensions;
using CD.API.ViewModels;
using CD.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CD.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              INotificador notificador,
                              IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Método responsável pela criação de uma nova conta de usuário
        /// </summary>
        /// <param name="usuarioCadastro"></param>
        /// <returns></returns>
        [HttpPost("new-account")]
        public async Task<IActionResult> NovaConta([FromBody] UsuarioCadastro usuarioCadastro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var novoUsuario = new IdentityUser
            {
                UserName = usuarioCadastro.Email,
                Email = usuarioCadastro.Email,
                EmailConfirmed = true,
            };

            var resultado = await _userManager.CreateAsync(novoUsuario, usuarioCadastro.Senha);

            if (resultado.Succeeded)
            {                
                return CustomResponse(await GerarTokenJWT(usuarioCadastro.Email));
            }

            foreach (var erro in resultado.Errors)
            {
                AdicionarErro(erro.Description);
            }

            return CustomResponse();
        }

        /// <summary>
        /// Método responsável em efetuar login usuário retornando um token jwt
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (resultado.Succeeded)
            {
                return CustomResponse(await GerarTokenJWT(usuarioLogin.Email));
            }

            if (resultado.IsLockedOut)
            {
                AdicionarErro("Usuário temporariamente bloqueado por tentativas inválidas.");
                return CustomResponse();
            }

            AdicionarErro("Usuário ou Senha inválidos.");
            return CustomResponse();
        }

        #region TokenJWT

        private async Task<LoginResponse> GerarTokenJWT(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var usuarioClaims = await _userManager.GetClaimsAsync(usuario);
            var usuarioRoles = await _userManager.GetRolesAsync(usuario);

            // Claims e Roles
            usuarioClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            usuarioClaims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));

            foreach (var usuarioRole in usuarioRoles)
            {
                usuarioClaims.Add(new Claim("role", usuarioRole));
            }
            
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(usuarioClaims);

            // Codificação do Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.ChaveToken);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.TempoExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var jwtToken = tokenHandler.WriteToken(token);

            // Gerar Resposta
            return new LoginResponse
            {
                AccessToken = jwtToken,
                ExpiracaoEm = TimeSpan.FromHours(_appSettings.TempoExpiracaoHoras).TotalSeconds,
                Usuario = new UsuarioToken
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = usuarioClaims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        #endregion
    }
}
