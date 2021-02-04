using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace Tests
{
    public class AssignmentRepositoryTest
    {    
        [Fact]
        public void GetAllAssignmentsTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "AssignmentGetAllDatabase")
                .Options;
 
            using (var context = new AppDBContext(options))
            {
                context.Assignment.Add(new Assignment { Id = "1", Name = "1", Description = "1" });
                context.Assignment.Add(new Assignment { Id = "2", Name = "2", Description = "2" });
                context.Assignment.Add(new Assignment { Id = "3", Name = "3", Description = "3" });
                context.Assignment.Add(new Assignment { Id = "4", Name = "4", Description = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository(context);
                List<Assignment> assignments = assignmentRepository.GetAll().ToList();

                Assert.Equal(4, assignments.Count);
            }
        }
        [Fact]
        public void DeleteAssignmentsTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "AssignmentDeleteDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.Assignment.Add(new Assignment { Id = "1", Name = "1", Description = "1" });
                context.Assignment.Add(new Assignment { Id = "2", Name = "2", Description = "2" });
                context.Assignment.Add(new Assignment { Id = "3", Name = "3", Description = "3" });
                context.Assignment.Add(new Assignment { Id = "4", Name = "4", Description = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository(context);
                assignmentRepository.Delete("4");
                context.SaveChanges();
                List<Assignment> assignments = assignmentRepository.GetAll().ToList();

                Assert.Equal(3, assignments.Count);
            }
        }
        [Fact]
        public void GetAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "AssignmentGetDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.Assignment.Add(new Assignment { Id = "1", Name = "1", Description = "1" });
                context.Assignment.Add(new Assignment { Id = "2", Name = "2", Description = "2" });
                context.Assignment.Add(new Assignment { Id = "3", Name = "3", Description = "3" });
                context.Assignment.Add(new Assignment { Id = "4", Name = "4", Description = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository(context);
                Assignment assignment = assignmentRepository.Get("1");

                Assert.Equal("1", assignment.Name);
            }
        }
        [Fact]
        public void CreateAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "AssignmentCreateDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.Assignment.Add(new Assignment { Id = "1", Name = "1", Description = "1" });
                context.Assignment.Add(new Assignment { Id = "2", Name = "2", Description = "2" });
                context.Assignment.Add(new Assignment { Id = "3", Name = "3", Description = "3" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository(context);
                assignmentRepository.Create(new Assignment { Id = "4", Name = "4", Description = "4" });
                context.SaveChanges();
                List<Assignment> assignments = assignmentRepository.GetAll().ToList();
                Assert.Equal(4, assignments.Count);
            }
        }
        [Fact]
        public void UpdateAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "AssignmentUpdateDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.Assignment.Add(new Assignment { Id = "1", Name = "1", Description = "1" });
                context.Assignment.Add(new Assignment { Id = "2", Name = "2", Description = "2" });
                context.Assignment.Add(new Assignment { Id = "3", Name = "3", Description = "3" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                AssignmentRepository assignmentRepository = new AssignmentRepository(context);
                assignmentRepository.Update(new Assignment { Id = "3", Name = "5", Description = "3" });
                context.SaveChanges();
                Assignment assignment = assignmentRepository.Get("3");

                Assert.Equal("5", assignment.Name);
            }
        }
    }
}
