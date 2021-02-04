using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class UserAssignmentDTO
    {
        public string Id { get; set; }
        public User User { get; set; }
        public AssignmentDTO Assignment { get; set; }
        public bool IsCompleted { get; set; }
        [Display(Name = "Enter your solution")]
        [StringLength(200)]
        [Required(ErrorMessage = "No more than 200 chars")]
        public string Text { get; set; }
    }
}
