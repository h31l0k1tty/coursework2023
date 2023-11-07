using MyCashier.MVVM.Models;
using System;
using System.Collections.Generic;

namespace MyCashier.Services
{
    public static class CurrentUser
    {
        public static Guid id { get; set; }

        public static string login { get; set; } = null!;

        public static string password { get; set; } = null!;

        public static string name { get; set; } = null!;

        public static string email { get; set; } = null!;

        public static ICollection<Account> accounts { get; set; } = new List<Account>();


        public static void Set(User? user)
        {
            if (user != null)
            {
                id = user.id;
                login = user.login;
                password = user.password;
                name = user.name;
                email = user.email;
                accounts = user.accounts;
            }
        }
    }
}
