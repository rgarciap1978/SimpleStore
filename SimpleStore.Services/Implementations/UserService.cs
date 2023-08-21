using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace SimpleStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<SimpleStoreUserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;
        private readonly IOptions<AppConfig> _options;

        public UserService(
            UserManager<SimpleStoreUserIdentity> userManager,
            RoleManager<IdentityRole> roleManager,
            ICustomerRepository customerRepository,
            IEmailService emailService,
            ILogger<UserService> logger,
            IOptions<AppConfig> options
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _customerRepository = customerRepository;
            _emailService = emailService;
            _logger = logger;
            _options = options;
        }

        public async Task<ResponseDTOLogin> LoginAsync(RequestDTOLogin request)
        {
            var response = new ResponseDTOLogin();

            try
            {
                var identity = await _userManager.FindByEmailAsync(request.Username);
                if (identity == null) throw new SecurityException("El usuario no existe");
                if (await _userManager.IsLockedOutAsync(identity)) throw new SecurityException("Demasiados intentos fallidos");

                var result = await _userManager.CheckPasswordAsync(identity, request.Password);
                if (!result)
                {
                    _logger.LogWarning("Error de autenticación");
                    await _userManager.AccessFailedAsync(identity);

                    throw new SecurityException("La clave ingresada no es correcta");
                }

                var roles = await _userManager.GetRolesAsync(identity);
                var expiredDate = DateTime.Now.AddHours(2);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, identity.FirstName),
                    new Claim(ClaimTypes.Email, identity.UserName!),
                    new Claim(ClaimTypes.Expiration, expiredDate.ToString("yyyy-MM-dd HH:mm:ss"))
                };
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

                response.Roles = new List<string>();
                response.Roles.AddRange(roles);

                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SecretKey));
                var credetials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(credetials);
                var payload = new JwtPayload(
                    _options.Value.Jwt.Issuer,
                    _options.Value.Jwt.Audience,
                    claims,
                    DateTime.Now,
                    expiredDate
                );
                var token = new JwtSecurityToken(header, payload);

                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.FullName = $"{identity.FirstName} {identity.LastName}";
                response.Success = true;
            }
            catch (SecurityException ex)
            {
                response.Message = "Error de autenticacion";
                _logger.LogWarning(response.Message);
            }
            catch (Exception ex)
            {
                response.Message = "Error al momento de autenticar";
                _logger.LogError(ex, $"{response.Message} {ex.Message}");
            }

            return response;
        }

        public async Task<ResponseGeneric<string>> RegisterAsync(RequestDTORegister request)
        {
            var response = new ResponseGeneric<string>();
            try
            {
                var user = new SimpleStoreUserIdentity
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var userIdentity = await _userManager.FindByEmailAsync(request.Email);
                    if(userIdentity != null)
                    {
                        await _userManager.AddToRoleAsync(userIdentity, "User");

                        var customer = new Customer()
                        {
                            FullName = $"{request.FirstName} {request.LastName}",
                            Email = request.Email
                        };
                        await _customerRepository.AddAsync(customer);

                        await _emailService.SendEmailAsync(request.Email, "Bienvenido a Simple Store", "<p style=\"font-family: Verdana, Helvetica\">Gracias por registrarse en Simple Store</p>");
                        response.Success=true;
                        response.Data = user.Id;
                    }
                }
                else
                {
                    response.Success = false;
                    var sb = new StringBuilder();
                    foreach (var error in result.Errors)
                        sb.AppendLine(error.Description);

                    response.Message = sb.ToString();
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error al registrar el usuario.";
                _logger.LogError(ex, $"{response.Message} {ex.Message}");
            }

            return response;
        }

        public async Task<ResponseBase> RequestTokenResetPaswordAsync(RequestDTOGenerateToken request)
        {
            var response = new ResponseBase();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null) throw new SecurityException("Usuario no existe");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                await _emailService.SendEmailAsync(
                    request.Email,
                    "Simple Store - Recuperación de contraseña",
                    $@"<p style=""font-family:Verdana, Helvetica"">Para recuperar su clave haga click en el siguiente enlace: <a href=""{_options.Value.WebAppUrl}/reset-password?email={request.Email}&token={encodedToken}"">Recuperar Contraseña</a></p>");
            }
            catch (SecurityException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, response.Message);
            }
            catch(Exception ex)
            {
                response.Message = "Error al solicitar el token de recuperación de contraseña.";
                _logger.LogError(ex, $"{response.Message} {ex.Message}");
            }

            return response;
        }

        public async Task<ResponseBase> ResetPasswordAsync(RequestDTOResetPassword request)
        {
            var response = new ResponseBase();
            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Email);
                if (userIdentity == null) throw new SecurityException("Usuario no existe.");

                var result = await _userManager.ResetPasswordAsync(userIdentity, request.Token, request.NewPassword);
                response.Success = result.Succeeded;

                if (!result.Succeeded)
                {
                    var sb = new StringBuilder();
                    foreach (var error in result.Errors)
                        sb.AppendLine(error.Description);

                    response.Message=sb.ToString();
                    sb.Clear();
                }
                else
                {
                    await _emailService.SendEmailAsync(
                        request.Email,
                        "Simple Store - Contraseña Cambiada.",
                        @"<p style=""font-family:Verdana, Helvatica"">Su contraseña fue cambiada exitosamente.</p>");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error de cambio de contrasña {ex.Message}");
            }

            return response;
        }
    }
}
