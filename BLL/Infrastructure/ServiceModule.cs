using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private AppDBContext appContext;
        public ServiceModule(AppDBContext db)
        {
            appContext = db;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(appContext);
        }
    }
}
