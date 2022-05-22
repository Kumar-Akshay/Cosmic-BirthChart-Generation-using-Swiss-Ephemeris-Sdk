using System.ComponentModel.DataAnnotations;

namespace CosmicGameAPI.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(25)]
        public string UserName { get; set; }
        [MaxLength(25)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public decimal IsActive { get; set; }
        public decimal IsAllowMultipelLogin { get; set; }
        public decimal IsApproved { get; set; }
        public decimal IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
