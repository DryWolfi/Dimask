using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class UserAssignment
    {
        public string Id { get; set; }
        public User User { get; set; }
        public Assignment Assignment { get; set; }
        public bool IsCompleted { get; set; }
        public string Text { get; set; }
    }
}
