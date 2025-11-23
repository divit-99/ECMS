using System.ComponentModel.DataAnnotations;

namespace ECMS.API.DTOs.Employee
{
    public class EmployeeSaveDto
    {
        [Required, StringLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? JobTitle { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public bool IsActive { get; set; }
    }
}
