using MyCashier.MVVM.Models;
using MyCashier.Services;
using System.Linq;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_RegistrationVM : ViewModelBase
    {
        private string _email = null!;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _login = null!;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password = null!;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
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
                        using (MyCashierDbContext db = new MyCashierDbContext())
                        {
                            if (db.User.FirstOrDefault(c => c.login == Login) != null)
                                MessageBox.Show("Пользователем с таким логином уже существует!");
                            else if (db.User.FirstOrDefault(c => c.email == Email) != null)
                                MessageBox.Show("Пользователем с такой эл. почтой уже существует!");
                            else
                            {
                                db.User.Add(new User()
                                {
                                    id = default,
                                    login = Login,
                                    password = Password,
                                    name = Name,
                                    email = Email
                                });
                                db.SaveChanges();
                                Navigator.Navigate(new UC_MainVM());
                            };
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
                    (obj => { Navigator.GoBack(); });
            }
        }
    }
}
