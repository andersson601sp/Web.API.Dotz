namespace Web.API.Dotz.Entities
{
    public class UserDotz
    {
        public UserDotz() {}

         public UserDotz(int id,
                     double dotz,   
                     int userId
         ) 
         {
             this.Id = id;
             this.Dotz = dotz;
             this.UserId = userId;
         }
        public int Id { get; set; }

        public double Dotz { get; set; }

         public int UserId { get; set; }
    }
}