using NetworkUtility.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Implementations
{
    public class DnsRepository : IDnsRepository
    {
        public bool GetStatus()
        {
            return true;
        }
    }
}
