using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("USERS")]

    public class User
    {

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;

        public DateTime Dob { get; set; }

        public string Full_Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public string SetFullName()
        {
            return Full_Name = $"{First_Name} {Last_Name}";
        }

        public void SetGender(string newGender)
        {

            if (newGender == "Male" || newGender == "Female" || newGender == "Other")
            {
                Gender = newGender;
            }
            else
            {

                Gender = string.Empty;
            }
        }

    }
}
