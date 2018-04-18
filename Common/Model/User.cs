using System;

namespace Common.Model
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int _id, string _username, string _password)
        {
            Id = _id;
            Username = _username;
            Password = _password;
        }
    }
}
