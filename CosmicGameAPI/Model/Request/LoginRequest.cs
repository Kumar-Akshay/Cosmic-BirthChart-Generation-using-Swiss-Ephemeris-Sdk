namespace CosmicGameAPI.Model.Request
{
    public class LoginRequest
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string deviceinfo { get; set; }
        public bool IsRemember { get; set; }
        public string URL { get; set; }
    }
}
