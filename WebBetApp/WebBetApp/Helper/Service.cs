using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Helper
{
    public class Service
    {
        public static string GenerateTicketCode()
        {
            Random random = new Random();
            var randomNum = random.Next(0, 9999).ToString("D4");

            return String.Format("{0}{1}", "wb", randomNum);
        }



    }
}
