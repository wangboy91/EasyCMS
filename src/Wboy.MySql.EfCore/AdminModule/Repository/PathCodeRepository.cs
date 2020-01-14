using System;
using System.Collections.Generic;
using System.Text;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;

namespace Wboy.MySql.EfCore.AdminModule.Repository
{
    public class PathCodeRepository : Repository<PathCodeEntity>, IPathCodeRepository
    {
        public PathCodeRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
