using System.Security;

namespace RentElectroScooter.CoreModels.Models
{
    public sealed class User
    {
        public User()
        {
            Modified = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!; 
        public DateTime Modified { get; set; }

        public UserProfile? UserProfile { get; set; }
    }
}