using System;

namespace JenkinsTray.Entities
{
    public class Credentials
    {
        public Credentials()
        {
        }

        public Credentials(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                password = "";
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Crumb { get; set; }
    }
}