using Api_Finish_Version.DTO.Auth;
using Api_Finish_Version.Exceptions.Auth;
using Api_Finish_Version.IServices.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api_Finish_Version.Controllers.Auth
{
    [ApiController]
    public class AuthController : Controller
    {
            private readonly IServiceAuth _authService;

            public AuthController(IServiceAuth authService)
            {
                _authService = authService;
            }

            [HttpPost("registroUsuario")]
            public async Task<IActionResult> RegisterUser(RegisterDto dto)
            {
                //Antes de ejecutar la funcion, se realiza la validacion de MODELSTATE correspondiente al Dto
                try
                {
                    var result = await _authService.RegisterUser(dto);
                    if (!result)
                        return Unauthorized("El correo ya está registrado."); //Codigo 401


                return Ok("Usuario registrado correctamente."); //Codigo 200
                }
                catch (InvalidOperationException e)
                {
                    return BadRequest(new { satus = 400, message = e.Message });
                }
                catch (EmailException e)
                {
                    return BadRequest(new { satus = 400, message = e.Message });
                }
                catch (UserNameException e)
                {
                    return BadRequest(new { satus = 400, message = e.Message });
                }

            }

            [HttpPost("loginUsuario")]
            public async Task<IActionResult> LoginUser(LoginDto dto)
            {
                //Antes de ejecutar la funcion, se realiza la validacion de MODELSTATE correspondiente al Dto
                try
                {
                    var token = await _authService.LoginUser(dto);

                    if (token == null)
                        return Unauthorized("Credenciales incorretas"); //Codigo 401

                    return Ok(new { token }); //Codigo 200
                }
                catch (InvalidOperationException e)
                {
                    return BadRequest(new { satus = 500, message = e.Message });
                }
                catch (EmailException e)
                {
                    return BadRequest(new { satus = 403, message = e.Message });
                }
            }
    }
}
