using System.ComponentModel.DataAnnotations;

namespace Web.API.Dotz.Dtos
{
    public class UserAuthDto
    {
        /// <summary>
        /// Nome usuario
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// SobreNome usuario
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// perfil usuario
        /// </summary>
        [Required]
        public string Role { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}