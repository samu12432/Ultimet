using System.ComponentModel.DataAnnotations;

namespace Api_Finish_Version.DTO.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Es necesario ingresar el nombre y apellido.")]
        [StringLength(100)]
        public string name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Es necesario ingresar un correo.")]
        [EmailAddress(ErrorMessage = "Formato de correo invalido.")]
        public string userEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "Es necesario ingresar un numero de contacto.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Largo de 9 digitos, inlcuyendo el 0.")]
        public string phoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar un nombre de usuario.")]
        [StringLength(100)]
        public string userName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar una contraseña.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Largo de la contraseña mayor a 6 y menor a 10")]
        public string password { get; set; } = string.Empty;
    }
}
