using System.Collections.Generic;

namespace Web.API.Dotz.Entities
{
    public class Order
    {
        public Order() {}

         public Order(int id,
                      string number,
                      int userId   
         ) 
         {
             this.Id = id;
             this.Number = number;
             this.UserId = userId;
         }
        public int Id { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }

        public bool Delivery { get; set; }

        public List<OrderItems> OrderItems {get; set; }
    }
}