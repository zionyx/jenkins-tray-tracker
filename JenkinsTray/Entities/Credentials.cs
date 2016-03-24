using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTray.Entities
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials()
        {
        }

        public Credentials(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                password = "";
            this.Username = username;
            this.Password = password;
        }
    }
}
