using System;

namespace Navchpract_2
{
    [Serializable]
    public class CUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public CUser(string login, string password, bool isAdmin)
        {
            Login = login;
            Password = password;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            string role = IsAdmin ? "Адмін" : "Юзер";
            return $"{Login} | {role}";
        }
    }
}