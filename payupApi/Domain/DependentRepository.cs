using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public class DependentRepository : RepositoryBase<Dependent>, IDependentRepository
    {
        public DependentRepository(AppDbContext context) : base(context)
        {

        }
    }
}
