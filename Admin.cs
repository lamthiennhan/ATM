using System;
using System.Collections.Generic;
using System.Text;

namespace Do_An2
{
    class Admin
    {
        public string user, pass;

        public Admin()
        {
            user = null;
            pass = null;
        }
        public Admin(string user, string pass)
        {
            this.user = user;
            this.pass = pass;
        }
    }
}
