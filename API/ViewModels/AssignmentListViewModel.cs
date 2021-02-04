using BLL.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class AssignmentListViewModel
    {
        public IEnumerable<AssignmentDTO> AllAssignments { get; set; }
        public AssignmentDTO CurrAssignment { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Solution")]
        public string Text { get; set; }
    }
}
