namespace Web.API.Dotz.Entities
{
    public class User
    {
        public User() {}

         public User(int id,
                     string firsName,
                     string lastName,
                     string userName,
                     string passWord,
                     string role,
                     string token,
                     UserAddress userAddress   
         ) 
         {
             this.Id = id;
             this.FirstName = firsName;
             this.LastName  = lastName;
             this.Username = userName;
             this.Password = passWord;
             this.Role = role;
             this.Token = token;
             this.UserAddress = userAddress;
         }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public int? UserAddressId { get; set; }

        public UserAddress UserAddress { get; set; }
    }
}