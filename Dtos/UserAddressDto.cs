namespace Web.API.Dotz.Dtos
{
    public class UserAddressDto
    {
        /// <summary>
        /// Id Endereço
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id Endereço
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Rua
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Numero
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }
    }
}