using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class UserAssignmentRepository : IRepository<UserAssignment>
    {
        private readonly AppDBContext appContext;
        public UserAssignmentRepository(AppDBContext appContext)
        {
            this.appContext = appContext;
        }
        public IEnumerable<UserAssignment> GetAll()
        {
            return appContext.UserAssignment.Include("Assignment").Include("User");
        }

        public UserAssignment Get(string id)
        {
            return appContext.UserAssignment.Include("Assignment").Include("User").FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Create(UserAssignment userAssignment)
        {
            userAssignment.Id = Guid.NewGuid().ToString();
            appContext.UserAssignment.Add(userAssignment);
        }

        public void Update(UserAssignment userAssignment)
        {
            appContext.Entry(userAssignment).State = EntityState.Modified;
        }

        public IEnumerable<UserAssignment> Find(Func<UserAssignment, Boolean> predicate)
        {
            return appContext.UserAssignment.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            UserAssignment userAssignment = appContext.UserAssignment.Find(id);
            if (userAssignment != null)
                appContext.UserAssignment.Remove(userAssignment);
        }
        public void Save()
        {
            appContext.SaveChanges();
        }
    }
}
