using MyCashier.MVVM.Models;
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
        private List<Account> accounts = MyCashierDbContext.db.Account.Where(c => c.userID == CurrentUser.Id).ToList();
        public List<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }


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
