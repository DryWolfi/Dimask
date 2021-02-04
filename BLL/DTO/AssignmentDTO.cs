using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class AssignmentDTO
    {
        [BindNever]
        public string Id { get; set; }
        [Display(Name = "Enter name")]
        [StringLength(50)]
        [Required(ErrorMessage = "No more than 50 chars")]
        public string Name { get; set; }
        [Display(Name = "Enter description")]
        [StringLength(200)]
        [Required(ErrorMessage = "No more than 200 chars")]
        public string Description { get; set; }
        [Display(Name = "Enter start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Display(Name = "Enter end time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}
