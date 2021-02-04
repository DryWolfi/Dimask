using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class UserAssignmentListViewModel
    {
        public IEnumerable<UserAssignmentDTO> AllUserAssignments { get; set; }
        public AssignmentDTO CurrAssignment { get; set; }
        public User Worker { get; set; }
        public UserAssignmentDTO UserAssignment { get; set; }
    }
}
