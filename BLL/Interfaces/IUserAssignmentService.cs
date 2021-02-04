using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserAssignmentService
    {
        public List<UserAssignmentDTO> UserAssignments();
        public void AddUserAssignment(AssignmentDTO assignmentDTO, User user);
        public List<AssignmentDTO> GetAssignments(User user);
        public void DeleteUserAssignment(string assignmentId);
        public void MarkAsComplete(AssignmentDTO assignmentDTO, User user);
        public UserAssignmentDTO GetUserAssignment(string id);
        public void AddSolution(AssignmentDTO assignmentDTO, User user, string text);
    }
}
