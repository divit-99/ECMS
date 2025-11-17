using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECMS.API.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? JobTitle { get; set; }

        [Required]
        public int CompanyID { get; set; }

        [JsonIgnore]
        public Company Company { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
