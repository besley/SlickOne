using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// user account
    /// </summary>
    [Table("SysUser")]
    public class UserAccountEntity
    {
        public int ID
        {
            get;
            set;
        }

        public string LoginName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public short AccountType
        {
            get;
            set;
        }

        public short Status
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public short PasswordFormat
        {
            get;
            set;
        }

        public string PasswordSalt
        {
            get;
            set;
        }

        public string EMail
        {
            get;
            set;
        }
    }
}
