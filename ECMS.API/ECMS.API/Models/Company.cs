using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECMS.API.Models
{
    public class Company
    {
        public int ID { get; set; }

        [Required, MaxLength(255)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Domain { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Industry { get; set; }

        [MaxLength(255)]
        public string? Website { get; set; }

        [JsonIgnore]
        public List<Employee>? Employees { get; set; } = new();
    }
}
