using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DataRequest
{
    public string banned;
    public string password;
    public string email;
    public DataRequest(string banned, string password, string email)
    {
        this.banned = banned;
        this.password = password;
        this.email = email;
    }
}