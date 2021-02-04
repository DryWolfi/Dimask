using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext db;
        private AssignmentRepository assignmentRepository;
        private UserAssignmentRepository userAssignmentRepository;

        public UnitOfWork(AppDBContext appContext)
        {
            db = appContext;
        }
        public IRepository<Assignment> Assignment
        {
            get
            {
                if (assignmentRepository == null)
                    assignmentRepository = new AssignmentRepository(db);
                return assignmentRepository;
            }
        }
        public IRepository<UserAssignment> UserAssignment
        {
            get
            {
                if (userAssignmentRepository == null)
                    userAssignmentRepository = new UserAssignmentRepository(db);
                return userAssignmentRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
