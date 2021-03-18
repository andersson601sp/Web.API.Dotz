namespace Web.API.Dotz.Entities
{
    public class Product
    {
        public Product() {}

         public Product(int id,
                        string description,
                        double dotz   
         ) 
         {
             this.Id = id;
             this.Description = description;
             this.Dotz = dotz;
         }
        public int Id { get; set; }
        public string Description { get; set; }
        public double Dotz { get; set; }
    }
}