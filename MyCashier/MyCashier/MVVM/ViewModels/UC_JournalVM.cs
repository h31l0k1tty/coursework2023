using MyCashier.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_JournalVM : ViewModelBase
    {
        private RelayCommand logOutCmd = null!;
        public RelayCommand LogOutCmd
        {
            get
            {
                return logOutCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_AuthorisationVM()); });
            }
        }

        private RelayCommand goToAddAccountCmd = null!;
        public RelayCommand GoToAddAccountCmd
        {
            get
            {
                return goToAddAccountCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_AddAccountVM()); });
            }
        }
    }
}
