using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        IUnitOfWork Database { get; set; }

        public AssignmentService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public AssignmentService(){}
        public void AddAssignment(AssignmentDTO assignmentDTO)
        {
            Assignment assignmet = new Assignment
            {
                Id = assignmentDTO.Id,
                Name = assignmentDTO.Name,
                Description = assignmentDTO.Description,
                StartTime = assignmentDTO.StartTime,
                EndTime = assignmentDTO.EndTime
            };
            Database.Assignment.Create(assignmet);
            Database.Save();
        }

        public IEnumerable<AssignmentDTO> Assignments()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Assignment, AssignmentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Assignment>, List<AssignmentDTO>>(Database.Assignment.GetAll());
        }

        public AssignmentDTO GetAssignment(string id)
        {
            if (id == null)
                throw new ValidationException("Id field is empty", "");
            var assignment = Database.Assignment.Get(id);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");

            return new AssignmentDTO 
            { 
                Id = assignment.Id, 
                Name = assignment.Name, 
                Description = assignment.Description, 
                StartTime = assignment.StartTime,
                EndTime = assignment.EndTime
            };
        }
        public void AddAssignmentToUser(string assignmentId, User user)
        {
            if (assignmentId == null)
                throw new ValidationException("Id field is empty", "");
            Assignment assignment = Database.Assignment.Get(assignmentId);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");
            if (user.UserAssignments == null)
            {
                user.UserAssignments = new List<Assignment>();
            }
            if (!user.UserAssignments.Contains(assignment))
            {
                user.UserAssignments.Add(assignment); 
            }
        }
        public List<AssignmentDTO> GetUserAssignments(User user)
        {
            List<Assignment> assignments = user.UserAssignments;
            List<AssignmentDTO> assignmentsDTO = new List<AssignmentDTO>();
            foreach (var el in assignments)
            {
                assignmentsDTO.Add(new AssignmentDTO
                {
                    Id = el.Id,
                    Name = el.Name,
                    Description = el.Description,
                    StartTime = el.StartTime,
                    EndTime = el.EndTime
                });
            }
            return assignmentsDTO;
        }
        public void DeleteAssignment(string assignmentId)
        {
            Assignment assignment = Database.Assignment.Get(assignmentId);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");
            Database.Assignment.Delete(assignmentId);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        } 
    }
}
