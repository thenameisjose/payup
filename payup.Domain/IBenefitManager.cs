using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public interface IBenefitManager
    {
        BenefitSummary GetBenefitSummary(Employee employee);
    }
}
