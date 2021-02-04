using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace XUnitTestProject1
{
    public class UnitTest1
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
    }
}
