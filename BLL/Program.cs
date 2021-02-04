using BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string hostemail = ConfigurationManager.AppSettings["email"];
            string hosppassword = ConfigurationManager.AppSettings["password"];
            Console.WriteLine($"{hostemail} {hosppassword}");
        }  
    }
}
