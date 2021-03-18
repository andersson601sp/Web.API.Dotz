namespace Web.API.Dotz.Entities
{
    public class UserAddress
    {
        public UserAddress() {}

         public UserAddress(int id,
                     string street,
                     int number,
                     string district,
                     string city,
                     string zipCode,
                     string state
         ) 
         {
             this.Id = id;
             this.Street = street;
             this.Number  = number;
             this.District = district;
             this.City = city;
             this.ZipCode = zipCode;
             this.State = state;
         }
        public int Id { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }
         public int userId { get; set; }

    }
}