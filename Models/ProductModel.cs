using System.ComponentModel.DataAnnotations;

namespace Web.API.Dotz.Models
{
    /// <summary>
    /// Produtos Dotz
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Id Produto
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        /// <value></value>
        public string Description { get; set; }

        /// <summary>
        /// Valor do Produto em Dotz
        /// </summary>
        /// <value></value>
        public double Dotz { get; set; }
    }
}