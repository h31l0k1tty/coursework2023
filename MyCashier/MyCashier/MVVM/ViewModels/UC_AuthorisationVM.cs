using MyCashier.MVVM.Models;
using MyCashier.MVVM.Views;
using MyCashier.Services;
using System;
using System.Linq;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_AuthorisationVM : ViewModelBase
    {
        public UC_AuthorisationVM(string? login = null, string? password = null)
        {
            if (login != null) _login = login;
            if (password != null) _password = password;
        }

        User? newUser;

        private string _login = Properties.Settings.Default.Login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value.Trim();
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password = Properties.Settings.Default.Password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value.Trim();
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool isRememberMeChecked = Properties.Settings.Default.WasRememberMeChecked;
        public bool IsRememberMeChecked
        {
            get { return isRememberMeChecked; }
            set
            {
                isRememberMeChecked = value;
                OnPropertyChanged(nameof(IsRememberMeChecked));
                RememberMeChanged(IsRememberMeChecked); //Привязываем Settings к CheckBox
            }
        }

#warning ГОВНО ДО СИХ ПОР
        private void RememberMeChanged(bool isRememberMeChecked)
        {
            Properties.Settings.Default.WasRememberMeChecked = isRememberMeChecked;
            Properties.Settings.Default.Login = Properties.Settings.Default.WasRememberMeChecked ? Login : null!;
            Properties.Settings.Default.Password = Properties.Settings.Default.WasRememberMeChecked ? Password : null!;
            Properties.Settings.Default.Save();
        }


        private RelayCommand authoriseCmd = null!;
        public RelayCommand AuthoriseCmd
        {
            get
            {
                return authoriseCmd ?? new RelayCommand
                    (obj =>
                    {
                        //Проверка входных данных
                        newUser = MyCashierDbContext.db.User.FirstOrDefault(c => c.login == Login && c.password == Password);
                        if (newUser != null)
                        {
                            CurrentUser.SetUser(newUser);
                            Navigator.Navigate(new UC_JournalVM());
                        }
                        else 
                            MessageBox.Show("Неверный логин или пароль");
                    }, 
                    obj => !string.IsNullOrEmpty(Login?.Trim()) && Password?.Trim().Length > 6);
            }
        }

        private RelayCommand goToRegisterCmd = null!;
        public RelayCommand GoToRegistrationCmd
        {
            get
            {
                return goToRegisterCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_RegistrationVM(Login, Password)); });
            }
        }
    }
}
