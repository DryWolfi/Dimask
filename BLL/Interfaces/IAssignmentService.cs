using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAssignmentService
    {
        AssignmentDTO GetAssignment(string assignmentID);
        IEnumerable<AssignmentDTO> Assignments();
        void AddAssignment(AssignmentDTO assignmentDTO);
        public void AddAssignmentToUser(string assignmentId, User user);
        public List<AssignmentDTO> GetUserAssignments(User user);
        public void DeleteAssignment(string assignmentId);
    }
}
