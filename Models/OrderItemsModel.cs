using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Models
{
    public class OrderItemsModel
    {
        /// <summary>
        /// Id 
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Id Pedido
        /// </summary>
        /// <value></value>
        public int OrderId { get; set; }
        /// <summary>
        /// Id Produto
        /// </summary>
        /// <value></value>
        public int ProductId { get; set; }
         /// <summary>
        /// Desccricao Produto
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
    }
}