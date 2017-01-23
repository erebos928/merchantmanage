using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManage.Models
{
    public interface ServiceClient
    {
        Task<String> SendRequest(Merchant mer,String currentNode);
    }
}
