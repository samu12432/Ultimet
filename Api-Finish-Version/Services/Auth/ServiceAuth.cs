using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api_Finish_Version.Data;
using Api_Finish_Version.DTO.Auth;
using Api_Finish_Version.Exceptions.Auth;
using Api_Finish_Version.IRepository.Auth;
using Api_Finish_Version.IServices.Auth;
using Api_Finish_Version.Models.Auth;
using API_REST_PROYECT.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api_Finish_Version.Services.Auth
{
    public class ServiceAuth : IServiceAuth
    {
        private readonly ContextDb _contextDbAplication;
        private readonly Token _optionToken;
        private readonly IAuthRepository _userRepository;
        private readonly IEmailAuthService _emailSender;
        private readonly JwtSettingsConfirmation _jwt;

        public ServiceAuth(ContextDb contextDb, Token token, IAuthRepository repo, IEmailAuthService emailSender, IOptions<JwtSettingsConfirmation> jwtOptions)
        {
            _contextDbAplication = contextDb;
            _optionToken = token;
            _userRepository = repo;
            _emailSender = emailSender;
            _jwt = jwtOptions.Value;
        }

        public async Task<string?> LoginUser(LoginDto dto)
        {
            //Validamos que el dto no sea vacio
            if (dto == null) throw new Exception("Datos incorrectos.");

            //Obtenemos el usuario
            var user = await _userRepository.GetUserByInfo(dto.userName);

            //Validamos que no exista un usuario y verificamos password (a traves del hash y salt, DEBEN SER LAS MISMAS)
            if (user == null || !VerificarPassword(dto.password, user.passwordHash, user.passwordSalt)) throw new Exception("Lo sentimos, credenciales incorrectas.");

            //Verificar si el usuario ha confirmado su correo electrónico
            if (!user.IsEmailConfirmed)
                throw new EmailException("Por favor, confirme su correo electrónico antes de iniciar sesión.");

            //Generamos un token y devolvemos para iniciar la sesion
            var t = _optionToken.GenerateToken(user);
            return t;
        }

        public async Task<bool> RegisterUser(RegisterDto dto)
        {

            //Validamos que el dto no sea vacio
            if (dto == null) throw new Exception("Datos incorrectos.");

            //Verificamos la existencia de el usuario a traves del Mail
            var exist = await _userRepository.GetByEmailAsync(dto.userEmail);
            if (exist != null)
                throw new EmailException("Ya existe un usuario creado con ese correo.");

            //Verificamos la existencia de el usuario a traves del UserName
            exist = await _userRepository.GetUserByInfo(dto.userName);
            if (exist != null)
                throw new UserNameException("Ya existe un usuario creado con ese nombre de usuario.");

            //Generamos un Hash con la contraseña
            CrearHashySalt(dto.password, out byte[] salt, out byte[] hash);

            //Generamos un usuario
            User newUser = new User(dto.name, dto.userEmail, dto.phoneNumber, dto.userName, hash, salt);

            //Accedemos a la Db y guardamos
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            //Se envia un mail de forma asicronica al progrma, para que el cliente verifique que existe realmente
            var token = GenerateEmailToken(newUser.userEmail);
            var link = $"https://localhost:5001/api/auth/confirm-email?token={token}";
            var body = $"Hola {newUser.userName}, haz clic aquí para confirmar tu correo: <a href='{link}'>Confirmar</a>";

            _ = Task.Run(() => _emailSender.SendAsync(
                newUser.userEmail,
                "Confirmación de correo",
                $"Hola {newUser.userName}, haz clic aquí para confirmar tu correo: <a href='{link}'>Confirmar</a>"));
            return true;
        }

        private void CrearHashySalt(string password, out byte[] salt, out byte[] hash)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerificarPassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hash.SequenceEqual(computedHash);
        }

        public async Task<bool> ConfirmEmailAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidAudience = _jwt.Audience,
                    IssuerSigningKey = key,
                    ValidateLifetime = true
                }, out _);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                if (email == null) return false;

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null) return false;

                user.IsEmailConfirmed = true;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateEmailToken(string email)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim("purpose", "email_confirmation")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
