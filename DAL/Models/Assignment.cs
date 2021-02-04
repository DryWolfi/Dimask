using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Assignment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
