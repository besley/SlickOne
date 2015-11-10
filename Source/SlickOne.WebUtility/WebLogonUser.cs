using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// Web用户
    /// </summary>
    public class WebLogonUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Ticket { get; set; }
    }
}
