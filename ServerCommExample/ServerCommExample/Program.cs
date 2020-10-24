using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommExample
{
    class Program
    {
        static TcpIpServer server = null;
        static void Main(string[] args)
        {
            server = new TcpIpServer();
        }
    }
}
