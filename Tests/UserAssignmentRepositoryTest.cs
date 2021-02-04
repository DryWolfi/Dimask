using DAL;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests
{
    public class UserAssignmentRepositoryTest
    {
        [Fact]
        public void GetAllUserAssignmentsTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UserAssignmentGetAllDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.UserAssignment.Add(new UserAssignment { Id = "1", Text = "1" });
                context.UserAssignment.Add(new UserAssignment { Id = "2", Text = "2" });
                context.UserAssignment.Add(new UserAssignment { Id = "3", Text = "3" });
                context.UserAssignment.Add(new UserAssignment { Id = "4", Text = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                UserAssignmentRepository userAssignmentRepository = new UserAssignmentRepository(context);
                List<UserAssignment> userAssignments = userAssignmentRepository.GetAll().ToList();

                Assert.Equal(4, userAssignments.Count);
            }
        }
        [Fact]
        public void DeleteUserAssignmentsTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UserAssignmentDeleteDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.UserAssignment.Add(new UserAssignment { Id = "1", Text = "1" });
                context.UserAssignment.Add(new UserAssignment { Id = "2", Text = "2" });
                context.UserAssignment.Add(new UserAssignment { Id = "3", Text = "3" });
                context.UserAssignment.Add(new UserAssignment { Id = "4", Text = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                UserAssignmentRepository userAssignmentRepository = new UserAssignmentRepository(context);
                userAssignmentRepository.Delete("4");
                context.SaveChanges();
                List<UserAssignment> userAssignments = userAssignmentRepository.GetAll().ToList();
                Assert.Equal(3, userAssignments.Count);
            }
        }
        [Fact]
        public void GetUserAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UserAssignmentGetDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.UserAssignment.Add(new UserAssignment { Id = "1", Text = "1" });
                context.UserAssignment.Add(new UserAssignment { Id = "2", Text = "2" });
                context.UserAssignment.Add(new UserAssignment { Id = "3", Text = "3" });
                context.UserAssignment.Add(new UserAssignment { Id = "4", Text = "4" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                UserAssignmentRepository userAssignmentRepository = new UserAssignmentRepository(context);
                UserAssignment userAssignment = userAssignmentRepository.Get("1");

                Assert.Equal("1", userAssignment.Text);
            }
        }
        [Fact]
        public void CreateUserAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UserAssignmentCreateDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.UserAssignment.Add(new UserAssignment { Id = "1", Text = "1" });
                context.UserAssignment.Add(new UserAssignment { Id = "2", Text = "2" });
                context.UserAssignment.Add(new UserAssignment { Id = "3", Text = "3" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                UserAssignmentRepository userAssignmentRepository = new UserAssignmentRepository(context);
                userAssignmentRepository.Create(new UserAssignment { Id = "4", Text = "4" });
                context.SaveChanges();
                List<UserAssignment> userAssignments = userAssignmentRepository.GetAll().ToList();
                Assert.Equal(4, userAssignments.Count);
            }
        }
        [Fact]
        public void UpdateUserAssignmentTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UserAssignmentUpdateDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.UserAssignment.Add(new UserAssignment { Id = "1", Text = "1" });
                context.UserAssignment.Add(new UserAssignment { Id = "2", Text = "2" });
                context.UserAssignment.Add(new UserAssignment { Id = "3", Text = "3" });
                context.SaveChanges();
            }

            using (var context = new AppDBContext(options))
            {
                UserAssignmentRepository userAssignmentRepository = new UserAssignmentRepository(context);
                userAssignmentRepository.Update(new UserAssignment { Id = "3", Text = "4" });
                context.SaveChanges();
                UserAssignment userAssignment = userAssignmentRepository.Get("3");

                Assert.Equal("4", userAssignment.Text);
            }
        }
    }
}
