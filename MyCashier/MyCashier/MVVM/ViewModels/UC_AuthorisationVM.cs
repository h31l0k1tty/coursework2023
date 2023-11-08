using MyCashier.MVVM.Views;
using MyCashier.Services;
using System.Linq;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_AuthorisationVM : ViewModelBase
    {
        private string _login = "admin";
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password = "1234567";
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool isRememberMeChecked;

        public bool IsRememberMeChecked
        {
            get { return isRememberMeChecked; }
            set
            {
                if (isRememberMeChecked != value)
                {
                    isRememberMeChecked = value;
                    OnPropertyChanged(nameof(IsRememberMeChecked));
                }
            }
        }


        private RelayCommand authoriseCmd = null!;
        public RelayCommand AuthoriseCmd
        {
            get
            {
                return authoriseCmd ?? new RelayCommand
                    (obj =>
                    {
                        using (MyCashierDbContext db = new MyCashierDbContext())
                        {
                            if (db.User.FirstOrDefault(c => c.login == Login && c.password == Password) != null)
                            {
                                CurrentUser.Set(db.User.FirstOrDefault(c => c.login == Login && c.password == Password));
                                Navigator.Navigate(new UC_Main());
                            }
                            else
                                MessageBox.Show("Авторизация не пройдена");
                        };
                    },
                    obj => !string.IsNullOrEmpty(Login) && Password?.Length > 6);
            }
        }

        private RelayCommand goToRegisterCmd = null!;
        public RelayCommand GoToRegistrationCmd
        {
            get
            {
                return goToRegisterCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_RegistrationVM()); });
            }
        }
    }
}
