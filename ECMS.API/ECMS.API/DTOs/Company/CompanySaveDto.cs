using System.ComponentModel.DataAnnotations;

namespace ECMS.API.DTOs.Company
{
    public class CompanySaveDto
    {
        [Required, StringLength(255)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Domain { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Industry { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }
    }
}
