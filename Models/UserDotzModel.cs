using System.ComponentModel.DataAnnotations;

namespace Web.API.Dotz.Models
{
    /// <summary>
    ///  Dotz
    /// </summary>
    public class UserDotzModel
    {
        /// <summary>
        /// Id 
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// Qunatidade de Dotz
        /// </summary>
        /// <value></value>
        public double Dotz { get; set; }

        /// <summary>
        /// Id Usuario
        /// </summary>
        /// <value></value>
        public int UserId { get; set; }
    }
}