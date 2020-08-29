using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server starting");
            NetworkManager networkManager = new NetworkManager();
        }
    }
}
