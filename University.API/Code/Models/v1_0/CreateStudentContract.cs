using System.ComponentModel.DataAnnotations;
using University.Domain.Entities.Enums;

namespace University.API.Code.Models.v1_0
{
    public class CreateStudentContract
    {
        [Required]
        public Gender Gender { get; set; }
        
        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        [StringLength(60)]
        public string MiddleName { get; set; }

        [StringLength(16, MinimumLength = 6)]
        public string UniqueName { get; set; }
    }
}
