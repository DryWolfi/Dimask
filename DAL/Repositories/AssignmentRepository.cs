using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class AssignmentRepository : IRepository<Assignment>
    {
        private readonly AppDBContext appContext;
        public AssignmentRepository(AppDBContext appContext)
        {
            this.appContext = appContext;
        }
        public IEnumerable<Assignment> GetAll()
        {
            return appContext.Assignment;
        }

        public Assignment Get(string id)
        {
            return appContext.Assignment.Find(id);
        }

        public void Create(Assignment assignment)
        {
            assignment.Id = Guid.NewGuid().ToString();
            appContext.Assignment.Add(assignment);
        }

        public void Update(Assignment assignment)
        {
            appContext.Entry(assignment).State = EntityState.Modified;
        }

        public IEnumerable<Assignment> Find(Func<Assignment, Boolean> predicate)
        {
            return appContext.Assignment.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Assignment assignment = appContext.Assignment.Find(id);
            if (assignment != null)
                appContext.Assignment.Remove(assignment);
        }
        public void Save()
        {
            appContext.SaveChanges();
        }
    }
}
