using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Placholders
{
    class LoginStatus
    {
        string status;
        string error;
        public LoginStatus(string status, string error)
        {
            this.status = status;
            this.error = error;
        }
        public string getStatus()
        {
            return status;
        }
        public string getError()
        {
            return error;
        }
    }
}
