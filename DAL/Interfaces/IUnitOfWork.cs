using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Assignment> Assignment { get; }
        IRepository<UserAssignment> UserAssignment { get; }
        void Save();
    }
}
