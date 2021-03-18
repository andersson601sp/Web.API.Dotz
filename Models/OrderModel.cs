using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Models
{
    /// <summary>
    /// Pedido
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Id Pedido
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// Numero do pedido
        /// </summary>
        /// <value></value>
        public string Number { get; set; }

        /// <summary>
        /// Id Usuario
        /// </summary>
        /// <value></value>
        public int UserId { get; set; }

        /// <summary>
        /// Status do Pedido
        /// </summary>
        /// <value></value>          
        public bool Delivery { get; set; } = false;


        /// <summary>
        /// Items do pedido
        /// </summary>
        /// <value></value>
        public List<OrderItemsModel> OrderItems { get; set; }
    }
}