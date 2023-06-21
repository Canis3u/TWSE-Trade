using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWSE_Trade_Web_API.Service.Interface
{
    public interface ITwseService
    {
       Task<int> UpdateDatabaseAsync(string endDate);
    }
}
