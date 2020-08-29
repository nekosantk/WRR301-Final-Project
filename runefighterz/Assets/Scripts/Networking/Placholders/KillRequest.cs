using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class KillRequest
{
    public int killCount;
    public KillRequest(string killCount)
    {
        Int32.TryParse(killCount, out this.killCount);
    }
}
