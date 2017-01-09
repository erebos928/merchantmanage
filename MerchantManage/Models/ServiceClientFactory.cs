using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManage.Models
{
    public interface ServiceClientFactory
    {
        ServiceClient GetServiceClient();
    }
}
