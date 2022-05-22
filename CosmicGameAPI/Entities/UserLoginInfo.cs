using System.ComponentModel.DataAnnotations;

namespace CosmicGameAPI.Entities
{
    public class UserLoginInfo
    {
        public int Id { get; set; }
        public int LoginType { get; set; }
        public int UserId { get; set; }
        [MaxLength(100)]
        public string IpAddress { get; set; }
        public decimal IsLogin { get; set; }
        public string Browser { get; set; }
        public DateTime DateTime { get; set; }
        public string Token { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
