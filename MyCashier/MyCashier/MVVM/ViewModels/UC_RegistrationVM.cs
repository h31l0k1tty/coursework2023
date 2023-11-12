using MyCashier.MVVM.Models;
using MyCashier.Services;
using System.Linq;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_RegistrationVM : ViewModelBase
    {
        public UC_RegistrationVM(string? login = null, string? password = null)
        {
            if (login != null) { _login = login; }
            if (password != null) { _password = password; }
        }

        private string _email = null!;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value.Trim();
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _login = null!;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value.Trim();
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password = null!;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value.Trim();
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _name = null!;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        private RelayCommand registerCmd = null!;
        public RelayCommand RegisterCmd
        {
            get
            {
                return registerCmd ?? new RelayCommand
                    (obj =>
                    {
                        if (MyCashierDbContext.db.User.FirstOrDefault(c => c.login == Login) != null)
                            MessageBox.Show("Пользователем с таким логином уже существует!");
                        else if (MyCashierDbContext.db.User.FirstOrDefault(c => c.email == Email) != null)
                            MessageBox.Show("Пользователем с такой эл. почтой уже существует!");
                        else
                        {
                            User newUser = new()
                            {
                                id = default,
                                login = Login.Trim(),
                                password = Password.Trim(),
                                name = Name.Trim(),
                                email = Email.Trim()
                            };
                            MyCashierDbContext.db.User.Add(newUser);
                            MyCashierDbContext.db.SaveChanges();
                            Navigator.Navigate(new UC_AuthorisationVM(Login, Password));
                        };
                    },
                    obj =>
                        !string.IsNullOrEmpty(Email) &&
                        !string.IsNullOrEmpty(Login) &&
                        Password?.Length > 6 &&
                        !string.IsNullOrEmpty(Name));
            }
        }

        private RelayCommand goToAuthorisationCmd = null!;
        public RelayCommand GoToAuthorisationCmd
        {
            get
            {
                return goToAuthorisationCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_AuthorisationVM(Login, Password)); });
            }
        }
    }
}
