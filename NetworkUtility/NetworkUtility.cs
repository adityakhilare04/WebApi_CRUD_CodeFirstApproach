using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility
{
    public class NetworkUtility
    {
        public string GetNetworkInfo()
        {
            return "Network Info";
        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public Employee GetEmployee()
        {
            return new Employee { Id = 1, Name = "Virat Kohli" };
        }
    }
}
