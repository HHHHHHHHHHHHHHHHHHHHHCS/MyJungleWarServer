using System;

namespace Common.Model
{
    [Serializable]
    public class UserData
    {
        public string Username { get; set; }
        public int TotalCount { get; set; }
        public int WinCount { get; set; }

        public UserData(string _username, int _totalCount, int _winCount)
        {
            Username = _username;
            TotalCount = _totalCount;
            WinCount = _winCount;
        }
    }
}
