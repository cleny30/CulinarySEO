using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IDAOs
{
    public interface IOrderDAO
    {
        Task<int> GetOrderCountByCustomerId(Guid customerId);
    }
}
