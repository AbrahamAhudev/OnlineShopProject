namespace OnlineShopProject.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int? Phone_number { get; set; }

        public int? CartId { get; set; }

    }

    
}

