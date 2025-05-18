namespace EgyEagles.MVC.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();

    }
}
