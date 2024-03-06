using System.ComponentModel.DataAnnotations;

namespace Project.IdentityService.Dtos
{
    public class AccountStatusDtos
    {
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public bool Enable { get; set; }
    }
}
