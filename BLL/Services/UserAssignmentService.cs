using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BLL.Services
{
    public class UserAssignmentService : IUserAssignmentService
    {
        IUnitOfWork Database { get; set; }

        public UserAssignmentService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public List<UserAssignmentDTO> UserAssignments()
        {
            IEnumerable<UserAssignment> userAssignments = Database.UserAssignment.GetAll();
            List<UserAssignmentDTO> userAssignmentDTO = new List<UserAssignmentDTO>();
            foreach (UserAssignment ua in userAssignments)
            {
                userAssignmentDTO.Add(new UserAssignmentDTO
                {
                    Id = ua.Id,
                    User = ua.User,
                    Assignment = new AssignmentDTO
                    {
                        Id = ua.Assignment.Id,
                        Name = ua.Assignment.Name,
                        Description = ua.Assignment.Description,
                        StartTime = ua.Assignment.StartTime,
                        EndTime = ua.Assignment.EndTime
                    },
                    IsCompleted = ua.IsCompleted,
                    Text = ua.Text
                });
            }
            return userAssignmentDTO;
        }
        public UserAssignmentDTO GetUserAssignment(string id)
        {
            UserAssignment userAssignment = Database.UserAssignment.Get(id);
            UserAssignmentDTO userAssignmentDTO = new UserAssignmentDTO
            {
                Id = userAssignment.Id,
                User = userAssignment.User,
                Assignment = new AssignmentDTO
                {
                    Id = userAssignment.Assignment.Id,
                    Name = userAssignment.Assignment.Name,
                    Description = userAssignment.Assignment.Description,
                    StartTime = userAssignment.Assignment.StartTime,
                    EndTime = userAssignment.Assignment.EndTime
                },
                IsCompleted = userAssignment.IsCompleted,
                Text = userAssignment.Text
            };
            return userAssignmentDTO;
        }
        public void AddUserAssignment(AssignmentDTO assignmentDTO, User user)
        {
            Assignment assignment = Database.Assignment.Get(assignmentDTO.Id);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");       
            if (!Database.UserAssignment.GetAll().Where(x => x.User.Id.Equals(user.Id) && x.Assignment.Id.Equals(assignment.Id)).Any())
            {
                UserAssignment userAssignment = new UserAssignment
                {
                    Id = Guid.NewGuid().ToString(),
                    Assignment = assignment,
                    User = user
                };
                Database.UserAssignment.Create(userAssignment);
                Database.Save();
            }
        }
        public List<AssignmentDTO> GetAssignments(User user)
        {
            List<UserAssignment> userAssignments = Database.UserAssignment.GetAll()
                .Where(x => x.User.Id.Equals(user.Id))
                .OrderBy(x => x.Assignment.StartTime)
                .ToList();
            List<AssignmentDTO> assignments = new List<AssignmentDTO>();
            foreach (var el in userAssignments)
            {
                assignments.Add(new AssignmentDTO
                {
                    Id = el.Assignment.Id,
                    Name = el.Assignment.Name,
                    Description = el.Assignment.Description,
                    StartTime = el.Assignment.StartTime,
                    EndTime = el.Assignment.EndTime
                });
            }
            return assignments;
        }
        public void DeleteUserAssignment(string assignmentId)
        {
            List<UserAssignment> userAssignment = Database.UserAssignment.GetAll()
                .Where(x => x.Assignment.Id.Equals(assignmentId))
                .ToList();
            if (userAssignment.Count == 0)
                return;
                //throw new ValidationException("Assignment not found", "");
            foreach (var el in userAssignment)
            {
                Database.UserAssignment.Delete(el.Id);
            }
            Database.Save();
        }
        public void MarkAsComplete(AssignmentDTO assignmentDTO, User user)
        {
            Assignment assignment = Database.Assignment.Get(assignmentDTO.Id);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");
            UserAssignment userAssignment = Database.UserAssignment.GetAll()
                            .First(x => x.User.Id.Equals(user.Id) && x.Assignment.Id.Equals(assignment.Id));
            if (userAssignment == null)
            {
                throw new ValidationException("User Assignment not found", "");
            }else
            {
                userAssignment.IsCompleted = true;
                Database.UserAssignment.Update(userAssignment);
                Database.Save();
            }
        }
        public void AddSolution(AssignmentDTO assignmentDTO, User user, string text)
        {
            Assignment assignment = Database.Assignment.Get(assignmentDTO.Id);
            if (assignment == null)
                throw new ValidationException("Assignment not found", "");
            UserAssignment userAssignment = Database.UserAssignment.GetAll()
                            .First(x => x.User.Id.Equals(user.Id) && x.Assignment.Id.Equals(assignment.Id));
            if (userAssignment == null)
            {
                throw new ValidationException("User Assignment not found", "");
            }
            else
            {
                userAssignment.Text = text;
                userAssignment.IsCompleted = true;
                Database.UserAssignment.Update(userAssignment);
                Database.Save();
            }
        }
    }
}
