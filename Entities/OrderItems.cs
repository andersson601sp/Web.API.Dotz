namespace Web.API.Dotz.Entities
{
    public class OrderItems
    {
        public OrderItems() {}

         public OrderItems(int id,
                      int orderId,
                      int productId,
                      string description    
         ) 
         {
             this.Id = id;
             this.OrderId = orderId;
             this.ProductId = productId;
             this.Description = description; 
         }
        public int Id { get; set; }     
        public int OrderId { get; set; }
        public int ProductId {get; set; }
        public string Description { get; set; }
    }
}