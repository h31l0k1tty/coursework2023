using MyCashier.MVVM.Models;
using MyCashier.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MyCashier.Services
{
    public class CurrentUser
    {
        public static Guid Id { get; set; }

        public static string Login { get; set; } = null!;

        public static string Password { get; set; } = null!;

        public static string Name { get; set; } = null!;

        public static string Email { get; set; } = null!;

        public static ICollection<Account> Accounts { get; set; } = new List<Account>();

        public static void SetUser(User user)
        {
            if (user != null)
            {
                Id = user.id;
                Login = user.login;
                Password = user.password;
                Name = user.name;
                Email = user.email;
                Accounts = user.accounts;
            }
            else 
                throw new Exception("CurrentUser.Set(null)");
        }

        public static User GetUser()
        {
            return new User()
            {
                id = Id,
                login = Login,
                password = Password,
                name = Name,
                email = Email,
                accounts = Accounts
            };
        }
    }
}
