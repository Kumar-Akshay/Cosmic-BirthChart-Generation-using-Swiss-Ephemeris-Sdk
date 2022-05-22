namespace CosmicGameAPI.Model.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsActive { get; set; }
    }
}
