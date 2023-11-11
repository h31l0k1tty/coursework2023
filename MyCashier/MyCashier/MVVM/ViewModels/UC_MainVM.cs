using MyCashier.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_MainVM : ViewModelBase
    {
        private string userName = CurrentUser.Name;
        public string UserName
        {
            get { return userName; }
            set 
            { 
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private RelayCommand logOutCmd = null!;
        public RelayCommand LogOutCmd
        {
            get
            {
                return logOutCmd ?? new RelayCommand
                    (obj => 
                    { 
                        Navigator.Navigate(new UC_AuthorisationVM());
                        Properties.Settings.Default.Login = null;
                        Properties.Settings.Default.Password = null;
                        Properties.Settings.Default.WasRememberMeChecked = false;
                        Properties.Settings.Default.Save();
                    });
            }
        }
    }
}
